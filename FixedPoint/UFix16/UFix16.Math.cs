using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
    public unsafe partial struct UFix16
    {
        #region Overflow Checked Math Functions

        public static UFix16 CheckedAdd(UFix16 left, UFix16 right)
        {
            var value = Add(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedSubtract(UFix16 left, UFix16 right)
        {
            var value = Subtract(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedMultiply(UFix16 left, UFix16 right)
        {
            var value = Multiply(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedDivide(UFix16 left, UFix16 right)
        {
            var value = Divide(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        #endregion Overflow Checked Math Functions

        #region Overflow Unchecked Math Functions

        public static UFix16 Add(UFix16 left, UFix16 right) =>
            Add(left, right, out _);

        public static UFix16 Subtract(UFix16 left, UFix16 right) =>
            Subtract(left, right, out _);

        public static UFix16 Multiply(UFix16 left, UFix16 right) =>
            Multiply(left, right, out _);

        public static UFix16 Divide(UFix16 left, UFix16 right) =>
            Divide(left, right, out _);

        public static UFix16? NullableDivide(UFix16? left, UFix16? right) =>
            left is UFix16 fLeft
                ? right is UFix16 fix
                    ? fix != Zero
                        ? Divide(fLeft, fix)
                        : null
                    : null
                : null;

        public static UFix16 Lerp(UFix16 left, UFix16 right, byte fraction)
        {
            var tempOut = left._value * ((1 << 8) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 8;
            var result = (uint)tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Lerp(UFix16 left, UFix16 right, ushort fraction)
        {
            var tempOut = left._value * ((1 << 16) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 16;
            var result = (uint)tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Lerp(UFix16 left, UFix16 right, uint fraction)
        {
            var tempOut = (long)left._value * (0 - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 32;
            var result = tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Sqrt(UFix16 value)
        {
            var num = *(uint*)&value;
            var result = 0u;
            var bit = 0u;
            byte n;

            // Many numbers will be less than 15, so
            // this gives a good balance between time spent
            // in if vs. time spent in the while loop
            // when searching for the starting value.
            bit = ( num & 0xFFF00000 ) != 0 ?
                      1u << 30 :
                      1u << 18;

            while (bit > num) bit >>= 2;

            // The main part is executed twice, in order to avoid
            // using 64 bit values in computations.
            for (n = 0; n < 2; n++)
            {
                // First we get the top 24 bits of the answer.
                while (bit != 0)
                {
                    if (num >= result + bit)
                    {
                        num -= result + bit;
                        result = ( result >> 1 ) + bit;
                    }
                    else
                        result >>= 1;
                    bit >>=2;
                }

                if (n == 0)
                {
                    // Then process it again to get the lowest 8 bits.
                    if (num > 65535)
                    {
                        // The remainder 'num' is too large to be shifted left
                        // by 16, so we have to add 1 to result manually and
                        // adjust 'num' accordingly.
                        // num = a - (result + 0.5)^2
                        //   = num + result^2 - (result + 0.5)^2
                        //   = num - result - 0.5
                        num -= result;
                        num = ( num << 16 ) - 0x8000;
                        result = ( result << 16 ) + 0x8000;
                    }
                    else
                    {
                        num <<= 16;
                        result <<= 16;
                    }

                    bit = 1 << 14;
                }
            }

            // Finally, if next bit would have been 1, round the result upwards.
            if (num > result)
                result++;

            return new UFix16() { _value = result };
        }

        #endregion Overflow Unchecked Math Functions

        #region Private Math Functions

        private static UFix16 Add(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(uint*)&left;
            var r = *(uint*)&right;
            var result = l+r;

            if (result < l || result < r)
                overflow = true;

            return overflow ? MaxValue : Raw(result);
        }

        private static UFix16 Subtract(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(uint*)&left;
            var r = *(uint*)&right;
            var result = l-r;

            if (r > l)
                overflow = true;

            return overflow ? MinValue : Raw(result);
        }

        private static UFix16 Multiply(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var product = (ulong)*(uint*)&left * *(uint*)&right;

            // The upper 16 bits should all be the same (the sign).
            var upper = product >> 48;

            if (upper is not 0)
                overflow = true;

            var result = (int)(product >> 16);
            var temp = *(UFix16*)&result;
            result += (int)( product & 0x8000 ) >> 15;

            return overflow ? MaxValue : *(UFix16*)&result;
        }

        private static UFix16 Divide(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;

            if (right == Zero)
                throw new DivideByZeroException();

            uint a = *(uint*)&left, b = *(uint*)&right;

            //var result = ((ulong)a <<16) / ((ulong)b<<16);
            //result >>=16;

            ulong remainder = a;
            ulong divider = b;
            ulong quotient = 0u;
            var bitPos = 17;

            while (( divider & 0xF ) is 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder is not 0 && bitPos >= 0)
            {
                // Shift remainder as much as we can without overflowing
                var shift = (int)Clz((uint)remainder);
                if (shift > bitPos) shift = bitPos;
                remainder <<= shift;
                bitPos -= shift;

                var div = remainder / divider;
                remainder %= divider;
                quotient += div << bitPos;

                var mask = 0x1FFFFFFFFu >> bitPos--;
                if (( div & ~mask ) is not 0)
                    overflow = true;

                remainder <<= 1;
            }

            quotient++;

            var result = quotient >> 1;

            return overflow ? MaxValue : *(UFix16*)&result;
        }

        private static byte Clz(uint x)
        {
            byte result = 255;
            var arch = RuntimeInformation.ProcessArchitecture;
            if (arch is Architecture.X86 or Architecture.X64 && Lzcnt.IsSupported)
                result = (byte)Lzcnt.LeadingZeroCount(x);
            else
                if (arch is Architecture.Arm or Architecture.Arm64 && ArmBase.IsSupported)
                result = (byte)ArmBase.LeadingZeroCount(x);
#if DEBUG
            var simd = result;
#else
            else
#endif
            {
                result = 0;
                if (x == 0) return 32;
                while (( x & 0xF0000000 ) is 0) { result += 4; x <<= 4; }
                while (( x & 0x80000000 ) is 0) { result += 1; x <<=1; }
            }
#if DEBUG
            if (simd is not 255 && simd != result)
                throw new Exception($"SIMD result: \"{simd}\" does not match non-SIMD result: \"{result}\"");
#endif
            return result;
        }

        #endregion Private Math Functions
    }
}
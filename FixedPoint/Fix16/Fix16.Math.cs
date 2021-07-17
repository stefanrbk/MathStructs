using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
    public unsafe partial struct Fix16
    {
        #region Overflow Checked Math Functions

        public static Fix16 CheckedAdd(Fix16 left, Fix16 right)
        {
            var value = Add(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedSubtract(Fix16 left, Fix16 right)
        {
            var value = Subtract(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedMultiply(Fix16 left, Fix16 right)
        {
            var value = Multiply(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedDivide(Fix16 left, Fix16 right)
        {
            var value = Divide(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        #endregion

        #region Overflow Unchecked Math Functions

        public static Fix16 Add(Fix16 left, Fix16 right) =>
            Add(left, right, out var _);

        public static Fix16 Subtract(Fix16 left, Fix16 right) =>
            Subtract(left, right, out var _);

        public static Fix16 Multiply(Fix16 left, Fix16 right) =>
            Multiply(left, right, out var _);

        public static Fix16 Divide(Fix16 left, Fix16 right) =>
            Divide(left, right, out var _);

        public static Fix16? NullableDivide(Fix16? left, Fix16? right) =>
            left is Fix16 fLeft
                ? right is Fix16 fix
                    ? fix != Zero
                        ? Divide(fLeft, fix)
                        : null
                    : null
                : null;

        public static Fix16 Lerp(Fix16 left, Fix16 right, byte fraction)
        {
            var tempOut = (long)left._value * ((1 << 8) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 8;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Lerp(Fix16 left, Fix16 right, ushort fraction)
        {
            var tempOut = (long)left._value * ((1 << 16) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 16;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Lerp(Fix16 left, Fix16 right, uint fraction)
        {
            var tempOut = left._value * (0 - fraction);
            tempOut += right._value * fraction;
            tempOut >>= 32;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Exp(Fix16 value)
        {
            if (value == Zero)
                return One;
            if (value == One)
                return E;
            if (value > new Fix16() { _value = 681391 })
                return MaxValue;
            if (value < new Fix16() { _value = -772243 })
                return Zero;

            var tempIndex = (*(int*)&value ^ (*(int*)&value >> 4)) & 0x0FFF;
            if (expCacheIndex[tempIndex] == value)
                return expCacheValue[tempIndex];

            var outValue = (Fix16)Math.Exp((double)value);

            expCacheIndex[tempIndex] = value;
            expCacheValue[tempIndex] = outValue;

            return outValue;
        }

        public static Fix16 Sqrt(Fix16 value)
        {
            var neg = value < 0;
            var num = neg ? (uint)-*(int*)&value : *(uint*)&value;
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

            return new Fix16() { _value = neg ? -(int)result : (int)result };
        }

        public static Fix16 Abs(Fix16 value)
        {
            var mask = *(int*)&value >> 31;
            return new Fix16() { _value = ( *(int*)&value + mask ) ^ mask };
        }

        #endregion Overflow Unchecked Math Functions

        #region Trig Functions

        public static Fix16 Sin(Fix16 angle)
        {
            var tempAngle = Raw(angle._value % ( Pi._value << 1 ));

            if (tempAngle > Pi)
                tempAngle -= Raw(Pi._value << 1);
            else if (tempAngle < -Pi)
                tempAngle += Raw(Pi._value << 1);

            var tempIndex = ( angle._value >> 5 ) & 0xFFF;
            if (sinCacheIndex[tempIndex] == angle)
                return sinCacheValue[tempIndex];

            var tempAngleSq = tempAngle * tempAngle;
            var tempOut = tempAngle._value;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 6;
            tempAngle *= tempAngleSq;
            tempOut += tempAngle._value / 120;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 5040;
            tempAngle *= tempAngleSq;
            tempOut += tempAngle._value / 362880;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 39916800;

            sinCacheIndex[tempIndex] = angle;
            sinCacheValue[tempIndex] = Raw(tempOut);

            return Raw(tempOut);
        }

        public static Fix16 Cos(Fix16 angle) =>
            Sin(angle + Raw(Pi._value >> 1));

        public static Fix16? Tan(Fix16 angle)
        {
            var cos = Cos(angle);
            var halfPi = Pi / (Fix16)2;
            return cos == Zero || angle == halfPi ? null : Sin(angle) / cos;
        }

        public static Fix16? Asin(Fix16 value)
        {
            if (( value > One ) || ( value < NegOne ))
                return null;
            if (value == One)
                return Pi / (Fix16)2;

            var tempOut = One - (value * value);
            tempOut = value / Sqrt(tempOut);
            tempOut = Atan(tempOut);
            return tempOut;
        }

        public static Fix16? Acos(Fix16 value) =>
            Raw(Pi._value >> 1) - Asin(value);

        public static Fix16 Atan2(Fix16 y, Fix16 x)
        {
            Fix16 r, r3, angle;

            var hash = x._value ^ y._value;
            hash ^= hash >> 20;
            hash &= 0xFFF;
            if (atanCacheIndex[hash] == (x, y))
                return atanCacheValue[hash];

            var absY = Abs(y);

            if (x >= Zero)
            {
                r = ( x - absY ) / ( x + absY );
                r3 = r * r * r;
                angle = ( Raw(0x3240) * r3 ) - ( Raw(0xFB50) * r ) + PiDivFour;
            }
            else
            {
                r = ( x + absY ) / ( absY - x );
                r3 = r * r * r;
                angle = ( Raw(0x3240) * r3 ) - ( Raw(0xFB50) * r ) + ThreePiDivFour;
            }
            if (y < Zero)
                angle = -angle;

            atanCacheIndex[hash] = (x, y);
            atanCacheValue[hash] = angle;

            return angle;
        }

        public static Fix16 Atan(Fix16 value) =>
            Atan2(value, One);

        #endregion Trig Functions

        #region Private Math Functions

        private static Fix16 Add(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(int*)&left;
            var r = *(int*)&right;
            var result = l+r;

            if (( ( ~( l^r ) )&0x80000000 & ( l^result )&0x80000000 ) is not 0)
                overflow = true;

            return overflow
                ? left >= 0
                    ? MaxValue
                    : MinValue
                : Raw(result);
        }

        private static Fix16 Subtract(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(int*)&left;
            var r = *(int*)&right;
            var result = l-r;

            if (( ( l^r )&0x80000000 & ( l^result )&0x80000000 ) is not 0)
                overflow = true;

            return overflow
                ? left >= 0
                    ? MaxValue
                    : MinValue
                : Raw(result);
        }

        private static Fix16 Multiply(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var product = (long)*(int*)&left * *(int*)&right;

            // The upper 17 bits should all be the same (the sign).
            var upper = product >> 47;

            if (product < 0)
            {
                if (( ~upper ) is not 0)
                    overflow = true;

                product--;
            }
            else if (upper is not 0)
                overflow = true;

            var result = (int)(product >> 16);
            result += (int)( product & 0x8000 ) >> 15;

            return overflow
                ? ( left >= 0 ) == ( right >= 0 )
                    ? MaxValue
                    : MinValue
                : *(Fix16*)&result;
        }

        private static Fix16 Divide(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;

            if (right == Zero)
                throw new DivideByZeroException();

            int a = *(int*)&left, b = *(int*)&right;

            var remainder = (uint)((a >= 0) ? a : -a);
            var divider = (uint)((b >= 0) ? b : -b);
            var quotient = 0u;
            var bitPos = 17;

            while (( divider & 0xF ) is 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder is not 0 && bitPos >= 0)
            {
                // Shift remainder as much as we can without overflowing
                var shift = (int)Clz(remainder);
                if (shift > bitPos) shift = bitPos;
                remainder <<= shift;
                bitPos -= shift;

                var div = remainder / divider;
                remainder %= divider;
                quotient += div << bitPos;

                if (( div & ~( 0xFFFFFFFF >> bitPos ) ) is not 0)
                    overflow = true;

                remainder <<= 1;
                bitPos--;
            }

            quotient++;

            var result = quotient >> 1;

            if (( ( a^b )&0x80000000 ) is not 0)
                result = (uint)-(int)result;

            return overflow
                ? ( left >= 0 ) == ( right >= 0 )
                    ? MaxValue
                    : MinValue
                : *(Fix16*)&result;
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
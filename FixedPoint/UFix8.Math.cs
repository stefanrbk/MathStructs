namespace System
{
    public unsafe partial struct UFix8
    {
        #region Overflow Checked Math Functions

        public static UFix8 CheckedAdd(UFix8 left, UFix8 right)
        {
            UFix16 value;
            try                         { value = UFix16.CheckedAdd(left, right); }
            catch (OverflowException)   { throw new OverflowException(); }

            if ((float)value < MinValue || (float)value > MaxValue)
                throw new OverflowException();

            return (UFix8)value;
        }

        public static UFix8 CheckedSubtract(UFix8 left, UFix8 right)
        {
            UFix16 value;
            try                         { value = UFix16.CheckedSubtract(left, right); }
            catch (OverflowException)   { throw new OverflowException(); }

            if ((float)value < MinValue || (float)value > MaxValue)
                throw new OverflowException();

            return (UFix8)value;
        }

        public static UFix8 CheckedMultiply(UFix8 left, UFix8 right)
        {
            UFix16 value;
            try                         { value = UFix16.CheckedMultiply(left, right); }
            catch (OverflowException)   { throw new OverflowException(); }

            if ((float)value < MinValue || (float)value > MaxValue)
                throw new OverflowException();

            return (UFix8)value;
        }

        public static UFix8 CheckedDivide(UFix8 left, UFix8 right)
        {
            UFix16 value;
            try                             { value = UFix16.CheckedDivide(left, right); }
            catch (OverflowException)       { throw new OverflowException(); }
            catch (DivideByZeroException)   { throw new DivideByZeroException(); }

            if ((float)value < MinValue || (float)value > MaxValue)
                throw new OverflowException();

            return (UFix8)value;
        }

        #endregion

        #region Overflow Unchecked Math Functions

        public static UFix8 Add(UFix8 left, UFix8 right) =>
            (UFix8)UFix16.Add(left, right);

        public static UFix8 Subtract(UFix8 left, UFix8 right) =>
            (UFix8)UFix16.Subtract(left, right);

        public static UFix8 Multiply(UFix8 left, UFix8 right) =>
            (UFix8)UFix16.Multiply(left, right);

        public static UFix8 Divide(UFix8 left, UFix8 right) =>
            (UFix8)UFix16.Divide(left, right);

        public static UFix8? NullableDivide(UFix8? left, UFix8? right) =>
            left is UFix8 fLeft
                ? right is UFix8 fix
                    ? fix != Zero
                        ? Divide(fLeft, fix)
                        : null
                    : null
                : null;

        public static UFix8 Lerp(UFix8 left, UFix8 right, byte fraction) =>
            (UFix8)UFix16.Lerp(left, right, fraction);

        public static UFix8 Lerp(UFix8 left, UFix8 right, ushort fraction) =>
            (UFix8)UFix16.Lerp(left, right, fraction);

        public static UFix8 Lerp(UFix8 left, UFix8 right, uint fraction) =>
            (UFix8)UFix16.Lerp(left, right, fraction);

        public static UFix8 Sqrt(UFix8 value) =>
            (UFix8)UFix16.Sqrt(value);

        #endregion Overflow Unchecked Math Functions
    }
}
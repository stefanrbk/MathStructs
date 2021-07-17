namespace System
{
    public partial struct UFix8
    {
        public static UFix8 Epsilon => new() { _value = 1 };
        public static UFix8 MaxValue => new() { _value = UInt16.MaxValue };
        public static UFix8 MinValue => new() { _value = UInt16.MinValue };
        public static UFix8 One => new() { _value = 256 };
        public static UFix8 Zero => new() { _value = 0 };
    }
}
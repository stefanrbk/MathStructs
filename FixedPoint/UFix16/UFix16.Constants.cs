namespace System
{
    public unsafe partial struct UFix16
    {
        public static UFix16 Epsilon => new() { _value = 1 };
        public static UFix16 MaxValue => new() { _value = UInt32.MaxValue };
        public static UFix16 MinValue => new() { _value = UInt32.MinValue };
        public static UFix16 Pi => new() { _value = 205887 };
        public static UFix16 E => new() { _value = 178145 };
        public static UFix16 One => new() { _value = 65536 };
        public static UFix16 Zero => new() { _value = 0 };
        public static UFix16 FourDivPi => new() { _value = 0x145F3 };
        public static UFix16 FourDivPi2 => new() { _value = 106243 };
        public static UFix16 PiDivFour => new() { _value = 0x0000C910 };
        public static UFix16 ThreePiDivFour => new() { _value = 0x00025B30 };
    }
}
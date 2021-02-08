namespace System
{
    public unsafe partial struct Fix16
    {
        public static Fix16 Epsilon => new() { _value = 1 };
        public static Fix16 MaxValue => new() { _value = Int32.MaxValue };
        public static Fix16 MinValue => new() { _value = Int32.MinValue };
        public static Fix16 Pi => new() { _value = 205887 };
        public static Fix16 E => new() { _value = 178145 };
        public static Fix16 One => new() { _value = 65536 };
        public static Fix16 Zero => new() { _value = 0 };
        public static Fix16 NegOne => new() { _value = -65536 };
        public static Fix16 FourDivPi => new() { _value = 0x145F3 };
        public static Fix16 FourDivPi2 => new() { _value = 106243 };
        public static Fix16 PiDivFour => new() { _value = 0x0000C910 };
        public static Fix16 ThreePiDivFour => new() { _value = 0x00025B30 };
    }
}
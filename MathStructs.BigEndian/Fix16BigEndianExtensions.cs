using System.IO;
using System.Runtime.CompilerServices;

namespace System.Extensions
{
    public static class Fix16BigEndianExtensions
    {
        public static void Write(this BeBinaryWriter writer, int value) =>
            writer.Write(Unsafe.As<int, uint>(ref value));

        public static int ReadFix16(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, int>(ref value);
        }
    }
}

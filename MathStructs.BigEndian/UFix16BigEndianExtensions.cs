using System.IO;
using System.Runtime.CompilerServices;
namespace System.Extensions
{
    public static class UFix16BigEndianExtensions
    {
        public static void Write(this BeBinaryWriter writer, uint value) =>
            writer.Write(Unsafe.As<uint, uint>(ref value));

        public static uint ReadU16Fixed16(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, uint>(ref value);
        }
    }
}

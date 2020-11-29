using System.IO;
using System.Runtime.CompilerServices;
namespace System.Extensions
{
    public static class UFix8BigEndianExtensions
    {
        public static void Write(this BeBinaryWriter writer, ushort value) =>
            writer.Write(Unsafe.As<ushort, ushort>(ref value));

        public static ushort ReadU8Fixed8(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt16();
            return Unsafe.As<ushort, ushort>(ref value);
        }
    }
}

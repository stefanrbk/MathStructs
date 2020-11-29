using System.IO;
using System.Runtime.CompilerServices;
namespace System.Extensions
{
    public static class UFix8Extensions
    {
        public static void Write(this BinaryWriter writer, ushort value) =>
            writer.Write(Unsafe.As<ushort, ushort>(ref value));

        public static ushort ReadUFix8(this BinaryReader reader)
        {
            var value = reader.ReadUInt16();
            return Unsafe.As<ushort, ushort>(ref value);
        }
    }
}

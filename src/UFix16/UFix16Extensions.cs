using System.IO;
using System.Runtime.CompilerServices;
namespace System.Extensions
{
    public static class UFix16Extensions
    {
        public static void Write(this BinaryWriter writer, uint value) =>
            writer.Write(Unsafe.As<uint, uint>(ref value));

        public static uint ReadU16Fixed16(this BinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, uint>(ref value);
        }
    }
}

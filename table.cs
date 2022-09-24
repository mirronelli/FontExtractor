using System.Buffers.Binary;
using System.Text;

namespace Main
{
    struct Table
    {
        public System.String Tag;
        public System.UInt32 Checksum;
        public System.UInt32 Offset;
        public System.UInt32 Length;

        public Table(BinaryReader reader)
        {
            Tag = Encoding.ASCII.GetString(reader.ReadBytes(4));
            Checksum = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            Offset = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            Length = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
        }
    }
}
using System.Buffers.Binary;

namespace Main
{
    class NameRecord
    {
        public UInt16 PlatformID;
        public UInt16 EncodingID;
        public UInt16 LanguageID;
        public UInt16 NameID;
        public UInt16 Length;
        public UInt16 Offset;

        public NameRecord(BinaryReader reader)
        {
            PlatformID = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            EncodingID = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            LanguageID = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            NameID = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            Length = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            Offset = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
        }
    }
}
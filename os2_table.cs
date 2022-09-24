using System.Buffers.Binary;

namespace Main
{
    struct Os2Table
    {
        public UInt16 Version;
        public UInt16 AvgCharWidth;
        public UInt16 WeightClass;
        public UInt16 WidthClass;

        public Os2Table(BinaryReader reader)
        {
            Version = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            AvgCharWidth = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            WeightClass = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            WidthClass = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
        }
    }
}
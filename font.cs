using System.Buffers.Binary;

namespace Main
{
    struct Font
    {
        public System.UInt32 Version;
        public System.UInt16 Count;
        public System.UInt16 SearchRange;
        public System.UInt16 EntrySelector;
        public System.UInt16 RangeShift;


        public Dictionary<string, Table> Tables;
        public NameTable NamesTable;
        public Os2Table Os2Table;

        public Font(BinaryReader reader)
        {
            Version = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            Count = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            SearchRange = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            EntrySelector = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            RangeShift = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));

            Tables = new Dictionary<string, Table>(Count);
            for (var i = 0; i < Count; i++)
            {
                Table table = new Table(reader);
                Tables.Add(table.Tag, table);
            }

            reader.BaseStream.Seek(Tables["name"].Offset, SeekOrigin.Begin);
            NamesTable = new NameTable(reader);

            reader.BaseStream.Seek(Tables["OS/2"].Offset, SeekOrigin.Begin);
            Os2Table = new Os2Table(reader);
        }
    }
}
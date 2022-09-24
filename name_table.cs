using System.Buffers.Binary;
using System.Text;

namespace Main
{
    class NameTable
    {
        public UInt16 Version;
        public UInt16 Count;
        public UInt16 FirstRowOffset;
        public Dictionary<UInt32, NameRecord> Records;

        public string FontFamily_01 = "";
        public string FontSubfamily_02 = "";
        public string TypographicFontfamily_16 = "";
        public string TypographicFontSubfamily_17 = "";

        private long _startPosition;

        public NameTable(BinaryReader reader)
        {
            _startPosition = reader.BaseStream.Position;

            Version = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            Count = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));
            FirstRowOffset = BinaryPrimitives.ReadUInt16BigEndian(reader.ReadBytes(2));

            Records = new Dictionary<UInt32, NameRecord>(Count);
            for (var i = 0; i < Count; i++)
            {
                NameRecord record = new NameRecord(reader);
                if (record.Length > 0)
                    Records.TryAdd(record.NameID, record); // name is not unique by itself, the first non empty name from all languages and platforms wins here
            }

            FontFamily_01 = ReadName(1, reader);
            FontSubfamily_02 = ReadName(2, reader);
            TypographicFontfamily_16 = ReadName(16, reader);
            TypographicFontSubfamily_17 = ReadName(17, reader);
        }

        private string ReadName(UInt32 index, BinaryReader reader)
        {
            if (Records.ContainsKey(index))
            {
                var record = Records[index];
                //Console.WriteLine($"platformid {record.PlatformID}, encodingid {record.EncodingID}, languageid {record.LanguageID}");
                reader.BaseStream.Seek(_startPosition + FirstRowOffset + record.Offset, SeekOrigin.Begin);
                byte[] bytes = reader.ReadBytes(record.Length);

                var encoding = record switch
                {
                    NameRecord r when r.PlatformID == 3 => Encoding.BigEndianUnicode,
                    NameRecord r when r.PlatformID == 0 && r.EncodingID == 3 => Encoding.BigEndianUnicode,
                    NameRecord r when r.PlatformID == 0 && r.EncodingID == 0 => Encoding.BigEndianUnicode,
                    _ => Encoding.ASCII
                };

                return encoding.GetString(bytes);
            }
            else return string.Empty;
        }
    }
}
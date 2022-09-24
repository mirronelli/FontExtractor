using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace Main
{
    class FontReader
    {
        static void Main()
        {
            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            var files = System.IO.Directory.EnumerateFiles("font_files", "*.ttf");
            foreach (var file in files)
            {
                using (var stream = File.Open(file, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.ASCII, false))
                    {
                        var font = new Font(reader);
                        Console.WriteLine($"------------------------------------File: {file}  ---------");
                        Console.WriteLine($"Version: {font.Version}\tTables count: {font.Count}");
                        Console.WriteLine($"Font Family: {font.NamesTable.FontFamily_01}");
                        Console.WriteLine($"Font Subfamily: {font.NamesTable.FontSubfamily_02}");
                        Console.WriteLine($"Typographic Family: {font.NamesTable.TypographicFontfamily_16}");
                        Console.WriteLine($"Typographic Subfamily: {font.NamesTable.TypographicFontSubfamily_17}");
                        Console.WriteLine($"Width Class: {font.Os2Table.WidthClass}");
                        Console.WriteLine($"Weight Class: {font.Os2Table.WeightClass}");
                    }
                }
            }

            stopWatch.Stop();
            Console.WriteLine($"=======================================================");
            Console.WriteLine($"Processed {files.Count()} files");
            Console.WriteLine($"Time taken {stopWatch.ElapsedMilliseconds}ms");
        }
    }
}

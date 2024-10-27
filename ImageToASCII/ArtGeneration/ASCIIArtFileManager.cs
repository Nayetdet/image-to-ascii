using System.IO.Compression;

namespace ImageToASCII.ArtGeneration
{
    internal static class ASCIIArtFileManager
    {
        private static string GetFilePath(string fileName, string fileExtension)
        {
            fileName = $"{fileName}-{Guid.NewGuid()}";
            string fileFullName = Path.ChangeExtension(fileName, fileExtension);
            return Path.Combine(Directory.GetCurrentDirectory(), fileFullName);
        }

        private static void SaveFramesAsZip(string[] asciiFrames)
        {
            using MemoryStream memoryStream = new();
            using (ZipArchive zipArchive = new(memoryStream, ZipArchiveMode.Create, true))
            {
                int maxDigits = asciiFrames.Length.ToString().Length;

                for (int i = 0; i < asciiFrames.Length; i++)
                {
                    string entryName = $"frame-{(i + 1).ToString($"D{maxDigits}")}.txt";
                    ZipArchiveEntry entry = zipArchive.CreateEntry(entryName);

                    using StreamWriter streamWriter = new(entry.Open());
                    streamWriter.Write(asciiFrames[i]);
                }
            }

            string zipFilePath = GetFilePath("ASCIIArtGIF", "zip");
            using FileStream fileStream = new(zipFilePath, FileMode.Create);
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.CopyTo(fileStream);
        }

        private static void SaveFrameAsTextFile(string asciiFrame)
        {
            string txtFilePath = GetFilePath("ASCIIArtImage", "txt");
            File.WriteAllText(txtFilePath, asciiFrame);
        }

        public static void SaveASCIIFrames(string[] asciiFrames)
        {
            if (asciiFrames.Length > 1)
            {
                SaveFramesAsZip(asciiFrames);
            }
            else
            {
                SaveFrameAsTextFile(asciiFrames[0]);
            }
        }
    }
}

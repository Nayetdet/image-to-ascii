using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using ImageToASCII.Settings;

namespace ImageToASCII.ArtGeneration
{
    public class ASCIIArtGenerator(DisplaySettings settings)
    {
        public DisplaySettings Settings { get; private set; } = settings;

        private string[] GetASCIIArtFrames(string filePath)
        {
            using Image<L8> file = Image.Load<L8>(filePath);

            string[] asciiFrames = new string[file.Frames.Count];
            byte brightnessMap = (byte)(255 / (Settings.Characters.Length - 1));

            for (int i = 0; i < file.Frames.Count; i++)
            {
                StringBuilder asciiFrame = new();

                Image<L8> frame = file.Frames.CloneFrame(i);
                frame.Mutate(f => f.Resize(Settings.ScreenWidth, Settings.ScreenHeight));

                for (int y = 0; y < frame.Height; y++)
                {
                    for (int x = 0; x < frame.Width; x++)
                    {
                        L8 pixel = frame[x, y];
                        byte pixelBrightness = pixel.PackedValue;
                        asciiFrame.Append(Settings.Characters[pixelBrightness / brightnessMap]);
                    }

                    if (y < frame.Height - 1)
                    {
                        asciiFrame.Append(Environment.NewLine);
                    }
                }

                asciiFrames[i] = asciiFrame.ToString();
            }

            return asciiFrames;
        }

        public void DisplayASCIIArt(string filePath)
        {
            string[] asciiFrames = GetASCIIArtFrames(filePath);
            ASCIIArtFileManager.SaveASCIIFrames(asciiFrames);

            while (true)
            {
                foreach (string asciiFrame in asciiFrames)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(asciiFrame);
                    Thread.Sleep(1000 / Settings.Fps);

                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        return;
                    }
                }
            }
        }
    }
}

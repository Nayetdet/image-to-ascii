using ImageToASCII.ArtGeneration;
using ImageToASCII.Settings;

namespace ImageToASCII
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SettingsLoader settingsLoader = new();
            DisplaySettings userSettings = settingsLoader.GetUserConfig();

            Console.Clear();
            Console.SetWindowSize(1, 1);
            Console.SetWindowSize(userSettings.ScreenWidth, userSettings.ScreenHeight);
            Console.Title = "ImageToASCII";
            Console.CursorVisible = false;

            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(userSettings.ScreenWidth, userSettings.ScreenHeight);
            }

            Console.WriteLine("""
                    ___                    _____      _   ___  ___ ___ ___ 
                   |_ _|_ __  __ _ __ _ __|_   _|__  /_\ / __|/ __|_ _|_ _|
                    | || '  \/ _` / _` / -_)| |/ _ \/ _ \\__ \ (__ | | | | 
                   |___|_|_|_\__,_\__, \___||_|\___/_/ \_\___/\___|___|___|
                                  |___/                                    
                   
                """);

            Console.Write(">> Enter a valid path for the image/gif: ");
            string filePath = Console.ReadLine() ?? string.Empty;

            ASCIIArtGenerator asciiArtGenerator = new(userSettings);
            asciiArtGenerator.DisplayASCIIArt(filePath);

            Console.ReadKey();
        }
    }
}

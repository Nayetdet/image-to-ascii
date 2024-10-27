using ImageToASCII.ArtGeneration;
using ImageToASCII.Settings;

namespace ImageToASCII
{
    internal class Program
    {
        private static void DisplayMenu(int screenWidth, int screenHeight)
        {
            Console.Clear();
            Console.SetWindowSize(1, 1);
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.Title = "ImageToASCII";
            Console.CursorVisible = false;

            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(screenWidth, screenHeight);
            }

            Console.WriteLine("""
                    ___                    _____      _   ___  ___ ___ ___ 
                   |_ _|_ __  __ _ __ _ __|_   _|__  /_\ / __|/ __|_ _|_ _|
                    | || '  \/ _` / _` / -_)| |/ _ \/ _ \\__ \ (__ | | | | 
                   |___|_|_|_\__,_\__, \___||_|\___/_/ \_\___/\___|___|___|
                                  |___/                                    
                   
                """);
        }

        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    SettingsLoader settingsLoader = new();
                    DisplaySettings userSettings = settingsLoader.GetUserSettings();

                    DisplayMenu(userSettings.ScreenWidth + 1, userSettings.ScreenHeight + 1);
                    Console.Write(">> Enter a valid file path for an image or GIF: ");
                    string filePath = Console.ReadLine() ?? string.Empty;

                    ASCIIArtGenerator asciiArtGenerator = new(userSettings);
                    asciiArtGenerator.DisplayASCIIArt(filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"ERROR: {e.Message}");
                }
            }
        }
    }
}

using System.Text.Json;
using System.Reflection;
using ImageToASCII.Exceptions;

namespace ImageToASCII.Settings
{
    internal class SettingsLoader
    {
        public const string SETTINGS_FILE_NAME = "settings.json";

        public readonly DisplaySettings DEFAULT_CONFIG = new(
            characters: @" _,.:;-~+=*!?/[(&$#@",
            screenWidth: 80,
            screenHeight: 40,
            fps: 12
            );

        public readonly string[] SUPPORTED_EXTENSIONS = [".gif", ".jpg", ".png"];

        public readonly string[] EXPECTED_FILES = [
            Path.GetFileName(Assembly.GetExecutingAssembly().Location),
            SETTINGS_FILE_NAME
            ];

        private void CheckCurrentDirectory()
        {
            string[] files = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory());
            foreach (string file in files)
            {
                if (SUPPORTED_EXTENSIONS.Any(x => file.EndsWith(x)) || !EXPECTED_FILES.Contains(file))
                {
                    throw new NotEmptyFolderException("This file must be placed in an empty folder");
                }
            }
        }

        public DisplaySettings GetUserConfig()
        {
            CheckCurrentDirectory();

            if (!File.Exists(SETTINGS_FILE_NAME))
            {
                string defaultSettingsJson = JsonSerializer.Serialize(DEFAULT_CONFIG);
                File.WriteAllText(SETTINGS_FILE_NAME, defaultSettingsJson);
            }

            string userSettingsJson = File.ReadAllText(SETTINGS_FILE_NAME);
            DisplaySettings? userConfig = JsonSerializer.Deserialize<DisplaySettings>(userSettingsJson);
            return userConfig ?? DEFAULT_CONFIG;
        }
    }
}

using System.Text.Json;
using System.Reflection;
using ImageToASCII.Exceptions;

namespace ImageToASCII.Settings
{
    internal class SettingsLoader
    {
        public const string SETTINGS_FILE_NAME = "ascii-art-settings.json";
        public readonly DisplaySettings DEFAULT_CONFIG = new(
            characters: @" _,.:;-~+=*!?/[(&$#@",
            screenWidth: 80,
            screenHeight: 40,
            fps: 12
            );

        public DisplaySettings GetUserConfig()
        {
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

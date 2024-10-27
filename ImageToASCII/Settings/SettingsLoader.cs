using System.Text;
using System.Text.Json;

namespace ImageToASCII.Settings
{
    internal class SettingsLoader
    {
        public const string SETTINGS_FILE_NAME = "image-to-ascii-settings.json";
        public readonly DisplaySettings DEFAULT_CONFIG = new(
            characters: " _,.:;-~=*!?/[($#@",
            screenWidth: 80,
            screenHeight: 40,
            fps: 12
            );

        public DisplaySettings GetUserConfig()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                TypeInfoResolver = DisplaySettingsContext.Default
            };

            if (!File.Exists(SETTINGS_FILE_NAME))
            {
                string defaultSettingsJson = JsonSerializer.Serialize(DEFAULT_CONFIG, options);
                File.WriteAllText(SETTINGS_FILE_NAME, defaultSettingsJson, Encoding.UTF8);
            }

            string userSettingsJson = File.ReadAllText(SETTINGS_FILE_NAME, Encoding.UTF8);
            DisplaySettings? userConfig = JsonSerializer.Deserialize<DisplaySettings>(userSettingsJson, options);
            return userConfig ?? DEFAULT_CONFIG;
        }
    }
}

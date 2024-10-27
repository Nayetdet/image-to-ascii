using ImageToASCII.Exceptions;

namespace ImageToASCII.Settings
{
    public class DisplaySettings
    {
        public string Characters { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int Fps { get; set; }

        public DisplaySettings(string characters, int screenWidth, int screenHeight, int fps)
        {
            int[] numericProperties = [screenWidth, screenHeight, fps];
            int[] invalidNumericProperties = numericProperties.Where(x => x <= 0).ToArray();
            if (invalidNumericProperties.Length > 0 || string.IsNullOrEmpty(characters))
            {
                throw new InvalidSettingsFileException("The settings file was modified improperly");
            }

            Characters = characters;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Fps = fps;
        }
    }
}

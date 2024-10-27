using ImageToASCII.Exceptions;

namespace ImageToASCII.Settings
{
    public class DisplaySettings
    {
        private string _characters;

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int Fps { get; set; }
        public bool IsCharacterReversalEnabled { get; set; }

        public string Characters
        {
            get
            {
                if (IsCharacterReversalEnabled)
                {
                    char[] charArray = _characters.ToCharArray();
                    Array.Reverse(charArray);
                    return new string(charArray);
                }

                return _characters;
            }

            set => _characters = value;
        }

        public DisplaySettings(
            string characters,
            int screenWidth,
            int screenHeight,
            int fps,
            bool isCharacterReversalEnabled
            )
        {
            _characters = characters;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Fps = fps;
            IsCharacterReversalEnabled = isCharacterReversalEnabled;

            ValidateSettings();
        }

        private void ValidateSettings()
        {
            int[] numericProperties = [ScreenWidth, ScreenHeight, Fps];
            int[] invalidNumericProperties = numericProperties.Where(x => x <= 0).ToArray();
            if (invalidNumericProperties.Length > 0 || string.IsNullOrEmpty(Characters))
            {
                throw new InvalidSettingsFileException("The settings file was modified improperly");
            }
        }
    }
}

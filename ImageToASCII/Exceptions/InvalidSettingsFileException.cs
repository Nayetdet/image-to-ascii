namespace ImageToASCII.Exceptions
{
    public class InvalidSettingsFileException : ArgumentException
    {
        public InvalidSettingsFileException()
        {
        }

        public InvalidSettingsFileException(string message)
            : base(message)
        {

        }

        public InvalidSettingsFileException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public InvalidSettingsFileException(string message, string paramName)
            : base(message, paramName)
        {

        }
    }
}

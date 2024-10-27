namespace ImageToASCII.Exceptions
{
    public class InvalidSettingsFileException : Exception
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
    }
}

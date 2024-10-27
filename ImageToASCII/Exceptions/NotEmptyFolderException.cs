namespace ImageToASCII.Exceptions
{
    public class NotEmptyFolderException : Exception
    {
        public NotEmptyFolderException()
        {

        }

        public NotEmptyFolderException(string message)
            : base(message)
        {

        }

        public NotEmptyFolderException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

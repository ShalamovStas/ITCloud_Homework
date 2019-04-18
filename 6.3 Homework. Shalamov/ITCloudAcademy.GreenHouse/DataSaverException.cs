using System;

namespace ITCloudAcademy.GreenHouse
{
    public class DataSaverException : Exception
    {
        public DataSaverException(string message) 
            : base(message)
        {
        }

        public DataSaverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

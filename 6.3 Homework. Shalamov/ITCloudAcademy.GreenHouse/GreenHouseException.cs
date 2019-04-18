using System;

namespace ITCloudAcademy.GreenHouse
{
    public class GreenHouseException : Exception
    {
        public GreenHouseException(string message) 
            : base(message)
        {
        }

        public GreenHouseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

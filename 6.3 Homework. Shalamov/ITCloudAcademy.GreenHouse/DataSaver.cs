using ITCloudAcademy.GreenHouse.Interfaces;
using System;
using System.IO;

namespace ITCloudAcademy.GreenHouse
{
    public class DataSaver : IDataSaver
    {
        public void LogMessage(string message)
        {
            File.AppendAllText("Messages.log", $"{message}{Environment.NewLine}");
        }

        public void SaveData(float temperature)
        {
            File.AppendAllText("Temperature.csv", $"{temperature}{Environment.NewLine}");
        }
    }
}

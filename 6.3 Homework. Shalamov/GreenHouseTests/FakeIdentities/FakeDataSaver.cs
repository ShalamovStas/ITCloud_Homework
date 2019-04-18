using ITCloudAcademy.GreenHouse.Interfaces;
using System;

namespace ITCloudAcademy.GreenHouse.FakeIdentities
{
    class FakeDataSaver : IDataSaver
    {
        public string LogMessageMethodInputValue { get; private set; }

        public float SaveDataMethodInputValue { get; private set; }

        public bool SaveDataMethodWasCalled { get;  private set; }

        public bool LogMessageMethodWasCalled { get;  private set; }

        public bool NeedThrowException { get; set; }


        public void LogMessage(string message)
        {
            if (NeedThrowException)
                throw new Exception("Fake exception");
            LogMessageMethodWasCalled = true;
            LogMessageMethodInputValue = message;
        }
        public void SaveData(float temperature)
        {
            if (NeedThrowException)
                throw new Exception("Fake exception");
            SaveDataMethodWasCalled = true;
            SaveDataMethodInputValue = temperature;
        } 
    }
}

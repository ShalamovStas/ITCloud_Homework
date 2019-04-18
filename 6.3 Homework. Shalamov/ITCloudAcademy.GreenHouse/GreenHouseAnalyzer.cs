using ITCloudAcademy.GreenHouse.Interfaces;
using System;

namespace ITCloudAcademy.GreenHouse
{
    public class GreenHouseAnalyzer
    {
        private readonly IDataSaver dataSaver;
        private readonly IClient greenHouseClient;
        private readonly TemperatureLevel saveDataLevel;
        private readonly TemperatureLevel logMessageLevel;


        public GreenHouseAnalyzer(TemperatureLevel saveDataLevel, TemperatureLevel logMessageLevel,
            IDataSaver fakeDataSaver, IClient fakeGreenHouseClient)
        {
            this.saveDataLevel = saveDataLevel;
            this.logMessageLevel = logMessageLevel;
            dataSaver = fakeDataSaver;
            greenHouseClient = fakeGreenHouseClient;
        }


        public GreenHouseAnalyzer(TemperatureLevel saveDataLevel, TemperatureLevel logMessageLevel)
        {
            this.saveDataLevel = saveDataLevel;
            this.logMessageLevel = logMessageLevel;
            dataSaver = new DataSaver();
            greenHouseClient = new GreenHouseClient();
        }

        public void AnalyzeData()
        {
            float temperature;

            try
            {
                temperature = greenHouseClient.GetTemperature();
            }
            catch (Exception exception)
            {
                throw new GreenHouseException("Failed to get data from green house. Look on inner exception for more details.", exception);
            }
            
            if (temperature <= 12 && (logMessageLevel & TemperatureLevel.Cold) > 0)
            {
                try
                {
                    dataSaver.LogMessage("ALARM! Experiencing cold temperature. Take an action immediatelly.");
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 12 && temperature <= 25 && (logMessageLevel & TemperatureLevel.Normal) > 0)
            {
                try
                {
                    dataSaver.LogMessage("Temperature is ok. Nothing to be afraid of.");
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 25 && temperature <= 38 && (logMessageLevel & TemperatureLevel.Warm) > 0)
            {
                try
                {
                    dataSaver.LogMessage("It's rather warm in the green house. Stay vigilant. It may become really hot soon.");
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 38 && (logMessageLevel & TemperatureLevel.Hot) > 0)
            {
                try
                {
                    dataSaver.LogMessage("ALARM! Green house is burning now! Take an action immediately. Otherwise you will lose your crop.");
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature <= 12 && (saveDataLevel & TemperatureLevel.Cold) > 0)
            {
                try
                {
                    dataSaver.SaveData(temperature);
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 12 && temperature <= 25 && (saveDataLevel & TemperatureLevel.Normal) > 0)
            {
                try
                {
                    dataSaver.SaveData(temperature);
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 25 && temperature <= 38 && (saveDataLevel & TemperatureLevel.Warm) > 0)
            {
                try
                {
                    dataSaver.SaveData(temperature);
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }

            if (temperature > 38 && (saveDataLevel & TemperatureLevel.Hot) > 0)
            {
                try
                {
                    dataSaver.SaveData(temperature);
                }
                catch (Exception exception)
                {
                    throw new DataSaverException("Failed to save data. Look on inner exception for more details.", exception);
                }
            }
        }
    }
}

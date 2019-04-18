using System;
using System.Collections.Generic;
using ITCloudAcademy.GreenHouse;
using ITCloudAcademy.GreenHouse.FakeIdentities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenHouseTests
{
    [TestClass]
    public class GreenHouseTests
    {
        private FakeDataSaver dataServer;
        private FakeGreenHouseClient greenHouseClient;

        [TestInitialize]
        public void InitialiseFakeIdentities()
        {
            dataServer = new FakeDataSaver();
            greenHouseClient = new FakeGreenHouseClient();  

        }
        //SaveData
        //1) Если вы установили конкретный saveDataLevel, то при соответствующей температуре
        //на DataSaver.SaveData передадутся данные. Проверить нужно отдельно для каждого
        //TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold, 12)]
        [DataRow(TemperatureLevel.Normal, 20)]
        [DataRow(TemperatureLevel.Warm, 30)]
        [DataRow(TemperatureLevel.Hot, 40)]
        public void AnalyzeData_SaveDataMethodInputValueShouldBeRight(TemperatureLevel level, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: level,
                logMessageLevel: TemperatureLevel.All, 
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();
            
            Assert.AreEqual(dataServer.SaveDataMethodInputValue, temperature);
        }

        //2)    Если вы не установили saveDataLevel (или установили другой saveDataLevel), то на 
        //DataSaver.SaveData НЕ будут передаваться данные. Проверить нужно отдельно для
        //каждого TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.None, 12)]
        [DataRow(TemperatureLevel.Cold, 15)]
        [DataRow(TemperatureLevel.Normal, 30)]
        [DataRow(TemperatureLevel.Warm, 40)]
        [DataRow(TemperatureLevel.Hot, 20)]
        public void AnalyzeData_SaveDataMethodShouldNotBeCalled(TemperatureLevel saveDataLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.SaveDataMethodWasCalled, false);
        }

        //3) Сделать одну комбинацию saveDataLevel(например Warm | Cold) и проверить что 
        //если выставить температуру соответствующую Warm, то на DataSaver.SaveData 
        //передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 10)]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 30)]
        public void AnalyzeData_SaveDataMethodShouldBeCalledBecauseTemperatureIsRight(TemperatureLevel saveDataLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.SaveDataMethodInputValue, temperature);
        }

        //4) Сделать одну комбинацию saveDataLevel (например Warm | Cold) и проверить что 
        //если выставить температуру не соответствующую ни одному выбраному уровню, то на 
        //DataSaver.SaveData НЕ передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 15)]
        public void AnalyzeData_SaveData_ShouldNotBeCalledDueToTheWrongTemperature(TemperatureLevel saveDataLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.SaveDataMethodWasCalled, false);
        }

        //5) Для каждого saveDataLevel отдельно проверить, что если на DataSaver.SaveData 
        //сгенерируется какой-нибудь exception, 
        //то он обернется в DataSaverException. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold, 12)]
        [DataRow(TemperatureLevel.Normal, 15)]
        [DataRow(TemperatureLevel.Warm, 30)]
        [DataRow(TemperatureLevel.Hot, 40)]
        public void AnalyzeData__InSaveDataMethodDataSaverExceptionHasToBeThrown(TemperatureLevel saveDataLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;
            dataServer.NeedThrowException = true;

            Assert.ThrowsException<DataSaverException>(() => analyzer.AnalyzeData());
        }

        //LogMessage
        //1) Если вы установили конкретный logMessageLevel, то при соответствующей температуре
        //на LogMessage передадутся данные. Проверить нужно отдельно для каждого
        //TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold, 10, "ALARM! Experiencing cold temperature. Take an action immediatelly.")]
        [DataRow(TemperatureLevel.Normal, 20, "Temperature is ok. Nothing to be afraid of.")]
        [DataRow(TemperatureLevel.Warm, 30, "It's rather warm in the green house. Stay vigilant. It may become really hot soon.")]
        [DataRow(TemperatureLevel.Hot, 40, "ALARM! Green house is burning now! Take an action immediately. Otherwise you will lose your crop.")]
        public void AnalyzeData_LogMessageInputValueShouldBeRight(TemperatureLevel level, float temperature, string inputValue)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: level,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.LogMessageMethodInputValue, inputValue);
        }

        //2)    Если вы не установили logMessageLevel (или установили другой logMessageLevel), то на 
        //LogMessage НЕ будут передаваться данные. Проверить нужно отдельно для
        //каждого TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.None, 12)]
        [DataRow(TemperatureLevel.Cold, 15)]
        [DataRow(TemperatureLevel.Normal, 30)]
        [DataRow(TemperatureLevel.Warm, 40)]
        [DataRow(TemperatureLevel.Hot, 20)]
        public void AnalyzeData_LogMessageMethodShouldNotBeCalled(TemperatureLevel logLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: logLevel,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.LogMessageMethodWasCalled, false);
        }

        //3) Сделать одну комбинацию logMessageLevel(например Warm | Cold) и проверить что 
        //если выставить температуру соответствующую Warm, то на logMessage
        //передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 10, "ALARM! Experiencing cold temperature. Take an action immediatelly.")]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 30, "It's rather warm in the green house. Stay vigilant. It may become really hot soon.")]
        public void AnalyzeData_LogMessageMethodShouldBeCalledWithRightValue(TemperatureLevel logLevel, float temperature, string rightValue)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: logLevel,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.LogMessageMethodInputValue, rightValue);
        }

        //4) Сделать одну комбинацию saveDataLevel (например Warm | Cold) и проверить что 
        //если выставить температуру не соответствующую ни одному выбраному уровню, то на 
        //DataSaver.SaveData НЕ передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 15)]
        public void AnalyzeData_LogMessageMethodShouldNotBeCalledDueToTheWrongTemperature(TemperatureLevel logLevel, float temperature)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: logLevel,
                fakeDataSaver: dataServer,
                fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.Temperature = temperature;

            analyzer.AnalyzeData();

            Assert.AreEqual(dataServer.LogMessageMethodWasCalled, false);
        }

        //5) Для каждого saveDataLevel отдельно проверить, что если на DataSaver.SaveData 
        //сгенерируется какой-нибудь exception, 
        //то он обернется в DataSaverException. (~4 теста)
        [TestMethod]
        [ExpectedException(typeof(GreenHouseException))]
        public void AnalyzeData_InGetTemperatureMethodGreenHouseExceptionHasToBeThrown()
        {
            var analyzer = new GreenHouseAnalyzer(
               saveDataLevel: TemperatureLevel.All,
               logMessageLevel: TemperatureLevel.All,
               fakeDataSaver: dataServer,
               fakeGreenHouseClient: greenHouseClient);

            greenHouseClient.NeedThrowException = true;

            analyzer.AnalyzeData();
        }
    }
}

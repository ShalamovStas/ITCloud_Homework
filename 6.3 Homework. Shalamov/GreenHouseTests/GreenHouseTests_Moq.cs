using System;
using ITCloudAcademy.GreenHouse;
using ITCloudAcademy.GreenHouse.FakeIdentities;
using ITCloudAcademy.GreenHouse.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GreenHouseTests
{
    [TestClass]
    public class GreenHouseTests_Moq
    {
        private Mock<IDataSaver> saver;
        private Mock<IClient> client;

        [TestInitialize]
        public void Init()
        {
            saver = new Mock<IDataSaver>();
            client = new Mock<IClient>();
        }

        //1) Если вы установили конкретный saveDataLevel, то при соответствующей температуре
        //на DataSaver.SaveData передадутся данные. Проверить нужно отдельно для каждого
        //TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold, 12)]
        [DataRow(TemperatureLevel.Normal, 15)]
        [DataRow(TemperatureLevel.Warm, 30)]
        [DataRow(TemperatureLevel.Hot, 40)]
        public void Moq_AnalyzeData_LogMessageShouldBeEqual(TemperatureLevel saveDataLevel, float temperature)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.SaveData(It.IsAny<float>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.SaveData(temperature), Times.Once());
        }

        //2)    Если вы не установили saveDataLevel (или установили другой saveDataLevel), то на 
        //DataSaver.SaveData НЕ будут передаваться данные. Проверить нужно отдельно для
        //каждого TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.None, 12)]
        [DataRow(TemperatureLevel.Cold, 20)]
        [DataRow(TemperatureLevel.Normal, 30)]
        [DataRow(TemperatureLevel.Warm, 40)]
        [DataRow(TemperatureLevel.Hot, 20)]
        public void Moq_AnalyzeData_SaveDataMethodShouldNotBeCalled(TemperatureLevel saveDataLevel, float temperature)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.SaveData(It.IsAny<float>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.SaveData(temperature), Times.Never());
        }

        //3) Сделать одну комбинацию saveDataLevel(например Warm | Cold) и проверить что 
        //если выставить температуру соответствующую Warm, то на DataSaver.SaveData 
        //передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 10)]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 30)]
        public void Moq_AnalyzeData_SaveDataMethodShouldBeCalledBecauseTemperatureIsRight(TemperatureLevel saveDataLevel, float temperature)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.SaveData(It.IsAny<float>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.SaveData(temperature), Times.AtMostOnce());
        }

        //4) Сделать одну комбинацию saveDataLevel (например Warm | Cold) и проверить что 
        //если выставить температуру не соответствующую ни одному выбраному уровню, то на 
        //DataSaver.SaveData НЕ передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 15)]
        public void Moq_AnalyzeData_SaveData_ShouldNotBeCalledDueToTheWrongTemperature(TemperatureLevel saveDataLevel, float temperature)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.SaveData(It.IsAny<float>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: saveDataLevel,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.SaveData(temperature), Times.Never());
        }

        //5) Для каждого saveDataLevel отдельно проверить, что если на DataSaver.SaveData 
        //сгенерируется какой-нибудь exception, 
        //то он обернется в DataSaverException. (~4 теста)
        [TestMethod]
        [DataRow(typeof(Exception), 12)]
        [DataRow(typeof(ArgumentNullException), 15)]
        [DataRow(typeof(NullReferenceException), 13)]
        [DataRow(typeof(IndexOutOfRangeException), 40)]
        [ExpectedException(typeof(DataSaverException))]
        public void Moq_AnalyzeData_InSaveDataMethodDataSaverExceptionHasToBeThrown(Type exceptionType, float temperature)
        {
            Exception e = (Exception)Activator.CreateInstance(exceptionType);

            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.SaveData(It.IsAny<float>())).Throws(e);

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();
        }

        //1) Если вы установили конкретный logMessageLevel, то при соответствующей температуре
        //на LogMessage передадутся данные. Проверить нужно отдельно для каждого
        //TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold, 10, "ALARM! Experiencing cold temperature. Take an action immediatelly.")]
        [DataRow(TemperatureLevel.Normal, 20, "Temperature is ok. Nothing to be afraid of.")]
        [DataRow(TemperatureLevel.Warm, 30, "It's rather warm in the green house. Stay vigilant. It may become really hot soon.")]
        [DataRow(TemperatureLevel.Hot, 40, "ALARM! Green house is burning now! Take an action immediately. Otherwise you will lose your crop.")]
        public void Moq_AnalyzeData_LogMessageMethodShouldBeCalled(TemperatureLevel logLevel, float temperature, string logMessage)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.LogMessage(It.IsAny<string>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: logLevel,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.LogMessage(logMessage), Times.Once());
        }

        //2)    Если вы не установили logMessageLevel (или установили другой logMessageLevel), то на 
        //LogMessage НЕ будут передаваться данные. Проверить нужно отдельно для
        //каждого TemperatureLevel. Комбинации TemperatureLevel здесь проверять не нужно. (~4 теста)
        [TestMethod]

        [DataRow(TemperatureLevel.Cold, 15, "LARM! Experiencing cold temperature. Take an action immediatelly.")]
        [DataRow(TemperatureLevel.Normal, 30, "Temperature is ok. Nothing to be afraid of.")]
        [DataRow(TemperatureLevel.Warm, 40, "It's rather warm in the green house. Stay vigilant. It may become really hot soon.")]
        [DataRow(TemperatureLevel.Hot, 20, "ALARM!Green house is burning now!Take an action immediately.Otherwise you will lose your crop.")]
        public void Moq_AnalyzeData_LogMessageMethodShouldNotBeCalled(TemperatureLevel logLevel, float temperature, string logMessage)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.LogMessage(It.IsAny<string>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: logLevel,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.LogMessage(logMessage), Times.Never());
        }

        //3) Сделать одну комбинацию logMessageLevel(например Warm | Cold) и проверить что 
        //если выставить температуру соответствующую Warm, то на logMessage
        //передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 10, "LARM! Experiencing cold temperature. Take an action immediatelly.")]
        [DataRow(TemperatureLevel.Cold | TemperatureLevel.Warm, 30, "Temperature is ok. Nothing to be afraid of.")]
        public void Moq_AnalyzeData_LogMessageMethodShouldBeCalledBecauseTemperatureIsRight(TemperatureLevel level, float temperature, string message)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.LogMessage(It.IsAny<string>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel:level,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.LogMessage(message), Times.AtMostOnce());
        }

        //4) Сделать одну комбинацию logMessageLevel (например Warm | Cold) и проверить что 
        //если выставить температуру не соответствующую ни одному выбраному уровню, то на 
        //logMessage НЕ передадутся данные. (1 тест)
        [TestMethod]
        [DataRow(TemperatureLevel.Normal | TemperatureLevel.Hot, 10, "Temperature is ok. Nothing to be afraid of.")]
        public void Moq_AnalyzeData_LogMessageMethodShouldNotBeCalledDueToTheWrongTemperature(TemperatureLevel saveDataLevel, float temperature, string message)
        {
            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.LogMessage(It.IsAny<string>()));

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: saveDataLevel,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();

            saver.Verify(d => d.LogMessage(message), Times.Never());
        }

        //5) Для каждого logMessageLevel отдельно проверить, что если на logMessage 
        //сгенерируется какой-нибудь exception, 
        //то он обернется в DataSaverException. (~4 теста)
        [TestMethod]
        [DataRow(typeof(Exception), 12)]
        [DataRow(typeof(ArgumentNullException), 15)]
        [DataRow(typeof(NullReferenceException), 13)]
        [DataRow(typeof(IndexOutOfRangeException), 40)]
        [ExpectedException(typeof(DataSaverException))]
        public void Moq_AnalyzeData_LogMessageMethodDataSaverExceptionHasToBeThrown(Type exceptionType, float temperature)
        {
            Exception e = (Exception)Activator.CreateInstance(exceptionType);

            client.Setup(i => i.GetTemperature()).Returns(temperature);
            saver.Setup(d => d.LogMessage(It.IsAny<string>())).Throws(e);

            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All,
                logMessageLevel: TemperatureLevel.All,
                fakeDataSaver: saver.Object,
                fakeGreenHouseClient: client.Object);

            analyzer.AnalyzeData();
        }
    }
}

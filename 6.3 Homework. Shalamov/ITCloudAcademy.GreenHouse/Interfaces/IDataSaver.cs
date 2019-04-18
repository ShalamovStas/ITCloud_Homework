namespace ITCloudAcademy.GreenHouse.Interfaces
{
    public interface IDataSaver
    {
        void LogMessage(string message);
        void SaveData(float temperature);

    }
}

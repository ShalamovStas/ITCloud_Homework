using System.Threading;

namespace ITCloudAcademy.GreenHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new GreenHouseAnalyzer(
                saveDataLevel: TemperatureLevel.All, 
                logMessageLevel: TemperatureLevel.Hot | TemperatureLevel.Warm);

            while (true)
            {
                System.Console.WriteLine("Starting to analyze...");
                analyzer.AnalyzeData();
                System.Console.WriteLine("Analyze is finished");
                Thread.Sleep(2000);
            }
        }
    }
}

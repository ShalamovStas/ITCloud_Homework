using ITCloudAcademy.GreenHouse.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace ITCloudAcademy.GreenHouse
{
    public class GreenHouseClient : IClient
    {
        public float GetTemperature()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://green.octopan.net:8080/api/data");
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeAnonymousType(json, new { temperature = 0.0F, humidity = 0.0F }).temperature;
            }
        }
    }
}

using ITCloudAcademy.GreenHouse.Interfaces;
using System;

namespace ITCloudAcademy.GreenHouse.FakeIdentities
{
    class FakeGreenHouseClient : IClient
    {
        public bool NeedThrowException { get; set; }

        public float Temperature { get; set; }

        public float GetTemperature()
        {
            if (NeedThrowException)
                throw new Exception("This is a fake exception");
            return Temperature;
        }
    }
}

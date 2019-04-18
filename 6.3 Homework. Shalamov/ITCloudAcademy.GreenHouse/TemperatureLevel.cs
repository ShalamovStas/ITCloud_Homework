using System;

namespace ITCloudAcademy.GreenHouse
{
    [Flags]
    public enum TemperatureLevel
    {
        None = 0,

        /// <summary>
        /// Less or equal than 12 degrees
        /// </summary>
        Cold = 1 << 0,

        /// <summary>
        /// More than 12 degrees and less or equal than 25 degrees
        /// </summary>
        Normal = 1 << 1,

        /// <summary>
        /// More than 25 degrees and less or equal than 38 degrees
        /// </summary>
        Warm = 1 << 2,

        /// <summary>
        /// More than 38 degrees
        /// </summary>
        Hot = 1 << 3,

        All = Cold | Normal | Warm | Hot
    }
}

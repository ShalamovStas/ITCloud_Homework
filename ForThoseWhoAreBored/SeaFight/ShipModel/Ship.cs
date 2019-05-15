using SeaFight.Common;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Session
{

    class Ship
    {
       
        public Square[] Parts { get; set; }

        public ShipType ShipType { get; set; }

        public Ship(Square[] parts)
        {
            Parts = parts;
            FoundType();
            SetColor();
            
        }

        private void FoundType()
        {
            switch (Parts.Length)
            {
                case 1:
                    ShipType = ShipType.Submarine;
                    break;
                case 2:
                    ShipType = ShipType.Destroyer;
                    break;
                case 3:
                    ShipType = ShipType.Cruiser;
                    break;
                case 4:
                    ShipType = ShipType.Battleship;
                    break;
                default:
                    ShipType = ShipType.Unknown;
                    break;
            }
        }

        private void SetColor()
        {
            ConsoleColor color = ConsoleColor.White;
            switch (ShipType)
            {
                case ShipType.Submarine:
                    color = ConsoleColor.DarkRed;
                    break;
                case ShipType.Destroyer:
                    color = ConsoleColor.DarkBlue;
                    break;
                case ShipType.Cruiser:
                    color = ConsoleColor.DarkYellow;
                    break;
                case ShipType.Battleship:
                    color = ConsoleColor.DarkCyan;
                    break;
                case ShipType.Unknown:
                    color = ConsoleColor.White;
                    break;
            }
            foreach (var item in Parts)
            {
                item.Color = color;
            }
        }
    }
}

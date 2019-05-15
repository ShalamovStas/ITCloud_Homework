using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{

    class Ship
    {
        public Square[] Parts { get; set; }

        public Ship(Square[] parts)
        {
            Parts = parts;
        }


    }
}

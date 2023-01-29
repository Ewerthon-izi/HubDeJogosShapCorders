using BattleShip.Board;
using Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleShip.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
            Name = "Battleship";
            Width = 3;
            LocaleType = LocaleType.Cruiser;
        }
    }
}

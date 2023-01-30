using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Board
{
    public enum LocaleType
    {
        [Description("o")]
        Empty,

        [Description("B")]
        Battleship,

        [Description("C")]
        Cruiser,

        [Description("S")]
        Submarine,

        [Description("X")]
        Hit,

        [Description("M")]
        Miss
    }

    public enum ShotResult
    {
        Miss,
        Hit
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Board
{
    public class GameBoard
    {
        public List<Panel> Panels { get; set; }

        public GameBoard()
        {
            Panels = new List<Panel>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Panels.Add(new Panel(i, j));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hub.Board;

namespace BattleShip.Board
{
    public class Panel{
        public LocaleType LocaleType { get; set; }
        public Coordinates Coordinates { get; set; }

        public Panel(int row, int column){
            Coordinates = new Coordinates(row, column);
            LocaleType = LocaleType.Empty;
        }

        public string Status{
            get{
                return LocaleType.GetAttributeOfType<DescriptionAttribute>().Description;

            }
        }

        public bool IsOccupied{
            get{
                return LocaleType == LocaleType.Battleship
                    || LocaleType == LocaleType.Cruiser
                    || LocaleType == LocaleType.Submarine;
            }
        }

        // Caso queira gerar um tabuleiro aleatorio este metodo ajudara
        public bool IsRandomAvailable{
            get{
                return (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
                    || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Board;

namespace Ships
{
    public abstract class Ship{
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public LocaleType LocaleType { get; set; }
        // retorna TRUE caso o numero de acertos forem maior ou igual ao seu tamanho
        public bool shipIsDead
        {
            get{
                return Hits >= Width;
            }
        }
    }
}

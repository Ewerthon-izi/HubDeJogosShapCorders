using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldWoman.Board
{
    public class BoardOldWoman
    {

        // Estado do tabuleiro
        public string[,] GameBoard = new string[,]
        {
            {"1", "2", "3"},
            {"4", "5", "6"},
            {"7", "8", "9"},
        };
        // printa o tabuleiro atual
		public void DisplayBoard()
        {
            Console.WriteLine();

            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    Console.Write("|{0}| ", GameBoard[i, j]);
                }
                Console.WriteLine("\n");
            }
        }
    }
}

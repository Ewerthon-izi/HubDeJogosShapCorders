using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldWoman.Board
{
    public class BoardOldWoman
    {

        // Tic Tac Toe Gameboard states
        public string[,] GameBoard = new string[,]
        {
            {"1", "2", "3"},
            {"4", "5", "6"},
            {"7", "8", "9"},
        };
        // Prints current board to the console
		public void DisplayBoard()
        {
            //Done: Output the board to the console
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

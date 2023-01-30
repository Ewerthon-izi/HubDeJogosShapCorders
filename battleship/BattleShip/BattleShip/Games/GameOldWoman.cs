using Hub.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Hub.Board;
using OldWoman.Board;

namespace Hub.OldWoman
{
    public class GameOldWoman
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player Winner { get; set; }
        public BoardOldWoman BoardOldWoman { get; set; }

        public GameOldWoman(Player p1, Player p2)
        {
            PlayerOne = p1;
            PlayerTwo = p2;
            BoardOldWoman = new BoardOldWoman();
        }

        public Player Play()
        {
            int maxTurnCount = 9;
            Player currentPlayer = null;
            int i = 0;
            for (i = 0; i < maxTurnCount; i++)
            {
                BoardOldWoman.DisplayBoard();
                currentPlayer = NextPlayer();
                currentPlayer.TakeTurn(BoardOldWoman);
                if (CheckForWinner(BoardOldWoman)) break;
                SwitchPlayer();
            }
            //Default return is null (signals a draw)
            BoardOldWoman.DisplayBoard();
            if (i == 9) return null;
            else return currentPlayer;
        }

        public bool CheckForWinner(BoardOldWoman board)
        {
            int[][] winners = new int[][]
            {
                new[] {1,2,3},
                new[] {4,5,6},
                new[] {7,8,9},

                new[] {1,4,7},
                new[] {2,5,8},
                new[] {3,6,9},

                new[] {1,5,9},
                new[] {3,5,7}
            };

            // Given all the winning conditions, Determine the winning logic. 
            for (int i = 0; i < winners.Length; i++)
            {
                Coordinates p1 = Player.CoordinatesForNumber(winners[i][0]);
                Coordinates p2 = Player.CoordinatesForNumber(winners[i][1]);
                Coordinates p3 = Player.CoordinatesForNumber(winners[i][2]);

                string a = BoardOldWoman.GameBoard[p1.Row, p1.Column];
                string b = BoardOldWoman.GameBoard[p2.Row, p2.Column];
                string c = BoardOldWoman.GameBoard[p3.Row, p3.Column];

                // DONE:  Determine a winner has been reached. 
                // return true if a winner has been reached. 
                if (a == b && a == c) return true;

            }

            return false;
        }

        public Player NextPlayer()
        {
            return (PlayerOne.IsTurn) ? PlayerOne : PlayerTwo;
        }

        public void SwitchPlayer()
        {
            if (PlayerOne.IsTurn)
            {
                PlayerOne.IsTurn = false;
                PlayerTwo.IsTurn = true;
            }
            else
            {
                PlayerOne.IsTurn = true;
                PlayerTwo.IsTurn = false;
            }
        }
    }
}



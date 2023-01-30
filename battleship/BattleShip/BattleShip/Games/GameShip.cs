using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Games
{
    public class GameShip
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public GameShip()
        {
            Player1 = new Player("Ewerthon", "12345");
            Player2 = new Player("Joao", "54321");

            Player1.PlaceShips();
            Player2.PlaceShips();

            Player1.OutputBoards();
            Player2.OutputBoards();
        }

        public void PlayRound()
        {
            //Inicio dos turnos.
            var coordinates = Player1.FireShot();
            var result = Player2.ProcessShot(coordinates);
            Player1.ProcessShotResult(coordinates, result);

            if (!Player2.HasLost) //Verifica a derrota do player 2.
            {
                coordinates = Player2.FireShot();
                result = Player1.ProcessShot(coordinates);
                Player2.ProcessShotResult(coordinates, result);
            }
        }

        public void PlayToEnd()
        {
            while (!Player1.HasLost && !Player2.HasLost)
            {
                PlayRound();
            }

            Player1.OutputBoards();
            Player2.OutputBoards();

            if (Player1.HasLost)
            {
                Console.WriteLine(Player2.Name + " Ganhou o jogo!");
            }
            else if (Player2.HasLost)
            {
                Console.WriteLine(Player1.Name + " Ganhou o jogo!");
            }
        }
    }
}

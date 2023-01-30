using Hub.Games;
using Hub.OldWoman;


namespace BattleShip
{
    class Program
    {

        static void PlayOldWoman()
        {
            Console.WriteLine("Bem vindo ao jogo da veia");

            //Set player attributes
            Player playerOne = new Player("Teste1");
            Player playerTwo = new Player("Teste2");

            Console.WriteLine("Player 1 entre com seu nome");
            playerOne.Name = Console.ReadLine();
            Console.WriteLine("Player 2, Entre com seu nome");
            playerTwo.Name = Console.ReadLine();

            playerOne.Marker = "X";
            playerTwo.Marker = "O";

            playerOne.IsTurn = true;
            playerTwo.IsTurn = false;

            //Initiate game play
            GameOldWoman currentGame = new GameOldWoman(playerOne, playerTwo);

            Player winner = currentGame.Play();

            //Display game results
            if (winner != null)
            {
                Console.WriteLine($"{winner.Name} venceu!");
            }
            else
            {
                Console.WriteLine($"Deu velha");
            }

        }

        static void PlayBattleShip()
        {
            int player1Wins = 0, player2Wins = 0;
            Console.WriteLine("Quantas jogos deseja jogar?");
            var numGames = int.Parse(Console.ReadLine());

            for (int i = 0; i < numGames; i++)
            {
                GameShip game1 = new GameShip();
                game1.PlayToEnd();
                if (game1.Player1.HasLost)
                {
                    player2Wins++;
                }
                else
                {
                    player1Wins++;
                }
            }

            Console.WriteLine("Player 1 Wins: " + player1Wins.ToString());
            Console.WriteLine("Player 2 Wins: " + player2Wins.ToString());
        }

        static void Main(string[] args)
        {

            PlayBattleShip();

            PlayOldWoman();
        }
    }
}
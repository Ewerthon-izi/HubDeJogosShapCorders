using Hub.Games;
using Hub.OldWoman;
using System.Security.AccessControl;


namespace BattleShip
{
    class Program
    {

        static void PlayOldWoman(Player player1)
        {
            Console.WriteLine("Bem vindo ao jogo da veia");

            //Set player attributes
            Player playerTwo = new Player("Teste2", "54321");

            Console.WriteLine("Player 2, Entre com seu nome");
            playerTwo.Name = Console.ReadLine();

            player1.Marker = "X";
            playerTwo.Marker = "O";

            player1.IsTurn = true;
            playerTwo.IsTurn = false;

            //Initiate game play
            GameOldWoman currentGame = new GameOldWoman(player1, playerTwo);

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

        static void ShowMenuBeforeLogin()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Login");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void ShowMenuAfterLogin()
        {
            Console.WriteLine("Qual jogo deseja jogar?");
            Console.WriteLine("1 - Battleship");
            Console.WriteLine("2 - Jogo Da velha");
            Console.Write("Digite a opção desejada: ");
        }

        static void Main(string[] args)
        {
            int option;
            List<Player> usuarios = new List<Player>();
            string nome;
            string password;
            Player currentUser = null;
            do
            {
                Console.WriteLine("-----------------");
                ShowMenuBeforeLogin();
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        Console.Write("Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("password: ");
                        password = Console.ReadLine();
                        usuarios.Add(new Player(nome, password));
                        break;
                    case 2:
                        Console.Write("Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("password: ");
                        password = Console.ReadLine();
                        //Shalow copy, caso eu altere o currentUser ira alterar o usurio original tbm
                        currentUser = (Player)usuarios.FirstOrDefault(x => x.Name == nome);
                        if (currentUser == null)
                            Console.WriteLine("Email ou senha invalida");
                        else
                        {
                            if (currentUser.Login(nome, password))
                                Console.WriteLine("Login efetuado com sucesso");
                            else
                            {
                                Console.WriteLine("Nome ou senha invalida");
                                currentUser = null;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Opção digitada invalida");
                        break;

                }
                while (currentUser != null && option != 0)
                {
                    ShowMenuAfterLogin();
                    option = int.Parse(Console.ReadLine());

                    Console.WriteLine("-----------------");

                    switch (option)
                    {
                        case 1:
                            PlayBattleShip();
                            break;
                        case 2:
                            PlayOldWoman(currentUser);
                            break;
                        default:
                            Console.WriteLine("Codigo digitado não encontrado");
                            break;
                    }

                    Console.WriteLine("-----------------");

                }
                Console.Clear();
            } while (currentUser == null && option != 0);
        }
    }
}
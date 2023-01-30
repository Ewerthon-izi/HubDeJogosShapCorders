using Hub.Games;
using Hub.OldWoman;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace BattleShip
{
    class Program
    {

        static void PlayOldWoman(Player player1, List<Player> usuarios)
        {
            string nome;
            string password;
            char option;
            Console.WriteLine("Bem vindo ao jogo da veia");
            Player player2 = null;

            do
            {
                Console.WriteLine("Deseja entrar como convidado? y/n");
                option = char.Parse(Console.ReadLine());
                if (option == 'y')
                {
                    player2 = new Player("guest", "guest");
                }
                else
                {
                    Console.WriteLine("Efetuar login:");
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("password: ");
                    password = Console.ReadLine();
                    //Shalow copy, caso eu altere o currentUser ira alterar o usurio original tbm
                    player2 = (Player)usuarios.FirstOrDefault(x => x.Name == nome);
                    if (player2 == null)
                        Console.WriteLine("Email ou senha invalida");
                    else
                    {
                        if (player2.Login(nome, password))
                            Console.WriteLine("Login efetuado com sucesso");
                        else
                        {
                            Console.WriteLine("Nome ou senha invalida");
                            player2 = null;
                        }
                    }
                }
                Console.Clear();
            } while (player2 == null);

            player1.Marker = "X";
            player2.Marker = "O";

            player1.IsTurn = true;
            player2.IsTurn = false;

            //Initiate game play
            GameOldWoman currentGame = new GameOldWoman(player1, player2);

            Player winner = currentGame.Play();
            winner.ContWins++;

            //Display game results
            if (winner != null)
            {
                Console.WriteLine($"{winner.Name} venceu!");
                Console.WriteLine($"{winner.Name} esta com {winner.ContWins} vitorias");
            }
            else
            {
                Console.WriteLine($"Deu velha");
            }

        }

        static void PlayBattleShip(Player player1, List<Player> usuarios)
        {
            int player1Wins = 0, player2Wins = 0;
            Player player2 = null;
            string nome;
            string password;
            char option;

            do
            {
                Console.WriteLine("Deseja entrar como convidado? y/n");
                option = char.Parse(Console.ReadLine());
                if (option == 'y')
                {
                    player2 = new Player("guest", "guest");
                }
                else
                {
                    Console.WriteLine("Efetuar login:");
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("password: ");
                    password = Console.ReadLine();
                    //Shalow copy, caso eu altere o currentUser ira alterar o usurio original tbm
                    player2 = (Player)usuarios.FirstOrDefault(x => x.Name == nome);
                    if (player2 == null)
                        Console.WriteLine("Email ou senha invalida");
                    else
                    {
                        if (player2.Login(nome, password))
                            Console.WriteLine("Login efetuado com sucesso");
                        else
                        {
                            Console.WriteLine("Nome ou senha invalida");
                            player2 = null;
                        }
                    }
                }
                Console.Clear();
            } while (player2 == null);

            Console.WriteLine("Quantas jogos deseja jogar?");
            var numGames = int.Parse(Console.ReadLine());

            for (int i = 0; i < numGames; i++)
            {
                GameShip game1 = new GameShip(player1, player2);
                game1.PlayToEnd();
                if (game1.Player1.HasLost)
                {
                    player2.ContWins++;
                }
                else
                {
                    player1.ContWins++;
                }
            }
            Console.WriteLine("Player 1 Wins: " + player1.ContWins);
            Console.WriteLine("Player 2 Wins: " + player2.ContWins);
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
            Console.WriteLine("3 - Sair da conta");
            Console.WriteLine("0 - Para sair do programa");
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
                            PlayBattleShip(currentUser, usuarios);
                            break;
                        case 2:
                            PlayOldWoman(currentUser, usuarios);
                            break;
                        case 3:
                            currentUser = null;
                            break;
                        case 0:
                            Console.WriteLine("Encerrando o programa");
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
using BattleShip.Board;
using BattleShip.Ships;
using Hub.Board;
using Ships;
using Hub.OldWoman;
using OldWoman.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Hub.Games
{
    public class Player
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public GameBoard GameBoard { get; set; }
        public FiringBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.shipIsDead);
            }
        }

        public string Marker { get; set; }
        public bool IsTurn { get; set; }

        public Player(string name, string password)
        {
            Name = name;
            Ships = new List<Ship>()
            {
                new Submarine(),
                new Cruiser(),
                new Battleship(),
            };
            GameBoard = new GameBoard();
            FiringBoard = new FiringBoard();
            Password = password;
        }

        public bool Login(string name, string password)
        {
            if (!(this.Name == name))
                return false;
            if (!(this.Password == password))
                return false;
            return true;
        }

        //Funções para a batalha naval
        public void OutputBoards()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Seu tabuleiro:                          Tabuleiro do oponente:");
            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(GameBoard.Panels.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(FiringBoard.Panels.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PlaceShips()
        {
            //Gerador de classe randomica retirada de: http://stackoverflow.com/a/18267477/106356
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                //Gerando os barcos aleatorios

                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(1, 11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2; 

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //verificação se esta fora do tabuleiro
                    if (endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    //Verificando se a coordenada esta ocupada
                    var affectedPanels = GameBoard.Panels.Range(startrow, startcolumn, endrow, endcolumn);
                    if (affectedPanels.Any(x => x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var panel in affectedPanels)
                    {
                        panel.LocaleType = ship.LocaleType;
                    }
                    isOpen = false;
                }
            }
        }

        public Coordinates FireShot()
        {
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            Coordinates coords;
            if (hitNeighbors.Any())
            {
                coords = SearchingShot();
            }
            else
            {
                coords = RandomShot();
            }
            Console.WriteLine(Name + " Ação: \"Atirando em " + coords.Row.ToString() + ", " + coords.Column.ToString() + "\"");
            return coords;
        }


        private Coordinates RandomShot()
        {
            var availablePanels = FiringBoard.GetOpenRandomPanels();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var panelID = rand.Next(availablePanels.Count);
            return availablePanels[panelID];
        }

        private Coordinates SearchingShot()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            var neighborID = rand.Next(hitNeighbors.Count);
            return hitNeighbors[neighborID];
        }

        public ShotResult ProcessShot(Coordinates coords)
        {
            var panel = GameBoard.Panels.At(coords.Row, coords.Column);
            if (!panel.IsOccupied)
            {
                Console.WriteLine(Name + " Ação: \"Miss!\"");
                return ShotResult.Miss;
            }
            var ship = Ships.First(x => x.LocaleType == panel.LocaleType);
            ship.Hits++;
            Console.WriteLine(Name + " Ação: \"Hit!\"");
            if (ship.shipIsDead)
            {
                Console.WriteLine(Name + " Ação: \"Voce destruiu meu " + ship.Name + "!\"");
            }
            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates coords, ShotResult result)
        {
            var panel = FiringBoard.Panels.At(coords.Row, coords.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    panel.LocaleType = LocaleType.Hit;
                    break;

                default:
                    panel.LocaleType = LocaleType.Miss;
                    break;
            }
        }

        //Funçoes para o jogo da velha

        public Coordinates GetCoordinates(BoardOldWoman board)
        {
            Coordinates desiredCoordinate = null;
            while (desiredCoordinate is null)
            {
                Console.WriteLine("Please select a location");
                Int32.TryParse(Console.ReadLine(), out int Coordinates);
                desiredCoordinate = CoordinatesForNumber(Coordinates);
            }
            return desiredCoordinate;

        }

        public static Coordinates CoordinatesForNumber(int Coordinates)
        {
            switch (Coordinates)
            {
                case 1: return new Coordinates(0, 0); // Top Left
                case 2: return new Coordinates(0, 1); // Top Middle
                case 3: return new Coordinates(0, 2); // Top Right
                case 4: return new Coordinates(1, 0); // Middle Left
                case 5: return new Coordinates(1, 1); // Middle Middle
                case 6: return new Coordinates(1, 2); // Middle Right
                case 7: return new Coordinates(2, 0); // Bottom Left
                case 8: return new Coordinates(2, 1); // Bottom Middle 
                case 9: return new Coordinates(2, 2); // Bottom Right

                default: return null;
            }
        }

        public void TakeTurn(BoardOldWoman board)
        {
            IsTurn = true;

            Console.WriteLine($"{Name} it is your turn");

            Coordinates Coordinates = GetCoordinates(board);

            if (Int32.TryParse(board.GameBoard[Coordinates.Row, Coordinates.Column], out int _))
            {
                board.GameBoard[Coordinates.Row, Coordinates.Column] = Marker;
            }
            else
            {
                Console.WriteLine("This space is already occupied");
            }
        }

    }
}

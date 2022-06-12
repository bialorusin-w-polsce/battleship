using Battleship.Business;
using Battleship.Business.Entities;
using Battleship.Common.Entities;
using Battleship.Common.Enums;
using Battleship.DAL;

namespace Battleship.ConsoleUI
{
    public class Program
    {
        private static GameService _gameService;

        public static void Main(string[] args)
        {
            _gameService = new GameService(
            new ShipRepository(),
            new ShotRepository(),
            10);

            Play();
        }

        private static void Play()
        {
            string? input;

            while (true)
            {
                UI ui = new UI(_gameService, new Coordinate() { Row = 0, Column = 0 });
                ui.Display();
                Console.SetCursorPosition(0, _gameService.Size + 2);
                input = Console.ReadLine();
                DoAction(input);
                Console.Clear();
            }
        }

        private static ShipModel ParseShipInput(string input)
        {
            int currentIndex = 0;
            int column = input[currentIndex] - 'a';
            ++currentIndex;
            int row;
            if (input.Length == 4)
            {
                row = int.Parse(input.Substring(currentIndex, 1));
                ++currentIndex;
            }
            else if (input.Length == 5)
            {
                row = int.Parse(input.Substring(currentIndex, 2));
                currentIndex += 2;
            }
            else
            {
                throw new ArgumentException(nameof(input));
            }

            Direction direction = input[currentIndex] switch
            {
                'l' => Direction.Left,
                'r' => Direction.Right,
                'u' => Direction.Up,
                'd' => Direction.Down
            };
            ++currentIndex;
            int length = input[currentIndex] - '0';

            return new ShipModel()
            { FirstCoordinate = new Coordinate() { Row = row - 1, Column = column },
                Direction = direction,
                Length = length };
        }

        private static Coordinate ParseShotInput(string input)
        {
            int column = input[0] - 'a';
            int row = input[1] - '0';
            return new Coordinate() { Row = row, Column = column };
        }

        private static void DoAction(string input)
        {
            if (input[0] == 'p')
            {
                _gameService.PutShip(ParseShipInput(input.Substring(1)));
            }
            else if (input[0] == 's')
            {
                _gameService.Shoot(ParseShotInput(input.Substring(1)));
            }
        }
    }
}
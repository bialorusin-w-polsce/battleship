using Battleship.Business;
using Battleship.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ConsoleUI
{
    public class UI
    {
        private static char ship = '#';
        private static char hit = 'X';
        private static char miss = '*';
        private static char empty = '█';
        public GameService _gameService;
        public Coordinate _start;

        public UI(GameService gameService, Coordinate start)
        {
            _gameService = gameService;
            _start = start;
        }

        public void Display()
        {
            DisplayLettersAndNumbers();
            for (int i = 0; i < _gameService.Size; ++i)
            {
                for (int j = 0; j < _gameService.Size; ++j)
                {
                    var coordinate = new Coordinate() { Row = i, Column = j };
                    Console.SetCursorPosition(_start.Column + coordinate.Column + 2, _start.Row + coordinate.Row + 1);
                    if (_gameService.IsShip(coordinate) && _gameService.IsShot(coordinate))
                    {
                        Console.Write(hit);
                    }
                    else if (_gameService.IsShip(coordinate))
                    {
                        Console.Write(ship);
                    }
                    else if (_gameService.IsShot(coordinate))
                    {
                        Console.Write(miss);
                    }
                    else
                    {
                        Console.Write(empty);
                    }
                }
            }
        }

        public void DisplayLettersAndNumbers()
        {
            for (int i = 0; i < _gameService.Size; ++i)
            {
                Console.SetCursorPosition(_start.Column + i + 2, _start.Row);
                Console.Write((char)('a' + i));
                Console.SetCursorPosition(_start.Column, _start.Row + i + 1);
                Console.Write(i + 1);
                Console.SetCursorPosition(_start.Column + i + 1, _start.Row + _gameService.Size + 1);
                Console.Write((char)('a' + i));
                Console.SetCursorPosition(_start.Column + _gameService.Size + 2, _start.Row + i + 1);
                Console.Write(i + 1);
            }
        }
    }
}

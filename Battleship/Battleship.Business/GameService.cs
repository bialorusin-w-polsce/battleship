using Battleship.Business.Entities;
using Battleship.Common.Entities;
using Battleship.DAL.Entities;
using Battleship.DAL.Interfaces;
using Battleship.Common.Enums;

namespace Battleship.Business
{
    public class GameService
    {
        private IShipRepository _shipRepository;
        private IShotRepository _shotRepository;

        public GameService(IShipRepository shipRepository, IShotRepository shotRepository, int size)
        {
            _shipRepository = shipRepository;
            _shotRepository = shotRepository;
            Size = size;
        }

        public int Size { get; private set; }

        public bool PutShip(ShipModel shipModel)
        {
            ShipDbModel shipDbModel = new ShipDbModel() { Coordinates = new Coordinate[shipModel.Length] };
            (int, int) vector = shipModel.Direction switch
            {
                Direction.Left => (0, -1),
                Direction.Right => (0, 1),
                Direction.Up => (-1, 0),
                Direction.Down => (1, 0),
                _ => throw new InvalidOperationException(),
            };

            for (int i = 0; i < shipModel.Length; ++i)
            {
                shipDbModel.Coordinates[i] = new Coordinate()
                {
                    Row = shipModel.FirstCoordinate.Row + vector.Item1 * i,
                    Column = shipModel.FirstCoordinate.Column + vector.Item2 * i
                };
            }

            if (shipDbModel.Coordinates.Any(
                c => c.Row < 0 ||
                c.Column < 0 ||
                c.Row >= Size ||
                c.Column >= Size ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row - 1, Column = c.Column - 1}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row - 1, Column = c.Column }) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row - 1, Column = c.Column + 1}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row, Column = c.Column - 1}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row, Column = c.Column}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row, Column = c.Column - 1}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row + 1, Column = c.Column - 1}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row + 1, Column = c.Column}) != null ||
                _shipRepository.GetByCoordinate(new Coordinate() { Row = c.Row + 1, Column = c.Column + 1}) != null))
            {
                return false;
            }

            _shipRepository.Add(shipDbModel);
            return true;
        }

        public bool Shoot(Coordinate coordinate)
        {
            bool coordinateIsAlreadyShot = _shotRepository.Get(coordinate) != null;
            if (coordinateIsAlreadyShot)
            {
                return false;
            }

            _shotRepository.Add(coordinate);
            return true;
        }

        public bool IsShip(Coordinate coordinate)
        {
            return _shipRepository.GetByCoordinate(coordinate) != null;
        }

        public bool IsShot(Coordinate coordinate)
        {
            return _shotRepository.Get(coordinate) != null;
        }
    }
}

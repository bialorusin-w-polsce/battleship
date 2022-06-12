using Battleship.Common.Entities;
using Battleship.DAL.Entities;

namespace Battleship.DAL.Interfaces
{
    public interface IShipRepository
    {
        public void Add(ShipDbModel ship);

        public ShipDbModel GetByCoordinate(Coordinate coordinate);
    }
}

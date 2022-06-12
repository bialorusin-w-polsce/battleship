using Battleship.Common.Entities;
using Battleship.DAL.Entities;
using Battleship.DAL.Interfaces;
namespace Battleship.DAL
{
    public class ShipRepository : IShipRepository
    {
        private List<ShipDbModel> _shipDbModels = new List<ShipDbModel>();

        public void Add(ShipDbModel ship)
        {
            _shipDbModels.Add(ship);
        }

        public ShipDbModel? GetByCoordinate(Coordinate coordinate)
        {
            ShipDbModel? ship = _shipDbModels.FirstOrDefault(s => s.Coordinates.Contains(coordinate));
            return ship;
        }
    }
}

using Battleship.Common.Entities;
using Battleship.Common.Enums;

namespace Battleship.Business.Entities
{
    public class ShipModel
    {
        public Coordinate FirstCoordinate { get; set; }

        public Direction Direction { get; set; }

        public int Length { get; set; }
    }
}

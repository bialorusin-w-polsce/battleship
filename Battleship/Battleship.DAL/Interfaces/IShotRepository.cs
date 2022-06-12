using Battleship.Common.Entities;

namespace Battleship.DAL.Interfaces
{
    public interface IShotRepository
    {
        public void Add(Coordinate coordinate);

        public Coordinate? Get(Coordinate coordinate);
    }
}

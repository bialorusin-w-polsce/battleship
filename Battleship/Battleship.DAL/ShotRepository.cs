using Battleship.Common.Entities;
using Battleship.DAL.Interfaces;

namespace Battleship.DAL
{
    public class ShotRepository : IShotRepository
    {
        private List<Coordinate> _shots = new List<Coordinate>();

        public void Add(Coordinate coordinate)
        {
            _shots.Add(coordinate);
        }

        public Coordinate? Get(Coordinate coordinate)
        {
            bool success = _shots.Any(s => s.Equals(coordinate));
            if (success)
            {
                return _shots.Find(s => s.Equals(coordinate));
            }

            return null;
        }
    }
}

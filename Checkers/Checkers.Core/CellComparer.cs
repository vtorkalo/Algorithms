using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public class CellComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell x, Cell y)
        {
            return Helpers.CompareCells(x, y);
        }

        public int GetHashCode(Cell obj)
        {
            return 0;
        }
    }
    public class PathComparer : IEqualityComparer<List<Cell>>
    {
        private CellComparer _cellComparer = new CellComparer();

        public bool Equals(List<Cell> x, List<Cell> y)
        {
            return x.SequenceEqual(y, _cellComparer);
        }

        public int GetHashCode(List<Cell> obj)
        {
            return 0;
        }
    }
}

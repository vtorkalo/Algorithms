using System.Collections.Generic;

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
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public interface IPathGenerator
    {
        List<Move> GetPossibleMovements(CellState[,] field, Cell startCell);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public interface IPathGenerator
    {
        List<List<Cell>> GetPossibleMovements(CellState[,] field, Cell startCell);
    }
}

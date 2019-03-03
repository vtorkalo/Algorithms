using System;

namespace Checkers.Core
{
    public enum CellState
    {
        Empty = 0,
        White = 1,
        Black = -1,
        WhiteKing = 2,
        BlackKing = -2
    }
}

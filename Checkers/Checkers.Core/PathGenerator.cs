using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class PathGenerator : IPathGenerator
    {
        private StandartPathGenerator _standartPathGenerator = new StandartPathGenerator();
        private KingPathGenerator _kingPathGenerator = new KingPathGenerator();
        public PathGenerator()
        {

        }

        public List<Move> GetPossibleMovements(CellState[] field, Cell startCell)
        {
            var result = new List<Move>();
            var state = Helpers.GetCellState(field, startCell);
            if (state == CellState.White || state == CellState.Black)
            {
                result = _standartPathGenerator.GetPossibleMovements(field, startCell);
            }
            if (state == CellState.WhiteKing || state == CellState.BlackKing)
            {
                result = _kingPathGenerator.GetPossibleMovements(field, startCell);
            }

            return result;
        }
    }
}

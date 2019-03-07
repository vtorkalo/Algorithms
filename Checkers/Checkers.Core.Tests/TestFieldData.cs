using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core.Tests
{
    public static class TestFieldData
    {
        public static CellState[,] King_Moves_Case1()
        {
            var result = Helpers.GetEmptyField();

            result[3, 4] = CellState.WhiteKing;
            result[4, 5] = CellState.Black;
            result[4, 3] = CellState.Black;
            result[6, 3] = CellState.Black;
            result[6, 5] = CellState.Black;
            result[1, 6] = CellState.Black;

            return result;
        }

        public static CellState[,] King_Moves_Case2()
        {
            var result = Helpers.GetEmptyField();

            result[3, 4] = CellState.WhiteKing;
            result[4, 5] = CellState.Black;
            result[4, 3] = CellState.Black;
            result[6, 3] = CellState.Black;
            result[6, 5] = CellState.Black;
            result[1, 6] = CellState.Black;

            return result;
        }

        public static CellState[,] King_Moves_Case3()
        {
            var result = Helpers.GetEmptyField();

            result[3, 4] = CellState.WhiteKing;
            result[4, 5] = CellState.Black;
            result[6, 5] = CellState.Black;
            result[5, 2] = CellState.Black;
            result[2, 1] = CellState.Black;

            return result;
        }

        public static CellState[,] King_Moves_Case4()
        {
            var result = Helpers.GetEmptyField();
            
            result[7, 0] = CellState.WhiteKing;
            result[6, 1] = CellState.Black;
            result[3, 2] = CellState.Black;
            result[2, 3] = CellState.Black;
            result[5, 4] = CellState.Black;

            return result;
        }

        public static CellState[,] Case5()
        {
            var result = Helpers.GetEmptyField();

            return result;
        }
    }
}

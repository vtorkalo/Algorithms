using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public static class Field
    {
        public static int GetCellIndex(int row, int col)
        {
            int index = row * 4 + (col + row % 2 )/2;
            return index;
        }

        public static void GetRowCol(int index, out int row, out int col)
        {
            row = index / 4;
            col = (index % 4) * 2 + 1 - row % 2;
        }

        public static CellState GetValue(CellState[] field, Cell cell)
        {
            return GetValue(field, cell.Row, cell.Col);
        }

        public static CellState GetValue(CellState[] field, int row, int col)
        {            
            int index = GetCellIndex(row, col);
            return field[index];
        }

        public static void SetValue(CellState[] field, int row, int col, CellState value)
        {
            int index = GetCellIndex(row, col);
            field[index] = value;
        }

        public static void SetValue(CellState[] field, Cell cell, CellState value)
        {
            SetValue(field, cell.Row, cell.Col, value);
        }

        public static CellState[] From2dTo1d(CellState[,] field2d)
        {
            var result = new CellState[32];
            for (int i=0; i<32; i++)
            {
                GetRowCol(i, out int row, out int col);
                result[i] = field2d[row, col];
            }
            return result;
        }
    }
}

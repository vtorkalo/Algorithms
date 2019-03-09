using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public static class Helpers
    {
        public static void SetCell(CellState[,] field, Cell cell, CellState cellState)
        {
            field[cell.Row, cell.Col] = cellState;
        }

        public static Move MakeMove(CellState[,] field, Move path)
        {
            var startCell = path.First();
            var endCell = path.Last();
            var kills = path.Where(x => x.Kill);
            foreach (var kill in kills)
            {
                SetCell(field, kill, CellState.Empty);
                path.Kills++;
            }

            var startCellState = GetCellState(field, startCell);
            SetCell(field, startCell, CellState.Empty);
            SetCell(field, endCell, startCellState);

            var whiteCells = Helpers.GetCellsOfSide(field, Side.White);
            foreach (var cell in whiteCells)
            {
                var state = GetCellState(field, cell);
                if (state == CellState.White && cell.Row == 0)
                {
                    SetCell(field, cell, CellState.WhiteKing);
                    path.NewKings++;
                }
            }

            var blackCells = Helpers.GetCellsOfSide(field, Side.Black);
            foreach (var cell in blackCells)
            {
                var state = GetCellState(field, cell);
                if (state == CellState.Black && cell.Row == 7)
                {
                    SetCell(field, cell, CellState.BlackKing);
                    path.NewKings++;
                }
            }

            return path;
        }

        public static CellState[,] CopyField(CellState[,] field)
        {
            var result = new CellState[8, 8];
            Array.Copy(field, result, 64);

            return result;
        }

        public static Side GetOppositeSide(Side side)
        {
            return side == Side.White ? Side.Black : Side.White;
        }

        public static List<Cell> GetCellsOfSide(CellState[,] field, Side side)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(0);
            var result = new List<Cell>();

            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    CellState state = field[row, col];
                    if ((side == Side.White && Helpers.IsWhite(state))
                        ||(side == Side.Black && Helpers.IsBlack(state)))
                    {
                        result.Add(new Cell(row, col));
                    }
                }

            return result;
        }

        public static Cell GetRigthBottomNextCell(Cell currentCell, int distance)
        {
            return new Cell
            (
                currentCell.Row + distance,
                currentCell.Col + distance
            );
        }

        public static Cell GetLeftTopNextCell(Cell currentCell, int distance)
        {
            return new Cell
            (
                currentCell.Row - distance,
                currentCell.Col - distance
            );
        }

        public static Cell GetLeftBottomNextCell(Cell currentCell, int distance)
        {
            return new Cell
            (
                currentCell.Row + distance,
                currentCell.Col - distance
            );
        }

        public static Cell GetRightTopNextCell(Cell currentCell, int distance)
        {
            return new Cell
            (
                currentCell.Row - distance,
                currentCell.Col + distance
            );
        }

        public static List<Cell> GetNeightbors(Cell currentCell, CellState currentCellState)
        {
            List<Cell> result;
            switch (currentCellState)
            {
                case CellState.White:
                    result = GetWhiteMoves(currentCell);
                    break;
                case CellState.Black:
                    result = GetBlackMoves(currentCell);
                    break;
                default:
                    throw new NotSupportedException();
            }

            var filteredResult = FilterInRange(result);

            return filteredResult;
        }

        public static List<Cell> GetBlackMoves(Cell currentCell)
        {
            var result = new List<Cell>
            {
                new Cell
                (
                    currentCell.Row + 1,
                    currentCell.Col - 1
                ),
                new Cell
                (
                    currentCell.Row + 1,
                    currentCell.Col + 1
                ),
                new Cell
                (
                    currentCell.Row - 1,
                    currentCell.Col - 1,
                    isBack: true
                ),
                new Cell
                (
                    currentCell.Row - 1,
                    currentCell.Col + 1,
                    isBack: true
                )
            };

            return result;
        }

        public static List<Cell> GetWhiteMoves(Cell currentCell)
        {
            var result = new List<Cell>
            {
                new Cell
                (
                    currentCell.Row - 1,
                    currentCell.Col - 1
                ),
                new Cell
                (
                    currentCell.Row - 1,
                    currentCell.Col + 1
                ),
                new Cell
                (
                    currentCell.Row + 1,
                    currentCell.Col - 1,
                    isBack: true
                ),
                new Cell
                (
                    currentCell.Row + 1,
                    currentCell.Col + 1,
                    isBack: true
                )
            };

            return result;
        }

        public static List<Cell> FilterInRange(List<Cell> cells)
        {
            return cells.Where(c => IsInRange(c)).ToList();
        }

        public static bool IsInRange(Cell cell)
        {
            bool inRange = cell.Col >= 0 && cell.Col < 8 && cell.Row >= 0 && cell.Row < 8;
            return inRange;
        }

        public static bool CompareCells(Cell cell1, Cell cell2)
        {
            bool equal = cell1.Row == cell2.Row && cell1.Col == cell2.Col;
            return equal;
        }

        public static CellState GetCellState(CellState[,] field, Cell currentCell)
        {
            var state = field[currentCell.Row, currentCell.Col];
            return state;
        }

        public static bool CanKill(CellState[,] field, Cell currentCell, Cell enemyCell, Cell startCell)
        {
            Cell targetCell = GetCellAfterKill(currentCell, enemyCell);
            bool canKill = Helpers.IsInRange(targetCell)
                        && IsFree(field, startCell, targetCell);

            return canKill;
        }

        public static bool IsEnemy(CellState currentState, CellState otherState)
        {
            bool result = (IsWhite(currentState) && IsBlack(otherState))
                       || (IsBlack(currentState) && IsWhite(otherState));

            return result;
        }

        public static bool IsWhite(CellState state)
        {
            bool isWhite = state == CellState.White
                        || state == CellState.WhiteKing;

            return isWhite;
        }

        public static bool IsBlack(CellState state)
        {
            bool isWhite = state == CellState.Black
                        || state == CellState.BlackKing;

            return isWhite;
        }

        public static Cell GetCellAfterKill(Cell currentCell, Cell enemyCell)
        {
            var cellAfterKill = new Cell
            (
                enemyCell.Row + (enemyCell.Row - currentCell.Row),
                enemyCell.Col + (enemyCell.Col - currentCell.Col)
            );

            return cellAfterKill;
        }

        public static bool IsFree(CellState[,] field, Cell startCell, Cell targetCell)
        {
            return Helpers.GetCellState(field, targetCell) == CellState.Empty || Helpers.CompareCells(targetCell, startCell);
        }

        public static CellState[,] GetEmptyField()
        {
            var result = new CellState[,]
            {
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
            };

            return result;
        }

        public static CellState[,] GetInitialField()
        {
            var result = new CellState[8, 8]
            {
                { CellState.Empty, CellState.Black,CellState.Empty,CellState.Black,CellState.Empty,CellState.Black,CellState.Empty,CellState.Black},
                { CellState.Black, CellState.Empty,CellState.Black,CellState.Empty,CellState.Black,CellState.Empty,CellState.Black,CellState.Empty},
                { CellState.Empty, CellState.Black,CellState.Empty,CellState.Black,CellState.Empty,CellState.Black,CellState.Empty,CellState.Black},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                { CellState.White, CellState.Empty,CellState.White,CellState.Empty,CellState.White,CellState.Empty,CellState.White,CellState.Empty},
                { CellState.Empty, CellState.White,CellState.Empty,CellState.White,CellState.Empty,CellState.White,CellState.Empty,CellState.White},
                { CellState.White, CellState.Empty,CellState.White,CellState.Empty,CellState.White,CellState.Empty,CellState.White,CellState.Empty}
            };

            return result;
        }
    }
}

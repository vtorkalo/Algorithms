using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Core
{
    public static class Algorithms
    {
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

        public static CellState GetCellState(CellState[,] field, Cell currentCell)
        {
            var state = field[currentCell.Row, currentCell.Col];
            return state;
        }

        public static List<List<Cell>> GetPossibleMovements(CellState[,] field, Cell currentCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleMovementsRecursive(paths, currentPath, field, currentCell, currentCell);
            if (paths.Any(p=>p.Any(x=>x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }
            return paths;
        }

        private static List<Cell> FilterInRange(List<Cell> cells)
        {
            return cells.Where(c => IsInRange(c)).ToList();
        }

        private static bool IsInRange(Cell cell)
        {
            bool inRange = cell.Col >= 0 && cell.Col < 8 && cell.Row >= 0 && cell.Row < 8;
            return inRange;
        }


        private static void GetPossibleMovementsRecursive(List<List<Cell>> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
        {
            var currentCellState = GetCellState(field, currentCell);
            var startCellState = GetCellState(field, startCell);
            var neightbords = GetNeightbords(currentCell, startCellState);
            var enemiesPath = new List<Cell>();

            var emptyCells = new List<Cell>();

            foreach (var neightbor in neightbords)
            {
                if (GetCellState(field, neightbor) == CellState.Empty && !neightbor.IsBack && !currentPath.Any()) //first movement in path to empty cell
                {
                    emptyCells.Add(neightbor);
                }
                else
                {
                    if (neightbor.Row != currentCell.Row && neightbor.Col != currentCell.Col) //don't allow go back
                    {
                        enemiesPath.Add(neightbor);
                    }
                }

            }
            if (emptyCells.Any())
            {
                paths.Add(FilterInRange(emptyCells)); //just move without kill - create path with 1 element
            }

            foreach (var nextCell in FilterInRange(enemiesPath))
            {
                var nextCellState = GetCellState(field, nextCell);
                if (IsEnemy(startCellState, nextCellState) && CanKill(field, currentCell, nextCell))
                {
                    currentPath.Add(new Cell { Col = nextCell.Col, Row = nextCell.Row, Kill = true });
                    Cell afterKillCell = GetCellAfterKill(currentCell, nextCell);
                    currentPath.Add(afterKillCell);
                    GetPossibleMovementsRecursive(paths, currentPath, field, startCell, afterKillCell);
                    paths.Add(currentPath);
                }
            }
        }

        private static List<Cell> GetNeightbords(Cell currentCell, CellState currentCellState)
        {
            var result = new List<Cell>();

            if (currentCellState == CellState.White)
            {
                result.Add(new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col - 1
                });

                result.Add(new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col + 1
                });

                result.Add(new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col - 1,
                    IsBack = true
                });

                result.Add(new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col + 1,
                    IsBack = true
                });
            }

            if (currentCellState == CellState.Black)
            {
                result.Add(new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col - 1
                });

                result.Add(new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col + 1
                });
                result.Add(new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col - 1,
                    IsBack = true
                });

                result.Add(new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col + 1,
                    IsBack = true
                });
            }

            return FilterInRange(result);
        }

        private static bool CanKill(CellState[,] field, Cell currentCell, Cell enemyCell)
        {
            bool canKill = false;

            Cell targetCell = GetCellAfterKill(currentCell, enemyCell);
            if (IsInRange(targetCell) && GetCellState(field, targetCell) == CellState.Empty)
            {
                canKill = true;
            }

            return canKill;
        }

        private static Cell GetCellAfterKill(Cell currentCell, Cell enemyCell)
        {
            return new Cell
            {
                Row = enemyCell.Row + (enemyCell.Row - currentCell.Row),
                Col = enemyCell.Col + (enemyCell.Col - currentCell.Col),
            };
        }

        private static bool IsEnemy(CellState currentState, CellState state)
        {
            bool result = ((currentState == CellState.White || currentState == CellState.WhiteKing) && (state == CellState.Black || state == CellState.BlackKing)
                || (currentState == CellState.Black || currentState == CellState.BlackKing) && (state == CellState.White || state == CellState.WhiteKing));
            return result;
        }
    }
}

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

        private static int counter = 0;
        public static List<List<Cell>> GetPossibleMovements(CellState[,] field, Cell currentCell)
        {
            counter = 0;
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
            if (currentCell.Row == 5 && currentCell.Col == 4)
            {

            }
         
            var currentCellState = GetCellState(field, currentCell);
            var startCellState = GetCellState(field, startCell);
            var neightbords = GetNeightbords(currentCell, startCellState);
            var possibleEnemies = new List<Cell>();

            var emptyCells = new List<Cell>();

            foreach (var neightbor in neightbords)
            {
                if (GetCellState(field, neightbor) == CellState.Empty && !neightbor.IsBack && !currentPath.Any()) //first movement in path to empty cell
                {
                    emptyCells.Add(neightbor);
                }
                else
                {
                    if (!currentPath.Any(p => p.Row == neightbor.Row && p.Col == neightbor.Col && p.Kill))
                    //    if (neightbor.Row != currentCell.Row && neightbor.Col != currentCell.Col && !neightbor.Kill) //don't allow go back
                    {
                        possibleEnemies.Add(neightbor);
                    }
                }

            }
            if (emptyCells.Any())
            {
                paths.Add(FilterInRange(emptyCells)); //just move without kill - create path with 1 element
            }
            bool anyKill = false;
            foreach (var nextCell in FilterInRange(possibleEnemies))
            {
                var nextCellState = GetCellState(field, nextCell);
                if (IsEnemy(startCellState, nextCellState) && CanKill(field, currentCell, nextCell))
                {
                    counter++;
                    if (counter == 7)
                    {

                    }

                    var killCell = new Cell
                    {
                        Row = nextCell.Row,
                        Col = nextCell.Col,
                        Kill = true
                    };

                    var newPath = new List<Cell>();
                    newPath.AddRange(currentPath);
                    newPath.Add(killCell);
                    Cell afterKillCell = GetCellAfterKill(currentCell, nextCell);
                    newPath.Add(afterKillCell);
                    anyKill = true;
             
                    
                    GetPossibleMovementsRecursive(paths, newPath, field, startCell, afterKillCell);
                }
            }
            if (!anyKill)
            {
                paths.Add(currentPath);
            }
        }

        private static void HasError(List<Cell> cells)
        {
            var lastTwo = cells.Skip(cells.Count - 2).Take(2).ToList();
            if (lastTwo.Count()== 2)
            {
                if (lastTwo[0].Row == 3 && lastTwo[0].Col == 2 
                    && lastTwo[1].Row == 4 && lastTwo[1].Col == 5)
                {

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

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


        public static List<List<Cell>> GetPossibleKingMovements(CellState[,] field, Cell currentCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleKingMovementsRecursive(paths, currentPath, field, currentCell, currentCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }
            return paths;
        }

        private static void GetPossibleKingMovementsRecursive(List<List<Cell>> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
        {
            var startCellState = GetCellState(field, startCell);
            var lines = GetKingNeightbords(field, startCellState, currentCell).Where(l=>l.Any(c=>c.Kill)).ToList();
            
            foreach (var line in lines)
            {
                bool killFlag = false;
                var newPath = new List<Cell>();
                newPath.AddRange(currentPath);
                
                foreach (var cell in line)
                {

                    newPath.Add(cell);
                    if (!currentPath.Any(c => c.Row == cell.Row && c.Col == cell.Col))
                    {
                        if (killFlag)
                        {
                            GetPossibleKingMovementsRecursive(paths, newPath.ToList(), field, startCell, cell);
                        }

                        if (cell.Kill)
                        {
                            killFlag = true;
                        }
                    }
                }
            }
            paths.Add(currentPath.ToList());
        }

        public static List<List<Cell>> GetPossibleMovements(CellState[,] field, Cell currentCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleMovementsRecursive(paths, currentPath, field, currentCell, currentCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
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

        public static List<List<Cell>> GetKingNeightbords(CellState[,] field, CellState startCellState, Cell currentCell)
        {
            var result = new List<List<Cell>>();
            result.Add(GetKingNeightbords(field, startCellState, currentCell, GetRightTopNextCell));
            result.Add(GetKingNeightbords(field, startCellState, currentCell, GetRigthBottomNextCell));
            result.Add(GetKingNeightbords(field, startCellState, currentCell, GetLeftTopNextCell));
            result.Add(GetKingNeightbords(field, startCellState, currentCell, GetLeftBottomNextCell));

            return result;
        }

        private static List<Cell> GetKingNeightbords(CellState[,] field, CellState startCellState, Cell currentCell,
            Func<Cell, int, Cell> getter)
        {
            var result = new List<Cell>();
            int distance = 1;
            Cell newCell = getter(currentCell, distance);
            while (IsInRange(newCell))
            {
                var newCellState = GetCellState(field, newCell);
                if (newCellState != CellState.Empty && !IsEnemy(startCellState, newCellState))
                {
                    break;
                }

                if (IsEnemy(startCellState, newCellState))
                {
                    var cellAfterEnemy = getter(currentCell, distance + 1);
                    var cellAfterEnemyState = GetCellState(field, cellAfterEnemy);
                    if (cellAfterEnemyState != CellState.Empty)
                    {
                        break;
                    }
                    newCell.Kill = true;
                }

                result.Add(newCell);

                distance++;
                newCell = getter(currentCell, distance);
            }

            return result;
        }

        private static Cell GetRigthBottomNextCell(Cell currentCell, int distance)
        {
            return new Cell
            {
                Row = currentCell.Row + distance,
                Col = currentCell.Col + distance
            };
        }

        private static Cell GetLeftTopNextCell(Cell currentCell, int distance)
        {
            return new Cell
            {
                Row = currentCell.Row - distance,
                Col = currentCell.Col - distance
            };
        }

        private static Cell GetLeftBottomNextCell(Cell currentCell, int distance)
        {
            return new Cell
            {
                Row = currentCell.Row + distance,
                Col = currentCell.Col - distance
            };
        }

        private static Cell GetRightTopNextCell(Cell currentCell, int distance)
        {
            return new Cell
            {
                Row = currentCell.Row - distance,
                Col = currentCell.Col + distance
            };
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

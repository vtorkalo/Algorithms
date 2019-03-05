﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Core
{

    public class CellComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell x, Cell y)
        {
            return Algorithms.CompareCells(x, y);
        }

        public int GetHashCode(Cell obj)
        {
            return obj.GetHashCode();
        }
    }

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


        public static List<List<Cell>> GetPossibleKingMovements(CellState[,] field, Cell startCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleKingMovementsRecursive(paths, currentPath, field, startCell, startCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }
            var comparer = new CellComparer();

            var sortedByLength = paths.OrderByDescending(p => p.Count).ToList();

            //var result = new List<List<Cell>>();
            //foreach (var path in sortedByLength)
            //{
            //    if (!result.Any(p => p.Intersect(path, comparer).SequenceEqual(path, comparer)))
            //    {
            //        result.Add(path);
            //    }
            //}
            //paths = result;


            //var startCellState = GetCellState(field, startCell);
            //foreach (var path in paths)
            //{
            //    if (path.Any(x => x.Kill))
            //    {
            //        var beforeLast = path.Skip(path.Count - 2).Take(1).Single();
            //        var lastCell = path.Last();
            //        var horDir = lastCell.Col - beforeLast.Col;
            //        var vertDir = lastCell.Row - beforeLast.Row;

            //        path.AddRange(
            //            GetKingNeightbords(field, startCell, startCellState, path.Last(),
            //            (c, distance) =>
            //            {
            //                return new Cell
            //                {
            //                    Row = startCell.Row + distance * vertDir,
            //                    Col = startCell.Col + distance * horDir
            //                };
            //            }));
            //    }
            //}


            var filtered = new List<List<Cell>>();
            foreach (var path in paths)
            {
                bool valid = true;
                for (int i=0; i<path.Count-1; i++)
                {
                    if (Math.Abs(path[i].Row - path[i + 1].Row) != 1 
                     || Math.Abs(path[i].Col - path[i+1].Col) != 1)
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    filtered.Add(path);
                }
            }
            paths = filtered;


            return paths.OrderByDescending(p=>p.Count(x=>x.Kill)).ToList();

          //  return paths;
        }

        private static void GetPossibleKingMovementsRecursive(List<List<Cell>> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
        {
            var startCellState = GetCellState(field, startCell);
            var lines = GetKingNeightbords(field, startCell, startCellState, currentCell).ToList();

            foreach (var line in lines)
            {
                bool killFlag = false;
                var newPath = new List<Cell>();
                newPath.AddRange(currentPath);

                foreach (var cell in line)
                {
                    if (!currentPath.Any(c => CompareCells(c, cell)))
                    {
                        newPath.Add(cell);
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
                killFlag = false;
                if (!newPath.Any(c => c.Kill) )
                {
                    paths.Add(newPath.ToList());
                }
            }

           
            paths.Add(currentPath.ToList());
        }

        private static Cell GetNextCell(Cell currentCell, int distance)
        {
            return new Cell
            {
                Row = currentCell.Row - distance,
                Col = currentCell.Col + distance
            };
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
                    if (!currentPath.Any(p => CompareCells(p, neightbor) && p.Kill))
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

        public static List<List<Cell>> GetKingNeightbords(CellState[,] field, Cell startCell, CellState startCellState, Cell currentCell)
        {
            var result = new List<List<Cell>>();
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetRightTopNextCell));
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetRigthBottomNextCell));
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetLeftTopNextCell));
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetLeftBottomNextCell));

            return result;
        }

        private static List<Cell> GetKingNeightbords(CellState[,] field, Cell startCell, CellState startCellState, Cell currentCell,
            Func<Cell, int, Cell> getter)
        {
            var result = new List<Cell>();
            int distance = 1;
            Cell newCell = getter(currentCell, distance);
            while (IsInRange(newCell))
            {
                var newCellState = GetCellState(field, newCell);
                if (newCellState != CellState.Empty && !IsEnemy(startCellState, newCellState) && !CompareCells(newCell, startCell))
                {
                    break;
                }

                if (IsEnemy(startCellState, newCellState))
                {
                    var cellAfterEnemy = getter(currentCell, distance + 1);
                    if (IsInRange(cellAfterEnemy))
                    {
                        var cellAfterEnemyState = GetCellState(field, cellAfterEnemy);

                        if (cellAfterEnemyState != CellState.Empty && !CompareCells(cellAfterEnemy, startCell))
                        {
                            break;
                        }
                        newCell.Kill = true;
                    }
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

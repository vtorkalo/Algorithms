using System;
using System.Collections.Generic;
using System.Linq;

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
            return 0;
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

                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
                //{ CellState.Empty, CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty,CellState.Empty},
            };

            //result[3, 4] = CellState.WhiteKing;


            //result[4, 5] = CellState.Black;
            //result[4, 3] = CellState.Black;
            //result[6, 3] = CellState.Black;
            //result[6, 5] = CellState.Black;
            //result[1, 6] = CellState.Black;



            //result[3, 4] = CellState.WhiteKing;
            //result[4, 5] = CellState.Black;
            //result[6, 5] = CellState.Black;
            //result[5, 2] = CellState.Black;
            //result[2, 1] = CellState.Black;




            //result[7, 0] = CellState.WhiteKing;

            //result[6, 1] = CellState.Black;
            //result[3, 2] = CellState.Black;
            //result[2, 3] = CellState.Black;


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

        static CellComparer _comparer = new CellComparer();

        private static bool AlreadyExists(List<List<Cell>> paths, List<Cell> currentPath)
        {

            bool exists = false;
            var result = new List<List<Cell>>();
            var trimmedCurrent = TrimmPath(currentPath);
            foreach (var path in paths)
            {
                var trimmedPath = TrimmPath(path);
                if (trimmedCurrent.Count > 1 &&
                    trimmedPath.Take(trimmedCurrent.Count).SequenceEqual(trimmedCurrent, _comparer))
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        static List<Cell> TrimmPath(List<Cell> path)
        {
            var lastKill = path.LastOrDefault(p => p.Kill);
            if (lastKill != null)
            {
                var lastKillIndex = path.IndexOf(lastKill);
                var trimmedPath = path.Take(lastKillIndex + 1).ToList();
                return trimmedPath;
            }
            else
            {
                return path.ToList();
            }
        }

        public static List<List<Cell>> GetPossibleKingMovements(CellState[,] field, Cell startCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleKingMovementsRecursive(paths, currentPath, field, startCell, startCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
                paths = TrimmPaths(paths);
                paths = ExpandPaths(field, startCell, paths);
            }

            return paths.OrderByDescending(p => p.Count(x => x.Kill)).ToList();
        }

        private static List<List<Cell>> ExpandPaths(CellState[,] field, Cell startCell, List<List<Cell>> paths)
        {
            var startCellState = GetCellState(field, startCell);
            var expandedPaths = new List<List<Cell>>();
            foreach (var path in paths)
            {
                if (path.Any(x => x.Kill))
                {
                    path.Insert(0, startCell);
                    var beforeLast = path.Skip(path.Count - 2).Take(1).Single();
                    var lastCell = path.Last();
                    int horDir = lastCell.Col - beforeLast.Col;
                    int vertDir = lastCell.Row - beforeLast.Row;


                    var neigthboards = GetKingNeightbords(field, startCell, startCellState, path.Last(),
                        (c, distance) =>
                        {
                            return new Cell
                            {
                                Row = lastCell.Row + distance * vertDir,
                                Col = lastCell.Col + distance * horDir
                            };
                        });

                    expandedPaths.Add(path);
                    expandedPaths.Last().Add(neigthboards.First());
                    foreach (var n in neigthboards.Skip(1))
                    {
                        var expandedPath = new List<Cell>();
                        expandedPath.AddRange(expandedPaths.Last());
                        expandedPath.Add(n);
                        expandedPaths.Add(expandedPath);
                    }
                }
            }

            return expandedPaths;
        }

        private static List<List<Cell>> TrimmPaths(List<List<Cell>> paths)
        {
            var trimmedResult = new List<List<Cell>>();
            foreach (var path in paths)
            {
                trimmedResult.Add(TrimmPath(path));
            }
            trimmedResult = trimmedResult.OrderByDescending(p => p.Count).ToList();


            var result = new List<List<Cell>>();
            foreach (var path in trimmedResult)
            {
                if (!result.Any(r => r.Take(path.Count).SequenceEqual(path, _comparer)))
                {
                    result.Add(path);
                }
            }

            return result;
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
                            var lastKill = newPath.LastOrDefault(x => x.Kill);
                            var cellNeightbords = GetKingNeightbords(field, startCell, startCellState, cell).Where(n => n.Any(x => x.Kill)).ToList();
                            if (lastKill != null)
                            {
                                var forceTurns = cellNeightbords.Where(n => !n.Intersect(newPath, _comparer).Any());

                                if (forceTurns.Any())
                                {
                                    foreach (var turn in forceTurns)
                                    {
                                        var turnPath = new List<Cell>();
                                        turnPath.AddRange(newPath);
                                        turn.Insert(0, cell);
                                        var firstKillInTurn = turn.First(x => x.Kill);
                                        var cellBeforeKull = turn[turn.IndexOf(firstKillInTurn) - 1];
                                        var cellAfterKill = GetCellAfterKill(cellBeforeKull, firstKillInTurn);
                                        if (IsInRange(cellAfterKill))
                                        {

                                            turnPath.AddRange(turn.Take(turn.IndexOf(firstKillInTurn) + 2));
                                            GetPossibleKingMovementsRecursive(paths, turnPath.ToList(), field, startCell, cellAfterKill);
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    GetPossibleKingMovementsRecursive(paths, newPath.ToList(), field, startCell, cell);
                                }
                            }
                        }
                        if (cell.Kill && !currentCell.Kill)
                        {
                            killFlag = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (!newPath.Any(c => c.Kill))
                {
                    paths.Add(newPath.ToList());
                }
            }

            if (currentPath.Any())
            {
                paths.Add(currentPath.ToList());
            }
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
                if (IsFree(field, startCell, neightbor)
                   && !neightbor.IsBack
                   && !currentPath.Any()) //first movement in path to empty cell
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
                if (IsEnemy(startCellState, nextCellState)
                     && CanKill(field, currentCell, nextCell, startCell))
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
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetLeftBottomNextCell));
            result.Add(GetKingNeightbords(field, startCell, startCellState, currentCell, GetLeftTopNextCell));

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
                if (newCellState != CellState.Empty
                    && !IsEnemy(startCellState, newCellState)
                    && !CompareCells(newCell, startCell))
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

        private static List<Cell> GetBlackMoves(Cell currentCell)
        {
            var result = new List<Cell>
            {
                new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col - 1
                },
                new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col + 1
                },
                new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col - 1,
                    IsBack = true
                },
                new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col + 1,
                    IsBack = true
                }
            };

            return result;
        }

        private static List<Cell> GetWhiteMoves(Cell currentCell)
        {
            var result = new List<Cell>
            {
                new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col - 1
                },
                new Cell
                {
                    Row = currentCell.Row - 1,
                    Col = currentCell.Col + 1
                },
                new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col - 1,
                    IsBack = true
                },
                new Cell
                {
                    Row = currentCell.Row + 1,
                    Col = currentCell.Col + 1,
                    IsBack = true
                }
            };

            return result;
        }

        private static bool CanKill(CellState[,] field, Cell currentCell, Cell enemyCell, Cell startCell)
        {
            Cell targetCell = GetCellAfterKill(currentCell, enemyCell);
            bool canKill = IsInRange(targetCell)
                        && IsFree(field, startCell, targetCell);

            return canKill;
        }

        private static bool IsFree(CellState[,] field, Cell startCell, Cell targetCell)
        {
            return GetCellState(field, targetCell) == CellState.Empty || CompareCells(targetCell, startCell);
        }

        private static Cell GetCellAfterKill(Cell currentCell, Cell enemyCell)
        {
            var cellAfterKill = new Cell
            {
                Row = enemyCell.Row + (enemyCell.Row - currentCell.Row),
                Col = enemyCell.Col + (enemyCell.Col - currentCell.Col),
            };

            return cellAfterKill;
        }

        private static bool IsWhite(CellState state)
        {
            bool isWhite = state == CellState.White
                        || state == CellState.WhiteKing;

            return isWhite;
        }

        private static bool IsBlack(CellState state)
        {
            bool isWhite = state == CellState.Black
                        || state == CellState.BlackKing;

            return isWhite;
        }

        private static bool IsEnemy(CellState currentState, CellState otherState)
        {
            bool result = (IsWhite(currentState) && IsBlack(otherState))
                       || (IsBlack(currentState) && IsWhite(otherState));

            return result;
        }
    }
}

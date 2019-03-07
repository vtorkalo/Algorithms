using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public class KingPathGenerator
    {
        private CellComparer _comparer = new CellComparer();
        private KingNeightborGenerator _kingNeightborsGenerator = new KingNeightborGenerator(); 
        private KingPathExtending _kingPathExtending = new KingPathExtending();

        public KingPathGenerator()
        {
        }

        public List<List<Cell>> GetPossibleKingMovements(CellState[,] field, Cell startCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleKingMovementsRecursive(paths, currentPath, field, startCell, startCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }

            paths = _kingPathExtending.TrimmPaths(paths);
            paths = _kingPathExtending.ExpandPaths(field, startCell, paths);

            return paths.OrderByDescending(p => p.Count(x => x.Kill)).ToList();
        }

        private void GetPossibleKingMovementsRecursive(List<List<Cell>> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
        {
            var startCellState = Helpers.GetCellState(field, startCell);
            var lines = _kingNeightborsGenerator.GetKingNeightbors(field, startCell, startCellState, currentCell).ToList();
            foreach (var line in lines)
            {
                bool killFlag = false;
                var newPath = new List<Cell>();
                newPath.AddRange(currentPath);
                foreach (var cell in line)
                {
                    if (!currentPath.Any(c => Helpers.CompareCells(c, cell)))
                    {
                        newPath.Add(cell);

                        if (killFlag)
                        {
                            var lastKill = newPath.LastOrDefault(x => x.Kill);
                            var cellNeightbors = _kingNeightborsGenerator.GetKingNeightbors(field, startCell, startCellState, cell).Where(n => n.Any(x => x.Kill)).ToList();
                            if (lastKill != null)
                            {
                                var forceTurns = cellNeightbors.Where(n => !n.Intersect(newPath, _comparer).Any());

                                if (forceTurns.Any())
                                {
                                    foreach (var turn in forceTurns)
                                    {
                                        var turnNewPath = new List<Cell>();
                                        turnNewPath.AddRange(newPath);
                                        turn.Insert(0, cell);
                                        var firstKillInTurn = turn.First(x => x.Kill);
                                        var cellBeforeKull = turn[turn.IndexOf(firstKillInTurn) - 1];
                                        var cellAfterKill = Helpers.GetCellAfterKill(cellBeforeKull, firstKillInTurn);
                                        if (Helpers.IsInRange(cellAfterKill))
                                        {
                                            turnNewPath.AddRange(turn.Take(turn.IndexOf(firstKillInTurn) + 2));
                                            GetPossibleKingMovementsRecursive(paths, turnNewPath.ToList(), field, startCell, cellAfterKill);
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
                        if (cell.Kill)
                        {
                            killFlag = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (newPath.Any() && !newPath.Any(c => c.Kill))
                {
                    paths.Add(newPath.ToList());
                }
            }

            if (currentPath.Any())
            {
                paths.Add(currentPath.ToList());
            }
        }

      
    }
}

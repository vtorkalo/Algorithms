using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public class KingPathGenerator : IPathGenerator
    {
        private CellComparer _comparer = new CellComparer();
        private KingNeightborGenerator _kingNeightborsGenerator = new KingNeightborGenerator();
        private KingPathExtending _kingPathExtending = new KingPathExtending();

        public KingPathGenerator()
        {
        }
        private PathComparer _pathComparer = new PathComparer();
        List<Cell> bugPath = new List<Cell> { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(0, 3), new Cell(1, 4), new Cell(2, 5), new Cell(3, 6) };

        public List<Move> GetPossibleMovements(CellState[,] field, Cell startCell)
        {
            var paths = new List<Move>();
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

        private void GetPossibleKingMovementsRecursive(List<Move> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
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
                    if (currentPath.Where(x => x.Kill).Any(c => Helpers.CompareCells(c, cell)))
                    {
                        break;
                    }

                    if (newPath.Any() && newPath.Last().Row == 5 && newPath.Last().Col ==2)
                    {
                       
                    }
                    newPath.Add(cell);

                    if (killFlag)
                    {
                        var cellNeightbors = _kingNeightborsGenerator.GetKingNeightbors(field, startCell, startCellState, cell)
                            .Where(n => n.Any(x => x.Kill)).ToList();
                        var forceTurns = cellNeightbors.Where(n => !newPath.Contains(n.First(x => x.Kill), _comparer));

                        if (forceTurns.Any())
                        {
                            foreach (var turn in forceTurns)
                            {
                                var turnNewPath = new List<Cell>();
                                turnNewPath.AddRange(newPath);
                                if (cell.Row == 1 && cell.Col == 2 && newPath.Count == 13)
                                {

                                }
                                var firstKillInTurn = turn.First(x => x.Kill);
                                var killIndex = turn.IndexOf(firstKillInTurn);
                                var cellBeforeKill = killIndex == 0 ?  cell : turn[killIndex - 1];
                                var cellAfterKill = Helpers.GetCellAfterKill(cellBeforeKill, firstKillInTurn);
                                if (Helpers.IsInRange(cellAfterKill))
                                {
                                    turnNewPath.AddRange(turn.Take(killIndex + 2));
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
                    if (cell.Kill)
                    {
                        killFlag = true;
                    }

                }

                if (newPath.Any() && !newPath.Any(c => c.Kill))
                {
                    paths.Add(new Move(newPath));
                }
            }

            if (currentPath.Any())
            {
                paths.Add(new Move(currentPath));
            }
        }


    }
}

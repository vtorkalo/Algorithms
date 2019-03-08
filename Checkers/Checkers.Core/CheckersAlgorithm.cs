using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Checkers.Core
{
    public enum Side
    {
        White,
        Black
    }

    public class CheckersAlgorithm
    {
        private PathGenerator _pathGenerator = new PathGenerator();

        public List<Cell> GetNextMove(CellState[,] field, Side aiSide)
        {
            var paths = new List<List<List<Cell>>>();
            var currentPath = new List<List<Cell>>();
            var currentField = Helpers.CopyField(field);
            GetPathsRecursive(paths, currentPath, currentField, aiSide, 0);
            var sorted = paths.OrderByDescending(x => GetTreeKills(x)).ToList();

            List<Cell> result = null;
            if (sorted.Any())
            {
                result = sorted.First().FirstOrDefault();
            }

            return result;
        }

        private int GetTreeKills(List<List<Cell>> path)
        {
            int aiKills = 0;
            int humanKills = 0;

            for (int i=0; i<path.Count; i++)
            {
                var moveKillCount = path[i].Count(x => x.Kill);

                if (i % 2 ==0)
                {
                    aiKills += moveKillCount;
                }
                else
                {
                    humanKills += moveKillCount;
                }
            }

            int total = aiKills - humanKills;
            return total;
        }

        private void GetPathsRecursive(List<List<List<Cell>>> paths, List<List<Cell>> currentPath, CellState[,] currentField, Side side, int depth)
        {
            if (depth > 15)
            {
                return;
            }
            var oppositeSide = Helpers.GetOppositeSide(side);
            var aiCells = Helpers.GetCellsOfSide(currentField, side);

            var possibleStartCells = aiCells.Select(c => _pathGenerator.GetPossibleMovements(currentField, c)).ToList();
            if (possibleStartCells.Any(cm=>cm.Any(m=>m.Any(x=>x.Kill))))
            {
                possibleStartCells = possibleStartCells.Where(cm => cm.Any(x => x.Any(c => c.Kill))).ToList();
            }

            foreach (var cellPaths in possibleStartCells)
            {
                foreach (var move in cellPaths)
                {
                    var newPath = new List<List<Cell>>();
                    newPath.AddRange(currentPath);
                    newPath.Add(move);

                    var newField = Helpers.CopyField(currentField);
                    Helpers.MakeMove(newField, move);

                    depth++;
                    GetPathsRecursive(paths, newPath, newField, oppositeSide, depth);
                }
            }

            paths.Add(currentPath);
        }

      
    }
}

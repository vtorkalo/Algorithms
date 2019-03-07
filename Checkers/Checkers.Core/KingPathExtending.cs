using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public class KingPathExtending
    {
        private CellComparer _comparer = new CellComparer();
        private KingNeightborGenerator _kingNeightborsGenerator = new KingNeightborGenerator();

        public List<List<Cell>> TrimmPaths(List<List<Cell>> paths)
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

        public List<Cell> TrimmPath(List<Cell> path)
        {
            var lastKill = path.LastOrDefault(p => p.Kill);
            if (lastKill != null)
            {
                var lastKillIndex = path.IndexOf(lastKill);
                var trimmedPath = path.Take(lastKillIndex + 2).ToList();
                return trimmedPath;
            }
            else
            {
                return path.Take(1).ToList();
            }
        }

        public List<List<Cell>> ExpandPaths(CellState[,] field, Cell startCell, List<List<Cell>> paths)
        {
            var startCellState = Helpers.GetCellState(field, startCell);
            var expandedPaths = new List<List<Cell>>();
            foreach (var path in paths)
            {
                path.Insert(0, startCell);
                List<Cell> neigthboards = GetLineAfterPathEnd(field, startCell, startCellState, path);

                expandedPaths.Add(path.ToList());
                foreach (var n in neigthboards)
                {
                    var expandedPath = new List<Cell>();
                    expandedPath.AddRange(expandedPaths.Last());
                    expandedPath.Add(n);
                    expandedPaths.Add(expandedPath.ToList());
                }
            }

            return expandedPaths;
        }

        private List<Cell> GetLineAfterPathEnd(CellState[,] field, Cell startCell, CellState startCellState, List<Cell> path)
        {
            var beforeLast = path.Skip(path.Count - 2).Take(1).Single();
            var lastCell = path.Last();
            int horDir = lastCell.Col - beforeLast.Col;
            int vertDir = lastCell.Row - beforeLast.Row;

            var neigthboards = _kingNeightborsGenerator.GetKingNeightbors(field, startCell, startCellState, path.Last(),
                (c, distance) =>
                {
                    return new Cell
                    (
                        lastCell.Row + distance * vertDir,
                        lastCell.Col + distance * horDir
                    );
                });
            return neigthboards;
        }
    }
}

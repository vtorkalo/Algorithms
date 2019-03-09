using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core
{
    public class KingPathExtending
    {
        private CellComparer _comparer = new CellComparer();
        private KingNeightborGenerator _kingNeightborsGenerator = new KingNeightborGenerator();

        public List<Move> TrimmPaths(List<Move> paths)
        {
            var trimmedResult = new List<Move>();
            foreach (var path in paths)
            {
                trimmedResult.Add(TrimmPath(path));
            }
            trimmedResult = trimmedResult.OrderByDescending(p => p.Count).ToList();

            var result = new List<Move>();
            foreach (var path in trimmedResult)
            {
                if (!result.Any(r => r.Take(path.Count).SequenceEqual(path, _comparer)))
                {
                    result.Add(path);
                }
            }

            return result;
        }

        public Move TrimmPath(Move path)
        {
            var lastKill = path.LastOrDefault(p => p.Kill);
            if (lastKill != null)
            {
                var lastKillIndex = path.IndexOf(lastKill);
                var trimmedPath = new Move(path.Take(lastKillIndex + 2));
                return trimmedPath;
            }
            else
            {
                return new Move (path.Take(1));
            }
        }

        public List<Move> ExpandPaths(CellState[,] field, Cell startCell, List<Move> paths)
        {
            var startCellState = Helpers.GetCellState(field, startCell);
            var expandedPaths = new List<Move>();
            foreach (var path in paths)
            {
                path.Insert(0, startCell);
                List<Cell> neigthboards = GetLineAfterPathEnd(field, startCell, startCellState, path);

                expandedPaths.Add(path);
                foreach (var n in neigthboards)
                {
                    var expandedPath = new Move();
                    expandedPath.AddRange(expandedPaths.Last());
                    expandedPath.Add(n);
                    expandedPaths.Add(expandedPath);
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

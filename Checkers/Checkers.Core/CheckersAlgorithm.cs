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

        public Move GetNextMove(CellState[,] field, Side aiSide)
        {
            var games = new List<Game>();
            var currentGame = new Game();
            var currentField = Helpers.CopyField(field);
            GetPathsRecursive(games, currentGame, currentField, aiSide, 0);
            var sorted = games.OrderByDescending(x => GetTreeKills(x));

            Move result = null;
            if (sorted.Any())
            {
                var topGame = sorted.Where(x => x.Any()).First();
                if (topGame.Any())
                {
                    result = topGame.FirstOrDefault();
                }
            }

            return result;
        }

        private double GetTreeKills(Game game)
        {
            double aiKills = 0;
            double humanKills = 0;

            double aiKings = 0;
            double humanKings = 0;
            

            for (int i=0; i<game.Count; i++)
            {
                double weight = game.Count - i;
                if (i % 2 ==0)
                {
                    aiKills += game[i].Kills * weight;
                    aiKings += game[i].NewKings * weight;
                }
                else
                {
                    humanKills += game[i].Kills * weight;
                    humanKings += game[i].NewKings * weight;
                }
            }

            double total = (aiKills - humanKills) + (aiKings - humanKings) * 2;
            return total;
        }

        public List<Cell> GetAvaliableCells(CellState[,] field, Side side)
        {
            var cells = Helpers.GetCellsOfSide(field, side);
            var possibleStartCells = GetAvaliableCellMoves(field, cells);
            return possibleStartCells.Select(c => c.First().First()).ToList();
        }

        public List<List<Move>> GetAvaliableCellMoves(CellState[,] field, List<Cell> cells)
        {
            var possibleStartCells = cells.Select(c => _pathGenerator.GetPossibleMovements(field, c).ToList()).Where(x=>x.Any()).ToList();
            if (possibleStartCells.Any(cm => cm.Any(m => m.Any(x => x.Kill))))
            {
                possibleStartCells = possibleStartCells.Where(cm => cm.Any(x => x.Any(c => c.Kill))).ToList();
            }
            return possibleStartCells;
        }

        private void GetPathsRecursive(List<Game> games, Game currentGame, CellState[,] currentField, Side side, int depth)
        {
            if (depth > 20)
            {
                return;
            }
            var oppositeSide = Helpers.GetOppositeSide(side);
            var aiCells = Helpers.GetCellsOfSide(currentField, side);
            var possibleStartCells = GetAvaliableCellMoves(currentField, aiCells);

            foreach (var cellPaths in possibleStartCells)
            {
                foreach (var move in cellPaths)
                {
                    var newPath = new Game();
                    newPath.AddRange(currentGame);
                    newPath.Add(move);

                    var newField = Helpers.CopyField(currentField);
                    Helpers.MakeMove(newField, move);

                    depth++;
                    GetPathsRecursive(games, newPath, newField, oppositeSide, depth);
                }
            }

            games.Add(currentGame);
        }

      
    }
}

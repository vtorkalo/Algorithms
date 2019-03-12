using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.Core
{
    public enum Side
    {
        White,
        Black
    }

    public struct QueueItem
    {
        public Game currentGame { get; set; }
        public CellState[,] currentField { get; set; }
        public Side side { get; set; }
    }


    public class CheckersAlgorithm
    {
        Object lockObj = new Object();
        Object countLock = new Object();

        private PathGenerator _pathGenerator = new PathGenerator();

        public Move GetNextMove(CellState[,] field, Side aiSide)
        {
            var games = new List<Game>();
            var currentField = Helpers.CopyField(field);
            int count = 0;

            var queue = new Queue<QueueItem>();
            queue.Enqueue(new QueueItem
            {
                currentGame = new Game(),
                currentField = Helpers.CopyField(field),
                side = aiSide
            });

            while (queue.Any())
            {
                var quequeItem = queue.Dequeue();
                lock (countLock)
                {
                    if (count > 100000)
                    {
                        continue;
                    }
                }

                var oppositeSide = Helpers.GetOppositeSide(quequeItem.side);
                var aiCells = Helpers.GetCellsOfSide(currentField, quequeItem.side);
                var possibleStartCells = GetAvaliableCellMoves(currentField, aiCells).ToList();

                Parallel.ForEach(possibleStartCells,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = 1// Environment.ProcessorCount
                    },
                    cellPaths =>
                    {

                        foreach (var move in cellPaths)
                        {
                            var newPath = new Game();
                            newPath.AddRange(quequeItem.currentGame);
                            newPath.Add(move);

                            var newField = Helpers.CopyField(currentField);
                            Helpers.MakeMove(newField, move);
                            //GetPathsRecursive(games, newPath, newField, oppositeSide, depth + 1);
                            queue.Enqueue(new QueueItem
                            {
                                currentGame = newPath,
                                currentField = newField,
                                side = oppositeSide
                            });
                            lock (lockObj)
                            {
                                if (quequeItem.currentGame.Any())
                                {
                                    games.Add(quequeItem.currentGame);
                                }
                                lock (countLock)
                                {
                                    count++;
                                }
                            }
                        }
                    });



            }



            var sorted = games.OrderByDescending(x => GetTreeKills(x));

            Move result = null;
            if (sorted.Any())
            {
                result = sorted.Where(x => x.Any()).First().First();
            }

            return result;
        }

        private double GetTreeKills(Game game)
        {
            double aiKills = 0;
            double humanKills = 0;

            double aiKings = 0;
            double humanKings = 0;


            for (int i = 0; i < game.Count; i++)
            {
                double weight = game.Count - i;
                if (i % 2 == 0)
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
            var possibleStartCells = cells.Select(c => _pathGenerator.GetPossibleMovements(field, c).ToList()).Where(x => x.Any()).ToList();
            if (possibleStartCells.Any(cm => cm.Any(m => m.Any(x => x.Kill))))
            {
                possibleStartCells = possibleStartCells.Where(cm => cm.Any(x => x.Any(c => c.Kill))).ToList();
            }
            return possibleStartCells;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public CellState[] currentField { get; set; }
        public Side side { get; set; }
        public int depth { get; set; }
    }


    public class CheckersAlgorithm
    {
        Object lockObj = new Object();

        private PathGenerator _pathGenerator = new PathGenerator();

        public Move GetNextMove(CellState[] field, Side aiSide)
        {
            var games = new List<Game>();
            int iterationCount = 0;

            var queue = new Queue<QueueItem>();
            queue.Enqueue(new QueueItem
            {
                currentGame = new Game(),
                currentField = Helpers.CopyField(field),
                side = aiSide,
                depth = 0
            });


            while (AnyInQueue(queue))
            {
                iterationCount++;
                var quequeItem = queue.Dequeue();

                if (iterationCount > 30000)
                {
                    games.Add(quequeItem.currentGame);
                    continue;
                }

                var oppositeSide = Helpers.GetOppositeSide(quequeItem.side);
                var aiCells = Helpers.GetCellsOfSide(quequeItem.currentField, quequeItem.side);
                var possibleStartCells = GetAvaliableCellMoves(quequeItem.currentField, aiCells).ToList();
                if (iterationCount == 1 && possibleStartCells.Count == 1 && possibleStartCells.Single().Count == 1)
                {
                    Thread.Sleep(500);
                    return possibleStartCells.First().First();
                }
                if (!possibleStartCells.Any())
                {
                    games.Add(quequeItem.currentGame);
                    continue;
                }

                Parallel.ForEach(possibleStartCells,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    },
                    cellPaths =>
                    {
                        foreach (var move in cellPaths)
                        {
                            var newPath = new Game();
                            newPath.AddRange(quequeItem.currentGame);
                            newPath.Add(move);

                            var newField = Helpers.CopyField(quequeItem.currentField);
                            Helpers.MakeMove(newField, move);

                            lock (lockObj)
                            {
                                queue.Enqueue(new QueueItem
                                {
                                    currentGame = newPath,
                                    currentField = newField,
                                    side = oppositeSide,
                                    depth = quequeItem.depth + 1
                                });
                            }
                        }
                    });
            }



            var sorted = games.OrderByDescending(x => GetTreeKills(x));

            Move result = null;
            if (sorted.Any())
            {
                result = sorted.Where(x => x.Any()).FirstOrDefault()?.FirstOrDefault();
            }

            return result;
        }

        private bool AnyInQueue(Queue<QueueItem> queue)
        {
            lock (lockObj)
            {
                return queue.Any();
            }
        }

        private double GetTreeKills(Game game)
        {
            double aiKills = 0;
            double humanKills = 0;

            double aiKillsKing = 0;
            double humanKillsKing = 0;

            double aiKings = 0;
            double humanKings = 0;
            int count = game.Count;

            for (int i = 0; i < count; i++)
            {
                double weight = 5 + game.Count - i;
                if (i % 2 == 0)
                {
                    aiKills += game[i].Kills * weight;
                    aiKillsKing += game[i].KingKills * weight;
                    aiKings += game[i].NewKings * weight;
                }
                else
                {
                    humanKills += game[i].Kills * weight;
                    humanKillsKing += game[i].KingKills * weight;
                    humanKings += game[i].NewKings * weight;
                }
            }

            double total = (aiKills - humanKills) 
                + (aiKings - humanKings)
                + (aiKillsKing - humanKillsKing);

            return total;
        }

        public List<Cell> GetAvaliableCells(CellState[] field, Side side)
        {
            var cells = Helpers.GetCellsOfSide(field, side);
            var possibleStartCells = GetAvaliableCellMoves(field, cells);
            return possibleStartCells.Select(c => c.First().First()).ToList();
        }

        public List<List<Move>> GetAvaliableCellMoves(CellState[] field, List<Cell> cells)
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

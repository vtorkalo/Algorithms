﻿using System;
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
            var sorted = paths.GroupBy(x=> GetTreeKills(x)).OrderByDescending(x => x.Key).ToList();

            List<Cell> result = null;
            if (sorted.Any())
            {
                var firstGroup = sorted.First().ToList();
                var random = new Random();
                int randomIndex = random.Next(firstGroup.Count-1);
                result = firstGroup[randomIndex].First();
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

        public List<Cell> GetAvaliableCells(CellState[,] field, Side side)
        {
            var cells = Helpers.GetCellsOfSide(field, side);
            var possibleStartCells = GetAvaliableCellMoves(field, cells);
            return possibleStartCells.Select(c => c.First().First()).ToList();
        }

        public List<List<List<Cell>>> GetAvaliableCellMoves(CellState[,] field, List<Cell> cells)
        {
            var possibleStartCells = cells.Select(c => _pathGenerator.GetPossibleMovements(field, c).ToList()).Where(x=>x.Any()).ToList();
            if (possibleStartCells.Any(cm => cm.Any(m => m.Any(x => x.Kill))))
            {
                possibleStartCells = possibleStartCells.Where(cm => cm.Any(x => x.Any(c => c.Kill))).ToList();
            }
            return possibleStartCells;
        }

        private void GetPathsRecursive(List<List<List<Cell>>> paths, List<List<Cell>> currentPath, CellState[,] currentField, Side side, int depth)
        {
            if (depth > 17)
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
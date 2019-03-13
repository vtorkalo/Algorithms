using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Core
{
    public class StandartPathGenerator : IPathGenerator
    {
        private KingPathGenerator _kingPathGenerator = new KingPathGenerator();

        public List<Move> GetPossibleMovements(CellState[] field, Cell currentCell)
        {
            var paths = new List<Move>();
            var currentPath = new Move();
            GetPossibleMovementsRecursive(paths, currentPath, field, currentCell, currentCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }
            foreach (var path in paths) // TODO: adding start cell to beginning of each path. TODO refactor
            {
                path.Insert(0, currentCell);
            }
            return paths;
        }
        
        private void GetPossibleMovementsRecursive(List<Move> paths, Move currentPath, CellState[] field, Cell startCell, Cell currentCell)
        {
            
            var startCellState = Helpers.GetCellState(field, startCell);
            var neightbors = Helpers.GetNeightbors(currentCell, startCellState);
            var possibleEnemies = new List<Cell>();

            foreach (var neightbor in neightbors)
            {
                var emptyCells = new List<Cell>();
                if (Helpers.IsFree(field, startCell, neightbor)
                   && !neightbor.IsBack
                   && !currentPath.Any()) //first movement in path to empty cell
                {
                    paths.Add(new Move { neightbor });
                }
                else
                {
                    if (!currentPath.Any(p => Helpers.CompareCells(p, neightbor) && p.Kill))
                    {
                        possibleEnemies.Add(neightbor);
                    }
                }
            }

            bool anyKill = false;
            foreach (var nextCell in possibleEnemies)
            {
                var nextCellState = Helpers.GetCellState(field, nextCell);
                if (Helpers.IsEnemy(startCellState, nextCellState)
                     && Helpers.CanKill(field, currentCell, nextCell, startCell))
                {
                    var killCell = new Cell
                    (
                        nextCell.Row,
                        nextCell.Col,
                        kill: true                        
                    );
                    killCell.KingKill = nextCellState == CellState.BlackKing 
                            || nextCellState == CellState.WhiteKing;

                    var newPath = new Move();
                    newPath.AddRange(currentPath);
                    newPath.Add(killCell);
                    Cell afterKillCell = Helpers.GetCellAfterKill(currentCell, nextCell);
                    newPath.Add(afterKillCell);
                    anyKill = true;

                    if (afterKillCell.Row == 0 && startCellState == CellState.White
                        || afterKillCell.Row == 7 && startCellState == CellState.Black)
                    {
                        var fieldCopy = Helpers.CopyField(field);
                        if (Helpers.IsWhite(startCellState))
                        {
                            Field.SetValue(fieldCopy, afterKillCell, CellState.WhiteKing);
                        }

                        if (Helpers.IsBlack(startCellState))
                        {
                            Field.SetValue(fieldCopy, afterKillCell, CellState.BlackKing);
                        }

                        foreach (var c in newPath.Where(x => x.Kill))
                        {
                            Field.SetValue(fieldCopy, c, CellState.Empty);
                        }

                        var moves = _kingPathGenerator.GetPossibleMovements(fieldCopy, afterKillCell);
                        var kingMovesWithKills = moves.Where(m => m.Any(x => x.Kill));

                        foreach (var move in kingMovesWithKills.Select(m=>m.Skip(1)))
                        {
                            var p = new Move(newPath);
                            p.AddRange(move);
                            paths.Add(p);
                        }
                    }
                    else
                    {
                        GetPossibleMovementsRecursive(paths, newPath, field, startCell, afterKillCell);
                    }
                }
            }
            if (!anyKill && currentPath.Any())
            {
                paths.Add(currentPath);
            }
        }
    }
}

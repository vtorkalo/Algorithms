using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Core
{
    public class StandartPathGenerator
    {

        public List<List<Cell>> GetPossibleMovements(CellState[,] field, Cell currentCell)
        {
            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell>();
            GetPossibleMovementsRecursive(paths, currentPath, field, currentCell, currentCell);
            if (paths.Any(p => p.Any(x => x.Kill))) //Any kill possible - remove no paths without kill
            {
                paths = paths.Where(p => p.Any(x => x.Kill)).ToList();
            }
            return paths;
        }
        
        private void GetPossibleMovementsRecursive(List<List<Cell>> paths, List<Cell> currentPath, CellState[,] field, Cell startCell, Cell currentCell)
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
                    paths.Add(new List<Cell> {neightbor });
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

                    var newPath = new List<Cell>();
                    newPath.AddRange(currentPath);
                    newPath.Add(killCell);
                    Cell afterKillCell = Helpers.GetCellAfterKill(currentCell, nextCell);
                    newPath.Add(afterKillCell);
                    anyKill = true;

                    GetPossibleMovementsRecursive(paths, newPath, field, startCell, afterKillCell);
                }
            }
            if (!anyKill && currentPath.Any())
            {
                paths.Add(currentPath);
            }
        }
    }
}

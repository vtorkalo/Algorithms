using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class KingNeightborGenerator
    {
        public List<List<Cell>> GetKingNeightbors(CellState[,] field, Cell startCell, CellState startCellState, Cell currentCell)
        {
            var result = new List<List<Cell>>();
            result.Add(GetKingNeightbors(field, startCell, startCellState, currentCell, Helpers.GetRightTopNextCell));
            result.Add(GetKingNeightbors(field, startCell, startCellState, currentCell, Helpers.GetRigthBottomNextCell));
            result.Add(GetKingNeightbors(field, startCell, startCellState, currentCell, Helpers.GetLeftBottomNextCell));
            result.Add(GetKingNeightbors(field, startCell, startCellState, currentCell, Helpers.GetLeftTopNextCell));

            return result;
        }

        public List<Cell> GetKingNeightbors(CellState[,] field, Cell startCell, CellState startCellState, Cell currentCell,
            Func<Cell, int, Cell> getter)
        {
            var result = new List<Cell>();
            int distance = 1;
            Cell newCell = getter(currentCell, distance);
            while (Helpers.IsInRange(newCell))
            {
                var newCellState = Helpers.GetCellState(field, newCell);
                if (newCellState != CellState.Empty
                    && !Helpers.IsEnemy(startCellState, newCellState)
                    && !Helpers.CompareCells(newCell, startCell))
                {
                    break;
                }

                if (Helpers.IsEnemy(startCellState, newCellState))
                {
                    var cellAfterEnemy = getter(currentCell, distance + 1);
                    if (Helpers.IsInRange(cellAfterEnemy))
                    {
                        var cellAfterEnemyState = Helpers.GetCellState(field, cellAfterEnemy);

                        if (cellAfterEnemyState != CellState.Empty && !Helpers.CompareCells(cellAfterEnemy, startCell))
                        {
                            break;
                        }
                        newCell.Kill = true;
                    }
                }

                result.Add(newCell);

                distance++;
                newCell = getter(currentCell, distance);
            }

            return result;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaveSearch
{
    class Program
    {

        // wave algorith implementation https://ru.wikipedia.org/wiki/%D0%90%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%9B%D0%B8
        static void Main(string[] args)
        {
            var data = PrepareData();

            PrintData(data);
            int startRow = 6;
            int startCol = 3;

            int endRow = 0;
            int endCol = 3;

            
            var startCell = new Cell { row = startRow, col = startCol };
            var endCell = new Cell { row = endRow, col = endCol };

            SetValue(data, startCell, 0);
            Fill(data,startCell);

            var path = new List<Cell>();
            path.Add(startCell);
            RestorePath(data, path, endCell , startCell);
            path.Add(endCell);

            foreach (var c in path)
            {
                SetValue(data, c, 0);
            }
            PrintData(data);

            Console.ReadLine();
        }

        private static void RestorePath(int?[,] data, List<Cell> path, Cell currentCell, Cell startCell)
        {
            var currentCellValue = GetValue(data, currentCell);
            var neighbors = GetNeighbors(currentCell, data.GetLength(0), data.GetLength(1));
            foreach (var neighbor in neighbors)
            {
                var cellValue = GetValue(data, neighbor);
                if (cellValue == currentCellValue - 1)
                {
                    path.Add(neighbor);
                    RestorePath(data, path, neighbor, startCell);
                    return;
                }
            }
        }

        private static void Fill(int?[,] data, Cell startCell)
        {
            var queue = new Queue<Cell>();
            queue.Enqueue(startCell);

            while (queue.Any())
            {
                Cell currentCell = queue.Dequeue();
                var neighbors = GetNeighbors(currentCell, data.GetLength(0), data.GetLength(1));
                foreach (var neighbor in neighbors)
                {

                    var currentCellValue = GetValue(data, currentCell);

                    var cellValue = GetValue(data, neighbor);
                    if (!cellValue.HasValue)
                    {
                        SetValue(data, neighbor, currentCellValue + 1);
                        PrintData(data);
                        Thread.Sleep(100);
                        queue.Enqueue(neighbor);
                    }
                }
            }

        }

        private static int? GetValue(int?[,] data, Cell cell)
        {
            return data[cell.row, cell.col];
        }
        private static void SetValue(int?[,] data, Cell cell, int? value)
        {
            data[cell.row, cell.col] = value;
        }

        private static List<Cell> GetNeighbors(Cell cell, int totalRows, int totalCols)
        {
            int row = cell.row;
            int col = cell.col;
            var result = new List<Cell>
            {
                new Cell{row = row-1, col = col-1},
                new Cell{row = row-1, col = col},
                new Cell{row = row-1, col = col+1},

                new Cell{row = row, col = col-1},
                new Cell{row = row, col = col+1},

                new Cell{row = row+1, col = col-1},
                new Cell{row = row+1, col = col},
                new Cell{row = row+1, col = col+1},
            };

            result = result.Where(c => c.row >= 0 && c.col >= 0 
                                 && c.row < totalRows && c.col < totalCols)
                                 .ToList();

            return result;
        }

        private static int?[,] PrepareData()
        {
            var data = new int?[8, 12];
            data[0, 2] = -1;
            data[1, 2] = -1;

            data[4, 1] = -1;
            data[4, 2] = -1;
            data[4, 3] = -1;
            data[4, 4] = -1;


            data[3, 8] = -1;
            data[3, 9] = -1;


            data[5, 9] = -1;
            data[5, 10] = -1;
            data[6, 9] = -1;
            data[6, 10] = -1;
            return data;
        }

        private static void PrintData(int?[,] data)
        {
            Console.Clear();
            for (int row = 0; row < data.GetLength(0); row++)
            {
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    if (data[row, col].HasValue)
                    {
                        var value = data[row, col].Value;
                        Console.Write((value == -1 ? "X" : value.ToString()));
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
        }


    }

    public struct Cell
    {
        public int row;
        public int col;
    }
}

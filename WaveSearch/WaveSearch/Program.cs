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
            var startCell = new Cell { row = 6, col = 3 };
            var endCell = new Cell { row = 0, col = 3 };

            SetValue(data, startCell, 0);
            Fill(data, startCell);

            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell> { endCell };
            RestorePath(data, endCell, paths, currentPath, endCell);

            int index = 0;
            foreach (var path in paths)
            {
                data = PrepareData();
                int cellIndex = 0;
                foreach (var c in path)
                {
                    SetValue(data, c, cellIndex);
                    cellIndex++;
                }
                PrintData(data);
                Console.WriteLine("Path " + index.ToString());
                Thread.Sleep(1000);
                index++;
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }


        private static void RestorePath(int?[,] data, Cell endCell, List<List<Cell>> paths, List<Cell> currentPath, Cell currentCell)
        {
            var endCellValue = GetValue(data, endCell);
            if (!endCellValue.HasValue)
            {
                return;
            }

            var neighbors = GetNeighbors(currentCell, data.GetLength(0), data.GetLength(1));
            foreach (var neighbor in neighbors)
            {
                var cellValue = GetValue(data, neighbor);
                if (cellValue == GetValue(data, currentCell) - 1)
                {
                    var newPath = new List<Cell>();
                    newPath.AddRange(currentPath);
                    newPath.Add(neighbor);

                    var alreadyExists = AlreadyExists(paths, newPath);

                    if (!alreadyExists)
                    {
                        RestorePath(data, endCell, paths, newPath, neighbor);
                    }
                    else
                    {
                        break;
                    }

                }
            }

            if (GetValue(data, currentCell) == 0)
            {
                paths.Add(currentPath);
            }
        }

        private static bool AlreadyExists(List<List<Cell>> paths, List<Cell> newPath) //TODO refactor this
        {
            bool alreadyExists = false;
            foreach (var p in paths)
            {
                var existingCellPath = p.Take(newPath.Count).ToList();
                if (existingCellPath.Count() == newPath.Count)
                {
                    bool match = true;
                    for (int i = 0; i < newPath.Count; i++)
                    {
                        if (newPath[i].row != existingCellPath[i].row || newPath[i].col != existingCellPath[i].col)
                        {
                            match = false;
                        }
                    }
                    alreadyExists = match;
                }
            }

            return alreadyExists;
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
                        Console.WriteLine("Preparing cell values");
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

        private static IEnumerable<Cell> GetNeighbors(Cell cell, int totalRows, int totalCols)
        {
            int row = cell.row;
            int col = cell.col;
            var result = new List<Cell>
            {
                new Cell{row = row-1, col = col-1},
                new Cell{row = row-1, col = col},
                new Cell{row = row-1, col = col+1},
                new Cell{row = row, col = col+1},
                new Cell{row = row+1, col = col+1},
                new Cell{row = row+1, col = col},
                new Cell{row = row+1, col = col-1},
                new Cell{row = row, col = col-1},
            };

            return result.Where(c => c.row >= 0 && c.col >= 0
                                  && c.row < totalRows && c.col < totalCols);
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

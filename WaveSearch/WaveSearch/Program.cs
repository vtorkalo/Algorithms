﻿using System;
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
            var startCell = new Cell { row = 0, col = 1 };
            var endCell = new Cell { row = 0, col = 11 };

            SetValue(data, startCell, 0);
            PrintData(data);
            Console.WriteLine("Press enter");
            Console.ReadLine();

            Fill(data, startCell);
            Console.WriteLine("Press enter");
            Console.ReadLine();

            var paths = new List<List<Cell>>();
            var currentPath = new List<Cell> { endCell };
            RestorePath(data, endCell, paths, currentPath, endCell);

            data = PrintPaths(data, paths);

            Console.WriteLine("Done");
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        private static int?[,] PrintPaths(int?[,] data, List<List<Cell>> paths)
        {
            int index = 0;
            foreach (var path in paths.OrderBy(p => GetPathLength(p)))
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
                Thread.Sleep(100);
                index++;
            }

            return data;
        }

        private static double GetPathLength(List<Cell> path)
        {
            double sqrt2 = Math.Sqrt(2);
            double length = 0;
          
            for (int i=0; i< path.Count-1; i++)
            {
                var currentCell = path[i];
                var nextCell = path[i + 1];

                if (nextCell.row == currentCell.row - 1 && nextCell.col == currentCell.col - 1
                    || nextCell.row == currentCell.row - 1 && nextCell.col == currentCell.col + 1
                    || nextCell.row == currentCell.row + 1 && nextCell.col == currentCell.col + 1
                    || nextCell.row == currentCell.row + 1 && nextCell.col == currentCell.col - 1
                    )
                {
                    length += sqrt2;
                }
                else
                {
                    length++;
                }
            }

            return length;
        }


        private static void RestorePath(int?[,] data, Cell endCell, List<List<Cell>> paths, List<Cell> currentPath, Cell currentCell)
        {
            if (paths.Count >= 1000)
            {
                return;
            }

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

                    RestorePath(data, endCell, paths, newPath, neighbor);
                }
            }

            if (GetValue(data, currentCell) == 0)
            {
                paths.Add(currentPath);
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
                        Console.WriteLine("Preparing cell values.");
                        Thread.Sleep(50);
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
                new Cell{row = row-1, col = col},
                new Cell{row = row, col = col+1},
                new Cell{row = row+1, col = col},
                new Cell{row = row, col = col-1},

                new Cell{row = row-1, col = col-1},
                new Cell{row = row-1, col = col+1},
                new Cell{row = row+1, col = col+1},
                new Cell{row = row+1, col = col-1},
            };

            return result.Where(c => c.row >= 0 && c.col >= 0
                                  && c.row < totalRows && c.col < totalCols);
        }

        private static int?[,] PrepareData()
        {
            var data = new int?[8, 12];
            data[3, 2] = -1;


            data[0, 2] = -1;
            data[1, 2] = -1;

            data[2, 2] = -1;
            data[2, 1] = -1;


            data[4, 1] = -1;
            data[4, 2] = -1;
            data[4, 3] = -1;
            data[4, 4] = -1;

            data[3, 8] = -1;
            data[3, 9] = -1;
            data[4, 9] = -1;

            data[0, 9] = -1;
            data[1, 9] = -1;
            data[2, 9] = -1;
            data[3, 9] = -1;


            data[1, 6] = -1;
            data[2, 6] = -1;
            data[3, 6] = -1;
            data[4, 6] = -1;
            data[5, 6] = -1;
            data[6, 6] = -1;
            data[7, 6] = -1;



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
                        Console.ForegroundColor = value == -1 ? ConsoleColor.Red : ConsoleColor.White;
                        Console.Write(value == -1 ? "X  " : string.Format("{0, -3}", value) );
                    }
                    else
                    {
                        Console.Write("   ");
                    }
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

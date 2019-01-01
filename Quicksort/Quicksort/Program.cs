using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(new ThreadStart(DoWork), 20000000);
            thread.Start();
       //     SortRecursive(array, 0, array.Length-1);
        }

        private static void DoWork()
        {
            var array = Enumerable.Range(0, 100000).Reverse().ToArray();
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            SortIteration(array);
            //SortRecursive(array, 0, array.Length - 1);
            s.Stop();
            var ms = s.ElapsedMilliseconds;
            Console.WriteLine(ms);
            Console.ReadLine();
        }


        private static void SortIteration(int[] array)
        {
            var stack = new Stack<Iteration>();
            int i = 0;
            int j = array.Length - 1;

            stack.Push(new Iteration { lo = i, hi = j });

            while (stack.Any())
            {
                var iteration = stack.Pop();
                         
                if (iteration.lo < iteration.hi)
                {
                    var p = Partition(array, iteration.lo, iteration.hi);
                    stack.Push(new Iteration { lo = iteration.lo, hi = p-1 });
                    stack.Push(new Iteration { lo = p+1, hi = iteration.hi });
                }
            }
        }

        private static void SortRecursive(int[] array, int lo, int hi)
        {
            if (lo < hi)
            {
                var p = Partition(array, lo, hi);
                SortRecursive(array, lo, p-1);
                SortRecursive(array, p+1, hi);
            }
        }

        private static int Partition(int[] array, int lo, int hi)
        {
            int reference = array[lo+(hi-lo)/2];

            int i = lo;
            int j = hi;
            while (i < j)
            {
                while (i <= hi && array[i] <= reference) 
                {
                    i++;
                }
                while (array[j] > reference)
                {
                    j--;
                } 
                if (i < j)
                {
                    Swap(array, i, j);
                }
            }

            if (lo != j)
            {
                Swap(array, lo, j);
            }
            return j;
        }

        private static void Swap(int[] array, int i, int j)
        {
            var temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
    }
    public struct Iteration
    {
        public int lo { get; set; }
        public int hi { get; set; }
    }
}

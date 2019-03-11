using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Depth_first_search_Iterative
{
    class Program
    {
        static void Main(string[] args)
        {
            // for depth first search
            var container = new QueueContainer<string>(); 

            // for width first search
            //var container = new StackContainer<string>();

            container.Add(@"c:\");
            var allNodes = new List<string>();

            while (container.Any())
            {
                var currentDir = container.GetAndRemove();
                allNodes.Add(currentDir);
                Console.WriteLine(currentDir);
                try
                {
                    var directories = Directory.GetDirectories(currentDir).AsEnumerable();
                    if (container is StackContainer<string>)
                    {
                        directories = directories.Reverse();
                    }

                    foreach (var subnode in directories)
                    {
                        container.Add(subnode);
                    }
                }
                catch (Exception) { }
            }

            foreach (var node in allNodes)
            {
                Console.WriteLine(node);
            }

            Console.ReadLine();
        }
    }

    public interface IDataContainer<T>
    {
        T GetAndRemove();
        void Add(T item);
        bool Any();
    }

    public class StackContainer<T> : IDataContainer<T>
    {
        private Stack<T> _stack = new Stack<T>();

        public bool Any()
        {
            return _stack.Any();
        }

        public T GetAndRemove()
        {
            return _stack.Pop();
        }

        public void Add(T item)
        {
            _stack.Push(item);
        }
    }

    public class QueueContainer<T> : IDataContainer<T>
    {
        private Queue<T> _queue = new Queue<T>();

        public bool Any()
        {
            return _queue.Any();
        }

        public T GetAndRemove()
        {
            return _queue.Dequeue();
        }

        public void Add(T item)
        {
            _queue.Enqueue(item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://ru.wikipedia.org/wiki/%D0%90%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%94%D0%B5%D0%B9%D0%BA%D1%81%D1%82%D1%80%D1%8B

            List<Node> nodes = PrepareGraph();

            Node endNode = nodes[6];
            Node startNode = nodes[1];

            var path = CalculatePath(nodes, endNode, startNode);
            PrintNodes(path);
            Console.ReadLine();
        }

        private static List<Node> CalculatePath(List<Node> nodes, Node endNode, Node startNode)
        {

            startNode.PathLength = 0;
            Node currentNode = startNode;
            while (currentNode != null)
            {
                var sortedConnectedNodes = currentNode.GetConnectedNodes()
                    .Where(n => !n.IsVisited)
                    .Select(conNode => new
                    {
                        Node = conNode,
                        Connection = conNode.GetConnectionToNode(currentNode)
                    })
                    .OrderBy(x => x.Connection.Length).ToList();

                foreach (var connectedNode in sortedConnectedNodes)
                {
                    var newPathLength = currentNode.PathLength + connectedNode.Connection.Length;
                    if (newPathLength < connectedNode.Node.PathLength)
                    {
                        connectedNode.Node.PathLength = newPathLength;
                        connectedNode.Node.PrevNode = currentNode;
                    }
                }

                currentNode.IsVisited = true;
                currentNode = sortedConnectedNodes.OrderBy(n => n.Node.PathLength).FirstOrDefault()?.Node;
            }

            var path = GetPathFromNodes(endNode);
            return path;
        }

        private static List<Node> GetPathFromNodes(Node endNode)
        {
            var path = new List<Node>();
            Node currNode = endNode;
            while (currNode != null)
            {
                path.Add(currNode);
                currNode = currNode.PrevNode;
            }

            return path;
        }


        private static List<Node> PrepareGraph()
        {
            var nodes = Enumerable.Range(0, 7).Select(i => new Node() { Name = "node " + i.ToString() }).ToList();
            nodes[1].ConnectNodes(nodes[2], 7);
            nodes[2].ConnectNodes(nodes[4], 15);
            nodes[4].ConnectNodes(nodes[5], 6);
            nodes[5].ConnectNodes(nodes[6], 9);
            nodes[6].ConnectNodes(nodes[1], 14);
            nodes[1].ConnectNodes(nodes[3], 9);
            nodes[2].ConnectNodes(nodes[3], 10);
            nodes[4].ConnectNodes(nodes[3], 11);
            nodes[6].ConnectNodes(nodes[3], 2);

            return nodes;
        }

        private static void PrintNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                Console.WriteLine(node.Name);     
            }
        }
    }

    public class Node
    {
        public bool IsVisited { get; set; }

        public List<Connection> Connections = new List<Connection>();
        public double PathLength = double.PositiveInfinity;
        public Node PrevNode { get; set; }

        public Connection ConnectNodes(Node node, double length)
        {
            var connection = new Connection
            {
                Start = this,
                End = node,
                Length = length
            };
            Connections.Add(connection);
            node.Connections.Add(connection);

            return connection;
        }

        public List<Node> GetConnectedNodes()
        {
            var result = this.Connections.Select(c => c.Start)
                             .Union(this.Connections.Select(c => c.End))
                             .Where(n => n != this).Distinct();

            return result.ToList();
        }

        public Connection GetConnectionToNode(Node node)
        {
            var connection = this.Connections.SingleOrDefault(c => c.Start == node || c.End == node);
            return connection;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Connection
    {
        public Node Start { get; set; }
        public Node End { get; set; }
        public double Length { get; set; }

        public bool IsConnectedToNode(Node node)
        {
            bool result = this.Start == node || this.End == node;
            return result;
        }
    }
}

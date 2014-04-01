namespace Dijkstra.Helpers
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using My = global::Dijkstra.DataStructures;

    public class GraphReader
    {
        public static My.BinaryHeap<My.Node> CreateBinHeap(string filetoread)
        {
            if (!File.Exists(filetoread))
            {
                throw new Exception("Cannot find or read file");
            }

            var bheap = new My.BinaryHeap<My.Node>();
            var nodeCount = 0;
            var edges = 0;

            var gfile = File.ReadAllLines(filetoread);
            var node = new My.Node();

            var nodeIdregex = new Regex(@"^\d+");
            var adjacentNodeRegex = new Regex(@"\s+\d+\s+\d+");

            foreach (var line in gfile)
            {
                if (Regex.IsMatch(line, @"="))
                {
                    // regex vertex count and edge count 
                    var vertsEdges = Regex.Matches(line, @"\d+");
                    nodeCount = Convert.ToInt32(vertsEdges[0].Value);
                    edges = Convert.ToInt32(vertsEdges[1].Value);
                    Console.WriteLine("This graph has {0} vertices and {1} edges.", nodeCount, edges);
                }
                else if (nodeIdregex.IsMatch(line))
                {
                    node.Id = Convert.ToInt32(nodeIdregex.Match(line).Value);
                }
                else if (adjacentNodeRegex.IsMatch(line))
                {
                    var neighborMatch = Regex.Matches(line, @"\d+");
                    var neighbor = new My.Node();
                    neighbor.Id = Convert.ToInt32(neighborMatch[0].Value);
                    neighbor.Distance = Convert.ToInt32(neighborMatch[1].Value);
                    node.AdjacentList.Add(neighbor);
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    bheap.Add(node);
                    node = new My.Node();
                }
            }

            if (bheap.Count != nodeCount)
            {
                Console.WriteLine("Graph File Indicated {0} vertices but we only added/parsed {1}!", nodeCount, bheap.Count);
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Processed graph with {0} vertices.", nodeCount);
            }

            return bheap;

        }
    }
}

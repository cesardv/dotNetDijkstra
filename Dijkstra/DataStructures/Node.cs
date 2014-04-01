
namespace Dijkstra.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Node : IComparable<Node>
    {
        public int Id { get; set; }

        public bool IsSource { get; set; }

        public int Distance { get; set; }

        public IList<Node> AdjacentList { get; set; }

        public bool Visited { get; set; }

        public Node()
        {
            this.AdjacentList = new List<Node>();
        }

        public Node(int id, int distance, IList<Node> neighbors, bool isSource = false)
        {
            this.Id = id;
            this.Distance = distance;
            this.AdjacentList = neighbors;
            this.IsSource = isSource;
        }

        int IComparable<Node>.CompareTo(Node other)
        {
            return other.Distance - this.Distance;
        }
    }
}

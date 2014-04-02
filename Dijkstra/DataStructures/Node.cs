
namespace Dijkstra.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a Node in a Graph
    /// </summary>
    public class Node : IComparable<Node>
    {

        public Node()
        {
            this.AdjacentList = new Dictionary<int, int>();
        }

        public Node(int id, int distance, Dictionary<int,int> neighbors, bool isSource = false)
        {
            this.Id = id;
            this.Distance = distance;
            this.AdjacentList = neighbors;
            this.IsSource = isSource;
        }

        public int Id { get; set; }

        public bool IsSource { get; set; }

        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets the Id of the neighbor and the distance
        /// </summary>
        public Dictionary<int, int> AdjacentList { get; set; }

        public bool Visited { get; set; }

        public Node ParentNode { get; set; }

        int IComparable<Node>.CompareTo(Node other)
        {
            return this.Distance - other.Distance;  // if we get a negative result, that means this node's distance is less than the other.
        }
    }
}

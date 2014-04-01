namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    using global::Dijkstra.DataStructures;

    public class Dijkstra
    {

        public IList<Node> Graph { get; private set; }

        /// <summary>
        /// Handle to the source or start node
        /// </summary>
        public Node Source { get; private set; }

        private IList<Node> visitedNodes;

        /// <summary>
        /// Initializes class instance to run Dijkstra's algorithm on a graph
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="source">Source or start node</param>
        public Dijkstra(IList<Node> graph, Node source)
        {
            this.Graph = graph;
            this.Source = source;
        }

        public IList<Node> Run()
        {
            if (this.Graph == null || this.Graph.Count == 0 || this.Source == null)
            {
                throw new Exception("Cannot run Dijkstra without proper graph and source node.");
            }


            this.Initialize();
            this.Source.Visited = true;
            this.visitedNodes = new List<Node>();
            var bheap = new BinaryHeap<Node>();
            foreach (var node in this.Graph)
            {
                bheap.Add(node);
            }

            while (bheap.Count > 0)
            {
                var u = bheap.Remove();  // extract node with smallest distance value, on first iteration always source node
                // visit u
                u.Visited = true;
                this.visitedNodes.Add(u);

                foreach (var neighbor in u.AdjacentList)
                {
                    this.RelaxEdge(u, neighbor);
                }
            }

            return this.Graph;
        }

        /// <summary>
        /// Initializes the graph's vertices in order to start
        /// </summary>
        protected void Initialize()
        {
            foreach (var node in this.Graph)
            {
                node.Distance = int.MaxValue; // MaxValue will be sentinel for infinity
                node.ParentNode = null;
            }

            this.Source.Distance = 0;
        }

        /// <summary>
        /// Relaxes a specific edge of the graph
        /// </summary>
        protected void RelaxEdge(Node startNode, Node endNeighbor)
        {
            var indexOfNeighbor = startNode.AdjacentList.IndexOf(endNeighbor);
            if (indexOfNeighbor < 0 || !startNode.AdjacentList.Contains(endNeighbor))
            {
                throw new Exception("Can't find neighbor, thus can relax edge between these two vertices.");
            }

            var edgeDistance = startNode.AdjacentList[indexOfNeighbor].Distance;

            if (endNeighbor.Distance > startNode.Distance + edgeDistance)
            {
                // Update endNeigbor
                endNeighbor.Distance = startNode.Distance + edgeDistance;
                endNeighbor.ParentNode = startNode;
            }
        }
    }
}

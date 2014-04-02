namespace Dijkstra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// <param name="graph">Graph to be used</param>
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

                foreach (var kvp in u.AdjacentList)
                {
                    var neighbornode = this.Graph.FirstOrDefault(n => n.Id == kvp.Key); // this.Graph.FirstOrDefault()
                    this.RelaxEdge(u, neighbornode);
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
                node.Distance = 999999; // (1 million - 1) will be sentinel value for infinity
                node.ParentNode = null;
            }

            this.Source.Distance = 0;
        }

        /// <summary>
        /// Relaxes a specific edge of the graph
        /// </summary>
        protected void RelaxEdge(Node startNode, Node endNeighbor)
        {
            var edgeDistance = startNode.AdjacentList[endNeighbor.Id];

            if (endNeighbor.Distance > startNode.Distance + edgeDistance)
            {
                // Update endNeigbor
                endNeighbor.Distance = startNode.Distance + edgeDistance;
                endNeighbor.ParentNode = startNode;
            }
        }
    }
}

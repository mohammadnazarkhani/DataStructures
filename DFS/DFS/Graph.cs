using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    /// <summary>
    /// Graph class represents a collection of nodes and edges.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Nodes in the graph.
        /// </summary>
        public List<Node> Nodes { get; set; } = new List<Node>();

        /// <summary>
        /// Adds a node to the graph.
        /// </summary>
        /// <remarks>
        /// It is assumed that the node does not already exist in the graph.
        /// Also it doesn't add edges.
        /// </remarks>
        /// <param name="node">
        /// Node to be added.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throw when given node is null.
        /// </exception>
        public void AddNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            Nodes.Add(node);
        }

        /// <summary>
        /// Adds an edge between two nodes in the graph.
        /// </summary>
        /// <param name="from">
        /// Node from which the edge starts.
        /// </param>
        /// <param name="to">
        /// Node to which the edge points.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either <paramref name="from"/> or <paramref name="to"/> is <c>null</c>.
        /// </exception>
        public void AddEdge(Node from, Node to)
        {
            if (from == null || to == null) throw new ArgumentNullException("Nodes cannot be null");
            from.AddNeighbor(to);
        }

        /// <summary>
        /// Resets the visited status of all nodes in the graph.
        /// </summary>
        public void ResetVisited()
        {
            foreach (var node in Nodes)
            {
                node.IsVisited = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    /// <summary>
    /// Node class represents a single node in a graph.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Integer value of the node.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// List of neighboring nodes.
        /// </summary>
        public List<Node> Neighbors { get; set; } = new List<Node>();
        /// <summary>
        /// Indicates whether the node has been visited during traversal.
        /// </summary>
        public bool IsVisited { get; set; }

        public Node()
        {
        }
        public Node(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Adds a neighbor to the current node.
        /// </summary>
        /// <param name="neighbor">
        /// A neighbor node to be added.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the neighbor is null.
        /// </exception>
        public void AddNeighbor(Node neighbor)
        {
            if (neighbor == null) throw new ArgumentNullException(nameof(neighbor));
            if (Neighbors.Contains(neighbor)) return;

            Neighbors.Add(neighbor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    public class Graph
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        public void AddNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            Nodes.Add(node);
        }

        public void AddEdge(Node from, Node to)
        {
            if (from == null || to == null) throw new ArgumentNullException("Nodes cannot be null");
            from.AddNeighbor(to);
        }

        public void ResetVisited()
        {
            foreach (var node in Nodes)
            {
                node.IsVisited = false;
            }
        }
    }
}

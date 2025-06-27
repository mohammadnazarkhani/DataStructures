using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public class Graph
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        public Graph() { }
        public Graph(List<Node> Nodes)
        {
            this.Nodes = Nodes;
        }

        public void AddNode(Node node)
        {
            if (node != null && !Nodes.Contains(node))
            {
                Nodes.Add(node);
            }
        }

        public void RemoveNode(Node node) { Nodes.Remove(node); }

        public void ResetVisited()
        {
            foreach (var node in Nodes)
            {
                node.ResetVisit(); // Reset visited status
            }
        }

        public void AddEdge(Node from, Node to)
        {
            if (from != null && to != null && Nodes.Contains(from) && Nodes.Contains(to))
                from.AddNeighbor(to);
        }
    }
}

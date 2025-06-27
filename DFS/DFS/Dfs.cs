using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    public class Dfs
    {
        public List<Node> Traverse(Graph graph, Node startNode)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);

            graph.ResetVisited();

            var visitedNodes = new List<Node>();
            TraverseRecursive(startNode, visitedNodes);
            return visitedNodes;
        }

        public void TraverseRecursive(Node current, List<Node> visitedNodes)
        {
            current.IsVisited = true;
            visitedNodes.Add(current);

            foreach (var neighbor in current.Neighbors)
                if (!neighbor.IsVisited) TraverseRecursive(neighbor, visitedNodes);
        }

        private void ThrowIfNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }
}

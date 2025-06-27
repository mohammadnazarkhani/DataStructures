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

        /// <summary>
        /// Searches for a node in the graph using DFS.
        /// </summary>
        /// <param name="graph">Graph to search</param>
        /// <param name="startNode">First node to start searching</param>
        /// <param name="searchingCriteria">
        /// A predicate function which takes in a instance of Node and returns true if the node matches the searching criteria.
        /// </param>
        /// <returns>
        /// Returns the first node that matches the searching criteria, or null if no such node is found.
        /// </returns>
        public Node? Search(Graph graph, Node startNode, Func<Node, bool> searchingCriteria)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);
            ThrowIfNull(searchingCriteria);

            graph.ResetVisited();

            return SearchRecursive(startNode, searchingCriteria);
        }

        public bool TrySearch(Graph graph, Node startNode, Func<Node, bool> searchingCriteria, out Node? foundNode)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);
            ThrowIfNull(searchingCriteria);

            graph.ResetVisited();

            foundNode = SearchRecursive(startNode, searchingCriteria);
            return foundNode != null;
        }

        public Node? SearchRecursive(Node current, Func<Node, bool> searchingCriteria)
        {
            current.IsVisited = true;
            if (searchingCriteria(current)) return current;

            foreach (var neighbor in current.Neighbors)
            {
                if (!neighbor.IsVisited)
                {
                    var result = SearchRecursive(neighbor, searchingCriteria);
                    if (result != null) return result;
                }
            }
            return null;
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

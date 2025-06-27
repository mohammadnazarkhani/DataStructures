using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    /// <summary>
    /// Provides methods to perform Depth First Search (DFS) traversal and search operations on a graph.
    /// </summary>
    public class Dfs
    {
        /// <summary>
        /// Traverses the graph using DFS starting from the specified node.
        /// </summary>
        /// <param name="graph">The graph to traverse.</param>
        /// <param name="startNode">The node from which to start the traversal.</param>
        /// <returns>
        /// A list of nodes in the order they were visited during the traversal.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/> or <paramref name="startNode"/> is <c>null</c>.
        /// </exception>
        public List<Node> Traverse(Graph graph, Node startNode)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);

            graph.ResetVisited();

            var visitedNodes = new List<Node>();
            TraverseRecursive(startNode, visitedNodes);
            return visitedNodes;
        }

        /// <summary>
        /// Recursively traverses the graph using DFS from the specified node.
        /// </summary>
        /// <param name="current">The current node being visited.</param>
        /// <param name="visitedNodes">The list of nodes that have been visited so far.</param>
        /// <remarks>
        /// This method is intended for internal use by <see cref="Traverse"/>.
        /// </remarks>
        public void TraverseRecursive(Node current, List<Node> visitedNodes)
        {
            current.IsVisited = true;
            visitedNodes.Add(current);

            foreach (var neighbor in current.Neighbors)
                if (!neighbor.IsVisited) TraverseRecursive(neighbor, visitedNodes);
        }

        /// <summary>
        /// Searches for a node in the graph that matches the specified criteria using DFS.
        /// </summary>
        /// <param name="graph">The graph to search.</param>
        /// <param name="startNode">The node from which to start the search.</param>
        /// <param name="searchingCriteria">
        /// A predicate function that determines whether a node matches the search criteria.
        /// </param>
        /// <returns>
        /// The first node that matches the search criteria, or <c>null</c> if no such node is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/>, <paramref name="startNode"/>, or <paramref name="searchingCriteria"/> is <c>null</c>.
        /// </exception>
        public Node? Search(Graph graph, Node startNode, Func<Node, bool> searchingCriteria)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);
            ThrowIfNull(searchingCriteria);

            graph.ResetVisited();

            return SearchRecursive(startNode, searchingCriteria);
        }

        /// <summary>
        /// Attempts to find a node in the graph that matches the specified criteria using DFS.
        /// </summary>
        /// <param name="graph">The graph to search.</param>
        /// <param name="startNode">The node from which to start the search.</param>
        /// <param name="searchingCriteria">
        /// A predicate function that determines whether a node matches the search criteria.
        /// </param>
        /// <param name="foundNode">
        /// When this method returns, contains the first node that matches the search criteria, or <c>null</c> if no such node is found.
        /// </param>
        /// <returns>
        /// <c>true</c> if a matching node is found; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/>, <paramref name="startNode"/>, or <paramref name="searchingCriteria"/> is <c>null</c>.
        /// </exception>
        public bool TrySearch(Graph graph, Node startNode, Func<Node, bool> searchingCriteria, out Node? foundNode)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);
            ThrowIfNull(searchingCriteria);

            graph.ResetVisited();

            foundNode = SearchRecursive(startNode, searchingCriteria);
            return foundNode != null;
        }

        /// <summary>
        /// Recursively searches for a node that matches the specified criteria using DFS.
        /// </summary>
        /// <param name="current">The current node being visited.</param>
        /// <param name="searchingCriteria">
        /// A predicate function that determines whether a node matches the search criteria.
        /// </param>
        /// <returns>
        /// The first node that matches the search criteria, or <c>null</c> if no such node is found.
        /// </returns>
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

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified object is <c>null</c>.
        /// </summary>
        /// <param name="obj">The object to check for <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        private void ThrowIfNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }
}

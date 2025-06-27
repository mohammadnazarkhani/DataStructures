using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DFS
{
    /// <summary>
    /// Provides methods to perform Depth First Search (DFS) traversal and search operations on a graph.
    /// Supports both recursive and iterative approaches, as well as pre-order and post-order traversals.
    /// </summary>
    public class Dfs
    {
        /// <summary>
        /// Traverses the graph using DFS starting from the specified node.
        /// </summary>
        /// <param name="graph">The graph to traverse.</param>
        /// <param name="startNode">The node from which to start the traversal.</param>
        /// <param name="visitAction">An action to perform on each visited node (optional).</param>
        /// <param name="order">The order in which to visit nodes (PreOrder or PostOrder).</param>
        /// <param name="approach">The DFS approach to use (Recursive or Iterative).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A list of nodes in the order they were visited during the traversal.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/> or <paramref name="startNode"/> is <c>null</c>.
        /// </exception>
        public List<Node> Traverse(
            Graph graph,
            Node startNode,
            Action<Node>? visitAction = null,
            DfsOrder order = DfsOrder.PreOrder,
            DfsApproach approach = DfsApproach.Recursive,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);

            graph.ResetVisited();

            var visitedNodes = new List<Node>();
            switch (approach)
            {
                case DfsApproach.Recursive:
                    TraverseRecursive(startNode, visitedNodes, visitAction, order, cancellationToken);
                    break;
                case DfsApproach.Iterative:
                    TraverseIterative(startNode, visitedNodes, visitAction, order, cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(approach), approach, "Invalid DFS approach.");
            }
            return visitedNodes;
        }

        /// <summary>
        /// Recursively traverses the graph using DFS from the specified node.
        /// </summary>
        /// <param name="current">The current node being visited.</param>
        /// <param name="visitedNodes">The list of nodes that have been visited so far.</param>
        /// <param name="visitAction">An action to perform on each visited node (optional).</param>
        /// <param name="order">The order in which to visit nodes (PreOrder  or PostOrder).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <remarks>
        /// This method is intended for internal use by <see cref="Traverse"/>.
        /// </remarks>
        private void TraverseRecursive(
            Node current,
            List<Node> visitedNodes,
            Action<Node>? visitAction,
            DfsOrder order,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            current.IsVisited = true;

            if (order == DfsOrder.PreOrder)
            {
                visitedNodes.Add(current);
                visitAction?.Invoke(current);
            }

            foreach (var neighbor in current.Neighbors)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (!neighbor.IsVisited)
                    TraverseRecursive(neighbor, visitedNodes, visitAction, order, cancellationToken);
            }

            if (!cancellationToken.IsCancellationRequested && order == DfsOrder.PostOrder)
            {
                visitedNodes.Add(current);
                visitAction?.Invoke(current);
            }
        }

        /// <summary>
        /// Iteratively traverses the graph using DFS from the specified node.
        /// </summary>
        /// <param name="startNode">The node from which to start the traversal.</param>
        /// <param name="visitedNodes">The list of nodes that have been visited so far.</param>
        /// <param name="visitAction">An action to perform on each visited node (optional).</param>
        /// <param name="order">The order in which to visit nodes (PreOrder or PostOrder).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <remarks>
        /// This method is intended for internal use by <see cref="Traverse"/>.
        /// </remarks>
        private void TraverseIterative(
            Node startNode,
            List<Node> visitedNodes,
            Action<Node>? visitAction,
            DfsOrder order,
            CancellationToken cancellationToken)
        {
            ThrowIfNull(visitedNodes);
            ThrowIfNull(startNode);

            var stack = new Stack<(Node node, bool postVisit)>();
            stack.Push((startNode, false));

            while (stack.Count > 0 && !cancellationToken.IsCancellationRequested)
            {
                var (node, postVisit) = stack.Pop();

                if (!node.IsVisited)
                {
                    if (order == DfsOrder.PreOrder && !postVisit)
                    {
                        node.IsVisited = true;
                        visitedNodes.Add(node);
                        visitAction?.Invoke(node);

                        foreach (var neighbor in node.Neighbors.Reverse<Node>())
                        {
                            if (!neighbor.IsVisited)
                                stack.Push((neighbor, false));
                        }
                    }
                    else if (order == DfsOrder.PostOrder)
                    {
                        if (!postVisit)
                        {
                            stack.Push((node, true));
                            foreach (var neighbor in node.Neighbors.Reverse<Node>())
                            {
                                if (!neighbor.IsVisited)
                                    stack.Push((neighbor, false));
                            }
                        }
                        else
                        {
                            node.IsVisited = true;
                            visitedNodes.Add(node);
                            visitAction?.Invoke(node);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches for a node in the graph that matches the specified criteria using DFS.
        /// </summary>
        /// <param name="graph">The graph to search.</param>
        /// <param name="startNode">The node from which to start the search.</param>
        /// <param name="searchingCriteria">A predicate function that determines whether a node matches the search criteria.</param>
        /// <param name="order">The order in which to visit nodes (PreOrder or PostOrder).</param>
        /// <param name="approach">The DFS approach to use (Recursive or Iterative).</param>
        /// <returns>
        /// The first node that matches the search criteria, or <c>null</c> if no such node is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/>, <paramref name="startNode"/>, or <paramref name="searchingCriteria"/> is <c>null</c>.
        /// </exception>
        public Node? Search(
            Graph graph,
            Node startNode,
            Func<Node, bool> searchingCriteria,
            DfsOrder order = DfsOrder.PreOrder,
            DfsApproach approach = DfsApproach.Recursive)
        {
            ThrowIfNull(graph);
            ThrowIfNull(startNode);
            ThrowIfNull(searchingCriteria);

            Node? foundNode = null;
            using var cts = new CancellationTokenSource();

            try
            {
                Traverse(graph, startNode, node =>
                {
                    if (searchingCriteria(node))
                    {
                        foundNode = node;
                        cts.Cancel();
                    }
                }, order, approach, cts.Token);
            }
            catch (OperationCanceledException)
            {
                // Search was cancelled because we found the node, this is expected
            }

            return foundNode;
        }

        /// <summary>
        /// Attempts to find a node in the graph that matches the specified criteria using DFS.
        /// </summary>
        /// <param name="graph">The graph to search.</param>
        /// <param name="startNode">The node from which to start the search.</param>
        /// <param name="searchingCriteria">A predicate function that determines whether a node matches the search criteria.</param>
        /// <param name="foundNode">
        /// When this method returns, contains the first node that matches the search criteria, or <c>null</c> if no such node is found.
        /// </param>
        /// <param name="order">The order in which to visit nodes (PreOrder or PostOrder).</param>
        /// <param name="approach">The DFS approach to use (Recursive or Iterative).</param>
        /// <returns>
        /// <c>true</c> if a matching node is found; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="graph"/>, <paramref name="startNode"/>, or <paramref name="searchingCriteria"/> is <c>null</c>.
        /// </exception>
        public bool TrySearch(
            Graph graph,
            Node startNode,
            Func<Node, bool> searchingCriteria,
            out Node? foundNode,
            DfsOrder order = DfsOrder.PreOrder,
            DfsApproach approach = DfsApproach.Recursive)
        {
            foundNode = Search(graph, startNode, searchingCriteria, order, approach);
            return foundNode != null;
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

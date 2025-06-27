using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public class Bfs
    {
        public Graph Graph { get; set; }

        public Bfs(Graph graph)
        {
            this.Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public void Traverse(Node startNode, Action<Node>? visitAction = null, CancellationToken cts = default)
        {
            if (cts.IsCancellationRequested) return;
            if (startNode == null || !Graph.Nodes.Contains(startNode))
                throw new ArgumentException("Start node is null or not part of the graph.");
            Graph.ResetVisited();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(startNode);
            startNode.Visit();
            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();
                visitAction?.Invoke(currentNode);
                if (cts.IsCancellationRequested) return;
                foreach (var neighbor in currentNode.Neighbors)
                {
                    if (!neighbor.IsVisited)
                    {
                        neighbor.Visit();
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public bool TrySearch(Node startNode, Func<Node, bool> predicate, out Node foundNode)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            bool found = false;
            Node targetNode = new Node();
            using (var cts = new CancellationTokenSource()) 
            {
                Traverse(startNode, node =>
                {
                    if (predicate(node))
                    {
                        targetNode = node;
                        found = true;
                        cts.Cancel(); // Signal cancellation
                    }
                }, cts.Token);
            }

            foundNode = targetNode;
            return found;
        }
    }
}

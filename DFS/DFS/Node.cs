using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    public class Node
    {
        public int Value { get; set; }
        public List<Node> Neighbors { get; set; } = new List<Node>();
        public bool IsVisited { get; set; }

        public Node()
        {
        }
        public Node(int value)
        {
            Value = value;
        }

        public void AddNeighbor(Node neighbor)
        {
            if (neighbor == null) throw new ArgumentNullException(nameof(neighbor));
            if (Neighbors.Contains(neighbor)) return;

            Neighbors.Add(neighbor);
        }
    }
}

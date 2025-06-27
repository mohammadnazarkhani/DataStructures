using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public class Node
    {
        public int Value { get; set; }
        public List<Node> Neighbors { get; set; } = new List<Node>();
        public bool IsVisited { get; private set; } = false;

        public Node()
        {
        }
        public Node(int value)
        {
            Value = value;
        }

        public void AddNeighbor(Node neighbor)
        {
            if (neighbor != null && !Neighbors.Contains(neighbor))
            {
                Neighbors.Add(neighbor);
                neighbor.Neighbors.Add(this); // Ensure bidirectional connection
            }
        }

        public void Visit()
        {
            IsVisited = true;
        }

        public void ResetVisit()
        {
            IsVisited = false;
        }
    }
}

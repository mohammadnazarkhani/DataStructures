using DFS;

Graph graph = new Graph();
Node node1 = new Node(2);
Node node3 = new Node(3);
Node node4 = new Node(4);
Node node34 = new Node(34);
Node node44 = new Node(44);
Node node77 = new Node(77);

// Add all nodes to the graph
graph.AddNode(node1);
graph.AddNode(node3);
graph.AddNode(node4);
graph.AddNode(node34);
graph.AddNode(node44);
graph.AddNode(node77);

// Add edges
graph.AddEdge(node1, node3);
graph.AddEdge(node1, node4);
graph.AddEdge(node1, node34);
graph.AddEdge(node34, node44);
graph.AddEdge(node1, node77);

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("=== Traversing the graph ===\n");
Console.ResetColor();

var dfs = new Dfs();
var traversingOrder = dfs.Traverse(graph, node1).ToList();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Traversing order:");
Console.ResetColor();

for (int i = 0; i < traversingOrder.Count; i++)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"[{i}] {traversingOrder[i].Value}\t");
    Console.ResetColor();
}
Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n=== Searching for number 44 ===");
Console.ResetColor();

if (dfs.TrySearch(graph, node1, n => n.Value == 44, out var foundNode))
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nFound node with value: {foundNode?.Value}");
    Console.ResetColor();

    for (int i = 0; i < traversingOrder.Count; i++)
    {
        if (traversingOrder[i].Value == foundNode?.Value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Node with value {foundNode.Value} found at index {i} in the traversing order.");
            Console.ResetColor();
            break;
        }
    }
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("✘ Node with value 44 not found.");
    Console.ResetColor();
}

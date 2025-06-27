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

var dfs = new Dfs();

// --- PreOrder Traversal ---
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("=== PreOrder Traversal ===\n");
Console.ResetColor();
var preOrder = dfs.Traverse(graph, node1, null, DfsOrder.PreOrder);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("PreOrder traversing order:");
Console.ResetColor();
for (int i = 0; i < preOrder.Count; i++)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"[{i}] {preOrder[i].Value}\t");
    Console.ResetColor();
}
Console.WriteLine();

// --- PostOrder Traversal ---
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n=== PostOrder Traversal ===\n");
Console.ResetColor();
var postOrder = dfs.Traverse(graph, node1, null, DfsOrder.PostOrder);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("PostOrder traversing order:");
Console.ResetColor();
for (int i = 0; i < postOrder.Count; i++)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"[{i}] {postOrder[i].Value}\t");
    Console.ResetColor();
}
Console.WriteLine();

// --- PreOrder Search ---
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n=== Searching for number 44 (PreOrder) ===");
Console.ResetColor();
if (dfs.TrySearch(graph, node1, n => n.Value == 44, out var foundNodePre, DfsOrder.PreOrder))
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nFound node with value: {foundNodePre?.Value}");
    Console.ResetColor();
    for (int i = 0; i < preOrder.Count; i++)
    {
        if (preOrder[i].Value == foundNodePre?.Value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Node with value {foundNodePre.Value} found at index {i} in the PreOrder traversing order.");
            Console.ResetColor();
            break;
        }
    }
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("✘ Node with value 44 not found in PreOrder search.");
    Console.ResetColor();
}

// --- PostOrder Search ---
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n=== Searching for number 44 (PostOrder) ===");
Console.ResetColor();
if (dfs.TrySearch(graph, node1, n => n.Value == 44, out var foundNodePost, DfsOrder.PostOrder))
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nFound node with value: {foundNodePost?.Value}");
    Console.ResetColor();
    for (int i = 0; i < postOrder.Count; i++)
    {
        if (postOrder[i].Value == foundNodePost?.Value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Node with value {foundNodePost.Value} found at index {i} in the PostOrder traversing order.");
            Console.ResetColor();
            break;
        }
    }
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("✘ Node with value 44 not found in PostOrder search.");
    Console.ResetColor();
}

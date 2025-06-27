using BFS;

Node node1 = new Node(1);
Node node2 = new Node(2);
Node node3 = new Node(3);
Node node4 = new Node(4);
Node node5 = new Node(5);

List<Node> nodes = new List<Node>()
{
    node1, node2, node3, node4, node5
};

Graph graph = new Graph(nodes);

graph.AddEdge(node1, node2);
graph.AddEdge(node1, node3);
graph.AddEdge(node1, node4);
graph.AddEdge(node2, node5);
graph.AddEdge(node2, node3);
graph.AddEdge(node3, node4);
graph.AddEdge(node4, node5);

Bfs bfs = new Bfs(graph);
bfs.Traverse(node1, node =>
{
    Console.WriteLine($"Visited Node: {node.Value}");
});

Console.WriteLine("\nSearching for value 3 in graph...");
Node foundNode;
if (bfs.TrySearch(node1, node => node.Value == 3, out foundNode))
    Console.WriteLine($"Found node with value: {foundNode.Value}");
else
    Console.WriteLine("Couldn't find any node with value of 3");
using System.Diagnostics;
using BinarySearch;

async Task RunSearchTest<T>(ISearcher<T> searcher, T[] array, T target, string searchType) where T : IComparable<T>
{
    var sw = Stopwatch.StartNew();
    int result = -1;

    switch (searchType)
    {
        case "Sync":
            result = searcher.Search(array, target);
            break;
        case "Async":
            result = await searcher.SearchAsync(array, target);
            break;
    }

    sw.Stop();
    Console.WriteLine($"{searchType,8} search took {sw.ElapsedMilliseconds,4}ms - Found at index: {result}");
}

async Task RunDemo()
{
    var sizes = new[] { 1_000_000, 10_000_000, 50_000_000 };
    var linearSearcher = new LinearSearch<int>();
    var binarySearcher = new BinarySearch<int>();

    foreach (var size in sizes)
    {
        Console.WriteLine($"\nTesting with array size: {size:N0}");
        Console.WriteLine("----------------------------------------");

        var array = Enumerable.Range(0, size).ToArray();
        var target = size - 1; // Search for last element

        Console.WriteLine("Linear Search:");
        await RunSearchTest(linearSearcher, array, target, "Sync");
        await RunSearchTest(linearSearcher, array, target, "Async");

        Console.WriteLine("\nBinary Search:");
        await RunSearchTest(binarySearcher, array, target, "Sync");
        await RunSearchTest(binarySearcher, array, target, "Async");
    }
}

await RunDemo();

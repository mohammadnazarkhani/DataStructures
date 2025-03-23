# Generic Search Algorithms

A .NET library implementing generic search algorithms with both synchronous and asynchronous support.

## Features

- Generic implementation supporting any comparable type
- Binary Search implementation - O(log n)
- Linear Search implementation - O(n)
- Synchronous and Asynchronous versions
- Iterative and Recursive approaches
- Fully documented API
- 100% test coverage

## Installation

Clone the repository and build using .NET 8.0 or later:

```bash
git clone https://github.com/mohammadnazarkhani/DataStructures/GenericSearch.git
cd DataStructures/GenericSearch
dotnet build
```

## Usage

```csharp
// Create a searcher instance
var binarySearcher = new BinarySearch<int>();
var linearSearcher = new LinearSearch<int>();

// Prepare data
int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
int target = 7;

// Synchronous search
int resultSync = binarySearcher.Search(array, target);

// Asynchronous search
int resultAsync = await binarySearcher.SearchAsync(array, target);

// Recursive search
int resultRecursive = binarySearcher.SearchRecursive(array, 0, array.Length - 1, target);
```

## Performance Comparison

| Algorithm     | Time Complexity | Space Complexity | Best For                    |
| ------------- | --------------- | ---------------- | --------------------------- |
| Binary Search | O(log n)        | O(1)             | Sorted arrays               |
| Linear Search | O(n)            | O(1)             | Unsorted arrays, small sets |

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

using System;
using System.Threading.Tasks;

namespace BinarySearch;

/// <summary>
/// Implements linear search algorithm for generic types.
/// Time Complexity: O(n) for all operations.
/// Space Complexity: O(1) for iterative, O(n) for recursive due to call stack.
/// </summary>
/// <typeparam name="T">The type of elements to search. Must implement IComparable{T}.</typeparam>
public class LinearSearch<T> : ISearcher<T> where T : IComparable<T>
{
    /// <summary>
    /// Performs an iterative linear search to find the target element in an array.
    /// </summary>
    /// <param name="arr">The array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>Index of the target if found; otherwise, -1.</returns>
    /// <remarks>Time Complexity: O(n), Space Complexity: O(1)</remarks>
    public int Search(T[] arr, T target)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].CompareTo(target) == 0)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Performs a recursive linear search to find the target element in an array.
    /// </summary>
    /// <param name="arr">The array to search in.</param>
    /// <param name="left">The current index to check.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>Index of the target if found; otherwise, -1.</returns>
    /// <remarks>Time Complexity: O(n), Space Complexity: O(n) due to recursion</remarks>
    public int SearchRecursive(T[] arr, int left, int right, T target)
    {
        if (left > right)
            return -1;

        if (arr[left].CompareTo(target) == 0)
            return left;

        return SearchRecursive(arr, left + 1, right, target);
    }

    /// <summary>
    /// Asynchronously performs a linear search operation.
    /// </summary>
    /// <param name="arr">The array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the index of the target if found; otherwise, -1.</returns>
    /// <remarks>Executes on a background thread using Task.Run</remarks>
    public async Task<int> SearchAsync(T[] arr, T target)
    {
        return await Task.Run(() => Search(arr, target));
    }

    /// <summary>
    /// Asynchronously performs a recursive linear search operation.
    /// </summary>
    /// <param name="arr">The array to search in.</param>
    /// <param name="left">The current index to check.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the index of the target if found; otherwise, -1.</returns>
    /// <remarks>Executes on a background thread using Task.Run</remarks>
    public async Task<int> SearchRecursiveAsync(T[] arr, int left, int right, T target)
    {
        return await Task.Run(() => SearchRecursive(arr, left, right, target));
    }
}

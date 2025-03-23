using System;
using System.Threading.Tasks;
using System.Linq;

namespace BinarySearch;

/// <summary>
/// Implements binary search algorithm for generic types.
/// Time Complexity: O(log n) for all operations.
/// Space Complexity: O(1) for iterative, O(log n) for recursive due to call stack.
/// </summary>
/// <typeparam name="T">The type of elements to search. Must implement IComparable{T}.</typeparam>
public class BinarySearch<T> : ISearcher<T> where T : IComparable<T>
{
    /// <summary>
    /// Performs an iterative binary search to find the target element in a sorted array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>Index of the target if found; otherwise, -1.</returns>
    /// <remarks>Time Complexity: O(log n), Space Complexity: O(1)</remarks>
    public int Search(T[] arr, T target)
    {
        int left = 0, right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = arr[mid].CompareTo(target);

            if (comparison == 0)
                return mid;

            if (comparison < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    /// <summary>
    /// Performs a recursive binary search to find the target element in a sorted array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="left">The left boundary index.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>Index of the target if found; otherwise, -1.</returns>
    /// <remarks>Time Complexity: O(log n), Space Complexity: O(log n) due to recursion</remarks>
    public int SearchRecursive(T[] arr, int left, int right, T target)
    {
        if (left > right)
            return -1;

        int mid = left + (right - left) / 2;
        int comparison = arr[mid].CompareTo(target);

        if (comparison == 0)
            return mid;

        if (comparison < 0)
            return SearchRecursive(arr, mid + 1, right, target);

        return SearchRecursive(arr, left, mid - 1, target);
    }

    /// <summary>
    /// Asynchronously performs a binary search operation.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the index of the target if found; otherwise, -1.</returns>
    /// <remarks>Executes on a background thread using Task.Run</remarks>
    public async Task<int> SearchAsync(T[] arr, T target)
    {
        return await Task.Run(() => Search(arr, target));
    }

    /// <summary>
    /// Asynchronously performs a recursive binary search operation.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="left">The left boundary index.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the index of the target if found; otherwise, -1.</returns>
    /// <remarks>Executes on a background thread using Task.Run</remarks>
    public async Task<int> SearchRecursiveAsync(T[] arr, int left, int right, T target)
    {
        return await Task.Run(() => SearchRecursive(arr, left, right, target));
    }
}

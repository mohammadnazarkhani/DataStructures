using System;
using System.Threading.Tasks;

namespace BinarySearch;

/// <summary>
/// Provides a generic interface for implementing search algorithms.
/// </summary>
/// <typeparam name="T">The type of elements to search. Must implement IComparable{T}.</typeparam>
public interface ISearcher<T> where T : IComparable<T>
{
    /// <summary>
    /// Performs a synchronous search for a target element in an array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>The index of the target element if found; otherwise, -1.</returns>
    int Search(T[] arr, T target);

    /// <summary>
    /// Performs a recursive synchronous search for a target element in an array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="left">The left boundary index.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>The index of the target element if found; otherwise, -1.</returns>
    int SearchRecursive(T[] arr, int left, int right, T target);

    /// <summary>
    /// Performs an asynchronous search for a target element in an array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task representing the asynchronous operation with the found index or -1 if not found.</returns>
    Task<int> SearchAsync(T[] arr, T target);

    /// <summary>
    /// Performs a recursive asynchronous search for a target element in an array.
    /// </summary>
    /// <param name="arr">The sorted array to search in.</param>
    /// <param name="left">The left boundary index.</param>
    /// <param name="right">The right boundary index.</param>
    /// <param name="target">The element to find.</param>
    /// <returns>A task representing the asynchronous operation with the found index or -1 if not found.</returns>
    Task<int> SearchRecursiveAsync(T[] arr, int left, int right, T target);
}

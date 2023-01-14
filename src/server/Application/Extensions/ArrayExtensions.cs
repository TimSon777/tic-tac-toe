// ReSharper disable once CheckNamespace
namespace System;

public static class ArrayExtensions
{
    public static TDest[][] ToJagged<TSrc, TDest>(this TSrc[,] array, Func<TSrc, TDest> mapper)
    {
        return array.Cast<TSrc>()
            .Select((x, i) => (mapper(x), i / array.GetLength(1)))
            .GroupBy(x => x.Item2)
            .Select(x => x.Select(s => s.Item1).ToArray())
            .ToArray();
    }
}
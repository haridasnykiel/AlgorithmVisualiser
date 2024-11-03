namespace AlgorithmVisualiser.Web.DataStructures;

internal sealed class HeapNode<T>
{
    internal HeapNode(T value)
    {
        Value = value;
    }

    internal T Value { get; }
    internal int Priority { get; private set; }

    internal void SetPriority(int priority)
    {
        Priority = priority;
    }
}
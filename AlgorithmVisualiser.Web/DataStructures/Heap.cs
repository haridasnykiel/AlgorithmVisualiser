namespace AlgorithmVisualiser.Web.DataStructures;

public class Heap<T>
{
    private HeapNode<T?>[] _arr;
    private int _length;
    private int _lastNodeIndex;

    public Heap(int length)
    {
        _arr = new HeapNode<T?>[length];
        _length = length;
        _lastNodeIndex = 0;
    }

    public int Length => _lastNodeIndex;
    public int Capacity => _length;

    public bool Insert(T value, int priority = 0)
    {
        var newNode = new HeapNode<T>(value);
        IncreaseArrIfAtCapacity(_lastNodeIndex);

        newNode.SetPriority(priority);

        _arr[_lastNodeIndex] = newNode;

        var parentIdx = GetParentIndex(_lastNodeIndex);

        var idx = _lastNodeIndex;
        while (_arr[parentIdx].Priority < newNode.Priority)
        {
            (_arr[parentIdx], _arr[idx]) = (_arr[idx], _arr[parentIdx]);
            idx = parentIdx;
            parentIdx = GetParentIndex(parentIdx);
        }

        _lastNodeIndex++;

        return true;
    }

    public T? Delete()
    {
        if (_lastNodeIndex == 0) return default;

        var removedItem = _arr[0];
        _arr[0] = _arr[_lastNodeIndex - 1];

        var idx = 0;
        var leftIdx = GetLeftChildIndex(idx);
        var rightIdx = GetRightChildIndex(idx);

        while ((leftIdx < _lastNodeIndex && _arr[idx].Priority < _arr[leftIdx].Priority) ||
               (rightIdx < _lastNodeIndex && _arr[idx].Priority < _arr[rightIdx].Priority))
        {
            if (rightIdx > _lastNodeIndex)
            {
                (_arr[leftIdx], _arr[idx]) = (_arr[idx], _arr[leftIdx]);
                break;
            }

            if (_arr[leftIdx].Priority > _arr[rightIdx].Priority)
            {
                (_arr[leftIdx], _arr[idx]) = (_arr[idx], _arr[leftIdx]);
                idx = leftIdx;
            }
            else
            {
                (_arr[rightIdx], _arr[idx]) = (_arr[idx], _arr[rightIdx]);
                idx = rightIdx;
            }

            leftIdx = GetLeftChildIndex(idx);
            rightIdx = GetRightChildIndex(idx);
        }

        _lastNodeIndex--;
        return removedItem.Value;
    }

    public T? Peek()
    {
        if (_lastNodeIndex < 1)
        {
            return default;
        }

        return _arr[0].Value;
    }

    private void IncreaseArrIfAtCapacity(int newLength)
    {
        if (newLength < _length) return;

        _length += _length;
        var newArr = new HeapNode<T?>[_length];
        Array.Copy(_arr, newArr, _arr.Length);
        _arr = newArr;
    }

    private static int GetParentIndex(int index) => (index - 1) / 2;
    private static int GetLeftChildIndex(int index) => index * 2 + 1;
    private static int GetRightChildIndex(int index) => index * 2 + 2;
}
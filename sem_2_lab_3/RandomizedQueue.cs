using System;
using System.Collections;

namespace Assignment
{
    public class QueueIterator<T> : IIterator<T>
    {
        private IRandomizedQueue<T> _thisQueue;

        public bool HasNext => !_thisQueue.isEmpty();

        // O(1)
        public QueueIterator(IRandomizedQueue<T> queue)
        {
            _thisQueue = queue;
        }

        // O(n)
        public T Next()
        {
            return _thisQueue.Dequeue();
        }
    }


    public class QueueEnumerator<T> : IEnumerator<T>
    {
        private IRandomizedQueue<T> _thisQueue;

        public T Current => _thisQueue.Dequeue();

        object IEnumerator.Current => Current;

        public QueueEnumerator(IRandomizedQueue<T> queue)
        {
            _thisQueue = queue;
        }

        public bool MoveNext()
        {
            return !_thisQueue.isEmpty();
        }

        public void Dispose() { }

        public void Reset() { }
    }


    // array-based randomized queue
    public class ARandomizedQueue<T> : IRandomizedQueue<T>, IIterable<T>
	{
        private T[] _array;
        private int _size;
        private int _elementCount;
        private int _tale;

        private Random _rnd = new();

        public int Count => _elementCount;
        public int Capacity { get => _size; set => Resize(value); }

        // O(1)
        public ARandomizedQueue()
		{
            _size = 4;
            _elementCount = 0;
            _tale = 0;

            _array = new T[_size];
        }

        // O(n)
        public ARandomizedQueue(params T[] values)
        {
            _size = values.Length;
            _array = new T[_size];
            _elementCount = 0;
            _tale = 0;

            foreach (T el in values)
            {
                Enqueue(el);
            }
        }

        // O(n)
        private void Resize(int toSize)
        {
            T[] tmp = new T[toSize];

            for (int i = 0; i < _elementCount; i++)
            {
                tmp[i] = _array[i];
            }

            _size = toSize;
            _array = tmp;
        }

        // O(1)
        public bool isEmpty()
        {
            return _elementCount == 0;
        }

        // O(1) / O(n)
        public void Enqueue(T item)
        {
            if (_elementCount == _size)
            {
                Resize(_size * 2);
            }

            _array[_tale] = item;
            _elementCount++;
            _tale++;
        }

        // O(n)
        public T Dequeue()
        {
            if (!isEmpty())
            {
                int index = _rnd.Next(0, _elementCount);
                T val = _array[index];

                for (int i = index; i < _elementCount - 1; i++)
                {
                    _array[i] = _array[i + 1];
                }
                _tale--;
                _elementCount--;

                return val;
            }
            else
            {
                throw new Exception("Queue is empty");
            }
        }

        // O(1)
        public T Peek()
        {
            return _array[_rnd.Next(0, _elementCount)];
        }

        // O(n)
        public object Clone()
        {
            ARandomizedQueue<T> queue = new();
            queue.Capacity = _size;

            _array.CopyTo(queue._array, 0);
            queue._tale = _tale;
            queue._elementCount = _elementCount;

            return queue;
        }

        // O(1)
        public IIterator<T> GetIterator()
        {
            return new QueueIterator<T>(this);
        }

        // O(1)
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(this);
        }

        // O(1)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

	// linked list-based randomized queue
	public class LRandomizedQueue<T>
	{
		public LRandomizedQueue()
		{

		}
	}
}
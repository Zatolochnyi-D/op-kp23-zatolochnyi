using System;
using System.Collections;

namespace Assignment
{
    // ARRAY-BASED DEQUE PART

    public class DequeIterator<T> : IIterator<T>
    {
        private IDeque<T> _thisDeque;

        public bool HasNext => !_thisDeque.isEmpty();

        // O(1)
        public DequeIterator(IDeque<T> deque)
        {
            _thisDeque = deque;
        }

        // O(1)
        public T Next()
        {
            if (HasNext)
            {
                return _thisDeque.RemoveFirst();
            }
            throw new Exception("Deque is empty");
        }
    }


    public class DequeEnumerator<T> : IEnumerator<T>
    {
        private IDeque<T> _thisDeque;

        // O(1)
        public DequeEnumerator(IDeque<T> deque)
        {
            _thisDeque = deque;
        }

        // O(1)
        public T Current => _thisDeque.RemoveLast();

        // O(1)
        object IEnumerator.Current => Current;

        public void Dispose() {}

        // O(1)
        public bool MoveNext()
        {
            return !_thisDeque.isEmpty();
        }

        public void Reset() {}
    }


    // array-based deque
    public class ADeque<T> : IDeque<T>, IIterable<T>
    {
        private T[] _array;
        private int _size;
        private int _elementCount;

        private int _head;
        private int _tale;

        public int Count => _elementCount;
        public int Capacity { get => _size; set => Resize(value); }

        // O(1)
        private Func<int, int, int> mod = (int x, int y) =>
        {
            int r = x % y;
            return r < 0 ? r + y : r;
        };

        // O(1)
        public ADeque()
        {
            _size = 4;
            _elementCount = 0;
            _head = 0;
            _tale = 0;

            _array = new T[_size];
        }

        // O(n)
        public ADeque(params T[] array)
        {
            _size = array.Length;
            _array = new T[_size];
            _elementCount = 0;
            _head = 0;
            _tale = 0;

            foreach (T el in array)
            {
                AddLast(el);
            }
        }

        // O(n)
        private void Resize(int toSize)
        {
            T[] tmp = new T[toSize];

            for (int i = 0; i < _elementCount; i++)
            {
                tmp[i] = _array[(_head + i) % _size];
            }

            _head = 0;
            _tale = _elementCount - 1;

            _size = toSize;
            _array = tmp;
        }

        // O(1)
        public bool isEmpty()
        {
            return _elementCount == 0;
        }

        // O(1) / O(n)
        public void AddFirst(T item)
        {
            if (_elementCount == _size)
            {
                Resize(_size * 2);
            }

            if (_elementCount == 0)
            {
                _array[_head] = item;
                _elementCount++;
            }
            else
            {
                _head = mod(_head - 1, _size);

                _array[_head] = item;
                _elementCount++;
            }
        }

        // O(1)
        public T PeekFirst()
        {
            if (!isEmpty())
            {
                return _array[_head];
            }
            else throw new Exception("Deque is empty");
        }

        // O(1)
        public T RemoveFirst()
        {
            if (!isEmpty())
            {
                _head = mod(_head + 1, _size);
                _elementCount--;
                return _array[mod(_head - 1, _size)];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }

        // O(1) / O(n)
        public void AddLast(T item)
        {
            if (_elementCount == _size)
            {
                Resize(_size * 2);
            }

            if (_elementCount == 0)
            {
                _array[_tale] = item;
                _elementCount++;
            }
            else
            {
                _tale = mod(_tale + 1, _size);

                _array[_tale] = item;
                _elementCount++;
            }
        }

        // O(1)
        public T PeekLast()
        {
            if (!isEmpty())
            {
                return _array[_tale];
            }
            else throw new Exception("Deque is empty");
        }

        // O(1)
        public T RemoveLast()
        {
            if (!isEmpty())
            {
                _tale = mod(_tale - 1, _size);
                _elementCount--;
                return _array[mod(_tale + 1, _size)];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }

        // O(n)
        public object Clone()
        {
            ADeque<T> deq = new();
            deq.Capacity = _size;
            _array.CopyTo(deq._array, 0);

            deq._size = _size;
            deq._elementCount = _elementCount;
            deq._head = _head;
            deq._tale = _tale;

            return deq;
        }

        // O(1)
        public IIterator<T> GetIterator()
        {
            return new DequeIterator<T>(this);
        }

        // O(1)
        public IEnumerator<T> GetEnumerator()
        {
            return new DequeEnumerator<T>(this);
        }

        // O(1)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    // LINKED LIST-BASED DEQUE PART

    // linked list-based deque
    public class LDeque<T> : IDeque<T>, IIterable<T>
    {
        private SLList<T> _list;

        // O(1)
        public LDeque()
        {
            _list = new();
        }

        // O(n)
        public LDeque(params T[] array)
        {
            _list = new(array);
        }

        public int Count => _list.Count;

        // O(1)
        public bool isEmpty()
        {
            return Count == 0;
        }

        // O(1)
        public void AddFirst(T item)
        {
            _list.Insert(0, item);
        }

        // O(1)
        public T PeekFirst()
        {
            return _list[0];
        }

        // O(1)
        public T RemoveFirst()
        {
            T value = _list[0];
            _list.RemoveAt(0);
            return value;
        }

        // O(1)
        public void AddLast(T item)
        {
            _list.Add(item);
        }

        // O(1)
        public T PeekLast()
        {
            return _list[Count - 1];
        }

        // O(n)
        public T RemoveLast()
        {
            T value = PeekLast();
            _list.RemoveAt(Count - 1);
            return value;
        }

        // O(n)
        public object Clone()
        {
            LDeque<T> deq = new();

            deq._list = (SLList<T>)_list.Clone();

            return deq;
        }

        // O(1)
        public IIterator<T> GetIterator()
        {
            return new DequeIterator<T>(this);
        }

        // O(1)
        public IEnumerator<T> GetEnumerator()
        {
            return new DequeEnumerator<T>(this);
        }

        // O(1)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System;

namespace Assignment
{
    // array-based deque
    public class ADeque<T> : IDeque<T>
    {
        private T[] _array;
        private int _sizeOfArray;
        private int _amountOfElements;

        private int _head;
        private int _tale;

        public int Count => _amountOfElements;

        public int Capacity { get => _sizeOfArray; set => Resize(value); }

        public ADeque()
        {
            _sizeOfArray = 4;
            _amountOfElements = 0;
            _head = 0;
            _tale = 0;

            _array = new T[_sizeOfArray];
        }

        public ADeque(params T[] array)
        {
            _sizeOfArray = array.Length;
            _array = new T[_sizeOfArray];
            _amountOfElements = 0;
            _head = 0;
            _tale = 0;

            foreach (T el in array)
            {
                AddLast(el);
            }
        }

        public ADeque(ADeque<T> deque)
        {
            _sizeOfArray = deque.Count;
            _array = new T[_sizeOfArray];
            _amountOfElements = 0;
            _head = 0;
            _tale = 0;

            for (int i = 0; i < _sizeOfArray; i++)
            {
                AddLast(deque.RemoveFirst());
            }
        }

        private void Resize(int toSize)
        {
            T[] tmp = new T[toSize];

            for (int i = 0; i < _amountOfElements; i++)
            {
                tmp[i] = _array[(_head + i) % _sizeOfArray];
            }

            _head = 0;
            _tale = _sizeOfArray - 1;

            _sizeOfArray = toSize;
        }

        public bool isEmpty()
        {
            return _amountOfElements == 0 ? true : false;
        }

        public void AddFirst(T item)
        {
            _head = (_head - 1) % _sizeOfArray;

            if (_head == _tale)
            {
                Resize(_sizeOfArray * 2);
                _head = _sizeOfArray - 1;
            }

            _array[_head] = item;
            _amountOfElements++;
        }

        public T PeekFirst(T item)
        {
            if (!isEmpty())
            {
                return _array[_head];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }

        public T RemoveFirst()
        {
            if (!isEmpty())
            {
                _head = (_head + 1) % _sizeOfArray;
                _amountOfElements--;
                return _array[(_head - 1) % _sizeOfArray];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }

        public void AddLast(T item)
        {
            _tale = (_tale + 1) % _sizeOfArray;

            if (_head == _tale)
            {
                Resize(_sizeOfArray * 2);
                _tale++;
            }

            _array[_tale] = item;
            _amountOfElements++;
        }

        public T PeekLast(T item)
        {
            if (!isEmpty())
            {
                return _array[_tale];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }

        public T RemoveLast()
        {
            if (!isEmpty())
            {
                _tale = (_tale - 1) % _sizeOfArray;
                _amountOfElements--;
                return _array[(_tale + 1) % _sizeOfArray];
            }
            else
            {
                throw new Exception("Deque is empty");
            }
        }
    }

    // linked list-based deque
    public class LDeque
    {
        public LDeque()
        {

        }
    }
}
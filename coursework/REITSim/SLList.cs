using System;
using System.Collections;

namespace CustomCollections
{
    // single-linked node
    public class SLNode<T>
    {
        public T data;
        public SLNode<T>? next;

        public SLNode(T item, SLNode<T>? nextNode = null)
        {
            data = item;
            next = nextNode;
        }
    }


    // single-linked list enumerator
    public class SLListEnumerator<T> : IEnumerator<T>
    {
        private SLNode<T>? current;

        // O(1)
        public SLListEnumerator(SLList<T> list)
        {
            current = new(default, list.Head);
        }

        public T Current => current.data;

        object IEnumerator.Current => Current;

        // O(1)
        public bool MoveNext()
        {
            if (current.next != null)
            {
                current = current.next;
                return true;
            }
            else return false;
        }

        public void Reset() { }

        public void Dispose() { }
    }


    // single-linked list
    public class SLList<T> : IList<T>, ICloneable
    {
        private SLNode<T>? _head;
        private SLNode<T>? _tale;
        private int _size;

        internal SLNode<T>? Head => _head;

        public int Count => _size;

        public bool IsReadOnly => false;

        // O(1) / O(n)
        public T this[int index] { get => GetAt(index); set => SetAt(index, value); }

        // O(1)
        public SLList()
        {
            _head = null;
            _tale = null;
            _size = 0;
        }

        // O(n)
        public SLList(params T[] values)
        {
            _head = null;
            _tale = null;
            _size = 0;

            foreach (T val in values)
            {
                Add(val);
            }
        }

        // O(1) / O(n)
        private T GetAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException();
            }
            else if (index == 0)
            {
                return _head.data;
            }
            else if (index == _size - 1)
            {
                return _tale.data;
            }
            else
            {
                SLNode<T> node = _head;
                for (int i = 0; i < index; i++)
                {
                    node = node.next;
                }

                return node.data;
            }
        }

        // O(1) / O(n)
        private void SetAt(int index, T item)
        {
            if (index < 0 || index > _size)
            {
                throw new IndexOutOfRangeException();
            }
            else if (index == 0)
            {
                _head.data = item;
            }
            else if (index == _size - 1)
            {
                _tale.data = item;
            }
            else if (index == _size)
            {
                Add(item);
            }
            else
            {
                SLNode<T> node = _head;
                for (int i = 0; i < index; i++)
                {
                    node = node.next;
                }

                node.data = item;
            }
        }

        // O(1)
        public void Add(T item)
        {
            switch (_tale)
            {
                case null:
                    _head = new(item);
                    _tale = _head;
                    _size++;
                    break;

                default:
                    _tale.next = new(item);
                    _tale = _tale.next;
                    _size++;
                    break;
            }
        }

        // O(1) / O(n)
        public void Insert(int index, T item)
        {
            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 0)
            {
                _head = new(item, _head);
                _size++;
            }
            else if (index == _size)
            {
                Add(item);
            }
            else
            {
                SLNode<T> node = _head;
                for (int i = 0; i < index - 1; i++)
                {
                    node = node.next;
                }

                node.next = new(item, node.next);
                _size++;
            }
        }

        // O(n)
        public int IndexOf(T item)
        {
            if (_head == null)
            {
                return -1;
            }

            SLNode<T> node = _head;
            for (int i = 0; i < _size; i++)
            {
                if (node.data.Equals(item))
                {
                    return i;
                }
                else node = node.next;
            }

            return -1;
        }

        // O(n)
        public bool Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1)
            {
                return false;
            }
            else
            {
                RemoveAt(i);
                return true;
            }
        }

        // O(1) / O(n)
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 0)
            {
                _head = _head.next;
                _size--;
            }
            else
            {
                SLNode<T> node = _head;
                for (int i = 0; i < index - 1; i++)
                {
                    node = node.next;
                }

                node.next = node.next.next;
                _tale = node;
                _size--;
            }
        }

        // O(1)
        public void Clear()
        {
            _head = null;
            _tale = null;
            _size = 0;
        }

        // O(n)
        public bool Contains(T item)
        {
            return IndexOf(item) == -1 ? false : true;
        }

        // O(n)
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_size == 0 || _size < arrayIndex)
            {
                throw new ArgumentException("ArrayIndex bigger than size of list");
            }
            else if (_size - arrayIndex > array.Length)
            {
                throw new ArgumentException("Destination array is too small");
            }

            SLNode<T> node = _head;
            for (int i = 0; i < arrayIndex; i++)
            {
                node = node.next;
            }

            for (int i = 0; i < _size - arrayIndex; i++)
            {
                array[i] = node.data;
                node = node.next;
            }
        }

        // O(n)
        public object Clone()
        {
            T[] array = new T[Count];
            CopyTo(array, 0);

            return new SLList<T>(array);

            // O(n^2)
            //for (int i = 0; i < Count; i++)
            //{
            //    list[i] = this[i];
            //}
        }

        // O(1)
        public IEnumerator<T> GetEnumerator()
        {
            return new SLListEnumerator<T>(this);
        }

        // O(1)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
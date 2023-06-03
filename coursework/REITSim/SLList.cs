using System;
using System.Collections;

namespace CustomCollections
{
    interface ILinkedList<T> : ICloneable, IEnumerable<T>
    {
        int Count { get; }

        void Add(T item);

        void Remove(T item);

        void RemoveAt(int index);

        int IndexOf(T item);

        bool Contains(T item);

        void Clear();

        void CopyTo(T[] array, int arrayIndex);
    }


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


    public class SLListEnumerator<T>: IEnumerator<T>
    {
        private SLNode<T> current;

        public SLListEnumerator(SLNode<T>? head)
        {
            current = new(default, head);
        }

        public T Current => current.data;

        object IEnumerator.Current => Current;

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


    public class SLList<T> : ILinkedList<T>
    {
        protected SLNode<T>? _head;
        protected SLNode<T>? _tale;
        protected int _size;

        public int Count => _size;

        public T this[int index] { get => GetAt(index); set => SetAt(index, value); }

        public SLList()
        {
            _head = null;
            _tale = null;
            _size = 0;
        }

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

        public SLList(SLList<T> list)
        {
            _head = null;
            _tale = null;
            _size = 0;

            foreach (T el in list)
            {
                Add(el);
            }
        }

        protected T GetAt(int index)
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

        protected void SetAt(int index, T item)
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

        public void Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1)
            {
                return;
            }
            else
            {
                RemoveAt(i);
            }
        }

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

        public void Clear()
        {
            _head = null;
            _tale = null;
            _size = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) == -1 ? false : true;
        }

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

        public object Clone()
        {
            T[] array = new T[Count];
            CopyTo(array, 0);

            return new SLList<T>(array);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SLListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class SortedSLList<T> : ILinkedList<T>
    {
        protected SLNode<T>? _head;
        protected SLNode<T>? _tale;
        protected int _size;

        // if true, put item on this place, else go ahead.
        protected Func<T, T, bool> _compareFunc;

        public T this[int index] { get => GetAt(index); }

        public int Count => _size;

        public SortedSLList(Func<T, T, bool> func)
        {
            _head = null;
            _tale = null;
            _size = 0;

            _compareFunc = func;
        }

        public SortedSLList(Func<T, T, bool> func, params T[] values)
        {
            _head = null;
            _tale = null;
            _size = 0;

            _compareFunc = func;

            foreach (T el in values)
            {
                Add(el);
            }
        }

        protected T GetAt(int index)
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

        public void Add(T item)
        {
            if (_head == null) // _size == 0
            {
                _head = new(item, null);
                _tale = _head;
                _size++;
            }
            else if (_head.next == null) // _size == 1
            {
                if (_compareFunc(_head.data, item))
                {
                    _head = new(item, _head);
                    _tale = _head.next;
                }
                else
                {
                    _head.next = new(item, null);
                    _tale = _head.next;
                }
                _size++;
            }
            else
            {
                SLNode<T> prev = _head;
                SLNode<T> cur = _head.next;

                if (_compareFunc(prev.data, item))
                {
                    _head = new(item, _head);
                }
                else
                {
                    while (true)
                    {
                        if (_compareFunc(cur.data, item))
                        {
                            prev.next = new(item, cur);
                            return;
                        }

                        if (cur.next == null)
                        {
                            break;
                        }

                        prev = cur;
                        cur = cur.next;
                    }

                    _tale.next = new(item, null);

                    _tale = _tale.next;
                }

                _size++;
            }
        }

        public void Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1)
            {
                return;
            }
            else
            {
                RemoveAt(i);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException("RemoveAt", "Index out of range");
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

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void Clear()
        {
            _head = null;
            _tale = null;
            _size = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_size == 0 || _size < arrayIndex)
            {
                throw new ArgumentException("ArrayIndex bigger than size of the list");
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

        public object Clone()
        {
            T[] array = new T[Count];
            CopyTo(array, 0);

            return new SortedSLList<T>(_compareFunc, array);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SLListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


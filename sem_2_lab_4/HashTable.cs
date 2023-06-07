using System;
using CustomCollections;

namespace Assignment
{
	interface IHashTable<TKey, TValue>
	{
        int Capacity { get; }

		int Count { get; }

		TKey[] Keys { get; }

		void Add(TKey key, TValue value);

        void Set(TKey key, TValue value);

		void Remove(TKey key);

		TValue Get(TKey key);

		bool ContainsKey(TKey key);

		bool ContainsValue(TValue value);

		void Clear();
	}


    public class HashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        protected int _capacity;
        protected double _loadFactor;

        protected SLList<TKey> _keys;
        protected DataCell<TKey, TValue>[] _values;

        public int Count => _keys.Count;

        public int Capacity { get { return _capacity; } set { Resize(value); } }
        public TValue this[TKey key] { get { return Get(key); } set { Set(key, value); } }

        public TKey[] Keys => _keys.ToArray();

        public HashTable()
        {
            _capacity = 997;
            _loadFactor = 0.0;
            _keys = new();
            _values = new DataCell<TKey, TValue>[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                _values[i] = new();
            }
        }

        public HashTable(int initialCapacity)
        {
            _capacity = Additions.NearestPrime(initialCapacity);
            _loadFactor = 0.0;
            _keys = new();
            _values = new DataCell<TKey, TValue>[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                _values[i] = new();
            }
        }

        // double hashing
        protected int Hash(TKey key, int i)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Bad key", "Key cannot be null");
            }

            int hash = key.GetHashCode();
            int index = Additions.Mod(Additions.Mod(hash, _capacity) + (i * Additions.Mod(hash, _capacity - 1) + 1), _capacity);

            return index;
        }

        protected void TryResize()
        {
            _loadFactor = _keys.Count / _capacity;

            if (_loadFactor > 0.9)
            {
                Resize(_capacity * 2);
            }
        }

        public void Resize(int newSize)
        {
            if (newSize < 0)
            {
                return;
            }

            HashTable<TKey, TValue> ht = new(newSize);

            foreach (TKey key in _keys)
            {
                ht.Add(key, Get(key));
            }

            _capacity = ht._capacity;
            _loadFactor = ht._loadFactor;
            _keys = ht._keys;
            _values = ht._values;
        }

        public void Add(TKey key, TValue value)
        {
            TryResize();

            for (int i = 0; true; i++)
            {
                int index = Hash(key, i);

                if (_values[index].IsEmpty)
                {
                    _values[index].Insert(key, value);
                    _keys.Add(key);
                    break;
                }
            }
        }

        public void Set(TKey key, TValue value)
        {
            for (int i = 0; true; i++)
            {
                int index = Hash(key, i);

                if (!_values[index].IsEmpty)
                {
                    if (key.Equals(_values[index].Key))
                    {
                        _values[index].Set(value);
                        break;
                    }
                }
                else
                {
                    if (_values[index].WasDeleted)
                    {
                        continue;
                    }
                    else
                    {
                        Add(key, value);
                        break;
                    }
                }
            }
        }

        public TValue Get(TKey key)
        {
            for (int i = 0; true; i++)
            {
                int index = Hash(key, i);

                if (!_values[index].IsEmpty)
                {
                    if (key.Equals(_values[index].Key))
                    {
                        return _values[index].Data;
                    }
                }
                else
                {
                    if (_values[index].WasDeleted)
                    {
                        continue;
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Key \"{key}\" not present in Hash table");
                    }
                }
            }
        }

        public void Remove(TKey key)
        {
            for (int i = 0; true; i++)
            {
                int index = Hash(key, i);

                if (!_values[index].IsEmpty)
                {
                    if (key.Equals(_values[index].Key))
                    {
                        _values[index].Delete();
                        _keys.Remove(key);
                        break;
                    }
                }
                else
                {
                    if (_values[index].WasDeleted)
                    {
                        continue;
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Key \"{key}\" not present in Hash table");
                    }
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _keys.Contains(key);
        }

        public bool ContainsValue(TValue value)
        {
            foreach (TKey key in _keys)
            {
                if (Get(key).Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _loadFactor = 0.0;
            _values = new DataCell<TKey, TValue>[_capacity];
            _keys = new();
        }
    }
}
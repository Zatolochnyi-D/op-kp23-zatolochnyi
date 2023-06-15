using System;

namespace Assignment
{
	public class DataCell<TKey, TValue>
	{
		protected TKey? _key;
		protected TValue? _data;
		protected bool _isEmpty;
		protected bool _wasDeleted;

		public TKey? Key => _key;
		public TValue? Data => _data;
		public bool IsEmpty => _isEmpty;
		public bool WasDeleted => _wasDeleted;

		public DataCell()
		{
			_key = default;
			_data = default;
			_isEmpty = true;
			_wasDeleted = false;
		}
		
		public DataCell(TKey key, TValue item)
		{
			_key = key;
			_data = item;
			_isEmpty = false;
			_wasDeleted = false;
		}

		public void Insert(TKey key, TValue? item)
		{
			_key = key;
			_data = item;
			_isEmpty = false;
		}

		public void Set(TValue item)
		{
			_data = item;
		}

		public void Delete()
		{
			_key = default;
			_data = default;
			_isEmpty = true;
			_wasDeleted = true;
		}
	}
}


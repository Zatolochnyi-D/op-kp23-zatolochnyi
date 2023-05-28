using System;

namespace Assignment
{
	interface IHashTable<Ktype, Vtype>
	{
		int Count { get; }

		Ktype[] Keys { get; }

		void Add(Ktype key, Vtype value);

		void Remove(Ktype key);

		Vtype Get(Ktype key);

		bool ContainsKey(Ktype key);

		bool ContainsValue(Vtype value);

		void Clear();
	}
}
using System;

namespace Assignment
{
	interface IDeque<T>
	{
		int Count { get; }

		int Capacity { get; set; }

		bool isEmpty();

		void AddFirst(T item);

        T PeekFirst(T item);

        T RemoveFirst();

		void AddLast(T item);

		T PeekLast(T item);

        T RemoveLast();
    }

    interface IIterator<T>
	{
		bool HasNext { get; }
		T Next();
	}
}
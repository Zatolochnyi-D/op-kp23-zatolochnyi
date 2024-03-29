﻿using System;

namespace Assignment
{
	public interface IDeque<T> : ICloneable, IEnumerable<T>
    {
		int Count { get; }

		bool isEmpty();

		void AddFirst(T item);

        T PeekFirst();

        T RemoveFirst();

		void AddLast(T item);

		T PeekLast();

        T RemoveLast();
    }

	public interface IRandomizedQueue<T> : ICloneable, IEnumerable<T>
	{
        int Count { get; }

        bool isEmpty();

        void Enqueue(T item);

        T Dequeue();

        T Peek();
    }

    public interface IIterator<T>
	{
		bool HasNext { get; }
		T Next();
	}

	public interface IIterable<T>
	{
		IIterator<T> GetIterator();
	}
}
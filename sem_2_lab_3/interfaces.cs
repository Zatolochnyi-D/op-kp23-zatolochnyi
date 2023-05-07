using System;

namespace Assignment
{
	interface IIterator<T>
	{
		bool HasNext { get; }
		T Next();
	}
}
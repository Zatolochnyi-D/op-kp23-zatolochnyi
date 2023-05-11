using System;
using System.Collections;

namespace Assignment
{
	class Program
	{
		static void Main(string[] args)
		{
            ADequeFullTest();
        }

		static void ADequeFullTest()
		{
			Console.WriteLine("Array-based deque test.");
			ADeque<int> deque = new();
            Console.WriteLine("Deque created");
            Console.WriteLine($"Deque is empty: {deque.isEmpty()}");
            Console.WriteLine("");

            Console.WriteLine("Adding elements to the deque in the end: 4, 8, 2, 7, 3");
			deque.AddLast(4);
            deque.AddLast(8);
            deque.AddLast(2);
            deque.AddLast(7);
            deque.AddLast(3);
            Console.WriteLine("");

            Console.WriteLine("Adding elements to the deque in the beginning: 3, 7, 9, 1");
			deque.AddFirst(3);
			deque.AddFirst(7);
			deque.AddFirst(9);
			deque.AddFirst(1);
            Console.WriteLine("");

            Console.WriteLine("Expected deque: 1, 9, 7, 3, 4, 8, 2, 7, 3");
            Console.WriteLine($"Deque is empty: {deque.isEmpty()}");
            Console.WriteLine("Peek first and last elements:");
            Console.WriteLine(deque.PeekFirst());
            Console.WriteLine(deque.PeekLast());
            Console.WriteLine("");

            Console.WriteLine("Create clone to use iterator");
			ADeque<int> deque2 = (ADeque<int>)deque.Clone();
            Console.WriteLine("");

            Console.WriteLine("Iterate clone of deque with RemoveFirst():");
            IIterator<int> iterator = deque2.GetIterator();
            while (iterator.HasNext)
            {
                Console.Write(iterator.Next() + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("Iterate original deque with enumerator with RemoveLast():");
            foreach(int n in deque)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Now both deques are empty:");
            Console.WriteLine(deque.isEmpty());
            Console.WriteLine(deque2.isEmpty());
        }
	}
}
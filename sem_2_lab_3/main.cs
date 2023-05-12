using System;
using System.Collections;

namespace Assignment
{
	class Program
	{
		static void Main(string[] args)
		{
            //SLListFullTest();
            //ADequeFullTest();
            //LDequeFullTest();
            ARandimizedQequeFullTest();
        }

        static void SLListFullTest()
        {
            Console.WriteLine("Single-linked list test.");
            Console.WriteLine("");

            Console.WriteLine("Creating two lists...");
            SLList<string> list1 = new();
            SLList<string> list2 = new(new string[] { "word1", "word2", "word3", "word4" });
            Console.WriteLine("Lists are created.");
            Console.WriteLine("");

            Console.WriteLine("Adding elements to the first list: word5, word6, word7");
            list1.Add("word5");
            list1.Add("word6");
            list1.Add("word7");
            Console.WriteLine("");

            Console.WriteLine("Accessing second element in each list:");
            Console.WriteLine(list1[1]);
            Console.WriteLine(list2[1]);
            Console.WriteLine("");

            Console.WriteLine("Traverse both lists:");
            foreach (string s in list1)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
            foreach (string s in list2)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Replace first element in list2 with word0:");
            list2[0] = "word0";
            Console.WriteLine(list2[0]);
            Console.WriteLine("");

            Console.WriteLine("Insert word1 in list2 at position 1. list2:");
            list2.Insert(1, "word1");
            foreach (string s in list2)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Index of word6 and word9:" + list1.IndexOf("word6") + list1.IndexOf("word9"));
            Console.WriteLine("");

            Console.WriteLine("Contains list1 word1? " + list1.Contains("word1"));
            Console.WriteLine("");

            Console.WriteLine("Remove word1 from both lists");
            Console.WriteLine("list1: " + list1.Remove("word1"));
            Console.WriteLine("list2: " + list2.Remove("word1"));
            Console.WriteLine("");

            Console.WriteLine("Remove from both lists from 0 position");
            list1.RemoveAt(0);
            list2.RemoveAt(0);
            Console.WriteLine("");

            Console.WriteLine("Expected lists:");
            Console.WriteLine("list1: word6 word7");
            Console.WriteLine("list2: word2 word3 word4");
            Console.WriteLine("Got:");
            foreach (string s in list1)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
            foreach (string s in list2)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("SLList works correctly!");
            Console.WriteLine("");
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

        static void LDequeFullTest()
        {
            Console.WriteLine("Linked list-based deque test.");
            LDeque<int> deque = new();
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
            LDeque<int> deque2 = (LDeque<int>)deque.Clone();
            Console.WriteLine("");

            Console.WriteLine("Iterate clone of deque with RemoveFirst():");
            IIterator<int> iterator = deque2.GetIterator();
            while (iterator.HasNext)
            {
                Console.Write(iterator.Next() + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("Iterate original deque with enumerator with RemoveLast():");
            foreach (int n in deque)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Now both deques are empty:");
            Console.WriteLine(deque.isEmpty());
            Console.WriteLine(deque2.isEmpty());
        }

        static void ARandimizedQequeFullTest()
        {
            Console.WriteLine("Array-based randomized queue test.");
            ARandomizedQueue<int> queue = new();
            Console.WriteLine("Queue created");
            Console.WriteLine($"Queue is empty: {queue.isEmpty()}");
            Console.WriteLine("");

            Console.WriteLine("Adding elements to the queue: 8, 2, 5, 1, 5, 8, 3, 5");
            queue.Enqueue(8);
            queue.Enqueue(2);
            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(5);
            queue.Enqueue(8);
            queue.Enqueue(3);
            queue.Enqueue(5);
            Console.WriteLine("");

            Console.WriteLine($"Deque is empty: {queue.isEmpty()}");
            Console.WriteLine("Peek two elements:");
            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Peek());
            Console.WriteLine("");

            Console.WriteLine("Create clone to use iterator");
            ARandomizedQueue<int> queue2 = (ARandomizedQueue<int>)queue.Clone();
            Console.WriteLine("");

            Console.WriteLine("Iterate clone of queue:");
            IIterator<int> iterator = queue2.GetIterator();
            while (iterator.HasNext)
            {
                Console.Write(iterator.Next() + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("Iterate original queue with enumerator:");
            foreach (int n in queue)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Now both queues are empty:");
            Console.WriteLine(queue.isEmpty());
            Console.WriteLine(queue2.isEmpty());
        }
    }
}
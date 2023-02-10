using System;
using System.IO;

namespace HW
{
    static class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[15];
            Random rand = new();

            for (int i = 0; i < 15; i++)
            {
                array[i] = rand.Next(100);
            }
            Print(array);
            File.WriteAllText("unsorted.txt", string.Join(" ", array));

            Sort(array);
            Print(array);
            File.WriteAllText("sorted.txt", string.Join(" ", array));
        }

        //insertion sort
        static void Sort(int[] arr)
        {
            int key, j;
            for (int i = 1; i < arr.Length; i++)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j -= 1;
                }
                arr[j + 1] = key;
            }
        }

        static void Print(int[] arr)
        {
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
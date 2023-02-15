using System;
using System.IO;

namespace OP
{
    static class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../";

            Console.WriteLine("Task1 demo:\n");
            Task1(path + "Task1Text.txt");
            Console.WriteLine("\nEnd of Task1 demo.");

            Console.WriteLine("Task2 demo:\n");
            Task2(path + "Task2Text.txt");
            Console.WriteLine("\nEnd of Task2 demo.");

            Console.WriteLine("Task3 demo:\n");
            Task3(path + "num.txt");
            Console.WriteLine("\nEnd of Task3 demo.");

            Console.WriteLine("Task4 demo:\n");
            Task4(path + "Task4Text.txt");
            Console.WriteLine("\nEnd of Task4 demo.");

            Console.WriteLine("Task5 demo:\n");
            Task5(path);
            Console.WriteLine("\nEnd of Task5 demo.");
        }

        static void Task1(string path)
        {
            using (StreamReader sr = new(path))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
        }

        static void Task2(string path)
        {
            using (StreamWriter sw = new(path, false))
            {
                string text = "this string will be written to some file with 3 words per line.";
                string[] words = text.Split(' ');

                for (int i = 0; i < (int)Math.Ceiling((double)(words.Length / 3.0)); i++)
                {
                    sw.WriteLine(string.Join(" ", words.Skip(3 * i).Take(3)));
                }
            }
        }

        static void Task3(string path)
        {
            using (StreamWriter sw = new(path, false))
            {
                int[] nums = new int[1000];

                for (int i = 1; i <= 1000; i++)
                {
                    nums[i - 1] = i;
                }

                sw.Write(string.Join(',', nums));
            }
        }

        static void Task4(string path)
        {
            using (StreamWriter sw = new(path, false))
            {
                string[] colors = new string[] { "red", "green", "blue", "black", "white" };

                foreach (string c in colors)
                {
                    sw.WriteLine(c);
                }
            }
        }

        static void Task5(string path)
        {
            using (StreamWriter sw = new(path + "1.txt", false))
            {
                Random rnd = new();

                for (int i = 0; i < 20; i++)
                {
                    sw.WriteLine(rnd.Next(8));
                }
            }

            using (StreamReader sr = new(path + "1.txt"))
            {
                int[] x = new int[20];

                for (int i = 0; i < 20; i++)
                {
                    x[i] = int.Parse(sr.ReadLine());
                }

                using (StreamWriter sw = new(path + "2.txt", false))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        sw.WriteLine($"(x = {x[i]},y = {Math.Sin(x[i])/(x[i] * x[i] - 1)})");
                    }
                }
            }
        }
    }
}
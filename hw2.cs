using System;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1, y1, x2, y2, res;

            Console.WriteLine("Enter x1:");
            x1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter x2:");
            x2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter y1:");
            y1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter y2:");
            y2 = Convert.ToInt32(Console.ReadLine());

            res = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            Console.WriteLine("Vector lenght is " + res);
        }
    }
}
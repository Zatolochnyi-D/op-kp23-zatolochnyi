using System;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, p, res;

            Console.WriteLine("Enter a:");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter b:");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter c:");
            c = Convert.ToInt32(Console.ReadLine());

            if (a < 0)
            {
                Console.WriteLine("a is negative");
            }
            else
            {
                if (b < 0)
                {
                    Console.WriteLine("b is negative");
                }
                else
                {
                    if (c < 0)
                    {
                        Console.WriteLine("c is negative");
                    }
                    else
                    {
                        p = (a + b + c) / 2;
                        res = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                        Console.WriteLine("Square of triangle is " + res);
                    }
                }
            }
        }
    }
}
using System;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, d, min;
            string minName;

            Console.WriteLine("Enter a");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter b");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter c");
            c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter d");
            d = Convert.ToInt32(Console.ReadLine());

            min = a;
            minName = "a";
            if (b < min)
            {
                min = b;
                minName = "b";
            }
            if (c < min)
            {
                min = c;
                minName = "c";
            }
            if (d < min)
            {
                min = d;
                minName = "d";
            }
            Console.WriteLine("Minimal value is " + minName + ": " + min);
        }
    }
}
using System;

namespace Homework
{
    class Mirror
    {
        static void Main(string[] args)
        {
            int width, height, limit; //width, height and width limit of the mirror

            //define width and height
            Console.WriteLine("Enter size of mirror: ");
            width = Convert.ToInt32(Console.ReadLine());
            height = width;

            //limit of the width
            Console.WriteLine("Enter width limit: ");
            limit = Convert.ToInt32(Console.ReadLine());
            if (width > limit)
            {
                width = limit;
            }

            //draw top border of the mirror
            Console.Write("#");
            for (int i = 0; i < width; i++)
            {
                Console.Write("====");
            }
            Console.Write("#\n");

            //draw top part of the mirror
            for (int i = 0; i < width; i++)
            {
                for (int j = i; j < width - 1; j++)
                {
                    Console.Write("  ");
                }

                Console.Write("|<>");
                for (int j = width - i; j < width; j++)
                {
                    Console.Write("....");
                }
                Console.Write("<>|");

                for (int j = i; j < width - 1; j++)
                {
                    Console.Write("  ");
                }
                Console.Write("\n");
            }

            //draw middle part if width was set greater than 9
            for (int i = 0; i < (height - width); i++)
            {
                //do it twice
                for (int k = 0; k < 2; k++)
                {
                    Console.Write("|<>");
                    for (int j = 0; j < width - 1; j++)
                    {
                        Console.Write("....");
                    }
                    Console.Write("<>|");
                    Console.Write("\n");
                }
            }
            
            //draw bottom part of the mirror
            for (int i = width - 1; i > -1; i--)
            {
                for (int j = i; j < width - 1; j++)
                {
                    Console.Write("  ");
                }

                Console.Write("|<>");
                for (int j = width - i; j < width; j++)
                {
                    Console.Write("....");
                }
                Console.Write("<>|");

                for (int j = i; j < width - 1; j++)
                {
                    Console.Write("  ");
                }
                Console.Write("\n");
            }

            //draw bottom border of the mirror
            Console.Write("#");
            for (int i = 0; i < width; i++)
            {
                Console.Write("====");
            }
            Console.Write("#\n");
        }
    }
}
using System;

namespace Assignment2
{
    class Percolate
    {
        //size of side
        static int n;
        //amount of opened sites
        static int opened = 0;
        //matrix to demonstrate results
        static int[,] matrix;
        //array to do calculations
        static int[] arr;

        static void Main(string[] args)
        {
            Test();
        }

        //create 2D matrix nxn with blocked sites, create 1D array n*n with indexs as elements
        static void Init()
        {
            matrix = new int[n, n];
            arr = new int[n * n];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
        }

        //open choosed site
        static void Open(int row, int col)
        {
            if ((row > 0 && row < n + 1) && (col > 0 && col < n + 1))
            {
                matrix[row - 1, col - 1] = 1;
                opened += 1;
            }
            else
            {
                Console.WriteLine("This site does not exist.");
            }
        }

        //return true if choosed site is opened
        static bool IsOpen(int row, int col)
        {
            if ((row > 0 && row < n + 1) && (col > 0 && col < n + 1))
            {
                switch (matrix[row - 1, col - 1])
                {
                    case 1:
                        return true;
                    case 2:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        //return true if choosed site is full
        static bool IsFull(int row, int col)
        {
            if ((row > 0 && row < n + 1) && (col > 0 && col < n + 1))
            {
                switch (matrix[row - 1, col - 1])
                {
                    case 2:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        //return number of opened sites
        static int NumberOfOpenSites()
        {
            return opened;
        }

        //print matrix
        static void PrintMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

        //print array
        static void PrintArray()
        {
            for (int i = 0; i < n * n; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("");
        }

        //print array like a matrix
        static void PrintArrayM()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(arr[Convert2in1(i, j)] + "\t");
                }
                Console.WriteLine("");
            }
        }

        //connect 2 points
        static void union(int x, int y)
        {
            int componentY = arr[y];
            int componentX = arr[x];
            if (componentY == componentX)
            {
                return;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == componentX)
                {
                    arr[i] = componentY;
                }
            }
        }

        //convert matrix coordinates into array coordinates
        static int Convert2in1(int i, int j)
        {
            return n * i + j;
        }

        //connect points
        static void Connect()
        {
            
        }

        //replace all 1 with 2 where it is necessary and print true if this matrix perlocates
        static bool Flow()
        {
            
        }

        //test of all functions
        static void Test()
        {
            
        }
    }
}
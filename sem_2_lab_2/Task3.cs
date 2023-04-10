using System;

namespace Assignment2
{
	class Program
	{
		static void Main()
		{

		}
	}

	public class Vector3
	{
		public readonly int x;
        public readonly int y;
        public readonly int z;

        public Vector3()
		{
			
		}

        public Vector3(int x, int y, int z)
        {
			
        }

		public static int operator +(Vector3 a, Vector3 b)
		{
			return 0;
		}

		public static int operator *(Vector3 a, Vector3 b)
		{
			return 0;
		}

		public static int operator /(Vector3 a, Vector3 b)
		{
			return 0;
		}
    }
}

//input:
//case 1:
//  a(-3, 5, 4), b(-4, 5, 0)
//case 2:
//  a(6, -2, -5), b(0, 0, 9)
//case 3:
//  a(1, 5, 7), b(0, 0, 0)

//expected output:
//case 1:
//  a + b = -7
//  a * b = -16
//  a / b = 1
//case 2:
//  a + b = -7
//  a * b = -12
//  a / b = 2
//case 3:
//  a + b = 0
//  a * b = 0
//  a / b = 3
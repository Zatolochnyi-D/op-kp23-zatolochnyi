using System;

namespace Assignment2
{
	class Program
	{
		static int[][] vectorsA =
		{
			new int[] { -3, 5, 4 },
			new int[] { 6, -2, -5 },
			new int[] { 1, 5, 7 },
            new int[] { 4, 9, 5 },
        };

        static int[][] vectorsB =
        {
            new int[] { -4, 5, 0 },
            new int[] { 0, 0, 9 },
            new int[] { 0, 0, 0 },
            new int[] { 1, 6, 4 },
        };

        static void Main()
		{
			for (int i = 0; i < vectorsA.Length; i++)
			{
				Vector3 a = new(vectorsA[i][0], vectorsA[i][1], vectorsA[i][2]);
                Vector3 b = new(vectorsB[i][0], vectorsB[i][1], vectorsB[i][2]);

				Console.Write("a + b = ");
                Console.Write(a + b + "\n");

                Console.Write("a * b = ");
                Console.Write(a * b + "\n");

                Console.Write("a / b = ");
                Console.Write(a / b + "\n");
                Console.Write("\n");
            }
		}
	}

	public class Vector3
	{
		public readonly int x;
        public readonly int y;
        public readonly int z;

        public Vector3()
		{
			x = 0;
			y = 0;
			z = 0;
		}

        public Vector3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

		public static int operator +(Vector3 a, Vector3 b)
		{
			return (a.x + a.y + a.z - Math.Abs(a.x) - Math.Abs(a.y) - Math.Abs(a.z)) / 2 +
				   (b.x + b.y + b.z - Math.Abs(b.x) - Math.Abs(b.y) - Math.Abs(b.z)) / 2;
		}

		public static int operator *(Vector3 a, Vector3 b)
		{
            if ((a.x * a.y * a.z * b.x * b.y * b.z) % 2 == 1)
            {
                return 0;
            }
            return Even(a.x) * Even(a.y) * Even(a.z) * Even(b.x) * Even(b.y) * Even(b.z);
		}

        private static int Even(int num)
        {
            if (num % 2 == 0)
            {
                return num;
            }
            return 1;
        }

		public static int operator /(Vector3 a, Vector3 b)
		{
			return IsZero(a.x) + IsZero(a.y) + IsZero(a.z) + IsZero(b.x) + IsZero(b.y) + IsZero(b.z);
		}

		private static int IsZero(int num)
		{
			if (num == 0)
			{
				return 1;
			}
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
//case 4:
//  a(4, 9, 5), b(1, 6, 4)

//expected output:
//case 1:
//  a + b = -7
//  a * b = 0
//  a / b = 1
//case 2:
//  a + b = -7
//  a * b = 0
//  a / b = 2
//case 3:
//  a + b = 0
//  a * b = 0
//  a / b = 3
//case 4:
//  a + b = 0
//  a * b = 96
//  a / b = 0

//result matched
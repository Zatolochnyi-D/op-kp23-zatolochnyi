using System;

namespace Assignment2
{
    class Program
    {
        static void Main()
        {
            Vessel[] vessels = new Vessel[5];
            Random rnd = new();

            for (int i = 0; i < vessels.Length; i++)
            {
                switch (rnd.Next(0, 2))
                {
                    case 0:
                        vessels[i] = new SailingVessel();
                        Console.WriteLine("Ship created");
                        break;

                    case 1:
                        vessels[i] = new Submarine();
                        Console.WriteLine("Submarine created");
                        break;
                }
            }

            Console.WriteLine();

            for (int i = 0; i < vessels.Length; i++)
            {
                vessels[i].PrepareToMove();
            }

            Console.WriteLine();

            for (int i = 0; i < vessels.Length; i++)
            {
                vessels[i].Move();
            }
        }
    }

    interface Vessel
    {
        public void PrepareToMove();

        public void Move();
    }

    public class SailingVessel : Vessel
    {
        public SailingVessel()
        {
            
        }

        public void Move()
        {
            Console.WriteLine("Ship is moving");
        }

        public void PrepareToMove()
        {
            Console.WriteLine("Ship raises its sails");
        }
    }

    public class Submarine : Vessel
    {
        public Submarine()
        {

        }

        public void Move()
        {
            Console.WriteLine("Submarine is moving");
        }

        public void PrepareToMove()
        {
            Console.WriteLine("Submarine starts its engines");
        }
    }
}
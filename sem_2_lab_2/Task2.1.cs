using System;

namespace Assignment2
{
	class Program
	{
		static void Main()
		{
            
        }
	}

	public abstract class Vessel
	{
		public abstract void PrepareToMove();

        public abstract void Move();
    }

    public class SailingVessel : Vessel
    {
        public SailingVessel()
        {

        }

        public override void Move()
        {
            
        }

        public override void PrepareToMove()
        {
            
        }
    }

    public class Submarine : Vessel
    {
        public Submarine()
        {

        }

        public override void Move()
        {
            
        }

        public override void PrepareToMove()
        {
            
        }
    }
}


using System;
using CustomCollections;

namespace GameMechanics
{
	public class Client
	{
		static protected readonly SLList<string> _first = FileManipulator.ReadStringList("../../../FirstNames.csv");
		static protected readonly SLList<string> _second = FileManipulator.ReadStringList("../../../SecondNames.csv");
		static protected readonly SLList<string> _third = FileManipulator.ReadStringList("../../../ThirdNames.csv");

		static protected readonly int[][] _sizes = new int[][]
		{
			new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 }, // factory
            new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2 }, // shop
            new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 3 }, // warehouse
            new int[] { 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }, // office
            new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // shopping centre
        };

		static protected readonly Type[] _types = new Type[] { typeof(Factory), typeof(Shop), typeof(Warehouse), typeof(Office), typeof(ShoppingCentre), };

        protected const int _maxLeaseAmount = 5;

        protected string _name;
		protected Requirement _requirement;
		protected int _leaseCount;

		public string Name => _name;

        public Client()
		{
			_name = $"{_first[World.Random.Next(0, _first.Count)]} {_second[World.Random.Next(0, _second.Count)]} {_third[World.Random.Next(0, _third.Count)]}";

			int position = World.Random.Next(0, _types.Length);
			_requirement = new(_sizes[position][World.Random.Next(0, 10)], _types[position]);

			_leaseCount = World.Random.Next(1, _maxLeaseAmount + 1);
		}
    }


	public class Requirement
	{
		protected int _size;
		protected Type _type;

		public int Size => _size;
		public Type BuildingType => _type;

		public Requirement(int size, Type type)
		{
			_size = size;
			_type = type;
		}

		static public bool operator ==(Requirement a, Requirement b)
		{
			return a._size == b._size && a._type == b._type;
		}

		static public bool operator !=(Requirement a, Requirement b)
		{
			return !(a == b);
		}
    }
}


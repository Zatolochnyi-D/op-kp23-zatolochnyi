using System;
using CustomCollections;

namespace GameMechanics
{
	// Client have randomly generated name.
	// Client have randomly generated requirement.
	// Client can be a holder only of one building.
	public class Client
	{
		protected static readonly string[] _first = FileManipulator.ReadStringList(Path.GetFullPath("../../../materials/FirstNames.csv"));
		protected static readonly string[] _second = FileManipulator.ReadStringList(Path.GetFullPath("../../../materials/SecondNames.csv"));
		protected static readonly string[] _third = FileManipulator.ReadStringList(Path.GetFullPath("../../../materials/ThirdNames.csv"));

        protected string _name;
		protected Requirement _requirement;
		protected bool _isHolder;

		public string Name => _name;
		public Requirement Requirement => _requirement;
		public bool IsHolder => _isHolder;

        public Client()
		{
			_name = $"{_first[World.Random.Next(0, _first.Length)]} {_second[World.Random.Next(0, _second.Length)]} {_third[World.Random.Next(0, _third.Length)]}";

			_requirement = new();

			_isHolder = false;
		}

		public Client(int size, Type type)
		{
            _name = $"{_first[World.Random.Next(0, _first.Length)]} {_second[World.Random.Next(0, _second.Length)]} {_third[World.Random.Next(0, _third.Length)]}";

			_requirement = new(size, type);

			_isHolder = false;
        }

		public void Rent()
		{
			_isHolder = true;
		}

		public void Leave()
		{
			_isHolder = false;
		}
    }

	// Requirement contains type and size of building.
	// Requirement gerenates randomly.
	// Requirements can be compared.
	// Building can be created from requirement.
	public class Requirement
	{
		protected static readonly Type[] _types = new Type[]
		{
			typeof(Factory),
			typeof(Shop),
			typeof(Warehouse),
			typeof(Office),
			typeof(ShoppingCentre),
        };

        protected static readonly int[][] _sizes = new int[][]
        {
            new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 }, // factory
            new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2 }, // shop
            new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 3 }, // warehouse
            new int[] { 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }, // office
            new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // shopping centre
        };

        protected int _size;
		protected Type _type;

		public int Size => _size;
		public Type BuildingType => _type;

		public Requirement()
		{
			int index = World.Random.Next(0, _types.Length);

            _type = _types[index];
			_size = _sizes[index][World.Random.Next(0, 10)];
		}

		public Requirement(int size, Type type)
		{
			_size = size;
			_type = type;
		}

		public Building GetBuilding()
		{
			return (Building)Activator.CreateInstance(_type, _size);
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


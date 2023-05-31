using System;
using CustomCollections;

namespace GameMechanics
{
	public class Client
	{
		protected static readonly SLList<string> _first = FileManipulator.ReadStringList("../../../FirstNames.csv");
		protected static readonly SLList<string> _second = FileManipulator.ReadStringList("../../../SecondNames.csv");
		protected static readonly SLList<string> _third = FileManipulator.ReadStringList("../../../ThirdNames.csv");

        protected string _name;
		protected Requirement _requirement;

		protected Building? _rentedBuilding;

		public string Name => _name;
		public bool IsHolder => _rentedBuilding != null;

        public Client()
		{
			_name = $"{_first[World.Random.Next(0, _first.Count)]} {_second[World.Random.Next(0, _second.Count)]} {_third[World.Random.Next(0, _third.Count)]}";

			_requirement = new();

			_rentedBuilding = null;
		}

		public Client(int size, Type type)
		{
            _name = $"{_first[World.Random.Next(0, _first.Count)]} {_second[World.Random.Next(0, _second.Count)]} {_third[World.Random.Next(0, _third.Count)]}";

			_requirement = new(size, type);

            _rentedBuilding = null;
        }

		public void Rent(Building building)
		{
			_rentedBuilding = building;
		}

		public void Leave()
		{
			_rentedBuilding = null;
		}
    }


	public class Requirement
	{
		protected static readonly Type[] _types = new Type[]
		{
			typeof(Factory),
			typeof(Shop),
			typeof(Warehouse),
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


using System;
using CustomCollections;

namespace GameMechanics
{
	// Client have randomly generated name.
	// Client have randomly generated requirement.
	// Client can be a holder only of one building.
	public class Client
	{
		protected static readonly string[] _first = FileManipulator.ReadStringList(Path.GetFullPath("materials/FirstNames.csv"));
		protected static readonly string[] _second = FileManipulator.ReadStringList(Path.GetFullPath("materials/SecondNames.csv"));
		protected static readonly string[] _third = FileManipulator.ReadStringList(Path.GetFullPath("materials/ThirdNames.csv"));

        protected string _name;
		protected Requirement _requirement;
		protected bool _isHolder;

		public string Name => _name;
		public Requirement Requirement => _requirement;
		public bool IsHolder => _isHolder;

		// randomly generated client
        public Client()
		{
			Random random = new();


			_name = $"{_first[random.Next(0, _first.Length)]} {_second[random.Next(0, _second.Length)]} {_third[random.Next(0, _third.Length)]}";

			_requirement = new();

			_isHolder = false;
		}

		// predefined client
		public Client(int size, string type)
		{
			Random random = new();

            _name = $"{_first[random.Next(0, _first.Length)]} {_second[random.Next(0, _second.Length)]} {_third[random.Next(0, _third.Length)]}";

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
	public class Requirement : IComparable<Requirement>
	{
		protected static readonly string[] _types = new string[] { "Factory", "Shop", "Warehouse", "Office", "ShoppingCentre", };

        protected static readonly int[][] _sizes = new int[][]
        {
            new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 }, // factory
            new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2 }, // shop
            new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 3 }, // warehouse
            new int[] { 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }, // office
            new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, // shopping centre
        };

        protected int _size;
		protected string _type;

		public int Size => _size;
		public string Type => _type;

		// randomly generated requirement
		public Requirement()
		{
			Random random = new();

			int index = random.Next(0, _types.Length);

            _type = _types[index];
			_size = _sizes[index][random.Next(0, 10)];
		}

        // Random type with predefined size
        public Requirement(int size)
        {
            Random random = new();

			_size = size;

            int index = 0;
			do
			{
				index = random.Next(0, _types.Length);
				_type = _types[index];
			} while (!_sizes[index].Contains(_size));
        }

        // predefined requirement
        public Requirement(int size, string type)
		{
			_size = size;
			_type = type;
		}

		public Building GetBuilding(Land land)
		{
			switch (_type)
			{
                case "Factory":
					return new Factory(land, _size);
                case "Shop":
                    return new Shop(land, _size);
                case "Warehouse":
                    return new Warehouse(land, _size);
                case "Office":
                    return new Office(land, _size);
                case "ShoppingCentre":
                    return new ShoppingCentre(land, _size);

				default:
                    return new Factory(land, _size);
            }
		}

        public int CompareTo(Requirement? other)
        {
			if (Nullable.Equals(null, other))
			{
				return -1;
			}
			else 
			{
				int thisIndex = Array.IndexOf(_types, _type);
				int otherIndex = Array.IndexOf(_types, other._type);

                if (thisIndex < otherIndex)
				{
					return -1;
				}
				else if (thisIndex > otherIndex)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
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


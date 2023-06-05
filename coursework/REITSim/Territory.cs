using System;
using System.Collections;
using CustomCollections;

namespace GameMechanics
{
    public class City : IEnumerable<Land>
    {
        public const int MinTaxPercent = 5;
        public const int MaxTaxPercent = 20;
        public const int MinLandAmount = 3;
        public const int MaxLandAmount = 4;
        public const int BuildingChance = 40;

        protected static readonly string[] _names = FileManipulator.ReadStringList(Path.GetFullPath("materials/CityNames.csv"));

        protected string _name;
        protected double _incomeTaxPercent;
        protected SortedSLList<Land> _lands;

        public string Name => _name;
        public double Taxation => _incomeTaxPercent;
        public Land this[int index] => _lands[index];

        // New random city.
        public City()
        {
            Random random = new();

            _name = _names[random.Next(0, _names.Length)];
            _incomeTaxPercent = random.Next(MinTaxPercent, MaxTaxPercent + 1);

            _lands = new((x, y) =>
            {
                // compare belonging to the player
                if (x.PlayerProperty && !y.PlayerProperty) return true;
                if (!x.PlayerProperty && y.PlayerProperty) return false;

                // compare presence of the building
                if (!x.HaveBuilding && y.HaveBuilding) return true;
                if (x.HaveBuilding && !y.HaveBuilding) return false;

                // compare lands with buildings
                if (x.HaveBuilding && y.HaveBuilding)
                {
                    // compare types of buildings
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == 1) return true;
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == -1) return false;

                    // compare sizes of buildings
                    if (x.Building.Requirement.Size > y.Building.Requirement.Size) return true;
                    if (x.Building.Requirement.Size < y.Building.Requirement.Size) return false;
                }
                // compare lands without buildings
                else
                {
                    // compare sizes of lands
                    if (x.Size > y.Size) return true;
                    if (x.Size < y.Size) return false;
                }

                return true;
            });

            for (int i = 0; i < random.Next(MinLandAmount, MaxLandAmount + 1); i++)
            {
                _lands.Add(new(this, random.Next(1, 4)));
            }

            foreach (Land land in _lands)
            {
                if (random.Next(1, 101) < BuildingChance)
                {
                    land.Build(new Requirement(random.Next(1, land.Size + 1)));
                }
            }

            _lands.Sort();
        }

        // New empty city.
        public City(int taxPercent)
        {
            Random random = new();

            _name = _names[random.Next(0, _names.Length)];
            _incomeTaxPercent = taxPercent;
            _lands = new((x, y) =>
            {
                // compare belonging to the player
                if (x.PlayerProperty && !y.PlayerProperty) return true;
                if (!x.PlayerProperty && y.PlayerProperty) return false;

                // compare presence of the building
                if (!x.HaveBuilding && y.HaveBuilding) return true;
                if (x.HaveBuilding && !y.HaveBuilding) return false;

                // compare lands with buildings
                if (x.HaveBuilding && y.HaveBuilding)
                {
                    // compare types of buildings
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == 1) return true;
                    if (x.Building.Requirement.CompareTo(y.Building.Requirement) == -1) return false;

                    // compare sizes of buildings
                    if (x.Building.Requirement.Size > y.Building.Requirement.Size) return true;
                    if (x.Building.Requirement.Size < y.Building.Requirement.Size) return false;
                }
                // compare lands without buildings
                else
                {
                    // compare sizes of lands
                    if (x.Size > y.Size) return true;
                    if (x.Size < y.Size) return false;
                }

                return true;
            });
        }

        public void AddLand(int size)
        {
            _lands.Add(new(this, size));
        }

        public void Sort()
        {
            _lands.Sort();
        }

        public IEnumerator<Land> GetEnumerator()
        {
            return _lands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class Land
    {
        public const double BaseTaxCost = 5.0;
        public const double BaseCostMultiplayer = 10.0;

        protected int _size;
        protected Building? _building;
        protected City _parentCity;
        protected bool _playerProperty;

        public int Size => _size;
        public Building? Building => _building;
        public bool HaveBuilding => _building != null;
        public City ParentCity => _parentCity;
        public bool PlayerProperty { get { return _playerProperty; } set { _playerProperty = value; } }

        public double LandCost => BaseTaxCost * BaseCostMultiplayer * _size;
        public double LandTax => -(BaseTaxCost * _size);

        public double Income => (HaveBuilding ? _building.Income : 0) + LandTax;

        public Land(City city, int size)
        {
            _size = size;
            _building = null;
            _parentCity = city;
            _playerProperty = false;
        }

        public void Build(Requirement requirement)
        {
            if (requirement.Size <= _size)
            {
                _building = requirement.GetBuilding(this);
            }
            else throw new ArgumentException("Land is too small for this building");
        }

        public void Build(Building building)
        {
            _building = building;
        }

        public void Raze()
        {
            _building = null;
        }
    }
}
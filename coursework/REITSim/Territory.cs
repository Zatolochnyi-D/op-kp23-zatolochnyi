using System;
using CustomCollections;

namespace GameMechanics
{
    public class City
    {
        public  const int MinTaxPercent = 5;
        public const int MaxTaxPercent = 20;
        public const int MinLandAmount = 2;
        public const int MaxLandAmount = 7;
        public const int BuildingChance = 30;

        protected static readonly string[] _names = FileManipulator.ReadStringList(Path.GetFullPath("../../../materials/CityNames.csv"));

        protected string _name;
        protected double _incomeTaxPercent;
        protected SLList<Land> _lands;

        public string Name => _name;
        public double Taxation => _incomeTaxPercent;
        public SLList<Land> Lands => _lands;

        public City()
        {
            _name = _names[World.Random.Next(0, _names.Length)];

            _incomeTaxPercent = World.Random.Next(MinTaxPercent, MaxTaxPercent + 1);

            _lands = new();
            for (int i = 0; i < World.Random.Next(MinLandAmount, MaxLandAmount + 1); i++)
            {
                _lands.Add(new(_incomeTaxPercent));
            }

            foreach (Land land in _lands)
            {
                if (World.Random.Next(0, 100) < BuildingChance)
                {
                    land.Build(new Requirement().GetBuilding());
                }
            }
        }

        public City(int taxPercent)
        {
            _name = _names[World.Random.Next(0, _names.Length)];

            _incomeTaxPercent = taxPercent;

            _lands = new();
        }

        public void AddLand(Land land)
        {
            _lands.Add(land);
        }
    }


    public class Land
    {
        public const double TaxCost = 5.0;

        protected int _size;
        protected Building? _building;
        protected double _cityTax;

        public int Size => _size;
        public Building? Building => _building;
        public double Taxation => Taxation;

        public Land(double cityTax)
        {
            _size = World.Random.Next(1, 4);
            _building = null;
            _cityTax = cityTax;
        }

        public Land(double cityTax, int size)
        {
            _size = size;
            _building = null;
            _cityTax = cityTax;
        }

        public void Build(Building building)
        {
            _building = building;
        }

        public void Destroy()
        {
            _building = null;
        }
    }
}
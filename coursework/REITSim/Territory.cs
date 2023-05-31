using System;
using CustomCollections;

namespace GameMechanics
{
    interface IRealEstate
    {
        double TotalCost { get; }

        void NextTurn();
    }


    public class City : IRealEstate
    {
        protected const int _minTaxPercent = 5;
        protected const int _maxTaxPercent = 20;
        protected const int _minLandAmount = 2;
        protected const int _maxLandAmount = 7;
        protected const int _buildingChance = 30;

        protected double _incomeTaxPercent;
        protected SLList<Land> _lands;

        public double Taxation => _incomeTaxPercent;
        public SLList<Land> Lands => _lands;

        public double TotalCost
        {
            get
            {
                double income = 0.0;

                foreach (Land land in _lands)
                {
                    double landIncome = land.TotalCost;
                    income += landIncome > 0.0 ? landIncome * (100.0 - _incomeTaxPercent) : landIncome;
                }

                return Math.Round(income, 2);
            }
        }

        public City()
        {
            _incomeTaxPercent = World.Random.Next(_minTaxPercent, _maxTaxPercent + 1);

            _lands = new();
            for (int i = 0; i < World.Random.Next(_minLandAmount, _maxLandAmount + 1); i++)
            {
                _lands.Add(new());
            }

            foreach (Land land in _lands)
            {
                if (World.Random.Next(0, 100) < _buildingChance)
                {
                    land.Build(new Requirement().GetBuilding());
                }
            }
        }

        public City(int taxPercent)
        {
            _incomeTaxPercent = taxPercent;

            _lands = new();
        }

        public void AddLand(Land land)
        {
            _lands.Add(land);
        }

        public void NextTurn()
        {
            foreach (Land land in _lands)
            {
                land.NextTurn();
            }
        }
    }


    public class Land : IRealEstate
    {
        protected const double _taxation = 100.0;

        protected int _size;
        protected Building? _building;

        public double TotalCost => (_building != null ? _building.TotalCost : 0) - _taxation * _size;
        public Building? Building => _building;
        public double Taxation => _taxation;

        public Land()
        {
            _size = World.Random.Next(1, 4);
            _building = null;
        }

        public Land(int size)
        {
            _size = size;
            _building = null;
        }

        public void Build(Building building)
        {
            _building = building;
        }

        public void Destroy()
        {
            _building = null;
        }

        public void NextTurn()
        {
            _building?.NextTurn();
        }
    }
}
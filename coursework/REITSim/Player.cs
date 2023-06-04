using System;
using CustomCollections;

namespace GameMechanics
{
    public class Player
    {
        public const double StartingCapital = 1000.0;
        public const double MinSharePrice = 5.0;

        // === stats ===
        protected string _name;
        protected int _reputation;
        protected double _money;
        protected double _income;

        // === shares ===
        protected Shares _playerShares;
        protected Shares _sharesOnExchange;
        protected Shares _investor;
        protected double _oneSharePrice;

        // === property ===
        protected SLList<Land> _property;



        // === data that can be accessed from outside ===

        public string Name => _name;
        public int Reputation => _reputation;
        public double SharePrice => _oneSharePrice;
        public double Shares => _playerShares.Percent;
        public double SharesOnExchange => _sharesOnExchange.Percent;
        public double Income => _income;
        public double Money => _money;
        public SLList<Land> Property => _property;

        public Player(string name)
        {
            _name = name;

            _reputation = 0;
            _money = StartingCapital;
            _income = 0.0;

            _oneSharePrice = 0.0;

            _playerShares = new(100.0);
            _sharesOnExchange = new(0.0);
            _investor = new(0.0);
            _property = new();
        }

        public void SharesToSell(double amount)
        {
            if (amount <= Shares)
            {
                _playerShares.Percent -= amount;
                _sharesOnExchange.Percent += amount;

                _reputation -= (int)Math.Floor(amount);
                if (_reputation < -100)
                {
                    _reputation = -100;
                }

                UpdateSharePrice();
            }
        }

        public void BuyShares(double amount)
        {
            if (amount <= _investor.Percent)
            {
                _reputation += (int)Math.Floor(amount);
                if (_reputation > 100)
                {
                    _reputation = 100;
                }

                _money = Math.Round(_money - amount * _oneSharePrice, 2);
                _playerShares.Percent += amount;
                _investor.Percent -= amount;

                UpdateSharePrice();
            }
        }

        // Player buys only unclaimed lands. +

        // Player not always have money. +
        public void BuyLand(Land land)
        {
            if (land.LandCost <= _money)
            {
                _money -= land.LandCost;
                _property.Add(land);
                land.PlayerProperty = true;
                _reputation += 5;
            }
        }

        // Land always belongs to the player. +
        // Player always builds on empty land. +
        // Building always have correct size. +

        // Player not always have money. +
        public void BuildBuilding(Land land, Requirement requirement)
        {
            Building building = requirement.GetBuilding(land);

            if (building.BuildCost <= _money)
            {
                _money -= building.BuildCost;
                land.Build(building);
            }
        }

        // Land always belongs to the player. +
        // Land always have building. +
        // Building is always free. +

        // Player not always have money. +
        public void RazeBuilding(Land land)
        {
            if (land.Building?.RazeCost <= _money)
            {
                _money -= land.Building.RazeCost;
                land.Raze();
            }
        }

        // Input land always belongs to the player. +
        // Input land always have building. +
        // Input client is always free. +
        public void RentOutBuilding(Land land, Client client)
        {
            land.Building.RentOut(client);
        }

        public void UpdateIncome()
        {
            _income = 0.0;

            foreach (Land land in _property)
            {
                _income += land.Income;

                land.Building?.NextTurn();
            }

            UpdateSharePrice();

            if (_income > 0.0)
            {
                _income = Math.Round(_income * (_playerShares.Percent / 100.0), 2);
            }
        }

        public void NextTurn()
        {
            UpdateIncome();

            SellShares();

            CollectIncome();
        }

        protected void CollectIncome()
        {
            _money += _income;
        }

        protected void UpdateSharePrice()
        {
            if (_income > 0.0)
            {
                _oneSharePrice = Math.Round((_income / 10.0) * (1.0 + _reputation / 100.0), 2);
            }
            else
            {
                _oneSharePrice = MinSharePrice;
            }
        }

        protected void SellShares()
        {
            if (_sharesOnExchange.Percent != 0.0 && _oneSharePrice != 0.0)
            {
                double[] parts = new double[] { 0.0, 0.25, 0.25, 0.5, 0.5, 0.75, 0.75, 1.0, 1.0, 1.0 };
                Random random = new();

                double sharesAmount = Math.Round(_sharesOnExchange.Percent * parts[random.Next(0, parts.Length)], 2);

                _sharesOnExchange.Percent -= sharesAmount;
                _investor.Percent += sharesAmount;
                _money = Math.Round(_money + sharesAmount * _oneSharePrice, 2);
            }
        }
    }


    public class Shares
    {
        protected double _percent;

        // O(1), O(1)
        public double Percent
        {
            get { return _percent; }
            set { _percent = Math.Round(value, 2); }
        }

        // O(1)
        public Shares(double percent)
        {
            _percent = percent;
        }
    }
}
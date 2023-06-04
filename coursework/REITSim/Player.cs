using System;
using CustomCollections;

namespace GameMechanics
{
    public class Player
    {
        public const double StartingCapital = 1000.0;
        public const double SharePriceMultiplier = 5.0;

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
                UpdateSharePrice();

                _money = Math.Round(_money - amount * _oneSharePrice, 2);
                _playerShares.Percent += amount;
                _investor.Percent -= amount;
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
                _reputation += 5;

                UpdateIncome();
            }
        }

        // Land always belongs to the player. +
        // Player always builds on empty land. +
        // Building always have correct size. +

        // Player not always have money. +
        public void BuildBuilding(Land land, Requirement requirement)
        {
            Building building = requirement.GetBuilding(land);

            if (building.BuilCost <= _money)
            {
                _money -= building.BuilCost;
                land.Build(building);

                UpdateIncome();
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
                UpdateIncome();
            }
        }

        // Input land always belongs to the player. +
        // Input land always have building. +
        // Input client is always free. +
        public void RentOutBuilding(Land land, Client client)
        {
            land.Building.RentOut(client);
        }

        public void NextTurn()
        {
            UpdateSharePrice();

            SellShares();

            UpdateIncome();

            CollectIncome();

            UpdateProperty();
        }

        protected void UpdateIncome()
        {
            _income = 0.0;

            foreach (Land land in _property)
            {
                _income += land.LandTax;

                if (land.Building != null)
                {
                    _income += land.Building.Income;
                }
            }

            Console.WriteLine(_income);
            if (_income > 0.0)
            {
                Console.WriteLine(_income * (_playerShares.Percent / 100.0));
                _income = Math.Round(_income * (_playerShares.Percent / 100.0), 2);
            }

            UpdateSharePrice();
        }

        protected void CollectIncome()
        {
            _money += _income;
        }

        protected void UpdateProperty()
        {
            foreach (Land land in _property)
            {
                land.Building?.NextTurn();
            }
        }

        protected void UpdateSharePrice()
        {
            if (_income > 0.0)
            {
                _oneSharePrice = (_income / 100.0) * (1.0 + _reputation / 100.0) * SharePriceMultiplier;

                _oneSharePrice = Math.Round(_oneSharePrice, 2);
            }
            else
            {
                _oneSharePrice = 0.0;
            }
        }

        protected void SellShares()
        {
            if (_sharesOnExchange.Percent != 0.0 && SharePrice != 0.0)
            {
                double[] parts = new double[] { 0.0, 0.25, 0.25, 0.5, 0.5, 0.75, 0.75, 1.0, 1.0, 1.0 };
                Random random = new();

                double sharesAmount = _sharesOnExchange.Percent * parts[random.Next(0, parts.Length)];
                sharesAmount = Math.Round(sharesAmount, 2);
                sharesAmount = Math.Min(_sharesOnExchange.Percent, sharesAmount);

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
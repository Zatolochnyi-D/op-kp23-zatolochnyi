using System;
using CustomCollections;

namespace GameMechanics
{
    public class Player
    {
        // === shares ===
        protected Shares _playerShares;
        protected Shares _sharesOnExchange;
        protected double _oneSharePrice = 0.0;
        protected Shares _investor;

        // === property ===
        protected SLList<Land> _property;

        // === stats ===
        protected int _reputation = 0;
        protected double _income = 1000.0;
        protected double _outcome = 0.0;
        protected double _money = 0.0;

        // === data that can be accessed and changed from outside ===

        public int Reputation
        {
            get { return _reputation; }
            set { _reputation = value; }
        }

        // === data that can be accessed from outside ===

        public double SharePrice => _oneSharePrice;
        public double Shares => _playerShares.Percent;
        public double Income => _income;
        public double Money => _money;

        // === getters for testing ===
        internal Shares PlayerShares => _playerShares;
        internal Shares SharesOnExchange => _sharesOnExchange;
        // SharePrice
        internal double InvestorShares => _investor.Percent;
        // Reputation
        // Income
        // Money

        public Player()
        {
            _playerShares = new(100.0);
            _sharesOnExchange = new(0.0);
            _investor = new(0.0);
            _property = new();
        }

        public void SharesToSell(double amount)
        {
            _playerShares.Percent -= amount;
            _sharesOnExchange.Percent += amount;

            _reputation -= (int)Math.Floor(amount);
            if (_reputation < -100)
            {
                _reputation = -100;
            }
        }

        public void BuyShares(double amount)
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

        public void NextTurn()
        {
            UpdateSharePrice();

            SellShares();
        }

        protected void UpdateSharePrice()
        {
            _oneSharePrice = (_income / 100.0) * (1.0 + _reputation / 100.0);

            _oneSharePrice = Math.Round(_oneSharePrice, 2);
        }

        protected void SellShares()
        {
            if (_sharesOnExchange.Percent != 0.0)
            {
                double[] parts = new double[] { 0.0, 0.25, 0.25, 0.5, 0.5, 0.75, 0.75, 1.0, 1.0, 1.0 };

                double sharesAmount = _sharesOnExchange.Percent * parts[World.Random.Next(0, parts.Length)];
                sharesAmount = Math.Round(sharesAmount, 2);
                sharesAmount = Math.Min(_sharesOnExchange.Percent, sharesAmount);

                _sharesOnExchange.Percent -= sharesAmount;
                _investor.Percent += sharesAmount;
                _money = Math.Round(_money + sharesAmount * _oneSharePrice, 2);
            }
        }

        protected void UpdateProperty()
        {
            foreach (Land land in _property)
            {
                _income = land.TotalCost;

                land.NextTurn();
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
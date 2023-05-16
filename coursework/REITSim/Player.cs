using System;
using CustomCollections;

namespace GameMechanics
{
	public class Player
	{
        // shares
        protected Shares _playerShares;
        protected Shares _sharesOnExchange;
        protected float _sharePercentPrice = 100.0f;
		protected Investor _investor;

		// stats
        protected int _reputation = 0;
		protected float _income = 1000.0f;
		protected float _money = 0.0f;

		public int Reputation
		{
			get { return _reputation; }
			set { _reputation = value; }
		}

		public float Income
		{
			get { return _income; }
			set { _income = value; }
		}

		public float SharePrice
		{
			get { return _sharePercentPrice; }
			set { _sharePercentPrice = value; }
		}

		public float Shares => _playerShares.Percent;

		public Player()
		{
			_playerShares = new(100.0f, this);
			_sharesOnExchange = new(0.0f, this);
			_investor = new(this);
		}

		// Set part of player shares to sale. Investors will by some part each turn.
		// Selling decrease reputation and price
		// O(TODO)
		public void SharesToSell(float amount)
		{
			_playerShares.Percent -= amount;
			_sharesOnExchange.Percent += amount;

            _reputation -= (int)Math.Floor(amount);
		}

		// Buy shares from investor
		// Buying increase reputation and price
		public void BuyShares(float amount)
		{

		}

		// Update data when moving to the next turn.
		public void NextTurn()
		{
			// Update price of shares:
			UpdateSharePrice();

			// Sell some shares to investor:
			SellShares();
		}

        // Update price of 1% of share
        // price = income from 1% share + <reputation>%
        // O(1)
        protected void UpdateSharePrice()
		{
            _sharePercentPrice = (_income / 100.0f) * (1.0f + (float)_reputation / 100.0f);
        }

        // Sell shares to investor.
		// Sells random amount of shares
		// O(1)
        protected void SellShares()
        {
			float sharesAmount = _sharesOnExchange.Percent * ((float)World.Random.Next(101) / 100.0f);

			_sharesOnExchange.Percent -= sharesAmount;
			_investor.Shares.Percent += sharesAmount;
			_money += sharesAmount * _sharePercentPrice;
        }
    }


    public class Investor
    {
        protected Shares _shares;

		// O(1)
        public Shares Shares => _shares;

		// O(1)
        public Investor(Player player)
        {
            _shares = new(0.0f, player);
        }
    }


    public class Shares
	{
		static protected Player _playerRef;

		protected float _percent;
		protected float _price;

		// O(1), O(1)
		public float Percent
		{
			get { return _percent; }
			set
			{
				_percent = value;
				UpdatePrice();
			}
		}

		// O(1)
		public float Price => _price;

		// O(1)
		public Shares(float percent, Player player)
		{
			_playerRef = player;
			_percent = percent;

			UpdatePrice();
		}

		// O(1)
		public void UpdatePrice()
		{
			_price = _playerRef.SharePrice * _percent;
		}
	}
}
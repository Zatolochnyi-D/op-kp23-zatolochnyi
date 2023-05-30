using System;

namespace GameMechanics
{
	interface IProperty
	{
		double TotalCost { get; }
	}


	public class Land : IProperty
	{
		protected int _size;
		protected double _taxation;

		protected Building? _building;

		public double TotalCost => Math.Round(_taxation + (_building != null ? _building.TotalCost : 0), 2);
		//public Building? Building => _building;

		public Land(int size, double taxation, Building? building = null)
		{
			_size = size;
			_taxation = taxation;
			_building = building;
		}

		public void NextTurn()
		{
			if (_building != null)
			{
				_building.NextTurn();
			}
		}
    }


	public abstract class Building : IProperty
	{
		// === building stats ===
		protected int _size;
		protected double _maintenance;
		protected double _taxation;
		protected double _income;

		// === rent info ===
		protected int _oneRentTime;
		protected int _rentExpireAfter;
		protected bool _autoExtension;
		protected Client? _holder;

		public virtual double TotalCost => Math.Round(_income - (_taxation + _maintenance));
		public virtual bool Occupied => _holder != null;

		public Building(int size)
		{
			_size = size;

			_rentExpireAfter = 0;
			_autoExtension = true;
			_holder = null;
		}

		public virtual void RentOut(Client client)
		{
			_holder = client;
			_rentExpireAfter = _oneRentTime;
		}

		public virtual void Release()
		{
			_holder = null;
		}

		public virtual void NextTurn()
		{
			if (Occupied)
			{
				_rentExpireAfter--;

                if (_rentExpireAfter == 0)
                {
                    if (_autoExtension)
                    {
                        _rentExpireAfter = _oneRentTime;
                    }
                    else
                    {
                        Release();
                    }
                }
            }
		}
	}

	public class Factory : Building
	{
		public Factory(int size, int tax) : base(size)
		{
			//_maintenanceCost;
			//_taxation;
			//_income;
			//_rentTime;
		}
	}

	//public class Shop : Building
	//{

	//}

	//public class Warehouse : Building
	//{

	//}

	//public class Office : Building
	//{

	//}

	//public class ShoppingCentre : Building
	//{

	//}
}


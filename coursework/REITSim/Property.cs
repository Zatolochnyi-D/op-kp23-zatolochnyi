using System;

namespace GameMechanics
{
	public class Land
	{
		protected int _size;
		protected double _taxation;

		protected Building? _building;

		//public double TotalCost => Math.Round(_taxation + (_building != null ? _building.TotalCost : 0), 2);

		public Land(int size, double taxation, Building? building = null)
		{
			_size = size;
			_taxation = taxation;
			_building = building;
		}
	}


	public abstract class Building
	{
		// === building stats ===
		protected int _size;
		protected double _maintenanceCost;
		protected double _taxation;
		protected double _income;

		// === lease info ===
		protected bool _leased;
		protected int _leasedFor;
		protected bool _canceled;
	}

    public class Factory : Building
    {

    }

    public class Shop : Building
    {

    }

    public class Warehouse : Building
    {

    }

    public class Office : Building
    {

    }

    public class ShoppingCentre : Building
    {

    }
}


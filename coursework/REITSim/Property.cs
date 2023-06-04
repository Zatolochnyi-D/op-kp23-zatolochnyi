using System;
using System.Drawing;

namespace GameMechanics
{
    public abstract class Building
    {
        // === building stats ===
        protected Requirement _requirement;
        protected double _maintenance;
        protected double _profit;
        protected Land _parentLand;

        // === rent info ===
        protected int _oneRentTime;
        protected int _rentExpireAfter;
        protected bool _autoExtension;
        protected Client? _holder;

        public abstract double BuildCost { get; }
        public abstract double RazeCost { get; }

        public virtual Requirement Requirement => _requirement;
        public virtual double Maintenance => _maintenance;
        public virtual double Profit => _profit;
        public virtual Land ParentLand => _parentLand;
        
        public virtual bool Occupied => _holder != null;
        public virtual bool AutoExtention { get { return _autoExtension; } set { _autoExtension = value; } }

        public virtual double Income => (_profit * (1.0 - _parentLand.ParentCity.Taxation / 100.0)) * (Occupied ? 1.0 : 0.0) - _maintenance;

        public Building(Land land)
        {
            _rentExpireAfter = 0;
            _autoExtension = true;
            _holder = null;
            _parentLand = land;
        }

        public virtual void RentOut(Client client)
        {
            _holder = client;
            _holder.Rent();

            _rentExpireAfter = _oneRentTime;
        }

        public virtual void Release()
        {
            _holder.Leave();
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
        public const double BaseMaintenance = 15.0;
        public const double BaseProfit = 30.0;
        public const int BaseRentTime = 20;

        public override double BuildCost => Math.Round(BaseMaintenance * _requirement.Size * 10.0, 2);

        public override double RazeCost => Math.Round(BaseMaintenance * _requirement.Size * 3.0, 2);

        public Factory(Land land, int size) : base(land)
        {
            _requirement = new(size, "Factory");

            _maintenance = BaseMaintenance * _requirement.Size;
            _profit = BaseProfit * (_requirement.Size * 1.5);
            _oneRentTime = BaseRentTime;
        }
    }


    public class Shop : Building
    {
        public const double BaseMaintenance = 5.0;
        public const double BaseProfit = 10.0;
        public const int BaseRentTime = 20;

        public override double BuildCost => Math.Round(BaseMaintenance * _requirement.Size * 3.0, 2);

        public override double RazeCost => Math.Round(BaseMaintenance * _requirement.Size * 1.0, 2);

        public Shop(Land land, int size) : base(land)
        {
            _requirement = new(size, "Shop");

            _maintenance = BaseMaintenance * _requirement.Size;
            _profit = BaseProfit * _requirement.Size;
            _oneRentTime = BaseRentTime;
        }
    }


    public class Warehouse : Building
    {
        public const double BaseMaintenance = 5.0;
        public const double BaseProfit = 15.0;
        public const int BaseRentTime = 30;

        public override double BuildCost => Math.Round(BaseMaintenance * _requirement.Size * 5.0, 2);

        public override double RazeCost => Math.Round(BaseMaintenance * _requirement.Size * 1.0, 2);

        public Warehouse(Land land, int size) : base(land)
        {
            _requirement = new(size, "Warehouse");

            _maintenance = BaseMaintenance * _requirement.Size;
            _profit = BaseProfit * _requirement.Size;
            _oneRentTime = BaseRentTime;
        }
    }


    public class Office : Building
    {
        public const double BaseMaintenance = 5.0;
        public const double BaseProfit = 15.0;
        public const int BaseRentTime = 30;

        public override double BuildCost => Math.Round(BaseMaintenance * _requirement.Size * 3.0, 2);

        public override double RazeCost => Math.Round(BaseMaintenance * _requirement.Size * 3.0, 2);

        public Office(Land land, int size) : base(land)
        {
            _requirement = new(size, "Office");

            _maintenance = BaseMaintenance * _requirement.Size;
            _profit = BaseProfit * _requirement.Size;
            _oneRentTime = BaseRentTime;
        }
    }


    public class ShoppingCentre : Building
    {
        public const double BaseMaintenance = 10.0;
        public const double BaseProfit = 40.0;
        public const int BaseRentTime = 30;

        public override double BuildCost => Math.Round(BaseMaintenance * _requirement.Size * 15.0, 2);

        public override double RazeCost => Math.Round(BaseMaintenance * _requirement.Size * 10.0, 2);

        public ShoppingCentre(Land land, int size) : base(land)
        {
            _requirement = new(size, "ShoppingCentre");

            _maintenance = BaseMaintenance * _requirement.Size;
            _profit = BaseProfit * _requirement.Size;
            _oneRentTime = BaseRentTime;
        }
    }
}


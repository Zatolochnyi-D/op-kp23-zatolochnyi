using System;
using System.Drawing;

namespace GameMechanics
{
    public abstract class Building
    {
        // === building stats ===
        protected Requirement _requirement;
        protected double _maintenance;
        protected double _cityTax;
        protected double _income;

        // === rent info ===
        protected int _oneRentTime;
        protected int _rentExpireAfter;
        protected bool _autoExtension;
        protected Client? _holder;

        public virtual double CityTax { get { return _cityTax; } set { _cityTax = value; } }
        public virtual double TotalCost => (_income * (1.0 - _cityTax / 100.0)) * (Occupied ? 1.0 : 0.0) - _maintenance;
        public virtual bool Occupied => _holder != null;
        public Requirement Requirement => _requirement;
        public double Maintenance => _maintenance;
        public double Income => _income;

        public Building(int size)
        {
            _rentExpireAfter = 0;
            _autoExtension = true;
            _holder = null;

            _requirement = new(size, this.GetType());
            _cityTax = 0.0;
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
        public const double Maintenance = 15.0;
        public const double Profit = 30.0;
        public const int RentTime = 20;

        public Factory(int size) : base(size)
        {
            _maintenance = Maintenance * size;
            _income = Profit * size;
            _oneRentTime = RentTime;
        }
    }

    public class Shop : Building
    {
        public const double Maintenance = 5.0;
        public const double Profit = 12.0;
        public const int RentTime = 20;

        public Shop(int size) : base(size)
        {
            _maintenance = Maintenance * size;
            _income = Profit * size;
            _oneRentTime = RentTime;
        }
    }

    public class Warehouse : Building
    {
        public const double Maintenance = 5.0;
        public const double Profit = 15.0;
        public const int RentTime = 30;

        public Warehouse(int size) : base(size)
        {
            _maintenance = Maintenance * size;
            _income = Profit * size;
            _oneRentTime = RentTime;
        }
    }

    public class Office : Building
    {
        public const double Maintenance = 5.0;
        public const double Profit = 15.0;
        public const int RentTime = 30;

        public Office(int size) : base(size)
        {
            _maintenance = Maintenance * size;
            _income = Profit * size;
            _oneRentTime = RentTime;
        }
    }

    public class ShoppingCentre : Building
    {
        public const double Maintenance = 15.0;
        public const double Profit = 30.0;
        public const int RentTime = 30;

        public ShoppingCentre(int size) : base(size)
        {
            _maintenance = Maintenance * size;
            _income = Profit * size;
            _oneRentTime = RentTime;
        }
    }
}


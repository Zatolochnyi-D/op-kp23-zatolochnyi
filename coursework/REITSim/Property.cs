using System;
using System.Drawing;

namespace GameMechanics
{
    public abstract class Building : IRealEstate
    {
        protected enum BuildingRentTime
        {
            Factory = 10,
            Shop = 20,
            Office = 30,
            Warehouse = 40,
            ShoppingCentre = 50,
        }

        protected enum BuildingMaintenance
        {
            Factory = 10,
            Shop = 20,
            Office = 30,
            Warehouse = 40,
            ShoppingCentre = 50,
        }

        protected enum BuildingProfit
        {
            Factory = 10,
            Shop = 20,
            Office = 30,
            Warehouse = 40,
            ShoppingCentre = 50,
        }

        // === building stats ===
        protected Requirement _requirement;
        protected double _maintenance;
        protected double _income;

        // === rent info ===
        protected int _oneRentTime;
        protected int _rentExpireAfter;
        protected bool _autoExtension;
        protected Client? _holder;

        public virtual double TotalCost => _income * (Occupied ? 1 : 0) - _maintenance;
        public virtual bool Occupied => _holder != null;

        public Building(int size)
        {
            _rentExpireAfter = 0;
            _autoExtension = true;
            _holder = null;

            _requirement = new(size, this.GetType());
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
        public Factory(int size) : base(size)
        {
            _maintenance = (double)BuildingMaintenance.Factory * size;
            _income = (double)BuildingProfit.Factory * size;
            _oneRentTime = (int)BuildingRentTime.Factory;
        }
    }

    public class Shop : Building
    {
        public Shop(int size) : base(size)
        {
            _maintenance = (double)BuildingMaintenance.Shop * size;
            _income = (double)BuildingProfit.Shop * size;
            _oneRentTime = (int)BuildingRentTime.Shop;
        }
    }

    public class Warehouse : Building
    {
        public Warehouse(int size) : base(size)
        {
            _maintenance = (double)BuildingMaintenance.Warehouse * size;
            _income = (double)BuildingProfit.Warehouse * size;
            _oneRentTime = (int)BuildingRentTime.Warehouse;
        }
    }

    public class Office : Building
    {
        public Office(int size) : base(size)
        {
            _maintenance = (double)BuildingMaintenance.Office * size;
            _income = (double)BuildingProfit.Office * size;
            _oneRentTime = (int)BuildingRentTime.Office;
        }
    }

    public class ShoppingCentre : Building
    {
        public ShoppingCentre(int size) : base(size)
        {
            _maintenance = (double)BuildingMaintenance.ShoppingCentre * size;
            _income = (double)BuildingProfit.ShoppingCentre * size;
            _oneRentTime = (int)BuildingRentTime.ShoppingCentre;
        }
    }
}


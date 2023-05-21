using System;
using CustomCollections;

namespace GameMechanics
{
    public abstract class City
    {
        protected int _value; // [0, 100]

        SLList<Land> _lands = new();

        public City(int value)
        {
            _value = value;
        }
    }


    public class SCity : City
    {
        public SCity(int value) : base(value)
        {

        }
    }


    public class MCity : City
    {
        public MCity(int value) : base(value)
        {

        }
    }


    public class LCity : City
    {
        public LCity(int value) : base(value)
        {

        }
    }
}
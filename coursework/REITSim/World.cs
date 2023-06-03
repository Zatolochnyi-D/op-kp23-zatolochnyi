using System;
using CustomCollections;

namespace GameMechanics
{
    public class World
    {
        public static readonly Random Random = new();

        protected Player _player;
        protected SLList<City> _cities;

        public Player Player => _player;
        public SLList<City> Cities => _cities;

        public World(string name)
        {
            _player = new(name);

            _cities = new();
            _cities.Add(new());
            _cities.Add(new());
            _cities.Add(new());
        }

        public void NextTurn()
        {
            _player.NextTurn();
        }
    }
}
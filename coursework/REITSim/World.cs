using System;
using CustomCollections;

namespace GameMechanics
{
    public class World
    {
        public static readonly Random Random = new();

        protected Player _player;

        public Player Player => _player;

        public World(string name)
        {
            _player = new(name);
        }

        public void NextTurn()
        {

        }
    }
}
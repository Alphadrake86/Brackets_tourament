using System;

namespace BracketsBrackets
{
    class BracketPlayer
    {
        public string name;
        public int[] games;

        public BracketPlayer(string name, int[] games)
        {
            this.name = name;
            this.games = games;
        }

        public override bool Equals(object obj)
        { 
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BracketPlayer p = (BracketPlayer)obj;
                return (name == p.name) && (games == p.games);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, games);
        }

        public override string ToString()
        {
            return name;
        }
    }
}

using System;

namespace BracketsBrackets
{
    class BracketGame
    {
        public BracketPlayer p1;
        public BracketPlayer p2;

        public BracketGame(BracketPlayer player1, BracketPlayer player2)
        {
            this.p1 = player1;
            this.p2 = player2;
        }

        /// <summary>
        /// Takes the game number(1,2,3) and determines which player wins)
        /// </summary>
        public BracketPlayer GetWinner(int game)
        {
            return p1.games[game - 1] > p2.games[game - 1] ? p1 : p2;
        }

        /// <summary>
        /// Works just like GetWinner, but returns the loser. 
        /// Only helpful on the final game when displaying who recieves 10$
        /// </summary>
        public BracketPlayer GetLoser(int game)
        {
            return p1.games[game - 1] < p2.games[game - 1] ? p1 : p2;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BracketGame g = (BracketGame)obj;
                return (p1 == g.p1) && (p2 == g.p2) || (p1 == g.p2) && (p2 == g.p1);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(p1, p2);
        }

        public override string ToString()
        {
            return p1.ToString() + " " + p2.ToString();
        }
    }
}

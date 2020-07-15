using System;
using System.Collections.Generic;
using System.Text;

namespace BracketsBrackets
{
    class Bracket
    {
        public static Random random;

        private BracketGame[] Games;

        public int NumOfPlayers { get; private set; }
        public int MaxPlayers { get; set; }

        static Bracket()
        {
            random = new Random();
        }

        public bool IsFull()
        {
            return NumOfPlayers < MaxPlayers;
        }

        public void AddGame(BracketGame game)
        {
            Games[NumOfPlayers] = game;
            NumOfPlayers++;
        }


        
    }
}

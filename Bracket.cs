using System;
using System.Collections.Generic;

namespace BracketsBrackets
{
    class Bracket
    {
        private static Random rng = new Random();


        public List<BracketPlayer> Players { get; private set; }
        public List<BracketGame> Games { get; private set; }

        public int NumOfPlayers { get; private set; }
        public int MaxPlayers { get; set; }

        

        public Bracket(int max)
        {
            MaxPlayers = max;
            Players = new List<BracketPlayer>();
            Games = new List<BracketGame>();
        }


        public bool IsFull()
        {
            return NumOfPlayers < MaxPlayers;
        }

        public void AddPlayer(BracketPlayer player)
        {
            Players.Add(player);
            NumOfPlayers++;
        }

        public void Populate()
        {
            if(NumOfPlayers == MaxPlayers)
            {
                Shuffle();
                for (int i = 0; i < Players.Count; i+=2)
                {
                    Console.WriteLine(Players[i].name);
                    Console.WriteLine(Players[i+1].name);
                    Games.Add(new BracketGame(Players[i], Players[i + 1]));
                }
            }
        }

        public BracketGame GetWinners()
        {
            return GetWinners( new List<BracketGame>(Games), 1);
        }

        private BracketGame GetWinners(List<BracketGame> gamesTemp, int gameNum)
        {
            if (gamesTemp.Count == 1) return gamesTemp[0];
            else
            {
                List<BracketPlayer> playersTemp = new List<BracketPlayer>();
                foreach (BracketGame game in gamesTemp)
                {
                    playersTemp.Add(game.GetWinner(gameNum));
                }
                gamesTemp.Clear();
                for (int i = 0; i < playersTemp.Count; i+=2)
                {
                    gamesTemp.Add(new BracketGame(playersTemp[i], playersTemp[i + 1]));
                }
                return GetWinners(gamesTemp, gameNum++);
            }
        }

        public bool IsSeeded(BracketGame bracket)
        {
            return Games.Contains(bracket);
        }

        // the Fisher-Yates unsorting algorithm
        public void Shuffle()
        {
            int n = Players.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                BracketPlayer value = Players[k];
                Players[k] = Players[n];
                Players[n] = value;
            }
        }
    }
}

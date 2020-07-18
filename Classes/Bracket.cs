using System;
using System.Collections.Generic;

namespace BracketsBrackets
{
    class Bracket
    {
        
        public List<BracketPlayer> Players { get; private set; }
        public List<BracketGame> Games { get; private set; }

        
        public int MaxGames { get; set; }

        

        public Bracket(int max)
        {
            MaxGames = max;
            Players = new List<BracketPlayer>();
            Games = new List<BracketGame>();
        }


        public bool IsFull()
        {
            return Games.Count >= MaxGames;
        }

        

        public void AddGame(BracketGame game)
        {
            Games.Add(game);
            Players.Add(game.p1);
            Players.Add(game.p2);
            
        }

        

        public BracketGame GetWinners()
        {
            return GetWinners( new List<BracketGame>(Games), 1);
        }

        private BracketGame GetWinners(List<BracketGame> gamesTemp, int gameNum)
        {
            //Console.Write("| ");
            if (gamesTemp.Count == 1)
            {
                //Console.WriteLine(gamesTemp[0].p1 + ", " + gamesTemp[0].p2 + " |");
                return gamesTemp[0];
            }
            else
            {
                List<BracketPlayer> playersTemp = new List<BracketPlayer>();
                foreach (BracketGame game in gamesTemp)
                {
                    //Console.Write(game.p1 + ", " + game.p2 + " |");
                    playersTemp.Add(game.GetWinner(gameNum));
                }
                gamesTemp.Clear();
                for (int i = 0; i < playersTemp.Count; i += 2)
                {
                    gamesTemp.Add(new BracketGame(playersTemp[i], playersTemp[i + 1]));
                }
                //Console.WriteLine();
                return GetWinners(gamesTemp, gameNum++);
            }
        }

        public bool IsSeeded(BracketGame bracket)
        {
            return Games.Contains(bracket);
        }

        public bool IsEligible(BracketGame bracket)
        {
            return !(Players.Contains(bracket.p1) || Players.Contains(bracket.p2));
        }

        public bool HasPlayer(BracketPlayer player)
        {
            return Players.Contains(player);
        }

        
    }
}

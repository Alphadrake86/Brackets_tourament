using System;
using System.Collections.Generic;

namespace BracketsBrackets
{
    class Bracket
    {
        
        public List<BracketPlayer> Players { get; private set; }
        public List<BracketGame> Games { get; private set; }

        
        public int MaxGames { get; set; }

        public int MaxRounds { get; private set; }

        

        public Bracket(int max)
        {
            MaxGames = max;
            Players = new List<BracketPlayer>();
            Games = new List<BracketGame>();

            // the number of games should be a power of two. the +1
            // is to account for the fact that there are 2 players per game
            MaxRounds = (int) Math.Log2(MaxGames) + 1;
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

        public void PrintGame()
        {
            for (int i = 0; i < MaxRounds; i++)
            {
                PrintRound(i);
            }
        }

        private List<BracketGame> GetRound(int round)
        {
            
            if (round == 0)
            {
                return new List<BracketGame>(Games);
            }

            List<BracketGame> lastRound = GetRound(round - 1);
            List<BracketGame> thisRound = new List<BracketGame>();

            for (int i = 0; i < lastRound.Count; i+=2)
            {
                thisRound.Add(new BracketGame(lastRound[i].GetWinner(round), lastRound[i + 1].GetWinner(round)));
            }

            return thisRound;
        }

        private void PrintRound(int round)
        {
            foreach (BracketGame bracket in GetRound(round))
            {
                bool p1win = bracket.GetWinner(round + 1) == bracket.p1;
                Console.Write($"|{(p1win?"*":"")}{bracket.p1.name} ({bracket.p1.games[round]}), " +
                    $"{(p1win ? "" : "*")}{bracket.p2.name} ({bracket.p2.games[round]})|");
            }
            Console.WriteLine();
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BracketsBrackets
{
    class Bracket
    {
        public static Random random;

        private ArrayList Games;

        public int NumOfPlayers { get; private set; }
        public int MaxPlayers { get; set; }

        static Bracket()
        {
            random = new Random();
        }

        public Bracket(int max)
        {
            MaxPlayers = max;
        }


        public bool IsFull()
        {
            return NumOfPlayers < MaxPlayers;
        }

        public void AddGame(BracketGame game)
        {
            Games.Add(game);
            NumOfPlayers++;
        }

        public BracketGame GetWinners()
        {
            return GetWinners( new ArrayList(Games), 1);
        }

        private BracketGame GetWinners(ArrayList gamesTemp, int gameNum)
        {
            if (gamesTemp.Count == 1) return (BracketGame)gamesTemp[0];
            else
            {
                ArrayList playersTemp = new ArrayList();
                foreach (BracketGame game in gamesTemp)
                {
                    playersTemp.Add(game.GetWinner(gameNum));
                }
                gamesTemp.Clear();
                for (int i = 0; i < playersTemp.Count; i+=2)
                {
                    gamesTemp.Add(new BracketGame((BracketPlayer)playersTemp[i], (BracketPlayer)playersTemp[i + 1]));
                }
                return GetWinners(gamesTemp, gameNum++);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BracketsBrackets
{
    class BracketsTourney
    {
        private List<Bracket> Brackets;
        private Dictionary<BracketPlayer, int> Players;

        private int TourneySize;
        private int PlayersPerBracket;

        private int GAMES_IN_SINGLES = 4;
        private int GAMES_IN_DOUBLES = 2;


        public BracketsTourney(bool isDoubles)
        {
            Brackets = new List<Bracket>();
            Players = new Dictionary<BracketPlayer, int>();

            TourneySize = isDoubles ? GAMES_IN_DOUBLES : GAMES_IN_SINGLES; // Doubles brackets have only 2 games per bracket

            PlayersPerBracket = TourneySize * 2; // 2 players per game
        }

        public List<BracketGame> GetAllWinners()
        {
            List<BracketGame> winners = new List<BracketGame>();

            Brackets.ForEach( b => winners.Add( b.GetWinners() ));

            return winners;
        }

        public void AddEntry(BracketPlayer player)
        {
            if (Players.ContainsKey(player))
            {
                Players[player]++;
            }
            else
            {
                Players.Add(player, 1);
            }
        }

        public void RemoveEntry(BracketPlayer player)
        {
            if (Players.TryGetValue(player, out int entries))
            {
                if (entries == 1)
                {
                    Players.Remove(player);
                }
                else
                {
                    Players[player]--;
                }
            }
        }

        public void AddPlayersFromFile(string[] strings)
        {
            foreach (string s in strings)
            {
                // each string is structured as a space-delimited list, like so: 
                // name int1 int2 int3
                // with either 3 or 2 ints

                string name = s.Split(' ')[0]; // gets the first string, the name.

                string[] nums = s.Substring(s.IndexOf(' ') + 1).Split(' '); //ignores the first string, then spits the scores into an array
                int[] scores = new int[nums.Length];

                for (int i = 0; i < nums.Length; i++)
                {

                    scores[i] = Convert.ToInt32(nums[i]);
                }


                AddPlayer(new BracketPlayer(name, scores));

            }
        }

        private List<BracketPlayer> GetValidMatchups(BracketPlayer player)
        {
            return Players.Where(x => x.Value > 0)
                .Where(x => x.Key != player)
                .Select(x => x.Key).ToList();
        }

        #region fullness methods

        /// <summary>
        /// Determines the number of people needed to fill out the brackets
        /// </summary>
        /// <returns></returns>
        public int GetEmptySlots()
        {
            int reqdPlayers = GetReqdBrackets() * PlayersPerBracket;

            return reqdPlayers - GetTotalPlayers();
        }

        /// <summary>
        /// gets the total number of bracket entries, duplicates included
        /// </summary>
        /// <returns></returns>
        private int GetTotalPlayers()
        {
            return Players.Values.Sum();
        }

        /// <summary>
        /// Gets the number of brackets required based off of the number of participants 
        /// and the number of entries each participant has signed up for
        /// </summary>
        /// <returns></returns>
        private int GetReqdBrackets()
        {
            int bracketsByPlayers = (int) Math.Ceiling(GetTotalPlayers() / 8.0);

            int bracketsBySignUps = Players.Values.Max();

            return Math.Max(bracketsByPlayers, bracketsBySignUps);
            
        }

        #endregion
    }


}

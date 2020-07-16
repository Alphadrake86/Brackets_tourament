using System;
using System.Collections.Generic;
using System.Text;

namespace BracketsBrackets
{
    class BracketsTourney
    {
        private List<Bracket> Brackets;
        private List<BracketPlayer> Players;

        public BracketsTourney()
        {
            Brackets = new List<Bracket>();
        }

        public List<BracketGame> GetAllWinners()
        {
            List<BracketGame> winners = new List<BracketGame>();

            foreach (Bracket b in Brackets)
            {
                winners.Add(b.GetWinners());
            }

            return winners;
        }

        public void AddPlayer(BracketPlayer player)
        {
            Players.Add(player);
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

    }
}

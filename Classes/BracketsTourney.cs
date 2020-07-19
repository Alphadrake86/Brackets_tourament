using System;
using System.Collections.Generic;
using System.Linq;


namespace BracketsBrackets
{
    class BracketsTourney
    {
        private List<Bracket> Brackets;
        private PlayerList Players;
        private static Random rng = new Random();

        private int TourneySize;
        private int PlayersPerBracket;

        private int GAMES_IN_SINGLES = 4;
        private int GAMES_IN_DOUBLES = 2;
        private int MAX_TRIES_BEFORE_FAIL = 1000;


        public BracketsTourney(bool isDoubles)
        {
            Brackets = new List<Bracket>();
            Players = new PlayerList();

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
            Players.AddEntry(player);
        }

        public void RemoveEntry(BracketPlayer player)
        {
            Players.RemoveEntry(player);
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


                AddEntry(new BracketPlayer(name, scores));



            }
        }

        public void GenerateTournament()
        {
            

            if (GetEmptySlots() > 0)
            {
                Console.WriteLine(GetEmptySlots() + " more bowlers required");
                return;
            }

            PlayerList PlayerTemp;
            List<BracketGame> Games = new List<BracketGame>();
            bool failing;
            int tries = 0;
            do
            {
                
                    failing = false;
                    PlayerTemp = new PlayerList(Players);
                    Games.Clear();

                while (PlayerTemp.GetTotalEntries() != 0)
                {
                    PlayerTemp.GetTopPlayer(out BracketPlayer player1, out int entries);

                    List<BracketPlayer> validPlayers = PlayerTemp.GetValidMatchups(player1);

                    for (int i = 0; i < entries; i++)
                    {
                        if (validPlayers.Count == 0)
                        {
                            failing = true;
                            break;
                        }
                        BracketPlayer player2 = validPlayers[rng.Next(validPlayers.Count)];



                        Games.Add(new BracketGame(player1, player2));
                        PlayerTemp.RemoveEntry(player1);
                        PlayerTemp.RemoveEntry(player2);
                        validPlayers.Remove(player2);
                    }

                    if (failing) break;
                }
                
                

                if (!GenerateBrackets(Shuffle(Games)))
                {
                    failing = true;
                    
                }


                if (failing)
                {
                    if (++tries > MAX_TRIES_BEFORE_FAIL)
                    {
                        throw new SeedingFailedException("Too many iterations. Possible Impossible situation.");
                    }

                    continue;
                }



            } while (failing);

            
        }

        
        private bool GenerateBrackets(List<BracketGame> games)
        {
            Brackets.Clear();

            for (int i = 0; i < GetReqdBrackets(); i++)
            {
                Brackets.Add(new Bracket(TourneySize));
            }

            foreach (BracketGame game in games)
            {
                bool addWorked = TryAddGame(game);
                if (!addWorked) return false;
            }

            foreach (Bracket bracket in Brackets)
            {
                if (!bracket.IsFull()) return false;
            }
            return true;
            
        }

        /// <summary>
        /// places the seeded players into brackets. if it gets stuck, returns false.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private bool TryAddGame(BracketGame game)
        {
            foreach (Bracket bracket in Brackets)
            {
                
                if(bracket.IsEligible(game) && !bracket.IsFull())
                {
                    bracket.AddGame(game);
                    return true;
                }
            }


            return false;
        }
        
        public List<BracketGame> Shuffle(List<BracketGame> game)

        {

            int n = game.Count;

            while (n > 1)

            {

                n--;

                int k = rng.Next(n + 1);

                BracketGame value = game[k];

                game[k] = game[n];

                game[n] = value;

            }
            return game;
        }


        /// <summary>
        /// Determines the number of people needed to fill out the brackets
        /// </summary>
        /// <returns></returns>
        public int GetEmptySlots()
        {
            int reqdPlayers = GetReqdBrackets() * PlayersPerBracket;

            return reqdPlayers - Players.GetTotalEntries();
        }

        /// <summary>
        /// Gets the number of brackets required based off of the number of participants 
        /// and the number of entries each participant has signed up for
        /// </summary>
        /// <returns></returns>
        private int GetReqdBrackets()
        {
            int bracketsByPlayers = (int) Math.Ceiling(Players.GetTotalEntries() / 8.0);

            int bracketsBySignUps = Players.GetMaxEntries();

            return Math.Max(bracketsByPlayers, bracketsBySignUps);
            
        }

    }

    class PlayerList
    {
        public Dictionary<BracketPlayer, int> Players;

        public PlayerList()
        {
            Players = new Dictionary<BracketPlayer, int>();
        }

        public PlayerList(PlayerList p)
        {
            Players = new Dictionary<BracketPlayer, int>(p.Players);
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

        /// <summary>
        /// gets the total number of bracket entries, duplicates included
        /// </summary>
        /// <returns></returns>
        public int GetTotalEntries()
        {
            return Players.Values.Sum();
        }

        public int GetMaxEntries()
        {
            return Players.Values.Max();
        }

        public List<BracketPlayer> GetValidMatchups(BracketPlayer player)
        {
            var a = Players.Where(x => x.Value > 0)
                .Select(x => x.Key).ToList();

            a.Remove(player);
            return a;
        }

        public void GetTopPlayer(out BracketPlayer player, out int entries)
        {
            entries = Players.Values.Max();
            player = Players.First(x => x.Value == Players.Values.Max()).Key;

        }
    }

    /// <summary>
    /// a simple exception that makes the seeding a little bit easier to read.
    /// </summary>
    class SeedingFailedException : Exception
    {
        

        public SeedingFailedException(string message)
            :base(message)
        {

        }
    }
}

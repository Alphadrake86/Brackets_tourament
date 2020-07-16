using System;

namespace BracketsBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] players = System.IO.File.ReadAllLines("tournament.txt");
            Bracket B = new Bracket(8);

            // normally, the BracketTourney class would handle this, but it doesn't exist yet.
            foreach (string s in players)
            {
                // each string is structured as a space-delimited list, like so: 
                // name int1 int2 int3
                // with either 3 or 2 ints
                
                string name = s.Split(' ')[0]; // gets the first string, the name.

                string[] nums = s.Substring(s.IndexOf(' ')+1).Split(' '); //ignores the first string, then spits the scores into an array
                int[] scores = new int[nums.Length];

                for (int i = 0; i < nums.Length; i++)
                {
                    
                    scores[i] = Convert.ToInt32(nums[i]);
                }

                
                B.AddPlayer(new BracketPlayer(name, scores));

            }

            B.Populate();

            BracketGame winners = B.GetWinners();

            Console.WriteLine("First Place: " + winners.GetWinner(3).name);

            Console.WriteLine("Second Place: " + winners.GetLoser(3).name);

        }

        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace BracketsBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] players = System.IO.File.ReadAllLines("tournament.txt");
            Bracket B = new Bracket(8);

            foreach (string s in players)
            {
                // each string is structured as a space-delimited list, like so: 
                // name int1 int2 int3
                
                string name = s.Split(' ')[0]; // gets the first string, the name.

                string[] nums = s.Substring(s.IndexOf(' ')+1).Split(' ');
                int[] scores = new int[nums.Length];

                for (int i = 0; i < nums.Length; i++)
                {
                    Console.WriteLine(nums[i]);
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

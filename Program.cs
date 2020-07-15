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
                string temp = s + " ";
                string name = temp.Substring(0, temp.IndexOf(" "));
                temp = temp.Substring(temp.IndexOf(" ")+1);
                int[] nums = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    
                    nums[i] = Convert.ToInt32( temp.Substring(0, temp.IndexOf(" ")));
                    temp = temp.Substring(temp.IndexOf(" ") + 1);
                }
                B.AddPlayer(new BracketPlayer(name, nums));

            }

            B.Populate();

            BracketGame winners = B.GetWinners();

            Console.WriteLine("First Place: " + winners.GetWinner(3).name);

            Console.WriteLine("Second Place: " + winners.GetLoser(3).name);

        }

        
    }
}

using System;
using System.IO;

namespace BracketsBrackets
{
    class Program
    {
        static void Main(string[] args)
        { 
            string[] players = File.ReadAllLines("tournament.txt");

            BracketsTourney bracketsTourney = new BracketsTourney(false);

            bracketsTourney.AddPlayersFromFile(players);

            bracketsTourney.GenerateTournament();

            //foreach (BracketGame winners in bracketsTourney.GetAllWinners())
            //{
            //    Console.WriteLine("First Place:  " + winners.GetWinner(3).name);

            //    Console.WriteLine("Second Place: " + winners.GetLoser(3).name);

            //    Console.WriteLine();
            //}
            for (int i = 0; i < 5000; i++)
            {
                bracketsTourney.GenerateTournament();
                bracketsTourney.GetAllWinners();
            }

            //"largeTourney.txt" // "tournament.txt"
        }

    }
}

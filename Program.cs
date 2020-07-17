using System;

namespace BracketsBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] players = System.IO.File.ReadAllLines("tournament.txt");

            BracketsTourney bracketsTourney = new BracketsTourney(false);

            bracketsTourney.AddPlayersFromFile(players);

            bracketsTourney.GenerateTournament();

            foreach (BracketGame winners in bracketsTourney.GetAllWinners())
            {
                Console.WriteLine("First Place: " + winners.GetWinner(3).name);

                Console.WriteLine("Second Place: " + winners.GetLoser(3).name);
            }
            Console.ReadLine();
        }
     
    }
}

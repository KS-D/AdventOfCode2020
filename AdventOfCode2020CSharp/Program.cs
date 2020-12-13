using System;
using System.Data;
using System.Threading;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayElevenSolution sol11 = new();
            //var input = sol11.GetInput("day11_test1.txt");
            //var input = sol11.GetInput("day11_test.txt");
            var input = sol11.GetInput("day11.txt");
            char[][] seats = sol11.GetSeats(input);
            
            int prevCount = 0;
            int currCount = -1;
            while (prevCount != currCount)
            {
               seats = sol11.FillSeats(seats);
               prevCount = currCount;
               currCount = sol11.CountFullSeats(seats);
            }
            
            //2251 is correct for my data
            Console.WriteLine($"full seats: {sol11.CountFullSeats(seats)}");

            //Console.WriteLine(prevCount);
            seats = sol11.GetSeats(input);
            //int row = 4;
            //int col = 3;
            //Console.WriteLine($"Row: {row} Col: {col}");
            //int i = sol11.GenerateLineOfSite(row, col, seats.Length, seats[0].Length, seats);
            //Console.WriteLine($"Seats: {i}");

            PrintSeats(seats, "seats 0");
            int count2 = 1;
            prevCount = 0;
            currCount = -1;
            while (prevCount != currCount)
            {
                seats = sol11.FillSeatsPart2(seats);
                
                PrintSeats(seats, $"seats {count2}");
                count2++;
                prevCount = currCount;
                currCount = sol11.CountFullSeats(seats);
            }
            
            // correct answer 2019
            Console.WriteLine($"Part 2 full seats: {currCount}");
        }

        public static void PrintSeats(char[][] seats, string title)
        {
            Console.WriteLine($"{title}: ");
            foreach (var row in seats)
            {
                Console.WriteLine(row);
            }
        }
    }
}

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

            seats = sol11.GetSeats(input);
            
            PrintSeats(seats, "seats 0");
            int count2 = 1;
            prevCount = 0;
            currCount = -1;
            while (prevCount != currCount)
            {
                seats = sol11.FillSeatsPart2(seats);
#if DEBUG                
                PrintSeats(seats, $"seats {count2}");
#endif
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

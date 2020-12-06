using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayFiveSolution sol5 = new();
           
            int highestSeatId = sol5.FindHighestSeatId();
            Console.WriteLine($"Highest Seat Id: {highestSeatId}");

            List<int> ids = sol5.GetAllSeatIds();
            int mySeat = sol5.FindMySeat(ids);
            Console.WriteLine($"My Seat is:{mySeat}");
            
            
        }
    }
}

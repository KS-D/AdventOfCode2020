using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DaySevenSolution sol7 = new();
            
            List<string> bagRules = sol7.GetInput("day7.txt");
            var bagsHold = sol7.BagHolds(bagRules);
            var heldBag = sol7.BagsHeldBy(bagsHold);
            
            var set = sol7.PathsToColor("shiny gold", heldBag);
            // answer was 326
            Console.WriteLine($"Count: {set.Count}");
            // answer was 5635
            int bagsInside = sol7.BagsInside("shiny gold", bagsHold);
            Console.WriteLine($"Bags inside of shiny gold: {bagsInside}");
        }
    }
}

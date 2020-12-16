using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayFifteenSolution sol15 = new();
            
            // test data result part 1
            Console.WriteLine(sol15.NumberGame("0,3,6"));
            // part 1 
            // correct answer 1085
            Console.WriteLine(sol15.NumberGame("1,20,11,6,12,0"));
            
            //part 2 test data
            var watch = new Stopwatch();
            watch.Start();
            var lastNumber = sol15.NumberGame("0,3,6", 30000000);
            watch.Stop();
            Console.WriteLine(lastNumber);
            Console.WriteLine($"Time elapsed: {watch.Elapsed.TotalSeconds} s");
 
            watch.Reset();
            watch.Start();
            lastNumber = sol15.NumberGame("1,20,11,6,12,0", 30000000);

            watch.Stop();
            // part 2 answer
            // Correct answer is 10652
            Console.WriteLine(lastNumber);
            
            Console.WriteLine($"Time elapsed: {watch.Elapsed.TotalSeconds} s");
           
        }
    }
}

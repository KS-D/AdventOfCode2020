using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayThirteenSolution sol13 = new();

            //sol13.GetInput("day13_test.txt");
            sol13.GetInput("day13.txt");
            Console.WriteLine($"{sol13.FindRemainder()}");
            
            //
            Console.WriteLine(sol13.FindTimeStampCRTCon());
        }
    }
}

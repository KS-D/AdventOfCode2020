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

            
            sol13.GetInput("day13_test.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            sol13.GetInput("day13_test2.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            sol13.GetInput("day13_test3.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            sol13.GetInput("day13_test4.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            sol13.GetInput("day13_test5.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            sol13.GetInput("day13_test6.txt");
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            
            
            sol13.GetInput("day13.txt");
            //422461916 is to low 
            Console.WriteLine(sol13.FindTimeStampCRTCon());
            

        }
    }
}

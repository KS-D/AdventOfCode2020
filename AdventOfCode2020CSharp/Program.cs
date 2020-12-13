using System;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayTwelveSolution sol12 = new();

            sol12.GetDirections("day12.txt");
            //sol12.GetDirections("day12_test.txt");
            sol12.Navigate();
            
            // 1106 is the correct answer for part 1
            Console.WriteLine($"Manhattan Distance: {sol12.ManhattanDistance()}");
            Console.WriteLine($"East: {sol12.East}, North: {sol12.North}");

            sol12 = new();
            //sol12.GetDirections("day12_test.txt");
            sol12.GetDirections("day12.txt");
            sol12.NavigateWithWayPoint();

            // 107281 is the correct answer for my data
            Console.WriteLine($"Manhattan Distance: {sol12.ManhattanDistance()}");
            Console.WriteLine($"East: {sol12.East}, North: {sol12.North}");
        }
    }
}

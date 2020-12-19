using System;
using System.Diagnostics;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DaySeventeenSolution sol17 = new();
            
            sol17.Parse("day17_test.txt");
            //sol17.Parse("day17.txt");
            
            sol17.PrintCubes(sol17.CubeLayers);

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"Boot: {i+1}");
                sol17.UpdateCubeGrid3D();
                sol17.PrintCubes(sol17.CubeLayers);
            }
            //part 1
            // 362 is the proper answer for my data
            Console.WriteLine($"Count Active: {sol17.CountActive3D(sol17.CubeLayers)}");
            sol17.CubeLayers = null; // get out of here old data
            sol17.CubeLayers = new(); // get out of here old data
            // part 2
            //sol17.Parse("day17_test.txt");
            sol17.Parse("day17.txt");
            
            sol17.HyperCube.Add(sol17.CubeLayers);
          
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"Boot: {i+1}");
                sol17.UpdateCubeGrid4D(); 
                sol17.PrintHyperCube();
            }

            

            long sumActive4D = 0;
            foreach (var cube in sol17.HyperCube)
            {
                sumActive4D += sol17.CountActive3D(cube);
            }
            
            // answer for part 2 for my data: 1980 
            Console.WriteLine($"part 2: {sumActive4D}");
            
            Console.WriteLine("end");
        }
    }
}

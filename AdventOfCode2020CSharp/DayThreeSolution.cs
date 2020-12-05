using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020CSharp
{
    class DayThreeSolution
    {
        public List<char[]> TestSlope = new List<char[]> 
        { 
            new [] { '.','.', '#', '#', '.', '.', '.', '.', '.', '.', '.' }, 
            new [] { '#', '.', '.', '.', '#', '.', '.', '.', '#', '.', '.' },
            new [] { '.', '#', '.', '.', '.', '.', '#', '.', '.', '#', '.' },
            new [] { '.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#' },
            new [] { '.', '#', '.', '.', '.', '#', '#', '.', '.', '#', '.' },
            new [] { '.', '.', '#', '.', '#', '#', '.', '.', '.', '.', '.' },
            new [] { '.', '#', '.', '#', '.', '#', '.', '.', '.', '.', '#' },
            new [] { '.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
            new [] {'#', '.', '#', '#', '.', '.', '.', '#', '.', '.', '.' },
            new [] {'#', '.', '.', '.', '#', '#', '.', '.', '.', '.', '#' },
            new [] {'.', '#', '.', '.', '#', '.', '.', '.', '#', '.', '#' }
        };

        public List<char[]> GetInput()
        {
            StreamReader sr = new("day3.txt");
            List<char[]> slope = new();

            while (!sr.EndOfStream)
            {
                string input = sr.ReadLine();
                char[] rowOfSlope = input.ToCharArray();

                slope.Add(rowOfSlope);
            }

            return slope;
        }


        public int SolveSlopeProblem(List<char[]> slope, int downChecks = 1, int rightChecks = 3)
        {
            int count = 0;
            
            //PrintLine(slope[0]);

            for (int i = downChecks, j = rightChecks; i < slope.Count; i+=downChecks, j+=rightChecks)
            {
                if (j > slope[i].Length - 1)
                {
                    j -= slope[i].Length;
                }
                
                if (slope[i][j] == '#')
                {
                   //slope[i][j] = 'X';
                   ++count;
                }
                //else
                //{
                //    slope[i][j] = 'O';
                //}
                
                //PrintLine(slope[i]);
            }

            return count;
        }

        private void PrintLine(char[] line)
        {
            foreach (var c in line)
            {
               Console.Write(c); 
            }
            Console.WriteLine();
        }
    }
}

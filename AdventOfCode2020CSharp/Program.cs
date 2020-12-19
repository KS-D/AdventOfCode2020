using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayEighteenSolution sol18 = new();

            long value = sol18.SolveEquations(new List<string> {"1", "+", "2", "*", "3", "+", "4", "*", "5", "+", "6"});
            Console.WriteLine(value);
            var postFix = sol18.GetPostFix(new List<string> {"1", "+", "2", "*", "3", "+", "4", "*", "5", "+", "6"});
            foreach (var p in postFix)
            {
                Console.Write(p + " ");
            }
            Console.WriteLine();
            
            var answer = sol18.EvaluatePostfix(postFix);
            Console.WriteLine($"{answer}");
            
            postFix = sol18.GetPostFix(new List<string> {"1", "+", "(", "2", "*", "3", ")","+", "(","4", "*", "(","5", "+", "6",")",")"});
            answer = sol18.EvaluatePostfix(postFix);
            
            Console.WriteLine($"{answer}");

            List<long> results = new();
            sol18.Parse("day18.txt");

            foreach (var equation in sol18.Input)
            {
                var postfix = sol18.GetPostFix(equation);
                results.Add(sol18.EvaluatePostfix(postfix));
            }

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            long partOneAnswer = results.Aggregate((x, y) => x+y);
            // 23507031841020 is the right answer for my data
            Console.WriteLine($"part 1: {partOneAnswer}");

            // part 2

            List<long> result2 = new();
            foreach (var equation in sol18.Input)
            {
                var postfix = sol18.GetPostFix(equation, true);
                result2.Add(sol18.EvaluatePostfix(postfix));
            }

            long partTwoAnswer = result2.Aggregate((x, y) => x+y);

            Console.WriteLine($"part 2: {partTwoAnswer}");
        }
    }
}

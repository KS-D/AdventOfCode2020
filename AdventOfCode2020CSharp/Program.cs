using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayOneSolution sol1 = new DayOneSolution();
            var expenses = sol1.GetExpenses();
            var answer = sol1.SolveExpenseReport(expenses);

            Console.WriteLine($"The answer is {answer}");

            var answer2 = sol1.SolveExpenseReport2(expenses);
            Console.WriteLine($"The answer is {answer2}");
            Console.WriteLine($"The correct answer is {272611658}");
        }
    }
}

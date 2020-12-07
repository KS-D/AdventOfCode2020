using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DaySixSolution sol6 = new();

            List<string> questions = sol6.GetQuestionInput();
            
            // 7128 is the correct answer
            Console.WriteLine($"Here are the unique answers: {sol6.SolveTotalQuestions(questions)}");
            // 3640 is to high
            Console.WriteLine($"Here are the shared group totaled: {sol6.SolveDuplicateQuestions(questions)}");
        }
    }
}

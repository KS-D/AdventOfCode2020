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
            DayNineteenSolution sol19 = new();
            
            sol19.Parse("day19_test.txt");
//            sol19.Parse("day19.txt");
            sol19.GetRules();

            Dictionary<int, List<string>> patterns = sol19.SeedPatterns();
            
            List<string> patternFor0 = sol19.PatternForKey(0, patterns);
            
            //129
            //Console.WriteLine($"{sol19.CountValidMessages(patternFor0)}");
            
            //part 2
            DayNineteenSolution sol19Part2 = new();
            sol19Part2.Parse("day19_test2.txt");
            //sol19Part2.Parse("day19.txt");
            sol19Part2.GetRules();
            
            List<int> leftRule = new List<int> { 42 };
            List<int> rightRule = new List<int> { 42, 8 };
            sol19Part2.RuleToRule[8] = new(' ', leftRule, rightRule);
            var leftRule2 = new List<int> { 42, 31 };
            var rightRule2 = new List<int> { 42, 11, 31 };
            sol19Part2.RuleToRule[11] = new(' ', leftRule2, rightRule2);
            
            Dictionary<int, List<string>> patterns2 = sol19Part2.SeedPatterns();
            
            var patternFor42 = sol19Part2.PatternForKey(42, patterns2);
            var patternFor31 = sol19Part2.PatternForKey(31, patterns2);
            var patternsFor8 = sol19Part2.GenerateSpecialCase8(8, patterns2, 3);

            var patternsFor11 = sol19Part2.GenerateSpecialCase11(11, patterns2, 1);
            Console.WriteLine("Past patterns for 11");
            patterns2 = sol19Part2.SeedPatterns();
            //patterns2.Add(8, patternsFor8.ToList());
            //patterns2.Add(11, patternsFor11.ToList());
            var validStrings = sol19Part2.PatternForKey(0, patterns2);
            
            Console.WriteLine($"{sol19Part2.CountValidMessages(validStrings)}");
            Console.WriteLine();
        }
    }
}

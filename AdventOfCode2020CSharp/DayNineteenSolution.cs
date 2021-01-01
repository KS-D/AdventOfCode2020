using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2020CSharp
{
    public class Rule {
        
        public char Letter { get; set; }
        public List<int> RuleOne { get; set; }
        public List<int> RuleTwo { get; set; }
        
        public Rule(char letter, List<int> firstRule, List<int> lastRule)
        {
            Letter = letter;
            RuleOne = firstRule;
            RuleTwo = lastRule;
        }

    }    
    
    class DayNineteenSolution
    {
        public Dictionary<int, Rule> RuleToRule { get; set; } = new();
        public List<string> Input { get; set; } = new();  
        public List<string> Rules { get; set; } = new();  
        
        public void Parse(string fileName)
        {
            using StreamReader sr = new(fileName);
            bool swap = false;
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(temp))
                {
                    swap = true;
                }
                else 
                {
                    if (swap)
                    {
                        Input.Add(temp);
                    }
                    else
                    {
                        Rules.Add(temp);
                    }
                }
            }
        }

        public void GetRules()
        {
            foreach (var rule in Rules)
            {
                var split = rule.Split(":");
                int ruleNumber = int.Parse(split[0]);

                if (split[1].Contains("|"))
                {
                    var twoRules = split[1].Split("|");

                    var oneRule = twoRules[0].Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    var twoRule = twoRules[1].Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    RuleToRule.Add(ruleNumber, new(' ', oneRule, twoRule));
                }
                else if (split[1].Contains("a"))
                {
                    RuleToRule.Add(ruleNumber, new('a', null, null));
                }
                else if (split[1].Contains("b"))
                {
                    RuleToRule.Add(ruleNumber, new('b', null, null));
                }
                else
                {
                    var splitNums = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    List<int> oneRule = splitNums.Select(int.Parse).ToList();
                    RuleToRule.Add(ruleNumber, new Rule(' ', oneRule, null));
                }
            }
        }

        public Dictionary<int, List<string>> SeedPatterns()
        {
            Dictionary<int, List<string>> initialKnownState = new();

            foreach (var rule in RuleToRule)
            {
                if (rule.Value.Letter != ' ')
                {
                    initialKnownState.Add(rule.Key, new());
                    initialKnownState[rule.Key].Add(rule.Value.Letter.ToString());
                }
            }

            return initialKnownState;
        }
       

        public List<string> PatternForKeyBigger(int rule, Dictionary<int, List<string>> patterns)
        {
            var ruleValue = RuleToRule[rule];
            List<string> rulePattern1 = new();
            List<string> rulePattern2 = new();
            foreach (var r in ruleValue.RuleOne)
            {
                int bound = rulePattern1.Count;
                rulePattern1.AddRange(patterns.TryGetValue(r, out List<string> pattern)
                    ? pattern
                    : PatternForKeyBigger(r, patterns));

                if (bound > 0)
                {
                    CreatePermutations(rulePattern1, bound);
                }
               
            }
                                      
            if (ruleValue.RuleTwo != null)
            {
                foreach (var r in ruleValue.RuleTwo)
                {
                    int bound = rulePattern2.Count;
                    rulePattern2.AddRange(patterns.TryGetValue(r, out List<string> pattern)
                        ? pattern
                        : PatternForKeyBigger(r, patterns));

                    if (bound > 0)
                    {
                        CreatePermutations(rulePattern2, bound);
                    }
                }
            }
            
            patterns.Add(rule, rulePattern1);
            patterns[rule].AddRange(rulePattern2);

            return rulePattern1;
        }


        public List<string> PatternForKey(int rule, Dictionary<int, List<string>> patterns)
        {
            var ruleValue = RuleToRule[rule];
            var list = PatternForKey(rule, ruleValue.RuleOne, patterns);
            if (ruleValue.RuleTwo != null)
            {
                list.AddRange(PatternForKey(rule, ruleValue.RuleTwo, patterns));
            }

            return list;
        }
        
        private List<string> PatternForKey(int rule,
                                           List<int> childRule,
                                           Dictionary<int, List<string>> patterns)
        {
            List<string> rulePattern = new();
            foreach (var r in childRule)
            {
                int bound = rulePattern.Count;
                rulePattern.AddRange(patterns.TryGetValue(r, out List<string> pattern)
                    ? pattern
                    : PatternForKey(r, patterns));

                if (bound > 0)
                {
                    CreatePermutations(rulePattern, bound);
                }
            }
  
            if (!patterns.ContainsKey(rule))
            {
                patterns.Add(rule, new());
            }
            patterns[rule].AddRange(rulePattern);
                                      
            return rulePattern;
        }
        
        private void CreatePermutations(List<string> patterns, 
                                        int range, 
                                        int innerStart = -1,
                                        bool trim = true)
        {
            int size = patterns.Count;
            int inner = innerStart < 0 ? range : innerStart; 
            
            for (int i = 0; i < range; i++)
            {
                for (int j = inner; j < size; j++)
                { 
                    patterns.Add(patterns[i] + patterns[j]);
                }
            }

            if (trim)
            {
                patterns.RemoveRange(0, size);
            }
        }

        public long CountValidMessages(List<string> validMessages)
        {
            long count = 0;
            foreach (var input in Input)
            {
                if (validMessages.Contains(input)) // probably would be faster if i made valid messages a dictionary
                {
                    count++;
                }
            }

            return count;
        }

        public HashSet<string> GenerateSpecialCase8(int key, Dictionary<int, List<string>> patterns, int numberOfMiddle)
        {
            List<List<string>> repeatingPatterns = new();

            List<string> temp = new();
            temp.AddRange(patterns[42]);
            temp.AddRange(patterns[42]);

            int currentLength = temp.Count;
            int middle = currentLength / 2; // this only works with 8
            int endOfOriginal = middle;
            
            // This could be an inner loop for the second special case
            for (int i = 0; i < numberOfMiddle; i++)
            {
                CreatePermutations(temp, endOfOriginal, middle, false);
                middle = currentLength;
                currentLength = temp.Count;
            }

            return temp.ToHashSet();
        }
        
        public HashSet<string> GenerateSpecialCase11(int key, Dictionary<int, List<string>> patterns, int numberOfMiddle)
        {
            var rule = RuleToRule[key];
            List<List<string>> repeatingPatterns = new();

            foreach (var r in rule.RuleTwo)
            {
                List<string> temp = new();
                if (r != key)
                {
                    temp.AddRange(patterns[r]);

                    repeatingPatterns.Add(temp);
                }
            }

            var list42 = repeatingPatterns[0];
            var list31 = repeatingPatterns[1];

            List<string> listOfCommonPermutations = new();
            listOfCommonPermutations.AddRange(repeatingPatterns[0]);
            listOfCommonPermutations.AddRange(repeatingPatterns[1]);
            CreatePermutations(listOfCommonPermutations, repeatingPatterns[0].Count);

            List<List<string>> result = new();
            result.Add(listOfCommonPermutations);
            // This could be an inner loop for the second special case
            for (int i = 0; i < numberOfMiddle; i++)
            {
                result.Add(CreatePermutationsFrontAndBack(result[i], 
                                                          list42,
                                                          list31));
            }
        

            return result.SelectMany(x => x).ToHashSet();
        }

        private List<string> CreatePermutationsFrontAndBack(List<string> sharedPermutations,
                                                            List<string> addFront,
                                                            List<string> addBack)
        {
            List<string> result = new();

            foreach (var permutation in sharedPermutations)
            {
                foreach (var front in addFront)
                {
                    result.Add(front + permutation);
                }
            }

            int originalCount = result.Count;

            for (int i = 0; i < originalCount; i++)
            {
                foreach (var back in addBack)
                {
                    result.Add(result[i] + back);
                }
            }

            result.RemoveRange(0, originalCount);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020CSharp
{
    class DaySevenSolution
    {
        public List<string> GetInput(string inputFile)
        {
            List<string> baggageRules = new();
            using StreamReader sr = new(inputFile);

            while (!sr.EndOfStream)
            {
                string rule = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    baggageRules.Add(rule);
                }
            }
            return baggageRules;
        }

        public Dictionary<string, Dictionary<string, int>> BagHolds(List<string> bagRules)
        {
            Dictionary<string, Dictionary<string, int>> bagHolds = new();
            string[] splitRules = {"contain", ","};
            Regex r = new(@"[0-9]+");
            
            foreach (var s in bagRules)
            {
                var noBag = s.Trim().Replace("bags", "")
                                          .Replace("bag","")
                                          .Replace(".", "");
                
                var parsed = noBag.Split(splitRules, StringSplitOptions.None)
                                          .Select(bag => bag.Trim()) 
                                          .ToArray();
                string key = "";
                foreach (var t in parsed)
                {
                    if (t.Contains("no other"))
                    {
                        // don't add anything
                    } 
                    else if (!r.IsMatch(t))                     
                    {
                        key = t;
                        bagHolds.Add(key, new());
                    }
                    else
                    {
                        var match = r.Match(t);
                        var numOfBags = int.Parse(match.Value);
                        var substringIndex = match.Index + match.Length + 1; // add 1 to remove the space
                        var bagType = t.Substring(substringIndex);
                        bagHolds[key].Add(bagType, numOfBags);
                    }
                }
            }
            return bagHolds;
        }

        public Dictionary<string, List<string>> BagsHeldBy(Dictionary<string, Dictionary<string, int>> bagsHold)
        {
            Dictionary<string, List<string>> heldBy = new();
            foreach (var bag in bagsHold)
            {
                foreach (var heldBag in bag.Value)
                {
                    if (!heldBy.ContainsKey(heldBag.Key))
                    {
                        heldBy.Add(heldBag.Key,new());
                    }
                    heldBy[heldBag.Key].Add(bag.Key);
                }
            }
            return heldBy;
        }

        public HashSet<string> PathsToColor(string bagColor,Dictionary<string, List<string>> bagHeldBy)
        {
            if (bagHeldBy.ContainsKey(bagColor))
            {
                HashSet<string> colors = bagHeldBy[bagColor].ToHashSet();
                foreach (var bag in bagHeldBy[bagColor])
                {
                    colors.UnionWith(PathsToColor(bag, bagHeldBy));
                }
                return colors;
            }
            return new();
        }

        public int BagsInside(string bagColor, Dictionary<string, Dictionary<string, int>> bagHolds)
        {
            if (bagHolds.ContainsKey(bagColor))
            {
                int sum = 0;
                foreach (var pair in bagHolds[bagColor])
                {
                    sum += pair.Value;
                    sum += pair.Value * BagsInside(pair.Key, bagHolds);
                }

                return sum;
            }
            return 0;
        }
    } 
}

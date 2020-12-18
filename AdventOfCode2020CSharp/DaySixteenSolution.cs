using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020CSharp
{
    class Ticket
    {
        public List<int> Fields { get; set; }

        private Ticket(List<int> ticketValues)
        {
            Fields = ticketValues;
        }

        public static Ticket ParseTicket(string ticketVales)
        {
            List<int> fields = ticketVales.Split(",")
                                          .Select(int.Parse)
                                          .ToList();
            return new Ticket(fields);
        }
    }
    
    class DaySixteenSolution
    {
        public Dictionary<string, int[]> Rules { get; set; } = new();
        public Ticket MyTicket { get; set; }
        public List<Ticket> OtherTickets { get; set; } = new();


        public void Parse(string fileName)
        {
            using StreamReader sr = new(fileName);
            Regex rule = new(@"[a-z]+ *[a-z]*: [0-9]+\-[0-9]+");

            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    if (rule.IsMatch(temp))
                    {
                        Console.WriteLine(temp);
                        var ruleSplit= temp.Split(":");
                        var key = ruleSplit[0];
                        string[] separator = {"or", "-"};
                        var splitRanges = ruleSplit[1]
                                    .Split(separator, StringSplitOptions.TrimEntries)
                                    .Select(int.Parse).ToArray();
                        Rules.Add(key, splitRanges);


                    }
                    else if (temp.Contains("your"))
                    {
                        temp = sr.ReadLine();
                        if (temp != null)
                            MyTicket = Ticket.ParseTicket(temp);
                    }
                    else if (temp.Contains("nearby"))
                    {
                        // do nothing
                    }
                    else
                    {
                        Console.WriteLine($"{temp}");
                        OtherTickets.Add(Ticket.ParseTicket(temp));
                    }
                }
            }
        }

        public HashSet<int> FindRange()
        {
            List<int> validTicketVals = new();
            foreach (var rule in Rules)
            {
                int leftBound1 = rule.Value[0];
                int rightBound1 = rule.Value[1];
                validTicketVals.AddRange(
                    Enumerable.Range(leftBound1, rightBound1 + 1 - leftBound1)
                );
                int leftBound2 = rule.Value[2];
                int rightBound2 = rule.Value[3];
                validTicketVals.AddRange(
                    Enumerable.Range(leftBound2, rightBound2 + 1 - leftBound2)
                );

            }

            return validTicketVals.ToHashSet();
        }

        public int ErrorRate(HashSet<int> range)
        {
            List<int> errors = new();
            List<Ticket> toRemove = new();
            foreach(var ticket in OtherTickets)
            {
                foreach (var field in ticket.Fields)
                {
                    if (!range.Contains(field))
                    {
                        errors.Add(field);
                        toRemove.Add(ticket); // remove invalid tickets
                    }
                }
            }
            // use linq to remove invalid tickets
            OtherTickets = OtherTickets.Where(x => !toRemove.Contains(x))
                                        .ToList();

            if (errors.Count > 1)
            {
                return errors.Aggregate((x,y) => x + y);
            }
            else if (errors.Count == 1)
            {
                return errors[0];
            }
            else
            {
                return 0;
            }
            
        }

        private Dictionary<string, List<int>> GenerateKeysAndPotentialIndices()
        {
            int rulesLength = Rules.Count;
            Dictionary<string, List<int>> ruleAndIndices = new();

            foreach (var rule in Rules.Keys)
            {
                ruleAndIndices.Add(rule, Enumerable.Range(0, rulesLength).ToList());
            }
            return ruleAndIndices;
        }

        public Dictionary<string, List<int>> FindValidFieldPosition()
        {
            Dictionary<string, List<int>> rulesAndIndices = 
                                        GenerateKeysAndPotentialIndices();

            foreach (var ticket in OtherTickets)
            {
                for (int i = 0; i < ticket.Fields.Count; i++)
                {
                    foreach (var rule in rulesAndIndices.Keys)
                    {
                        if (!CheckBounds(rule, ticket.Fields[i]))
                        {
                            rulesAndIndices[rule].Remove(i);
                        }
                    }
                }
            }

            return rulesAndIndices;
        }

        public Dictionary<string, List<int>> Solve(Dictionary<string, List<int>> trimmedIndices)
        {
            foreach (var key in trimmedIndices.Keys)
            {
                if (trimmedIndices[key].Count == 1)
                {
                    int position = trimmedIndices[key][0];
                    foreach (var pair in trimmedIndices)
                    {
                        if (pair.Value.Count > 1)
                        {
                            pair.Value.Remove(position);
                        }
                    }
                } 
            }
            
            return trimmedIndices;
        }

        public bool ContainsDuplicatePositions(Dictionary<string, List<int>> fieldAndPos)
        {
            foreach (var positions in fieldAndPos.Values)
            {
                if (positions.Count > 1)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckBounds(string field, int position)
        {
            int[] bounds = Rules[field];

            if (position >= bounds[0] && position <= bounds[1] ||
                position >=  bounds[2]  && position <= bounds[3])
            {
                return true;
            }

            return false;
        }

        public long GetDepartureProduct(Dictionary<string, List<int>> fieldAndPos)
        {
            long product = 1;
            foreach (var fieldPosition in fieldAndPos)
            {
                if (fieldPosition.Key.Contains("departure"))
                {
                    product *= MyTicket.Fields[fieldPosition.Value[0]];
                }
                    
            }
            return product;
        }
    }
}

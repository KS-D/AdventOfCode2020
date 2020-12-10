using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace AdventOfCode2020CSharp
{
    class DayEightSolution
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

        public (int accumulator, Stack<int> repeat) RunAssembly(List<string> assembly)
        {
            Regex acc = new Regex(@"acc");
            Regex jump = new Regex(@"jmp");
            
            HashSet<int> indices = new();
            Stack<int> traversedIndexes = new();
           
            int operation = 0;
            bool newIndex = true;
            int accumulator = 0;
            indices.Add(operation);
            int operationOffset = 1;
            
            while (operation < assembly.Count && newIndex)
            {
                operationOffset = 1;
                traversedIndexes.Push(operation);
                if (acc.IsMatch(assembly[operation]))
                {
                    string change = acc.Split(assembly[operation]).Single(x => x != "");
                    
                    Console.WriteLine(change);
                    int i = int.Parse(change);
                    accumulator += i;
                }
                else if (jump.IsMatch(assembly[operation]))
                {
                    string jumpOffset = jump.Split(assembly[operation]).Single(x => x != "");
                    Console.WriteLine(jumpOffset);
                    int i = int.Parse(jumpOffset);
                    operationOffset = i;

                } 
                // do nothing for nop
                operation += operationOffset;
                newIndex = indices.Add(operation);
            }

            return (accumulator, traversedIndexes);
        }

        // either a nop needs to be replaced with a jump or a jump needs to be replaced with a nop
        public void FixAssembly(List<string> assembly, Stack<int> indexes)
        {
            bool success = false;
            Regex nop = new(@"nop");
            Regex jmp = new(@"jmp");
            Regex acc = new(@"acc");

            while (indexes.Count != 0 && !success)
            {
                int index = indexes.Pop();
                string temp = assembly[index];
                var oldOperation = temp;

                if (nop.IsMatch(temp))
                {
                    temp = nop.Replace(temp, "jmp");
                }
                else if (jmp.IsMatch(temp))
                {
                    temp = jmp.Replace(temp, "nop");
                }
                
                if (!acc.IsMatch(temp))
                {
                    assembly[index] = temp;
                    (int _ , Stack<int> updatedIndex) = RunAssembly(assembly);
                    if (updatedIndex.Pop() == assembly.Count - 1)
                    {
                        success = true;
                    }
                    else
                    {
                        assembly[index] = oldOperation;
                    }
                }
            }
        }
    }
}

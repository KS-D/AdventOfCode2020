using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020CSharp
{
    class DayEighteenSolution
    {
        public Stack<string> Infix { get; set; } = new();
        public List<List<string>> Input { get; set; } = new();
        
        public void Parse(string fileName)
        {
            using StreamReader sr = new(fileName);
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    Input.Add(temp.Replace("(", "( ").Replace(")", " )").Split(" ").ToList());
                }
            }
        }

        public long SolveEquations(List<string> equation)
        {
            
            Regex re = new(@"[0-9]+");
            string op = "";
            Stack<long> stack = new();
            for (int i = 0; i < equation.Count; i++)
            {
                if (re.IsMatch(equation[i]))
                {
                    stack.Push(long.Parse(equation[i]));
                }
                else if (equation[i].Contains("("))
                {
                    
                }
                else
                {
                    op = equation[i];
                }

                if (stack.Count >= 2 && (op.Contains("*") || op.Contains("+")))
                {
                    stack.Push(ProductOrSum(stack.Pop(), stack.Pop(), op));
                    op = "";
                }
                
            }
            
            return stack.Pop();
        }

        private long ProductOrSum(long left, long right, string op)
        {
            if (op.Contains("*"))
            {
                return left * right;
            }
            else
            {
                return left + right;
            }
        }

        public List<string> GetPostFix(List<string> infix, bool plusPrecedence = false)
        {
            Regex re = new(@"[0-9]+");
            List<string> postfix = new();

            foreach (var infixValue in infix)
            {
                if (re.IsMatch(infixValue))
                {
                    postfix.Add(infixValue);
                }
                else if (infixValue.Contains('('))
                {
                    Infix.Push(infixValue);
                }
                else if (infixValue.Contains(')'))
                {
                    while (!Infix.Peek().Contains('('))
                    {
                        postfix.Add(Infix.Pop());
                    }
                    Infix.Pop();
                }
                else
                {
                    while (Infix.Count != 0 && CalculatePrecedent(Infix.Peek(), true, plusPrecedence) > CalculatePrecedent(infixValue, false, plusPrecedence))
                    {
                        postfix.Add(Infix.Pop());
                    }

                    Infix.Push(infixValue);
                }
            }

            while (Infix.Count != 0)
            {
                postfix.Add(Infix.Pop());
            }

            return postfix;
        }
        
        public int CalculatePrecedent(string c, bool onStack, bool plusPrecedence)
        {
            Dictionary<string, int> valueOnStack = new() {{"(", 0}, {")", 0}, {"*", 2}, {"+", 2}};
            Dictionary<string, int> valueNoStack = new() {{"(", 100}, {")", 0}, {"*", 1}, {"+", 1}};
            if (plusPrecedence)
            {
                valueOnStack["+"]++;
                valueNoStack["+"]++;
            }
            
            if(!onStack){ 
                return valueNoStack[c];
            }
            else{
                return valueOnStack[c];
            }
        }

        public long EvaluatePostfix(List<string> postFix)
        {
            Regex re = new(@"[0-9]+");
            Stack<long> operands = new();
            
            foreach (var post in postFix)
            {
                if (re.IsMatch(post))
                {
                    operands.Push(long.Parse(post));
                }
                else
                {
                    long result = 0;
                    string op = post;

                    var right = operands.Pop();
                    var left = operands.Pop();

                    switch (op)
                    {
                        case "+":
                            result = right + left;
                            break;
                        case "*":
                            result = right * left;
                            break;
                    }
                    operands.Push(result);
                }
            }

            if (operands.Count == 1)
            {
                return operands.Pop();
            }
            else
            {
                throw new ArgumentException("Argument is not a valid ");
            }

        } 
    }
}

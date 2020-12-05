using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    public record PasswordRequirement
    {
        public int Lower { get; }
        public int Higher { get; }
        public char Required { get; }
        public string Password { get; }

        public PasswordRequirement(int lower, int higher, char required, string password) 
                => (Lower, Higher, Required, Password) = (lower, higher, required, password);
    }

    struct DayTwoSolutions
    {
        public List<PasswordRequirement> GetInput()
        {
            StreamReader sr = new StreamReader("day2.txt");

            PasswordRequirement req = new(1, 2, 'c', "asba");
            var passwordDefinitions = new List<PasswordRequirement>();

            while (!sr.EndOfStream)
            { 
                string passwordReq = sr.ReadLine();
                if (passwordReq is not null)
                {
                    passwordDefinitions.Add(ParseInput(passwordReq));
                }
            }

            return passwordDefinitions;
        }

        private PasswordRequirement ParseInput(string input)
        {
            string[] parse = input.Split(" ");
            string[] bounds = parse[0].Split("-");
            char required = parse[1][0]; // index the first character
            string password = parse[2];

            int lower = Int32.Parse(bounds[0]);
            int higher = Int32.Parse(bounds[1]);

            PasswordRequirement passwordRequirement = new(lower, higher, required, password);
            return passwordRequirement;
        }

        public void SolveValidPassword(List<PasswordRequirement> requirements)
        {
            int count = 0;
            
            foreach (var req in requirements)
            {
                int requiredOccurance = req.Password.Count(x => x == req.Required);
                if (requiredOccurance <= req.Higher && requiredOccurance >= req.Lower)
                {
                    ++count;
                }
            }

            Console.WriteLine($"Valid Passwords: {count}");
        }

        public void SolveValidPassword2(List<PasswordRequirement> requirements)
        {
            int count = 0;

            foreach (var req in requirements)
            {
                bool charInLowerPosition = req.Password[req.Lower - 1] == req.Required;
                bool charInHigherPosition = req.Password[req.Higher - 1] == req.Required;

                if (charInLowerPosition && charInHigherPosition == false || charInLowerPosition == false && charInHigherPosition)
                {
                    ++count;
                }
            }

            Console.WriteLine($"Valid Passwords: {count}");
        }
    }

}

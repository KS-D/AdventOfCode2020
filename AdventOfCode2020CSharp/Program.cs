using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayTwoSolutions sol2;
            List<PasswordRequirement> passwordReqs = sol2.GetInput();

            sol2.SolveValidPassword(passwordReqs);
            Console.WriteLine("The correct Answer is 572");

            sol2.SolveValidPassword2(passwordReqs);
            Console.WriteLine("The correct Answer is 306");
        }
    }
}

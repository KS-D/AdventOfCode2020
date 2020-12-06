using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayFourSolutions sol4 = new ();
            
            var input = sol4.GetInput();
           
            Regex rx = new (@$"(?=.*byr\:)(?=.*iyr\:)(?=.*eyr\:)(?=.*hgt\:)(?=.*hcl\:)(?=.*ecl\:)(?=.*pid\:)");
            int count = sol4.GetValidPassports(input, rx);
          
            //Correct answer for part 1 is 242 
            Console.WriteLine($"The count of valid passports is: { count }");
            
            Regex rx2 = new(@"(?=.*byr\:((19)[2-9][0-9]|(200)[0-2]))(?=.*iyr\:((201)[0-9]|(2020)))" +
                                   @"(?=.*eyr\:((202)[0-9]|2030))(?=.*hgt\:(((1)[5-8][0-9]|19[0-3])cm|(59|6[0-9]|7[0-6])in))" +
                                   @"(?=.*hcl\:#[0-9a-fA-F]{6})(?=.*ecl\:(amb|blu|brn|gry|grn|hzl|oth))(?=.*pid\:[0-9]{9}\b)");

            count = sol4.GetValidPassports(input, rx2);
            Console.WriteLine($"The count of valid passports is: { count }");
            //Correct answer is 186
       }
    }
}

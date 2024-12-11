using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayThree
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName);

            var answer = 0;

            // Loop over lines
            foreach (string line in lines)
            {
                // Regex for finding multiplication instructions
                var pattern = @"mul\(\d{1,3},\d{1,3}\)";

                MatchCollection matches = Regex.Matches(line, pattern);

                if (matches.Count > 0)
                {
                    // Perform multiplication instruction and add result to answer
                    foreach (Match match in matches) 
                    {
                        var numberPattern = @"\d{1,3}";

                        var numbers = Regex.Matches(match.Value, numberPattern);

                        answer += Int32.Parse(numbers.First().Value) * Int32.Parse(numbers.Last().Value);
                    }
                }
            }

            // Answer is the sum of the multiplication instruction results
            return answer;
        }
    }
}
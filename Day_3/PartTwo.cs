using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayThree
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName);

            var answer = 0;

            // Loop over lines
            foreach (string line in lines)
            {
                // Removing substrings starting with don't until do found
                var firstRemovalPattern = @"don't\(\).*?do\(\)";

                var prunedLine = Regex.Replace(line, firstRemovalPattern, string.Empty, RegexOptions.Singleline);

                // Removing substring until EoL
                var secondRemovalPattern = @"don't\(\).*";

                prunedLine = Regex.Replace(prunedLine, secondRemovalPattern, string.Empty, RegexOptions.Singleline);


                // Regex for finding multiplication instructions
                var pattern = @"mul\(\d{1,3},\d{1,3}\)";

                MatchCollection matches = Regex.Matches(prunedLine, pattern);

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
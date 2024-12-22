namespace AdventOfCode.DayNineteen
{
    public class PartOne
    {
        public Dictionary<string, long> possibleCombos = [];

        private long MatchDesign(string current, List<string> patterns)
        {
            // Complete design has been recreated with patterns
            if (current.Length == 0)
            {
                return 1;
            }

            if (possibleCombos.TryGetValue(current, out long matches))
            {
                return matches;
            }

            long numberOfCombos = 0;

            foreach (var pattern in patterns.SkipWhile(x => x.Length > current.Length))
            {
                if (current.StartsWith(pattern))
                {
                    numberOfCombos += MatchDesign(current[pattern.Length..], patterns);
                }
            }

            possibleCombos.TryAdd(current, numberOfCombos);

            return numberOfCombos;
        }

        public long GetAnswer(string fileName, int byteCount = 1024, int maxRange = 70)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            var patterns = lines[0].Split(", ").OrderByDescending(x => x.Length).ToList();

            // Loop over the designs
            for (int i = 2; i < lines.Length; i++)
            {
                long matches = MatchDesign(lines[i], patterns);

                if (matches > 0)
                {
                    answer += 1;
                }
            }

            // Answer is the number of designed that could be recreated
            return answer;
        }
    }
}
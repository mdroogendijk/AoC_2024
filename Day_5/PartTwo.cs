namespace AdventOfCode.DayFive
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var brokenUpdates = new List<string>(); 

            var rules = lines
                .Where(x => x.Contains('|'))
                .Select(rule => rule.Split('|'))
                .Select(items => new
                {
                    Key = Int32.Parse(items[0]),
                    Value = Int32.Parse(items[1])
                })
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key);

            var updates = lines.Where(x => x.Contains(','));

            foreach (var update in updates)
            {
                var pageNumbers = update.Split(',').Select(Int32.Parse);

                var valid = true;

                // Check rules against preceding pages in update
                for (int i = pageNumbers.Count() - 1; i >= 1; i--)
                {
                    var current = pageNumbers.ElementAt(i);
                    var rulesFound = rules.TryGetValue(current, out var applicableRules);

                    if (applicableRules != null)
                    {
                        // Rule was broken by preceding page number
                        if (pageNumbers
                            .Take(i)
                            .Any(applicableRules
                            .Select(x => x.Value)
                            .Contains))
                        {
                            valid = false;
                            break;
                        }
                    }

                }

                if (!valid)
                {
                    // Collect updates that need fixing
                    brokenUpdates.Add(update);
                }
            }

            foreach (var brokenUpdate in brokenUpdates)
            {
                var pageNumbers = brokenUpdate.Split(',').Select(Int32.Parse).ToList();

                var originalCount = pageNumbers.Count;

                var swapsNeeded = true;

                // Continue until update is correctly ordered
                while (swapsNeeded)
                {
                    swapsNeeded = false;

                    // Check rules against preceding pages in update
                    for (int i = pageNumbers.Count - 1; i >= 1; i--)
                    {

                    Again:
                        var current = pageNumbers.ElementAt(i);
                        var rulesFound = rules.TryGetValue(current, out var applicableRules);

                        for (int j = i - 1; j >= 0; j--)
                        {
                            var previous = pageNumbers.ElementAt(j);

                            if (applicableRules != null && applicableRules.Any(x => x.Value == previous))
                            {
                                // Swap places
                                pageNumbers[i] = previous;
                                pageNumbers[j] = current;

                                swapsNeeded = true;

                                i = 0;
                                goto Again;
                            }
                        }
                    }
                }

                answer += pageNumbers.ElementAt(pageNumbers.Count / 2);
            }
            // Answer is the sum of XMAS words found
            return answer;
        }
    }
}
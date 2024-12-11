namespace AdventOfCode.DaySeven
{
    public class PartOne
    {
        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            foreach (var line in lines)
            {
                var testValue = long.Parse(line.Split(':').First());

                var numbers = line
                    .Split(':')
                    .Last()
                    .TrimStart()
                    .Split(' ')
                    .Select(long.Parse)
                    .ToList();

                var currentValues = new Dictionary<int, List<long>>
                {
                    { 0, [numbers.First()] }
                };

                for (int i = 1; i < numbers.Count; i++)
                {
                    var nextValues = new List<long>();

                    // Add and multiply previous results with the next number
                    for (int j = 0; j < currentValues.Last().Value.Count; j++)
                    {
                        nextValues.Add(currentValues.Last().Value[j] + numbers[i]);
                        nextValues.Add(currentValues.Last().Value[j] * numbers[i]);
                    }

                    currentValues.Add(i, nextValues);
                }

                // Add to sum if at least one of the calculations matches
                if (currentValues.Last().Value.Contains(testValue))
                {
                    answer += testValue;
                }
            }

            // Answer is the sum of test values with matching calculations
            return answer;
        }
    }
}
namespace AoC_2024.Day_1
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            int answer = 0;
            var leftList = new List<int>();
            var rightList = new List<int>();

            var lines = File.ReadLines(fileName);
            int linesCount = lines.Count();

            // Loop over lines
            for (int count = 0; count < linesCount; count++)
            {
                var line = lines.ElementAt(count).Split(' ');

                leftList.Add(Convert.ToInt32(line.First()));

                rightList.Add(Convert.ToInt32(line.Last()));
            }

            // Calculating and multiplying occurrences in right list and summing result
            foreach (var number in leftList)
            {
                answer += number * rightList
                                    .Where(x => x == number)
                                    .Count();
            }

            return answer;
        }
    }
}
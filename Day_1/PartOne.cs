using System.Diagnostics.Metrics;

namespace AoC_2024.Day_1
{
    public class PartOne
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

            // Sorting lists and calculating differences
            leftList.Sort();
            rightList.Sort();

            for (int count = 0; count < leftList.Count; count++)
            {
                answer += Math.Abs(leftList.ElementAt(count) - rightList.ElementAt(count));
            }

            return answer;
        }
    }
}
namespace AoC_2024.Day_2
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            int answer = 0;

            var lines = File.ReadLines(fileName);

            // Loop over lines
            foreach (string line in lines)
            {
                var reports = Array.ConvertAll(line.Split(' '), int.Parse).ToList();

                // Only add reports where all increments are between 1 and 3
                var pairs = Enumerable.Zip(reports, reports.Skip(1));

                if (pairs.All(x => (x.Second - x.First) >= 1 && (x.Second - x.First) <= 3))
                {
                    answer += 1;
                }
                else if (pairs.All(x => (x.First - x.Second) >= 1 && (x.First - x.Second) <= 3))
                { 
                    answer += 1;
                }
            }

            // Answer is the number of matching reports
            return answer;
        }
    }
}
namespace AoC_2024.Day_2
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            int answer = 0;

            var lines = File.ReadLines(fileName);

            // Loop over lines
            foreach (string line in lines)
            {
                var reports = Array.ConvertAll(line.Split(' '), int.Parse).ToList();

                var expandedReports = new List<List<int>>
                {
                    reports
                };

                var valid = false;

                // Add report versions with one level omitted
                for (int i = 0; i <= reports.Count; i++)
                {
                    var before = reports.Take(i - 1);
                    var after = reports.Skip(i);
                    expandedReports.Add(Enumerable.Concat(before, after).ToList());
                }

                // Add it if at least one of the expanded reports is valid
                foreach (var expandedReport in expandedReports)
                {
                    // Only add reports where all increments are between 1 and 3
                    var pairs = Enumerable.Zip(expandedReport, expandedReport.Skip(1));

                    if (pairs.All(x => (x.Second - x.First) >= 1 && (x.Second - x.First) <= 3))
                    {
                        valid = true;
                    }
                    else if (pairs.All(x => (x.First - x.Second) >= 1 && (x.First - x.Second) <= 3))
                    {
                        valid = true;
                    }
                }

                if (valid)
                {
                    answer += 1;
                }
            }

            // Answer is the number of matching reports
            return answer;
        }
    }
}
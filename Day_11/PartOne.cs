namespace AdventOfCode.DayEleven
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var arrangement = lines.First().Split(' ').Select(long.Parse).ToList();

            for (int i = 0; i < 25; i++)
            {
                var newArrangement = new List<long>();

                foreach (var stone in arrangement)
                {
                    var newStones = new List<long>();

                    var stoneDigitCount = stone.ToString().Length;

                    // Check for each stone which rule applies
                    if (stone == 0)
                    {
                        newStones.Add(1);
                    }
                    else if (stoneDigitCount % 2 == 0)
                    {
                        newStones.Add(long.Parse(stone.ToString()[..(stoneDigitCount / 2)]));
                        newStones.Add(long.Parse(stone.ToString()[(stoneDigitCount / 2)..]));
                    }
                    else 
                    {
                        newStones.Add(stone * 2024); 
                    }

                    newArrangement.AddRange(newStones);
                }

                arrangement = new List<long>(newArrangement);
            }

            answer = arrangement.Count;

            // Answer is the number of stones
            return answer;
        }
    }
}
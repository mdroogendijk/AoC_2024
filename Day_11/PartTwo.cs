namespace AdventOfCode.DayEleven
{
    public class PartTwo
    {
        public static long Blink(long initialStone)
        {
            var stones = new Dictionary<long, long>
            {
                { initialStone, 1L }
            };

            for (var i = 0; i < 75; i++)
            {
                foreach (var (key, count) in stones.ToList())
                {
                    stones[key] -= count;

                    if (key is 0L)
                    {
                        AddStone(1L, count);
                    }
                    else
                    {
                        var digits = key.ToString();

                        if (digits.Length % 2 is 1)
                        {
                            AddStone(key * 2024, count);
                        }
                        else
                        {
                            AddStone(long.Parse(digits[..(digits.Length / 2)]), count);
                            AddStone(long.Parse(digits[(digits.Length / 2)..]), count);
                        }
                    }
                }
            }

            return stones.Values.Sum();

            // Add distinct stones and how many instances thereoff to the dictionary 
            void AddStone(long key, long value)
            {
                stones.TryAdd(key, 0L);
                stones[key] += value;
            }
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            var initialArrangement = lines.First().Split(' ').Select(long.Parse).ToList();

            foreach (var initialStone in initialArrangement)
            {
                    // Count the number of stones generated per initial stone
                    answer += Blink(initialStone);
            }

            // Answer is the number of stones
            return answer;
        }
    }
}
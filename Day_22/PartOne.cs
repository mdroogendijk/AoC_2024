namespace AdventOfCode.DayTwentyTwo
{
    public class PartOne
    {
        public static long Multiply(long secret, int multiplier)
        {
            var intermediate = secret * multiplier;

            intermediate ^= secret;

            return intermediate % 16777216;
        }

        public static long Divide(long secret, int divider)
        {
            var intermediate = (long)Math.Floor((double)secret / divider);
            
            intermediate ^= secret;

            return intermediate % 16777216;

        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            // Generate secret numbers
            foreach (var line in lines)
            {
                var secretNumber = long.Parse(line);

                for (int i = 0; i < 2000; i++)
                {
                    secretNumber = Multiply(secretNumber, 64);
                    secretNumber = Divide(secretNumber, 32);
                    secretNumber = Multiply(secretNumber, 2048);
                }

                answer += secretNumber;
            }

            // Answer is the sum of the 2000th secret numbers for each buyer
            return answer;
        }
    }
}
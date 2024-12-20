namespace AdventOfCode.DaySeventeen
{
    public class PartOne
    {

        public static string GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = String.Empty;

            long a = long.Parse(lines[0].Split(' ')[2]);
            long b = long.Parse(lines[1].Split(' ')[2]);
            long c = long.Parse(lines[2].Split(' ')[2]);

            var numbers = lines[4].Split(' ')[1].Split(',').Select(int.Parse).ToList();

            int i = 0;

            while (i < numbers.Count)
            {
                var literal = numbers[i + 1];

                var combo = numbers[i + 1] switch
                {
                    4 => a,
                    5 => b,
                    6 => c,
                    _ => numbers[i + 1]
                };

                switch (numbers[i])
                {
                    // Instruction: adv - division A
                    case 0:
                        a = (long)Math.Floor(a / Math.Pow(2, combo));
                        break;

                    // Instruction: bxl - bitwise XOR literal
                    case 1:
                        b ^= literal;
                        break;

                    // Instruction: bst - modulo B
                    case 2:
                        b = combo % 8;
                        break;

                    // Instruction: jnz - non zero
                    case 3:
                        if (a != 0)
                        {
                            i = (int)literal;
                            continue;
                        }
                        break;

                    // Instruction: bxc - bitwise XOR C
                    case 4:
                        b ^= c;
                        break;

                    // Instruction: out - modulo output
                    case 5:
                        answer += (combo % 8).ToString() + ',';
                        break;

                    // Instruction: bdv - division B
                    case 6:
                        b = (long)Math.Floor(a / Math.Pow(2, combo));
                        break;

                    // Instruction: cdv - division C
                    case 7:
                        c = (long)Math.Floor(a / Math.Pow(2, combo));
                        break;
                }

                i += 2;
            }

            // Trim the last comma from the string
            answer = answer[..^1];

            // Answer is the value produced by the out instructions
            return answer;
        }
    }
}
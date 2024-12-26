namespace AdventOfCode.DayTwentyTwo
{
    public class Program
    {
        public static List<string> Main()
        {
            var resultSet = new List<string>();

            // Part selector
            bool inputPartOne = true;

            if (inputPartOne)
            {
                long answerPartOne = PartOne.GetAnswer("Day_22/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            return resultSet;
        }
    }
}
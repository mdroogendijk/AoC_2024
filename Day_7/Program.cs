namespace AdventOfCode.DaySeven
{
    public class Program
    {
        public static List<string> Main()
        {
            var resultSet = new List<string>();

            // Part selector
            bool inputPartOne = true;
            bool inputPartTwo = true;

            if (inputPartOne)
            {
                long answerPartOne = PartOne.GetAnswer("Day_7/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            if (inputPartTwo)
            {
                long answerPartTwo = PartTwo.GetAnswer("Day_7/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 2 = {answerPartTwo}");
            }

            return resultSet;
        }
    }
}
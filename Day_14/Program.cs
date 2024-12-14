namespace AdventOfCode.DayFourteen
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
                long answerPartOne = PartOne.GetAnswer("Day_14/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            if (inputPartTwo)
            {
                long answerPartTwo = PartTwo.GetAnswer("Day_14/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 2 = {answerPartTwo}");
            }

            return resultSet;
        }
    }
}
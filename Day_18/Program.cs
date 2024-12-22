namespace AdventOfCode.DayEighteen
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
                int answerPartOne = PartOne.GetAnswer("Day_18/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            if (inputPartTwo)
            {
                string answerPartTwo = PartTwo.GetAnswer("Day_18/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartTwo}");
            }

            return resultSet;
        }
    }
}
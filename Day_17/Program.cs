namespace AdventOfCode.DaySeventeen
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
                string answerPartOne = PartOne.GetAnswer("Day_17/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            return resultSet;
        }
    }
}
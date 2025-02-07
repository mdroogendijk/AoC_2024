﻿namespace AdventOfCode
{
    public class Program
    {
        public static void Main()
        {
            // Day selector
            int day = 22;

            // Solutions to run for selected day
            var resultSet = day switch
            {
                1 => AoC_2024.Day_1.Program.Main(),
                2 => AoC_2024.Day_2.Program.Main(),
                3 => DayThree.Program.Main(),
                4 => DayFour.Program.Main(),
                5 => DayFive.Program.Main(),
                6 => DaySix.Program.Main(),
                7 => DaySeven.Program.Main(),
                8 => DayEight.Program.Main(),
                9 => DayNine.Program.Main(),
                10 => DayTen.Program.Main(),
                11 => DayEleven.Program.Main(),
                12 => DayTwelve.Program.Main(),
                13 => DayThirteen.Program.Main(),
                14 => DayFourteen.Program.Main(),
                15 => DayFifteen.Program.Main(),
                16 => DaySixteen.Program.Main(),
                17 => DaySeventeen.Program.Main(),
                18 => DayEighteen.Program.Main(),
                19 => DayNineteen.Program.Main(),
                20 => DayTwenty.Program.Main(),
                21 => DayTwentyOne.Program.Main(),
                22 => DayTwentyTwo.Program.Main(),
                _ => []
            };

            Console.WriteLine($"The results of day {day}:");

            // Write results
            if (resultSet.Count != 0)
            {
                foreach (string result in resultSet) Console.WriteLine(result);
            }
        }
    }
}
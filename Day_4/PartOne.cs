using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayFour
{
    public class PartOne
    {
        public static (int row, int col)[] Directions => [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1)];

        public static bool IsInGrid((int row, int col) coord, int rowLength, int colLength) => 0 <= coord.row
            && coord.row < rowLength
            && 0 <= coord.col
            && coord.col < colLength;

        public static string ConstructXmasStrings(string[] lines, int row, int col, (int row, int col) dir, int rowLen, int colLen)
        {
            var result = "X";
            for (var i = 0; i < 3; i++)
            {
                var nextCoord = (row: row + dir.row, col: col + dir.col);
                if (IsInGrid(nextCoord, rowLen, colLen))
                {
                    result += lines[nextCoord.row][nextCoord.col];
                }
                row = nextCoord.row;
                col = nextCoord.col;
            }
            return result;
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var rowLength = lines.Length;
            var colLength = lines[0].Length;

            var grid = new List<Tuple<char, int, int>>();

            // Loop over lines
            for (var i = 0; i < rowLength; i++)
            {
                // Loop over characters on line
                for (var j = 0; colLength > j; j++)
                {
                    if (lines[i][j] == 'X')
                    {
                        var strings = new List<string>();

                        // Build list in all 8 directions then check for XMAS
                        foreach (var d in Directions)
                        {
                            strings.Add(ConstructXmasStrings(lines, i, j, d, rowLength, colLength));
                        }
                        foreach (var s in strings)
                        {
                            if (s.Equals("XMAS"))
                            {
                                answer++;
                            }
                        }
                    }
                }
            }

            // Answer is the sum of XMAS words found
            return answer;
        }
    }
}
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayFour
{
    public class PartTwo
    {
        public static (int row, int col)[] Directions => [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1)];

        public static bool IsInGrid((int row, int col) coord, int rowLength, int colLength) => 0 <= coord.row
            && coord.row < rowLength
            && 0 <= coord.col
            && coord.col < colLength;

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var rowLength = lines.Length;
            var colLength = lines[0].Length;

            // Loop over lines
            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < colLength; j++)
                {
                    if (lines[i][j] == 'A')
                    {
                        var leftDiagToFind = new HashSet<char>() { 'M', 'S' };
                        var rightDiagToFind = leftDiagToFind.ToHashSet();
                        var bottomLeft = (row: i - 1, col: j - 1);
                        var topRight = (row: i + 1, col: j + 1);

                        // Check for bottom left to top right diag
                        if (IsInGrid(bottomLeft, rowLength, colLength)
                            && leftDiagToFind.Remove(lines[bottomLeft.row][bottomLeft.col])
                            && IsInGrid(topRight, rowLength, colLength)
                            && leftDiagToFind.Remove(lines[topRight.row][topRight.col]))
                        {
                            var bottomRight = (row: i - 1, col: j + 1);
                            var topLeft = (row: i + 1, col: j - 1);
                            // Check for bottom right to top left diag
                            if (IsInGrid(bottomRight, rowLength, colLength)
                                && rightDiagToFind.Remove(lines[bottomRight.row][bottomRight.col])
                                && IsInGrid(topLeft, rowLength, colLength)
                                && rightDiagToFind.Remove(lines[topLeft.row][topLeft.col]))
                            {
                                answer++;
                            }
                        }
                    }
                }
            }

            // Answer is the sum of the multiplication instruction results
            return answer;
        }
    }
}
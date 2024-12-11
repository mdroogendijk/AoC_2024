namespace AdventOfCode.DaySix
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var grid = new List<ValueTuple<int, int, char>>();

            // Generate grid
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    grid.Add((x, y, lines[y][x]));
                }
            }

            var maxHorizontal = lines[0].Length - 1;
            var maxVertical = lines.Length - 1;

            var parallelGrids = new List<List<ValueTuple<int, int, char>>>() { grid };

            // Add parallel grids with each containing one obstacle
            for (int  y = 0;  y <= maxVertical;  y++)
            {
                for (int x = 0; x <= maxHorizontal; x++)
                { 
                    if (grid.Where(item => item.Item1 == x && item.Item2 == y).Select(item => item.Item3).First() == '.')
                    {
                        var parallelGrid = grid.Select(x => x).ToList();

                        parallelGrid[parallelGrid.IndexOf((x, y, '.'))] = (x, y, 'O');

                        parallelGrids.Add(parallelGrid);
                    }
                }
            }

            foreach (var parallelGrid in parallelGrids)
            {
                var currentLocation = parallelGrid.Where(x => x.Item3 == '^').FirstOrDefault();

                // Remove starting location identifier
                parallelGrid[parallelGrid.IndexOf(currentLocation)] = (currentLocation.Item1, currentLocation.Item2, '.');

                var insideGrid = true;

                var looping = false;

                // Directions when visited the obstacle 
                var obstacleDirections = new List<char>();

                var direction = 'u';

                while (insideGrid && !looping)
                {
                    switch (direction)
                    {
                        case 'u':
                            if (0 <= currentLocation.Item2 - 1)
                            {
                                var nextChar = parallelGrid.Where(x =>
                                    x.Item1 == currentLocation.Item1 &&
                                    x.Item2 == currentLocation.Item2 - 1)
                                    .Select(x => x.Item3).First();

                                // Check if obstacle encountered in same direction as before
                                if (nextChar == 'O')
                                {
                                    if (obstacleDirections.Contains(direction))
                                    {
                                        looping = true;
                                        }
                                    else
                                    {
                                        obstacleDirections.Add(direction);
                                        direction = 'r';
                                    }
                                }
                                else if (nextChar != '.')
                                {
                                    direction = 'r';
                                }
                                else
                                {
                                    currentLocation.Item2 -= 1;
                                }
                            }
                            else
                            {
                                insideGrid = false;
                            }
                            break;

                        case 'd':
                            if (maxVertical >= currentLocation.Item2 + 1)
                            {
                                var nextChar = parallelGrid.Where(x =>
                                    x.Item1 == currentLocation.Item1 &&
                                    x.Item2 == currentLocation.Item2 + 1)
                                    .Select(x => x.Item3).First();

                                // Check if obstacle encountered in same direction as before
                                if (nextChar == 'O')
                                {
                                    if (obstacleDirections.Contains(direction))
                                    {
                                        looping = true;
                                    }
                                    else
                                    {
                                        obstacleDirections.Add(direction);
                                        direction = 'l';
                                    }
                                }
                                else if (nextChar != '.')
                                {
                                    direction = 'l';
                                }
                                else
                                {
                                    currentLocation.Item2 += 1;
                                }
                            }
                            else
                            {
                                insideGrid = false;
                            }
                            break;

                        case 'l':
                            if (0 <= currentLocation.Item1 - 1)
                            {
                                var nextChar = parallelGrid.Where(x =>
                                    x.Item1 == currentLocation.Item1 - 1 &&
                                    x.Item2 == currentLocation.Item2)
                                    .Select(x => x.Item3).First();

                                // Check if obstacle encountered in same direction as before
                                if (nextChar == 'O')
                                {
                                    if (obstacleDirections.Contains(direction))
                                    {
                                        looping = true;
                                    }
                                    else
                                    {
                                        obstacleDirections.Add(direction);
                                        direction = 'u';
                                    }
                                }
                                else if (nextChar != '.')
                                {
                                    direction = 'u';
                                }
                                else
                                {
                                    currentLocation.Item1 -= 1;
                                }
                            }
                            else
                            {
                                insideGrid = false;
                            }
                            break;

                        case 'r':
                            if (maxHorizontal >= currentLocation.Item1 + 1)
                            {
                                var nextChar = parallelGrid.Where(x =>
                                    x.Item1 == currentLocation.Item1 + 1 &&
                                    x.Item2 == currentLocation.Item2)
                                    .Select(x => x.Item3).First();

                                // Check if obstacle encountered in same direction as before
                                if (nextChar == 'O')
                                {
                                    if (obstacleDirections.Contains(direction))
                                    {
                                        looping = true;
                                    }
                                    else
                                    {
                                        obstacleDirections.Add(direction);
                                        direction = 'd';
                                    }
                                }
                                else if (nextChar != '.')
                                {
                                    direction = 'd';
                                }
                                else
                                {
                                    currentLocation.Item1 += 1;
                                }
                            }
                            else
                            {
                                insideGrid = false;
                            }
                            break;
                    }
                }

                if (looping)
                {
                    answer += 1;
                }
            }

            // Answer is the sum of looping worlds
            return answer;
        }
    }
}
namespace AdventOfCode.DaySix
{
    public class PartOne
    {
        public static int UpdateLocationsVisited(ValueTuple<int, int, char> currentLocation, List<ValueTuple<int, int>> locationsVisited)
        {
            // Add new locations visited
            if (!locationsVisited
                .Any(x => x.Item1 == currentLocation.Item1 &&
                        x.Item2 == currentLocation.Item2))
            {
                locationsVisited.Add((currentLocation.Item1, currentLocation.Item2));
                return 1;
            }

            return 0;
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 1;

            var grid = new List<ValueTuple<int, int, char>>();

            // Generate grid
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    grid.Add(( x, y, lines[y][x]));
                }
            }

            var maxHorizontal = lines[0].Length - 1;
            var maxVertical = lines.Length - 1;

            var currentLocation = grid.Where(x => x.Item3 == '^').FirstOrDefault();

            var locationsVisited = new List<ValueTuple<int, int>>() { (currentLocation.Item1, currentLocation.Item2) };

            // Remove starting location identifier
            grid[grid.IndexOf(currentLocation)] = (currentLocation.Item1, currentLocation.Item2, '.');

            var insideGrid = true;

            var direction = 'u';

            while (insideGrid)
            {
                switch (direction)
                {
                    case 'u':
                        if (0 <= currentLocation.Item2 - 1)
                        {
                            // Change direction if blocked
                            if (grid.Where(x =>
                                x.Item1 == currentLocation.Item1 &&
                                x.Item2 == currentLocation.Item2 - 1)
                                .Select(x => x.Item3).First() != '.')
                            {
                                direction = 'r';
                            }
                            else
                            {
                                currentLocation.Item2 -= 1;
                                answer += UpdateLocationsVisited(currentLocation, locationsVisited);
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
                            // Change direction if blocked
                            if (grid.Where(x =>
                                x.Item1 == currentLocation.Item1 &&
                                x.Item2 == currentLocation.Item2 + 1)
                                .Select(x => x.Item3).First() != '.')
                            {
                                direction = 'l';
                            }
                            else
                            {
                                currentLocation.Item2 += 1;
                                answer += UpdateLocationsVisited(currentLocation, locationsVisited);

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
                            // Change direction if blocked
                            if (grid.Where(x =>
                                x.Item1 == currentLocation.Item1 - 1 &&
                                x.Item2 == currentLocation.Item2)
                                .Select(x => x.Item3).First() != '.')
                            {
                                direction = 'u';
                            }
                            else 
                            {
                                currentLocation.Item1 -= 1;
                                answer += UpdateLocationsVisited(currentLocation, locationsVisited);
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
                            // Change direction if blocked
                            if (grid.Where(x =>
                                x.Item1 == currentLocation.Item1 + 1 &&
                                x.Item2 == currentLocation.Item2)
                                .Select(x => x.Item3).First() != '.')
                            {
                                direction = 'd';
                            }
                            else
                            {
                                currentLocation.Item1 += 1;
                                answer += UpdateLocationsVisited(currentLocation, locationsVisited);
                            }
                        }
                        else
                        {
                            insideGrid = false;
                        }
                        break;
                }
            }

            // Answer is the sum of unique locations visited
            return answer;
        }
    }
}
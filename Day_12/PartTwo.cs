namespace AdventOfCode.DayTwelve
{
    public class PartTwo
    {
        public class Position
        {
            public int positionX;
            public int positionY;
            public char plant;
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0, area, sides;

            var map = new List<Position>();

            var regions = new Dictionary<int, List<Position>>();

            HashSet<(int, int)> visited = [];

            // Generate map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map.Add(new Position()
                    {
                        positionX = x,
                        positionY = y,
                        plant = lines[y][x]
                    });
                }
            }

            // Get initial regions and perimiters (4 - same plant neighbours)
            foreach (var initialPosition in map)
            {
                var location = (initialPosition.positionX, initialPosition.positionY);

                if (visited.Contains(location))
                {
                    continue;
                }

                Dictionary<(char, int), List<int>> allSides = [];

                area = 0;
                sides = 0;

                // Use stack to visit all locations in the region
                Stack<(int, int)> stack = [];
                stack.Push(location);

                do
                {
                    (int x, int y) pos = stack.Pop();
                    visited.Add(pos);
                    area += 1;

                    // Check neighbours if they're to reside in the same region or requiring a fence
                    var neighbours = map
                        .Where(item => item.plant == initialPosition.plant &&
                            ((item.positionX == pos.x - 1 &&
                            item.positionY == pos.y) ||

                            (item.positionX == pos.x + 1 &&
                            item.positionY == pos.y) ||

                            (item.positionX == pos.x &&
                            item.positionY == pos.y - 1) ||

                            (item.positionX == pos.x &&
                            item.positionY == pos.y + 1)));

                    // Add fences (to top, bottom, ...) for non-regional neighbours and for edges of the map
                    if (!map.Any(item => item.positionX == pos.x && item.positionY == pos.y - 1 && item.plant == initialPosition.plant) &&
                        !allSides.TryAdd(('t', pos.y), [pos.x]))
                    {                    
                        allSides[('t', pos.y)].Add(pos.x);
                    }
                    if (!map.Any(item => item.positionX == pos.x && item.positionY == pos.y + 1 && item.plant == initialPosition.plant) &&
                        !allSides.TryAdd(('b', pos.y), [pos.x]))
                    {
                        allSides[('b', pos.y)].Add(pos.x);
                    }
                    if (!map.Any(item => item.positionX == pos.x - 1 && item.positionY == pos.y && item.plant == initialPosition.plant) &&
                        !allSides.TryAdd(('l', pos.x), [pos.y]))
                    {
                        allSides[('l', pos.x)].Add(pos.y);
                    }
                    if (!map.Any(item => item.positionX == pos.x + 1 && item.positionY == pos.y && item.plant == initialPosition.plant) &&
                        !allSides.TryAdd(('r', pos.x), [pos.y]))
                    {
                        allSides[('r', pos.x)].Add(pos.y);
                    }

                    // Add unvisited regional neighbours to stack
                    foreach (var neighbour in neighbours.Where(item => item.plant == initialPosition.plant))
                    {
                        (int x, int y) newPos = (neighbour.positionX, neighbour.positionY);

                        if (!visited.Contains(newPos) && !stack.Contains(newPos))
                        {
                            stack.Push(newPos);
                        }
                    }
                } while (stack.Count > 0);

                // Get distinct ranges in the various fence directions
                foreach ((char type, int val) in allSides.Keys)
                {
                    sides++;
                    allSides[(type, val)].Sort();

                    for (int i = 0; i < allSides[(type, val)].Count - 1; i++)
                    {
                        if (allSides[(type, val)][i] != allSides[(type, val)][i + 1] - 1)
                        {
                            sides++;
                        }
                    }
                }

                // Calculate price based on the area times the sum of perimeters
                answer += area * sides;
            }

            // Answer is the sum of prices of all regions
            return answer;
        }
    }
}
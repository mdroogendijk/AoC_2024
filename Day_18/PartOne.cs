namespace AdventOfCode.DayEighteen
{
    public class PartOne
    {
        public class Way
        {
            public (int, int) currentLoc = (0, 0);
            public int cost = 0;
        }

        public static int GetAnswer(string fileName, int byteCount = 1024, int maxRange = 70)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = 0;

            var map = new List<(int, int)>();

            // Generate map
            for (int y = 0; y <= maxRange; y++)
            {
                    for (int x = 0; x <= maxRange; x++)
                    {
                        map.Add((x, y));
                    }
            }

            // Remove corrupted coordinates from map
            for (int i = 0; i < byteCount; i++)
            {
                var coordinates = lines[i].Split(',').Select(int.Parse);
                map.Remove((coordinates.First(), coordinates.Last()));
            }

            var visited = new Dictionary<(int, int), int>();

            var startpoint = (0, 0);
            var endpoint = (maxRange, maxRange);

            Stack<Way> stack = [];
            stack.Push(new Way() { currentLoc = startpoint, cost = 0 });

            do
            {
                Way way = stack.Pop();

                // Check if location on the way is reached via the cheapest way possible
                if (visited.TryGetValue(way.currentLoc, out int value))
                {
                    if (way.cost < value)
                    {
                        visited[way.currentLoc] = way.cost;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    visited.Add(way.currentLoc, way.cost);
                }

                // Endpoint reached
                if (way.currentLoc == endpoint)
                {
                    continue;
                }

                // Add way for all possible directions from here
                if (map.Contains((way.currentLoc.Item1 - 1, way.currentLoc.Item2)))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1 - 1, way.currentLoc.Item2), cost = way.cost + 1 });
                }
                if (map.Contains((way.currentLoc.Item1 + 1, way.currentLoc.Item2)))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1 + 1, way.currentLoc.Item2), cost = way.cost + 1 });
                }
                if (map.Contains((way.currentLoc.Item1, way.currentLoc.Item2 - 1)))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1, way.currentLoc.Item2 - 1), cost = way.cost + 1 });
                }
                if (map.Contains((way.currentLoc.Item1, way.currentLoc.Item2 + 1)))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1, way.currentLoc.Item2 + 1), cost = way.cost + 1 });
                }

            } while (stack.Count > 0);

            // Get shortest way to endpoint
            answer = visited[endpoint];

            // Answer is the shortest way from start to end
            return answer;
        }
    }
}
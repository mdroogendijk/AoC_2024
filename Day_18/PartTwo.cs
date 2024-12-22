namespace AdventOfCode.DayEighteen
{
    public class PartTwo
    {
        public class Way
        {
            public (int, int) currentLoc = (0, 0);
            public int cost = 0;
        }

        public static string GetAnswer(string fileName, int byteCount = 1024, int maxRange = 70)
        {
            var lines = File.ReadAllLines(fileName);

            var answer = String.Empty;

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

            var startpoint = (0, 0);
            var endpoint = (maxRange, maxRange);

            for (int i = byteCount; i < lines.Length; i++)
            {
                var coordinates = lines[i].Split(',').Select(int.Parse);
                map.Remove((coordinates.First(), coordinates.Last()));

                var visited = new Dictionary<(int, int), int>();

                Stack<Way> stack = [];
                stack.Push(new Way() { currentLoc = startpoint, cost = 0 });

                do
                {
                    Way way = stack.Pop();

                    // Check if location on the way was already reached
                    if (visited.TryGetValue(way.currentLoc, out int value))
                    {
                            continue;
                    }
                    else
                    {
                        visited.Add(way.currentLoc, way.cost);
                    }

                    // Endpoint reached
                    if (way.currentLoc == endpoint)
                    {
                        break;
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

                if (!visited.ContainsKey(endpoint))
                {
                    // Answer is the coordinates of corruption that blocks the exit
                    return String.Join(',', coordinates);
                }
            }

            // Failsave
            return answer;
        }
    }
}
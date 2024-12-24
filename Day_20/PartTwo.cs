using System.Collections.Generic;

namespace AdventOfCode.DayTwenty
{
    public class PartTwo
    {
        public class Way
        {
            public (int, int) currentLoc;
            public int picoseconds;
        }

        public static int GetAnswer(string fileName, int timeSavings = 100)
        {
            var lines = File.ReadAllLines(fileName);

            int answer = 0;

            var map = new List<ValueTuple<(int, int), char>>();

            // Generate map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map.Add(new ValueTuple<(int, int), char>()
                    {
                        Item1 = (x, y),
                        Item2 = lines[y][x]
                    });
                }
            }

            var startpoint = map.Where(x => x.Item2 == 'S').First().Item1;
            var endpoint = map.Where(x => x.Item2 == 'E').First().Item1;

            // Determine fastest route without cheating            
            var visited = new Dictionary<(int, int), int>();

            Stack<Way> stack = [];
            stack.Push(new Way() { currentLoc = startpoint, picoseconds = 0 });

            do
            {
                Way way = stack.Pop();

                // Check if location on the way is reached via the cheapest way possible
                if (visited.TryGetValue(way.currentLoc, out int value))
                {
                    if (way.picoseconds < value)
                    {
                        visited[way.currentLoc] = way.picoseconds;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    visited.Add(way.currentLoc, way.picoseconds);
                }

                // Endpoint reached
                if (way.currentLoc == endpoint)
                {
                    continue;
                }

                // Add way for all possible directions from here
                if (map.Any(x => x.Item1 == (way.currentLoc.Item1 - 1, way.currentLoc.Item2) && x.Item2 != '#'))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1 - 1, way.currentLoc.Item2), picoseconds = way.picoseconds + 1 });
                }
                if (map.Any(x => x.Item1 == (way.currentLoc.Item1 + 1, way.currentLoc.Item2) && x.Item2 != '#'))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1 + 1, way.currentLoc.Item2), picoseconds = way.picoseconds + 1 });
                }
                if (map.Any(x => x.Item1 == (way.currentLoc.Item1, way.currentLoc.Item2 - 1) && x.Item2 != '#'))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1, way.currentLoc.Item2 - 1), picoseconds = way.picoseconds + 1 });
                }
                if (map.Any(x => x.Item1 == (way.currentLoc.Item1, way.currentLoc.Item2 + 1) && x.Item2 != '#'))
                {
                    stack.Push(new Way() { currentLoc = (way.currentLoc.Item1, way.currentLoc.Item2 + 1), picoseconds = way.picoseconds + 1 });
                }

            } while (stack.Count > 0);

            //var noCheatingTime, route = TimeLap(map, startpoint, endpoint, int.MaxValue);

            // Check route for all possible cheats
            foreach ((var loc, var time) in visited)
            {
                foreach (var cheatLoc in visited.Where(x => Math.Abs(x.Key.Item1 - loc.Item1) + Math.Abs(x.Key.Item2 - loc.Item2) <= 20).Select(x => x.Key).ToList())
                {
                    if (visited.TryGetValue(cheatLoc, out int value))
                    {
                        int saved = value - visited[loc] - (Math.Abs(cheatLoc.Item1 - loc.Item1) + Math.Abs(cheatLoc.Item2 - loc.Item2));
                        if (saved >= timeSavings)
                        {
                            answer += 1;
                        }
                    }
                }
            }

            // Answer is the number of cheats saving at least x picoseconds
            return answer;
        }
    }
}
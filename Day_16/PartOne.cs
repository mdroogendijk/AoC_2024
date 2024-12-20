namespace AdventOfCode.DaySixteen
{
    public class PartOne
    {
        public class Position
        {
            public (int, int) position;
            public char value;
            public Dictionary<char, long> cheapestPathForDirection = [];
        }

        public static (int, int) SetDirection(char direction)
        {
            return direction switch
            {
                'W' => (-1, 0),
                'E' => (1, 0),
                'N' => (0, -1),
                'S' => (0, +1),
                _ => (0, 0)
            };
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            var map = new List<Position>();

            var visited = new HashSet<((int, int), char, long)> ();

            Queue<((int, int), char, long)> nextPositions = new();

            // Generate map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map.Add(new Position()
                    {
                        position = (x, y),
                        value = lines[y][x]
                    });
                }
            }

            var direction = 'E';
            var endPosition = map.Where(x => x.value == 'E').First().position;
            var startPosition = map.Where(x => x.value == 'S').First().position;

            // Add start to queue
            nextPositions.Enqueue((startPosition, direction, 0));

            // Iterate through all possible paths from current location
            do
            {
                var currentLoc = nextPositions.Dequeue();

                var currentPos = currentLoc.Item1;
                var currentCost = currentLoc.Item3;
                direction  = currentLoc.Item2;

                List<char> possibleDirections = [];

                // Skip if any previous route to current pos with current direction was cheaper
                if (visited.Any(x =>
                            x.Item1 == currentPos))
                {
                    var previousCostAvailable = visited.Where(x => x.Item1 == currentPos && x.Item2 == direction);

                    if (previousCostAvailable.Count() > 0 && previousCostAvailable.First().Item3 < currentCost)
                    {
                        continue;
                    }
                    else
                    {
                        // Add or update direction and cost
                        try
                        {
                            visited.RemoveWhere(x => x.Item1 == currentPos && x.Item2 == direction);
                        }
                        catch
                        {

                        }
                        visited.Add((currentPos, direction, currentCost));
                    }
                }
                else
                {
                    visited.Add(currentLoc);
                }

                // End location reached
                if (map.Any(x =>
                    x.position == currentPos &&
                    x.value == 'E'))
                {
                    continue;
                }

                switch (direction)
                {
                    case 'W':
                        possibleDirections.AddRange(['S', 'W', 'N']);
                        break;
                    case 'E':
                        possibleDirections.AddRange(['N', 'E', 'S']);
                        break;
                    case 'N':
                        possibleDirections.AddRange(['W', 'N', 'E']);
                        break;
                    case 'S':
                        possibleDirections.AddRange(['E', 'S', 'W']);
                        break;
                }

                foreach (char c in possibleDirections)
                {
                    var movement = SetDirection(c);

                    // Move costs 1000 extra if 90 degree turn required
                    var movementCost = c == direction ? 1 : 1001;

                    var nextPos = (currentPos.Item1 + movement.Item1, currentPos.Item2 + movement.Item2);

                    if (map.Any(x =>
                        x.position == nextPos &&
                        x.value != '#'))
                    {
                        ((int, int), char, long) newItem = (nextPos, c, currentCost + movementCost);

                        if (!visited.Any(x => x.Item1 == newItem.Item1 && x.Item2 == c && x.Item3 <= currentCost + movementCost) &&
                            !nextPositions.Any(x => x.Item1 == newItem.Item1 && x.Item2 == c && x.Item3 <= currentCost + movementCost))
                        {
                            nextPositions.Enqueue(newItem);
                        }
                    }
                }
            } while (nextPositions.Count > 0);

            // Calculate the cheapest path to the end
            answer = visited.Where(x => x.Item1 == endPosition).Min(x => x.Item3);

            // Answer is the cheapest path
            return answer;
        }
    }
}
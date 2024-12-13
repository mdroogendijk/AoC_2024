namespace AdventOfCode.DayTwelve
{
    public class PartOne
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

            long answer = 0, area, perimiter;

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

                area = 0;
                perimiter = 0;

                // Use stack to visit all locations in the region
                Stack<(int, int)> stack = [];
                stack.Push(location);

                do
                {
                    (int x, int y) pos = stack.Pop();
                    visited.Add(pos);
                    area += 1;

                    // Check neighbours if they're to reside in the same region
                    var regionalNeighbours = map
                        .Where(item => item.plant == initialPosition.plant &&
                            ((item.positionX == pos.x - 1 &&
                            item.positionY == pos.y) ||

                            (item.positionX == pos.x + 1 &&
                            item.positionY == pos.y) ||

                            (item.positionX == pos.x &&
                            item.positionY == pos.y - 1) ||

                            (item.positionX == pos.x &&
                            item.positionY == pos.y + 1)));

                    perimiter += 4 - regionalNeighbours.Count();

                    // Add unvisited regional neighbours to stack
                    foreach (var neighbour in regionalNeighbours)
                    {
                        (int x, int y) newPos = (neighbour.positionX, neighbour.positionY);

                        if (!visited.Contains(newPos) && !stack.Contains(newPos))
                        {
                            stack.Push(newPos);
                        }
                    }
                } while (stack.Count > 0);

                // Calculate price based on the area times the sum of perimeters
                answer += area * perimiter;
            }

            // Answer is the sum of prices of all regions
            return answer;
        }
    }
}
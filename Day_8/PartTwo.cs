namespace AdventOfCode.DayEight
{
    public class PartTwo
    {
        public class Distance
        {
            public int startingPointX;
            public int startingPointY;
            public int distanceX;
            public int distanceY;
        }

        public class AntiNode
        {
            public int positionX;
            public int positionY;
        }

        public static int AddAntiNode(List<AntiNode> antiNodes, int x , int y)
        {
            antiNodes.Add(
                        new AntiNode()
                        {
                            positionX = x,
                            positionY = y
                        });

            return 1;
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            int answer = 0;

            var grid = new List<ValueTuple<int, int, char>>();

            var antiNodes = new List<AntiNode>();

            // Generate grid
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    grid.Add((x, y, lines[y][x]));
                }
            }

            var distinctAntennas = grid
                .Where(x => x.Item3 != '.')
                .Select(x => x.Item3)
                .Distinct();

            // Loop over all the distinct antennas
            foreach (var distinctAntenna in distinctAntennas)
            {
                var antennas = grid.Where(x => x.Item3 == distinctAntenna).ToList();

                var distances = new List<Distance>();

                // Add distance calculations per antenna to all the next antennas
                for (int i = 0; i < antennas.Count; i++)
                {
                    for (int j = i + 1; j < antennas.Count; j++)
                    {
                        distances.Add(new Distance()
                        {
                            startingPointX = antennas[i].Item1,
                            startingPointY = antennas[i].Item2,
                            distanceX = antennas[j].Item1 - antennas[i].Item1,
                            distanceY = antennas[j].Item2 - antennas[i].Item2,
                        });
                    }
                }

                foreach (var distance in distances)
                {
                    // Add antinode for the two original antennas
                    if (!antiNodes.Any(x => 
                        x.positionX == distance.startingPointX && 
                        x.positionY == distance.startingPointY))
                    {
                        answer += AddAntiNode(antiNodes, distance.startingPointX, distance.startingPointY);
                    }

                    if (!antiNodes.Any(x =>
                        x.positionX == distance.startingPointX + distance.distanceX &&
                        x.positionY == distance.startingPointY + distance.distanceY))
                    {
                        answer += AddAntiNode(antiNodes, distance.startingPointX + distance.distanceX, distance.startingPointY + distance.distanceY);
                    }

                    var firstAntiNode = (distance.startingPointX - distance.distanceX, distance.startingPointY - distance.distanceY);

                    var secondAntiNode = (distance.startingPointX + (2 * distance.distanceX), distance.startingPointY + (2 * distance.distanceY));

                    // Check if first antinode fits in grid and not already counted for
                    while (                        
                        firstAntiNode.Item1 >= 0 && firstAntiNode.Item1 <= grid.Max(x => x.Item1) &&
                        firstAntiNode.Item2 >= 0 && firstAntiNode.Item2 <= grid.Max(x => x.Item2))
                    {
                        if (!antiNodes.Any(x => x.positionX == firstAntiNode.Item1 && x.positionY == firstAntiNode.Item2))
                        {
                            answer += AddAntiNode(antiNodes, firstAntiNode.Item1, firstAntiNode.Item2);                                
                        }

                        // Update location for next antinode
                        firstAntiNode = (firstAntiNode.Item1 - distance.distanceX, firstAntiNode.Item2 - distance.distanceY);
                    }

                    // Check if second antinode fits in grid and not already counted for
                    while (
                        secondAntiNode.Item1 >= 0 && secondAntiNode.Item1 <= grid.Max(x => x.Item1) &&
                        secondAntiNode.Item2 >= 0 && secondAntiNode.Item2 <= grid.Max(x => x.Item2))
                    {
                        if (!antiNodes.Any(x => x.positionX == secondAntiNode.Item1 && x.positionY == secondAntiNode.Item2))
                        {
                            answer += AddAntiNode(antiNodes, secondAntiNode.Item1, secondAntiNode.Item2);
                        }

                        // Update location for next antinode
                        secondAntiNode = (secondAntiNode.Item1 + (distance.distanceX), secondAntiNode.Item2 + (distance.distanceY));
                    }
                }
            }

            // Answer is the sum of test values with matching calculations
            return answer;
        }
    }
}
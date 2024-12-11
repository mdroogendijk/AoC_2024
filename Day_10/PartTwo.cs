namespace AdventOfCode.DayTen
{
    public class PartTwo
    {
        public class Position
        {
            public int positionX;
            public int positionY;
            public int height;
        }

        public class Trailhead
        {
            public int positionX;
            public int positionY;
            public List<Position> trailEndPositions = [];
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            int answer = 0;

            var map = new List<Position>();

            var trailheads = new List<Trailhead>();

            // Generate map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map.Add(new Position()
                    {
                        positionX = x,
                        positionY = y,
                        height = (int)char.GetNumericValue(lines[y][x])
                    });
                }
            }

            foreach (var startingPoint in map.Where(x => x.height == 0))
            {
                var activeTrailroutes = new List<Position>()
                {
                    new()
                    {
                        positionX = startingPoint.positionX,
                        positionY = startingPoint.positionY,
                        height = startingPoint.height
                    }
                };

                // Continue until all possible trailroutes have been explored
                for (int i = 0; i < 9; i++)
                {
                    var nextPositions = new List<Position>();

                    foreach (var activeTrailroute in activeTrailroutes)
                    {
                        // Find all adjacent positions that are one increment higher
                        nextPositions.AddRange(map.Where(x =>
                        (x.positionX == activeTrailroute.positionX - 1 && x.positionY == activeTrailroute.positionY && x.height == activeTrailroute.height + 1) ||
                        (x.positionX == activeTrailroute.positionX + 1 && x.positionY == activeTrailroute.positionY && x.height == activeTrailroute.height + 1) ||
                        (x.positionX == activeTrailroute.positionX && x.positionY == activeTrailroute.positionY - 1 && x.height == activeTrailroute.height + 1) ||
                        (x.positionX == activeTrailroute.positionX && x.positionY == activeTrailroute.positionY + 1 && x.height == activeTrailroute.height + 1)
                        ));
                    }

                    // Remove previous positions
                    activeTrailroutes.Clear();

                    if (nextPositions.Count != 0)
                    {
                        activeTrailroutes.AddRange(nextPositions);
                    }
                }

                // Log all finished trailroutes that end at distict endpoints
                trailheads.Add(new Trailhead()
                {
                    positionX = startingPoint.positionX,
                    positionY = startingPoint.positionY,
                    trailEndPositions = activeTrailroutes
                });
            }

            // Count only distinct trail end positions per trailhead
            answer = trailheads.Sum(x => x.trailEndPositions.Count);

            // Answer is the sum of trailroutes per trailhead
            return answer;
        }
    }
}
namespace AdventOfCode.DayFourteen
{
    public class PartTwo
    {
        public class Robot
        {
            public (int, int) position;
            public (int, int) velocity;
        }

        public class Quadrants
        {
            public int topleft;
            public int topright;
            public int bottomleft;
            public int bottomright;
        }

        public static long GetAnswer(string fileName, int width = 101, int height = 103)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            var robots = new List<Robot>();

            var quadrants = new Quadrants();

            // Gather all claw machine specifics
            foreach (var line in lines)
            {
                robots.Add(new Robot()
                {
                    position = (Int32.Parse(line.Split('=')[1].Split(',')[0]), Int32.Parse(line.Split(',')[1].Split(' ')[0])),
                    velocity = (Int32.Parse(line.Split('=')[2].Split(',')[0]), Int32.Parse(line.Split(',')[2]))
                });
            }

            for (int i = 1; 1 < 10000; i++)
            {
                foreach (var robot in robots)
                {
                    int positionX;
                    int positionY;

                    if (robot.position.Item1 + robot.velocity.Item1 >= width)
                    {
                        positionX = robot.position.Item1 + robot.velocity.Item1 - width;
                    }
                    else if (robot.position.Item1 + robot.velocity.Item1 < 0)
                    {
                        positionX = width + robot.position.Item1 + robot.velocity.Item1;
                    }
                    else
                    {
                        positionX = robot.position.Item1 + robot.velocity.Item1;
                    }

                    if (robot.position.Item2 + robot.velocity.Item2 >= height)
                    {
                        positionY = robot.position.Item2 + robot.velocity.Item2 - height;
                    }
                    else if (robot.position.Item2 + robot.velocity.Item2 < 0)
                    {
                        positionY = height + robot.position.Item2 + robot.velocity.Item2;
                    }
                    else
                    {
                        positionY = robot.position.Item2 + robot.velocity.Item2;
                    }

                    robot.position = (positionX, positionY);
                }

                // Stop when we find the easter egg (should occur when the robots are all at a distinct location
                // and there at least 10 robots on a horizontal and vertical line)
                if (robots.Select(r => ((r.position.Item1, r.position.Item2))).Distinct().Count() == robots.Count &&
                    robots.Select(r => new { x = r.position.Item1, y = r.position.Item2 }).GroupBy(r => r.x).Any(r => r.Count() > 10) &&
                    robots.Select(r => new { x = r.position.Item1, y = r.position.Item2 }).GroupBy(r => r.y).Any(r => r.Count() > 10))
                {
                    answer = i;

                    // Draw christmas tree
                    for (var y = 0; y < height; y++)
                    {
                        for (var x = 0; x < width; x++)
                        {
                            Console.Write(robots.Any(r => r.position.Item1 == x && r.position.Item2 == y) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }

                    break;
                }
            }

            // Answer is sum of robots per quadrant multiplied
            return answer;
        }
    }
}
namespace AdventOfCode.DayFourteen
{
    public class PartOne
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

            var quadrants = new Quadrants();

            // Gather all claw machine specifics
            foreach (var line in lines)
            {
                var robot = new Robot()
                {
                    position = (Int32.Parse(line.Split('=')[1].Split(',')[0]), Int32.Parse(line.Split(',')[1].Split(' ')[0])),
                    velocity = (Int32.Parse(line.Split('=')[2].Split(',')[0]), Int32.Parse(line.Split(',')[2]))
                };

                // Calculate robot position after 100 seconds
                robot.position = (robot.position.Item1 + (robot.velocity.Item1 * 100), robot.position.Item2 + (robot.velocity.Item2 * 100));

                var endpositionX = robot.position.Item1 % width < 0 ? width + robot.position.Item1 % width : robot.position.Item1 % width;
                var endpositionY = robot.position.Item2 % height < 0 ? height + robot.position.Item2 % height : robot.position.Item2 % height;

                // Add robot to the appropriate quadrant, if not in the middle
                if (endpositionX < width / 2 &&
                    endpositionY < height / 2)
                {
                    quadrants.topleft += 1;
                }
                else if (endpositionX > width / 2 &&
                         endpositionY < height / 2)
                {
                    quadrants.topright += 1;
                }
                else if (endpositionX < width / 2 &&
                         endpositionY > height / 2)
                {
                    quadrants.bottomleft += 1;
                }
                else if (endpositionX > width / 2 &&
                         endpositionY > height / 2)
                {
                    quadrants.bottomright += 1;
                }
            }

            // Answer is sum of robots per quadrant multiplied
            return quadrants.topleft * quadrants.topright * quadrants.bottomleft * quadrants.bottomright;
        }
    }
}
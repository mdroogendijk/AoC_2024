namespace AdventOfCode.DayFifteen
{
    public class PartOne
    {
        public class Position
        {
            public (int, int) position;
            public char value;
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            long answer = 0;

            var map = new List<Position>();

            Queue<char> movements = new();

            // Generate map
            for (int y = 0; y < lines.Length; y++)
            {
                if (lines[y].Contains('#'))
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
                else if (!string.IsNullOrWhiteSpace(lines[y]))
                {
                    // Add robot movements to queue
                    lines[y].ToList().ForEach(movements.Enqueue);
                }
            }

            // Iterate through all the robot movements
            while (movements.TryDequeue(out char movement))
            {
                (int, int) direction = (0, 0);

                switch (movement)
                {
                    case '<':
                        direction = (-1, 0);
                        break;
                    case '>':
                        direction = (+1, 0);
                        break;
                    case '^':
                        direction = (0, -1);
                        break;
                    case 'v':
                        direction = (0, +1);
                        break;
                }

                var currentLoc = map.Where(x => x.value == '@').First().position;

                Queue<Position> newValues = new();
                newValues.Enqueue(new Position() 
                    { 
                        position = currentLoc, 
                        value = '.'
                    } 
                );

                // Continue until wall or empty space found
                while(true)
                {
                    var previousLoc = currentLoc;
                    currentLoc = (currentLoc.Item1 + direction.Item1, currentLoc.Item2 + direction.Item2);

                    var valueOldLoc = map.Where(x => x.position == previousLoc).Select(x => x.value).First();
                    var valueCurrentLoc = map.Where(x => x.position == currentLoc).Select(x => x.value).First();

                    newValues.Enqueue(new Position()
                    {
                        position = currentLoc,
                        value = valueOldLoc
                    });

                    if (valueCurrentLoc == '#')
                    {
                        break;
                    }
                    else if (valueCurrentLoc == '.')
                    {
                        while(newValues.TryDequeue(out Position? newPosition))
                        {
                            map[map.FindIndex(x => 
                                x.position.Item1 == newPosition.position.Item1 &&
                                x.position.Item2 == newPosition.position.Item2)] = newPosition;
                        }

                        break;
                    }
                }  
            }

            // Calculate and sum up GPS coordinates of all boxes
            answer = map.Where(x => x.value == 'O').Sum(x => x.position.Item1 + (100 * x.position.Item2));

            // Answer is the sum of all boxes' GPS coordinates
            return answer;
        }
    }
}
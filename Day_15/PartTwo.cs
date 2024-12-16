using System.Linq;

namespace AdventOfCode.DayFifteen
{
    public class PartTwo
    {
        public class Position
        {
            public (int, int) position;
            public char value;
        }

        public static void CreateMap(int x, int y, char value, List<Position> map)
        {
            map.Add(new Position()
            {
                position = (x, y),
                value = value
            });
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
                    var newX = 0;

                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        var value = lines[y][x];
                        switch (value)
                        {
                            case '#':
                                CreateMap(newX, y, value, map);
                                newX += 1;
                                CreateMap(newX, y, value, map);
                                break;
                            case 'O':
                                CreateMap(newX, y, '[', map);
                                newX += 1;
                                CreateMap(newX, y, ']', map);
                                break;
                            case '.':
                                CreateMap(newX, y, value, map);
                                newX += 1;
                                CreateMap(newX, y, value, map);
                                break;
                            case '@':
                                CreateMap(newX, y, value, map);
                                newX += 1;
                                CreateMap(newX, y, '.', map);
                                break;
                        }

                        newX += 1;
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

                Queue<(int, int)> currentLocs = new();
                currentLocs.Enqueue(map.Where(x => x.value == '@').First().position);

                Queue<Position> newValues = new();
                newValues.Enqueue(new Position()
                {
                    position = currentLocs.First(),
                    value = '.'
                });

                var allValid = true;

                do
                {
                    var dequeue = currentLocs.TryDequeue(out (int, int) currentLoc);

                    // Continue until wall or empty space found
                    while (dequeue)
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
                            allValid = false;
                            break;
                        }
                        else if (valueCurrentLoc == '.')
                        {
                            break;
                        }
                        // Loop over the other box edges as well (ugly but it works)
                        else if (direction.Item2 != 0)
                        {
                            if (valueCurrentLoc == '[')
                            {
                                if (!newValues.Any(x => x.position == (currentLoc.Item1 + 1, currentLoc.Item2)))
                                {
                                    newValues.Enqueue(new Position()
                                    {
                                        position = (currentLoc.Item1 + 1, currentLoc.Item2),
                                        value = '.'
                                    });
                                }

                                newValues.Enqueue(new Position()
                                {
                                    position = (currentLoc.Item1 + 1, currentLoc.Item2 + direction.Item2),
                                    value = ']'
                                });

                                currentLocs.Enqueue((currentLoc.Item1 + 1, currentLoc.Item2));
                            }
                            else if (valueCurrentLoc == ']')
                            {
                                if (!newValues.Any(x => x.position == (currentLoc.Item1 - 1, currentLoc.Item2)))
                                {
                                    newValues.Enqueue(new Position()
                                    {
                                        position = (currentLoc.Item1 - 1, currentLoc.Item2),
                                        value = '.'
                                    });
                                }

                                newValues.Enqueue(new Position()
                                {
                                    position = (currentLoc.Item1 - 1, currentLoc.Item2 + direction.Item2),
                                    value = '['
                                });

                                currentLocs.Enqueue((currentLoc.Item1 - 1, currentLoc.Item2));
                            }
                        }
                    }

                } while (currentLocs.Count > 0);

                if (map.Where(x => x.value == '@').First().position == (31,23))
                {
                    var debugtwo = true;
                }

                while (allValid && newValues.TryDequeue(out Position? newPosition))
                {
                    map[map.FindIndex(x =>
                        x.position.Item1 == newPosition.position.Item1 &&
                        x.position.Item2 == newPosition.position.Item2)] = newPosition;
                }
            }

            // Calculate and sum up GPS coordinates of all boxes
            answer = map.Where(x => x.value == '[').Sum(x => x.position.Item1 + (100 * x.position.Item2));

            // Answer is the sum of all boxes' GPS coordinates
            return answer;
        }
    }
}
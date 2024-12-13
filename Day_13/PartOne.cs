using System.ComponentModel;

namespace AdventOfCode.DayThirteen
{
    public class PartOne
    {
        public class ClawMachine
        {
            public (int, int) buttonA;
            public (int, int) buttonB;
            public (long, long) prizeLoc;
        }

        public class Way
        {
            public (long, long) currentLoc = (0, 0);
            public int pressCountButtonA = 0;
            public int pressCountButtonB = 0;
            public int cost = 0;
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var clawMachines = new Dictionary<int, ClawMachine>();

            var tokenCostButtonA = 3;
            var tokenCostButtonB = 1;

            long answer = 0;

            int clawMachineIndex = 0;

            var newMachine = new ClawMachine();

            // Gather all claw machine specifics
            foreach (var line in lines)
            {
                if (line.StartsWith("Button A:"))
                {
                    newMachine = new ClawMachine();

                    var x = Int32.Parse(new string(line.Split('+')[1].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    var y = Int32.Parse(new string(line.Split('+')[2].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    newMachine.buttonA = (x, y);
                }
                else if (line.StartsWith("Button B:"))
                {
                    var x = Int32.Parse(new string(line.Split('+')[1].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    var y = Int32.Parse(new string(line.Split('+')[2].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    newMachine.buttonB = (x, y);
                }
                else if (line.StartsWith("Prize:"))
                {
                    var x = Int32.Parse(new string(line.Split('=')[1].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    var y = Int32.Parse(new string(line.Split('=')[2].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    newMachine.prizeLoc = (x, y);

                    clawMachines.Add(clawMachineIndex, newMachine);
                    clawMachineIndex++;
                }
            }

            // Check cheapest way (if possible) for each claw machine
            foreach (var clawMachine in clawMachines)
            {
                var successfulWays = new List<Way>();
                var visited = new Dictionary<(long, long), long>();

                Stack<Way> stack = [];
                stack.Push(new Way());

                do
                {
                    Way way = stack.Pop();

                    // Skip if prize can't be achieved in this way
                    if (way.currentLoc.Item1 > clawMachine.Value.prizeLoc.Item1 ||
                        way.currentLoc.Item2 > clawMachine.Value.prizeLoc.Item2)
                    {
                        continue;
                    }
                    // Found successful way
                    else if (way.currentLoc == clawMachine.Value.prizeLoc)
                    {
                        successfulWays.Add(way);

                        continue;
                    }

                    // Check if location on the way is reached via the cheapest way possible
                    if (visited.TryGetValue(way.currentLoc, out long value))
                    {
                        if (way.cost < value)
                        {
                            visited[way.currentLoc] = way.cost;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        visited.Add(way.currentLoc, way.cost);
                    }

                    // Add way for when button A was pressed
                    stack.Push(
                        new Way()
                        {
                            pressCountButtonA = way.pressCountButtonA + 1,
                            pressCountButtonB = way.pressCountButtonB,
                            currentLoc = (way.currentLoc.Item1 + clawMachine.Value.buttonA.Item1, way.currentLoc.Item2 + clawMachine.Value.buttonA.Item2),
                            cost = way.cost + tokenCostButtonA
                        }
                    );

                    // Add way for when button B was pressed
                    stack.Push(
                        new Way()
                        {
                            pressCountButtonA = way.pressCountButtonA,
                            pressCountButtonB = way.pressCountButtonB + 1,
                            currentLoc = (way.currentLoc.Item1 + clawMachine.Value.buttonB.Item1, way.currentLoc.Item2 + clawMachine.Value.buttonB.Item2),
                            cost = way.cost + tokenCostButtonB
                        }
                    );

                } while (stack.Count() > 0);

                if (successfulWays.Count() > 0)
                {
                    // Select the cheapest way to the prize
                    answer += successfulWays.Min(x => x.cost);
                }
            }

            // Answer is the sum of cheapest ways to all attainable prizes
            return answer;
        }
    }
}
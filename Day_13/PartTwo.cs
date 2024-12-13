namespace AdventOfCode.DayThirteen
{
    public class PartTwo_backup
    {
        public class ClawMachine
        {
            public (int, int) buttonA;
            public (int, int) buttonB;
            public (long, long) prizeLoc;
        }

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var clawMachines = new Dictionary<int, ClawMachine>();

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
                    var x = 10000000000000 + Int32.Parse(new string(line.Split('=')[1].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    var y = 10000000000000 + Int32.Parse(new string(line.Split('=')[2].TakeWhile(x => Char.IsDigit(x)).ToArray()));
                    newMachine.prizeLoc = (x, y);

                    clawMachines.Add(clawMachineIndex, newMachine);
                    clawMachineIndex++;
                }
            }

            // Check cheapest way (if possible) for each claw machine
            foreach (var clawMachine in clawMachines)
            {
                // Updated to copy-pasted linear algebra (still new to me)
                var x = clawMachine.Value.prizeLoc.Item1;
                var y = clawMachine.Value.prizeLoc.Item2;

                var x1 = clawMachine.Value.buttonA.Item1;
                var x2 = clawMachine.Value.buttonB.Item1;
                var y1 = clawMachine.Value.buttonA.Item2;
                var y2 = clawMachine.Value.buttonB.Item2;

                var B = (y * x1 - x * y1) / (y2 * x1 - x2 * y1);
                var A = (x - B * x2) / x1;

                if (A >= 0 && B >= 0 && B * x2 + A * x1 == x && B * y2 + A * y1 == y)
                {
                    answer += A * 3 + B;
                }                
            }

            // Answer is the sum of cheapest ways to all attainable prizes
            return answer;
        }
    }
}
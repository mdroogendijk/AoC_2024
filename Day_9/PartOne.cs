namespace AdventOfCode.DayNine
{
    public class PartOne
    {
        public static long GetAnswer(string fileName)
        {
            var diskMap = Array.ConvertAll(File.ReadAllLines(fileName).First().ToCharArray(), c => (int)Char.GetNumericValue(c));

            long answer = 0;

            var blocks = new List<string>();

            var blockCount = 0;

            // Generate file block layout
            for (int i = 0; i < diskMap.Length; i++)
            {
                // Alternate between files and free space
                if (i % 2 == 0)
                {
                    blocks.AddRange(Enumerable.Repeat(blockCount.ToString(), diskMap.ElementAt(i)));
                    blockCount += 1;
                }
                else
                {
                    blocks.AddRange(Enumerable.Repeat(".", diskMap.ElementAt(i)));
                }
            }

            // Move blocks to free space until there's no free space left between file blocks
            while (blocks.FindLastIndex(x => x != ".") > blocks.FindIndex(x => x == "."))
            {
                var origin = blocks.FindLastIndex(x => x != ".");
                var destination = blocks.FindIndex(x => x == ".");

                blocks[destination] = blocks[origin];
                blocks[origin] = ".";
            }

            // Calculate checksum
            for (int i = 0; i < blocks.FindIndex(x => x == "."); i++)
            {
                answer += Int32.Parse(blocks[i]) * i;
            }

            // Answer is the sum of position * file id checksum
            return answer;
        }
    }
}
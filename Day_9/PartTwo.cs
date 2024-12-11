namespace AdventOfCode.DayNine
{
    public class PartTwo
    {
        public class DiskContent
        {
            public string content = String.Empty;
            public int index;
            public int length;
        }

        public static long GetAnswer(string fileName)
        {
            var diskMap = Array.ConvertAll(File.ReadAllLines(fileName).First().ToCharArray(), c => (int)Char.GetNumericValue(c));

            long answer = 0;

            var blocks = new List<string>();

            var blockCount = 0;

            var fileRanges = new List<DiskContent>();
            var freeSpaceRanges = new List<DiskContent>();

            // Generate file block layout
            for (int i = 0; i < diskMap.Length; i++)
            {
                // Alternate between files and free space
                if (i % 2 == 0)
                {
                    blocks.AddRange(Enumerable.Repeat(blockCount.ToString(), diskMap.ElementAt(i)));
                    fileRanges.Add(
                        new DiskContent() 
                            { content = blockCount.ToString(), index = blocks.Count - diskMap.ElementAt(i), length = diskMap.ElementAt(i) }
                    );
                    blockCount += 1;
                }
                else
                {
                    blocks.AddRange(Enumerable.Repeat(".", diskMap.ElementAt(i)));
                    freeSpaceRanges.Add(
                        new DiskContent()
                            { content = ".", index = blocks.Count - diskMap.ElementAt(i), length = diskMap.ElementAt(i) }
                    );
                }
            }

            fileRanges.Reverse();

            // Move file range to earliest free space range if possible
            foreach (var fileRange in fileRanges)
            {
                var destination = freeSpaceRanges
                    .Where(x => x.length >= fileRange.length && x.index < fileRange.index)
                    .FirstOrDefault();

                if (destination != null)
                {
                    for (int i = 0; i < fileRange.length; i++)
                    {
                        blocks[destination.index + i] = fileRange.content;
                        blocks[fileRange.index + i] = ".";
                    }

                    freeSpaceRanges[freeSpaceRanges.IndexOf(destination)].index += fileRange.length;
                    freeSpaceRanges[freeSpaceRanges.IndexOf(destination)].length -= fileRange.length;
                }
            }

            // Calculate checksum
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != ".")
                {
                    answer += Int64.Parse(blocks[i]) * i;
                }
            }

            // Answer is the sum of position * file id checksum
            return answer;
        }
    }
}
namespace AdventOfCode2023.Puzzles
{
    public class Day5
    {
        public static long Puzzle1()
        {
            var res = long.MaxValue;
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay5Puzzle1.txt");

            var seeds = input[0].Split().Skip(1).Select(seed => long.Parse(seed)).ToList();
            var myMaps = new List<List<(long StartRangeSource, long EndRangeSource, long AdjustmentDestination)>>();
            List<(long StartRangeSource, long EndRangeSource, long AdjustmentDestination)> currentMap = null;
            foreach (var line in input.Skip(2))
            {
                if (line.EndsWith(":"))
                {
                    currentMap = new List<(long StartRangeSource, long EndRangeSource, long AdjustmentDestination)>();
                }
                else if (line.Length != 0)
                {
                    var numbers = line.Split().Select(number => long.Parse(number)).ToArray();
                    currentMap!.Add((numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]));
                }
                else if (currentMap != null)
                {
                    myMaps.Add(currentMap);
                    currentMap = null;
                }
            }
            if (currentMap != null)
            {
                myMaps.Add(currentMap);
            }

            foreach (var seed in seeds)
            {
                var currentValue = seed;
                foreach (var map in myMaps)
                {
                    foreach (var range in map)
                    {
                        if (currentValue >= range.StartRangeSource && currentValue <= range.EndRangeSource)
                        {
                            currentValue += range.AdjustmentDestination;
                            break;
                        }
                    }
                }

                res = Math.Min(res, currentValue);
            }

            return res;
        }
    }
}

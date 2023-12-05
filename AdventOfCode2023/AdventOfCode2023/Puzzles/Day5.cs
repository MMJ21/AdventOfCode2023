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

        public static long Puzzle2()
        {
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay5Puzzle2.txt");

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

            var seedRanges = new List<(long StartRangeSeed, long EndRangeSeed)>();
            for (int i = 0; i < seeds.Count; i += 2)
            {
                seedRanges.Add((seeds[i], seeds[i] + seeds[i + 1] - 1));
            }

            foreach (var map in myMaps)
            {
                var orderedMap = map.OrderBy(x => x.StartRangeSource).ToList();

                var currentRanges = new List<(long StartRangeCurrent, long EndRangeCurrent)>();
                foreach (var range in seedRanges)
                {
                    var currentRange = range;

                    foreach (var mapRange in orderedMap)
                    {
                        if (currentRange.StartRangeSeed < mapRange.StartRangeSource)
                        {
                            currentRanges.Add((currentRange.StartRangeSeed, Math.Min(currentRange.EndRangeSeed, mapRange.StartRangeSource - 1)));
                            currentRange.StartRangeSeed = mapRange.StartRangeSource;
                            if (currentRange.StartRangeSeed > currentRange.EndRangeSeed)
                            {
                                break;
                            }
                        }
                        if (currentRange.StartRangeSeed <= mapRange.EndRangeSource)
                        {
                            currentRanges.Add((currentRange.StartRangeSeed + mapRange.AdjustmentDestination, Math.Min(currentRange.EndRangeSeed, mapRange.EndRangeSource) + mapRange.AdjustmentDestination));
                            currentRange.StartRangeSeed = mapRange.EndRangeSource + 1;
                            if (currentRange.StartRangeSeed > currentRange.EndRangeSeed)
                            {
                                break;
                            }
                        }
                    }

                    if (currentRange.StartRangeSeed <= currentRange.EndRangeSeed)
                    {
                        currentRanges.Add(currentRange);
                    }
                }

                seedRanges = currentRanges;
            }

            return seedRanges.Min(r => r.StartRangeSeed);
        }
    }
}

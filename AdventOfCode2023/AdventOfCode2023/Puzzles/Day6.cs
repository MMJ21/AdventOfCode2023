namespace AdventOfCode2023.Puzzles
{
    public class Day6
    {
        public static int Puzzle1()
        {
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay6Puzzle1.txt");
            var times = input[0].Split(":")[1]
                .Split()
                .Select(entry => int.TryParse(entry, out var time) ? time : (int?)null)
                .Where(time => time.HasValue)
                .Select(time => time.Value)
                .ToList();

            var distances = input[1].Split(":")[1]
                .Split()
                .Select(entry => int.TryParse(entry, out var distance) ? distance : (int?)null)
                .Where(distance => distance.HasValue)
                .Select(distance => distance.Value)
                .ToList();
            
            var recordsBeaten = new int[times.Count];

            for (int i = 0; i < times.Count; i++)
            {
                var myTime = times[i];
                for (int v = 1; v < myTime; v++)
                {
                    var travelledDistance = v * (myTime - v);
                    if (travelledDistance > distances[i])
                    {
                        recordsBeaten[i]++;
                    }
                }
            }

            return recordsBeaten.Aggregate(1, (a, b) => a * b);
        }

        public static int Puzzle2()
        {
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay6Puzzle2.txt");
            var time = long.Parse(input[0].Split(":")[1]
                .Split()
                .Select(entry => long.TryParse(entry, out var _) ? entry : string.Empty)
                .Where(entry => entry != string.Empty)
                .Aggregate((a, b) => a + b));


            var distance = long.Parse(input[1].Split(":")[1]
                .Split()
                .Select(entry => long.TryParse(entry, out var _) ? entry : string.Empty)
                .Where(entry => entry != string.Empty)
                .Aggregate((a, b) => a + b));

            var recordBeaten = 0;

            for (long v = 1; v < time; v++)
            {                
                var travelledDistance = v * (time - v);
                if (travelledDistance > distance)
                {
                    recordBeaten++;
                }
            }

            return recordBeaten;
        }

    }
}

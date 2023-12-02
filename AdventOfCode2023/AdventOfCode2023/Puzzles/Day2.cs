namespace AdventOfCode2023.Puzzles
{
    public class Day2
    {
        public static int Puzzle1()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay2Puzzle1.txt");
            foreach (var line in input)
            {
                var ballsOfEachColor = GetMaxBallsOfEachColor(line, out var id);

                if (ballsOfEachColor["red"] <= 12 && ballsOfEachColor["green"] <= 13 && ballsOfEachColor["blue"] <= 14)
                {
                    sum += id;
                }
            }

            return sum;
        }

        public static int Puzzle2()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay2Puzzle2.txt");
            foreach (var line in input)
            {
                var ballsOfEachColor = GetMaxBallsOfEachColor(line, out _);

                sum += ballsOfEachColor["red"] * ballsOfEachColor["green"] * ballsOfEachColor["blue"];
            }

            return sum;
        }

        public static Dictionary<string, int> GetMaxBallsOfEachColor(string line, out int id)
        {
            var result = new Dictionary<string, int>()
            {
                { "blue", 0 },
                { "red", 0 },
                { "green", 0 }
            };

            var splitFromId = line.Split(":");
            id = int.Parse(splitFromId[0].Split().Last().ToString());

            var splitSets = splitFromId[1].Split(";");
            foreach (var set in splitSets)
            {
                var splitBalls = set.Split(",");
                foreach (var balls in splitBalls)
                {
                    var splitNumberFromBall = balls.Split();
                    var numberOfBallsShown = int.Parse(splitNumberFromBall[1]);
                    if (numberOfBallsShown > result[splitNumberFromBall[2]])
                    {
                        result[splitNumberFromBall[2]] = numberOfBallsShown;
                    }
                }
            }

            return result;
        }
    }    
}

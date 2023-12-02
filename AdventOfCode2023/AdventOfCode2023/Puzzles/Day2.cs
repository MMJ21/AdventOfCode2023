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
                var myTempDictionary = new Dictionary<string, int>()
                {
                    { "blue", 0 },
                    { "red", 0 },
                    { "green", 0}
                };

                var splitId = line.Split(":");
                var id = int.Parse(splitId[0].Split().Last().ToString());

                var splitSets = splitId[1].Split(";");
                foreach (var set in splitSets)
                {
                    var splitBalls = set.Split(",");
                    foreach (var balls in splitBalls)
                    {
                        var splitNumberFromBall = balls.Split();
                        var numberOfBallsShown = int.Parse(splitNumberFromBall[1]);
                        if (numberOfBallsShown > myTempDictionary[splitNumberFromBall[2]])
                        {
                            myTempDictionary[splitNumberFromBall[2]] = numberOfBallsShown;
                        }
                    }
                }

                if (myTempDictionary["red"] <= 12 && myTempDictionary["green"] <= 13 && myTempDictionary["blue"] <= 14)
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
                var myTempDictionary = new Dictionary<string, int>()
                {
                    { "blue", 0 },
                    { "red", 0 },
                    { "green", 0}
                };

                var splitFromId = line.Split(":");

                var splitSets = splitFromId[1].Split(";");
                foreach (var set in splitSets)
                {
                    var splitBalls = set.Split(",");
                    foreach (var balls in splitBalls)
                    {
                        var splitNumberFromBall = balls.Split();
                        var numberOfBallsShown = int.Parse(splitNumberFromBall[1]);
                        if (numberOfBallsShown > myTempDictionary[splitNumberFromBall[2]])
                        {
                            myTempDictionary[splitNumberFromBall[2]] = numberOfBallsShown;
                        }
                    }
                }

                sum += myTempDictionary["red"] * myTempDictionary["green"] * myTempDictionary["blue"];
            }

            return sum;
        }
    }
}

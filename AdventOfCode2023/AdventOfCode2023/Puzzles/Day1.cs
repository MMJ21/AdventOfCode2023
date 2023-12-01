namespace AdventOfCode2023.Puzzles
{
    public static class Day1
    {
        public static int Puzzle1()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay1Puzzle1.txt");
            foreach (var line in input)
            { 
                var allNumbers = line.ToCharArray().Where(c => Char.IsDigit(c)).ToArray();
                var thisNumber = int.Parse(new string(new char[] { allNumbers[0], allNumbers[allNumbers.Length - 1] }));
                sum += thisNumber;
            }

            return sum;
        }

        public static int Puzzle2()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay1Puzzle1.txt");
            foreach (var line in input)
            {
                var allNumbers = line.ToCharArray().Where(c => Char.IsDigit(c)).ToArray();
                var thisNumber = int.Parse(new string(new char[] { allNumbers[0], allNumbers[allNumbers.Length - 1] }));
                sum += thisNumber;
            }

            return sum;
        }
    }
}

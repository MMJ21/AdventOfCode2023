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
                var allNumbers = line.ToCharArray().Where(c => char.IsDigit(c)).ToArray();
                var thisNumber = int.Parse(new string(new char[] { allNumbers[0], allNumbers[allNumbers.Length - 1] }));
                sum += thisNumber;
            }

            return sum;
        }

        public static int Puzzle2()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay1Puzzle2.txt");
            foreach (var line in input)
            {
                List<string> numbersInLine = new List<string>();

                var temp = string.Empty;
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        numbersInLine.Add(line[i].ToString());
                        temp = string.Empty;
                    }
                    else
                    {
                        temp += line[i];
                        foreach (var number in NumberDictionary.Numbers)
                        {
                            if (temp.Contains(number))
                            {
                                numbersInLine.Add(number);
                                temp = temp[temp.Length - 1].ToString();

                                break;
                            }
                        }
                    }
                }

                var firstDigit = char.IsDigit(numbersInLine.First()[0]) ? numbersInLine.First() : NumberDictionary.Dictionary.GetValueOrDefault(numbersInLine.First());
                var lastDigit = char.IsDigit(numbersInLine.Last()[0]) ? numbersInLine.Last() : NumberDictionary.Dictionary.GetValueOrDefault(numbersInLine.Last());

                var thisNumber = int.Parse(firstDigit + lastDigit);
                sum += thisNumber;
            }

            return sum;
        }
    }
}

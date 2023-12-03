namespace AdventOfCode2023.Puzzles
{
    public class Day3
    {
        public static int Puzzle1()
        {
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay3Puzzle1.txt");
            var sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var currentLine = input[i];
                var previousLine = i != 0 ? input[i - 1] : string.Empty;
                var nextLine = i + 1 < input.Length ? input[i + 1] : string.Empty;

                var myNumber = string.Empty;

                for (int j = 0; j < currentLine.Length; j++)
                {
                    var character = currentLine[j];
                    if (char.IsDigit(character))
                    {                        
                        myNumber += character;
                    }
                    else 
                    {
                        if (string.IsNullOrEmpty(myNumber)) { continue; }

                        sum += AddIfPartNumber(currentLine, previousLine, nextLine, myNumber, j);
                        myNumber = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(myNumber))
                {
                    sum += AddIfPartNumber(currentLine, previousLine, nextLine, myNumber, currentLine.Length);
                }
            }

            return sum;
        }

        public static int AddIfPartNumber(string currentLine, string previousLine, string nextLine, string myNumber, int iterator)
        {
            var isPartNumber = false;
            var startPos = iterator - myNumber.Length - 1 >= 0 ? iterator - myNumber.Length - 1 : 0;
            var finalPos = iterator < currentLine.Length ? iterator : currentLine.Length - 1;

            if (!char.IsLetterOrDigit(currentLine[startPos]) && currentLine[startPos] != '.') { isPartNumber = true; }
            if (!isPartNumber && !char.IsLetterOrDigit(currentLine[finalPos]) && currentLine[finalPos] != '.') { isPartNumber = true; }
            if (!isPartNumber && !string.IsNullOrEmpty(previousLine))
            {
                for (int k = startPos; k <= finalPos; k++)
                {
                    if (!char.IsLetterOrDigit(previousLine[k]) && previousLine[k] != '.')
                    {
                        isPartNumber = true;
                        break;
                    }
                }
            }
            if (!isPartNumber && !string.IsNullOrEmpty(nextLine))
            {
                for (int v = startPos; v <= finalPos; v++)
                {
                    if (!char.IsLetterOrDigit(nextLine[v]) && nextLine[v] != '.')
                    {
                        isPartNumber = true;
                        break;
                    }
                }
            }

            if (isPartNumber) { return int.Parse(myNumber); }
            else { return 0; }
        }
    }
}

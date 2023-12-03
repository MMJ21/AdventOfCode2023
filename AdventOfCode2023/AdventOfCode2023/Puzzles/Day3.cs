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

        public static int Puzzle2()
        {
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay3Puzzle2.txt");
            var sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var currentLine = input[i];
                var previousLine = i != 0 ? input[i - 1] : string.Empty;
                var nextLine = i + 1 < input.Length ? input[i + 1] : string.Empty;

                for (int j = 0; j < currentLine.Length; j++)
                {
                    var character = currentLine[j];
                    if (character == '*')
                    {
                        sum += AddIfGearRatio(currentLine, previousLine, nextLine, j);
                    }
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

        public static int AddIfGearRatio(string currentLine, string previousLine, string nextLine, int iterator)
        {
            var firstAdjacent = string.Empty;
            var secondAdjacent = string.Empty;

            var numberInPreviousLine = string.Empty;
            if (!string.IsNullOrEmpty(previousLine))
            {
                for (int i = 0; i < previousLine.Length; i++)
                {
                    if (char.IsDigit(previousLine[i]))
                    {
                        numberInPreviousLine += previousLine[i];
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(numberInPreviousLine)) { continue; }
                        if (iterator >= i - numberInPreviousLine.Length - 1 && iterator <= i)
                        {
                            if (string.IsNullOrEmpty(firstAdjacent))
                            {
                                firstAdjacent = numberInPreviousLine;
                            }
                            else
                            {
                                secondAdjacent = numberInPreviousLine;
                                break;
                            }
                        }

                        numberInPreviousLine = string.Empty;
                    }
                }
                if (!string.IsNullOrEmpty(numberInPreviousLine))
                {
                    if (iterator >= previousLine.Length - numberInPreviousLine.Length - 1 && iterator <= previousLine.Length)
                    {
                        firstAdjacent = numberInPreviousLine;
                    }
                }
            }

            var numberInCurrentLine = string.Empty;
            if (string.IsNullOrEmpty(secondAdjacent))
            {
                for (int i = 0; i < currentLine.Length; i++)
                {
                    if (char.IsDigit(currentLine[i]))
                    {
                        numberInCurrentLine += currentLine[i];
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(numberInCurrentLine)) { continue; }
                        if (iterator >= i - numberInCurrentLine.Length - 1 && iterator <= i)
                        {
                            if (string.IsNullOrEmpty(firstAdjacent))
                            {
                                firstAdjacent = numberInCurrentLine;
                            }
                            else
                            {
                                secondAdjacent = numberInCurrentLine;
                                break;
                            }
                        }

                        numberInCurrentLine = string.Empty;
                    }
                }
                if (!string.IsNullOrEmpty(numberInCurrentLine))
                {
                    if (iterator >= currentLine.Length - numberInCurrentLine.Length - 1 && iterator <= currentLine.Length)
                    {
                        if (string.IsNullOrEmpty(firstAdjacent))
                        {
                            firstAdjacent = numberInCurrentLine;
                        }
                        else
                        {
                            secondAdjacent = numberInCurrentLine;
                        }
                    }
                }
            }
            

            var numberInNextLine = string.Empty;
            if (string.IsNullOrEmpty(secondAdjacent) && !string.IsNullOrEmpty(nextLine))
            {
                for (int i = 0; i < nextLine.Length; i++)
                {
                    if (char.IsDigit(nextLine[i]))
                    {
                        numberInNextLine += nextLine[i];
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(numberInNextLine)) { continue; }
                        if (iterator >= i - numberInNextLine.Length - 1 && iterator <= i)
                        {
                            if (string.IsNullOrEmpty(firstAdjacent))
                            {
                                firstAdjacent = numberInNextLine;
                            }
                            else
                            {
                                secondAdjacent = numberInNextLine;
                                break;
                            }
                        }

                        numberInNextLine = string.Empty;
                    }
                }
                if (!string.IsNullOrEmpty(numberInNextLine))
                {
                    if (iterator >= nextLine.Length - numberInNextLine.Length - 1 && iterator <= nextLine.Length)
                    {
                        secondAdjacent = numberInNextLine;
                    }
                }
            }

            if (!string.IsNullOrEmpty(firstAdjacent) && !string.IsNullOrEmpty(secondAdjacent))
            {
                return int.Parse(firstAdjacent) * int.Parse(secondAdjacent);
            }
            else { return 0; }
        }
    }
}

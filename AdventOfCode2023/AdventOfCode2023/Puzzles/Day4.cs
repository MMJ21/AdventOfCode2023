using System.Diagnostics;

namespace AdventOfCode2023.Puzzles
{
    public class Day4
    {
        public static int Puzzle1()
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay4Puzzle1.txt");

            foreach (var line in input)
            {
                var splitCards = line.Split(':')[1].Split("|");
                var winnersCard = splitCards[0];
                var myCard = splitCards[1];

                var winningNumbers = winnersCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value)
                    .ToList();

                sum += (int)Math.Pow(2,
                    myCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue && winningNumbers.Contains(n.Value)).Count() - 1);
            }

            Console.WriteLine("Imperative: " + stopWatch.ElapsedMilliseconds);
            return sum;
        }

        public static int Puzzle1Functional()
        {
            var stopWatch = Stopwatch.StartNew();
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay4Puzzle1.txt");

            var sum = input.Select(line =>
            {
                var splitCards = line.Split(':')[1].Split("|");
                var winnersCard = splitCards[0];
                var myCard = splitCards[1];

                var winningNumbers = winnersCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value)
                    .ToList();

                return (int)Math.Pow(2,
                    myCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Count(n => n.HasValue && winningNumbers.Contains(n.Value)) - 1);
            }).Sum();

            Console.WriteLine("Functional: " + stopWatch.ElapsedMilliseconds);
            return sum;
        }

        public static int Puzzle2()
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay4Puzzle2.txt");
            Dictionary<int, int> cardsPerGame = new Dictionary<int, int>();
            for (int i = 1; i <= input.Length; i++)
            {
                cardsPerGame.Add(i, 1);
            }

            for (int j = 0; j < input.Length; j++)
            {
                var line = input[j];
                var splitCards = line.Split(':')[1].Split("|");
                var winnersCard = splitCards[0];
                var myCard = splitCards[1];
                var winningNumbers = winnersCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value)
                    .ToList();

                var addedCards = myCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue && winningNumbers.Contains(n.Value)).Count();

                for (int k = 1; k <= addedCards; k++)
                {
                    cardsPerGame[j + k + 1] += cardsPerGame[j + 1];
                }

                sum += cardsPerGame[j+1];
            }

            Console.WriteLine("Imperative: " + stopWatch.ElapsedMilliseconds);
            return sum;
        }

        public static int Puzzle2Functional()
        {
            var stopWatch = Stopwatch.StartNew();
            var input = File.ReadAllLines(Consts.MyInputPath + @"\inputDay4Puzzle2.txt");

            var cardsPerGame = Enumerable.Range(1, input.Length)
                .ToDictionary(i => i, _ => 1);

            var sum = input.Select((line, j) =>
            {
                var splitCards = line.Split(':')[1].Split("|");
                var winnersCard = splitCards[0];
                var myCard = splitCards[1];

                var winningNumbers = winnersCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value)
                    .ToList();

                var addedCards = myCard.Split(' ')
                    .Select(x => int.TryParse(x, out var n) ? n : (int?)null)
                    .Where(n => n.HasValue && winningNumbers.Contains(n.Value))
                    .Count();

                cardsPerGame = cardsPerGame
                    .ToDictionary(entry => entry.Key,
                        entry => entry.Key > j + 1 && entry.Key <= j + addedCards + 1
                            ? entry.Value + cardsPerGame[j + 1]
                            : entry.Value);

                return cardsPerGame[j + 1];
            }).Sum();
            Console.WriteLine("Functional: " + stopWatch.ElapsedMilliseconds);
            return sum;
        }
    }
}

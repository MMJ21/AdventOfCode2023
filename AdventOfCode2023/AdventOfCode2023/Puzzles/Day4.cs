﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles
{
    public class Day4
    {
        public static int Puzzle1()
        {
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
            return sum;
        }
        public static int Puzzle2()
        {
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
                var addedCards = 0;

                foreach (var number in myCard.Split(' '))
                {
                    if (int.TryParse(number, out var myNumber) && winningNumbers.Contains(myNumber))
                    {
                        addedCards++;
                    }
                }

                for (int k = 1; k <= addedCards; k++)
                {
                    cardsPerGame[j + k + 1] += cardsPerGame[j + 1];
                }

                sum += cardsPerGame[j+1];
            }
            return sum;
        }
    }
}

namespace AdventOfCode2023.Puzzles
{
    public class Day7
    {
        public static int Puzzle1()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay7Puzzle1.txt");
            var hands = input.Select(x => new Hand(x, false)).ToList();
            hands.Sort();
            for (int i = 0; i < hands.Count; i++)
            {
                sum += hands[i].Bid * (i + 1);
            }

            return sum;
        }

        public static int Puzzle2()
        {
            var sum = 0;
            var input = File.ReadAllLines(Consts.MyInputPath + "/inputDay7Puzzle2.txt");
            var hands = input.Select(x => new Hand(x, true)).ToList();
            hands.Sort();
            for (int i = 0; i < hands.Count; i++)
            {
                sum += hands[i].Bid * (i + 1);
            }

            return sum;
        }
    }
    public class Hand : IComparable<Hand>
    {
        public string Cards { get; set; }
        public int Bid { get; set; }
        public bool UseJokers { get; set; }
        public HandType Type { get; set; }

        public Hand(string line, bool useJokers)
        {
            Cards = line.Split(' ')[0];
            Bid = Convert.ToInt32(line.Split(' ')[1]);
            UseJokers = useJokers;
            Type = GetType(Cards);
        }
        private HandType GetType(string cards)
        {
            string allCards = "AKQJT98765432";
            HandType type = HandType.HighCard;
            foreach (char card in allCards)
            {
                if (UseJokers && card == 'J') { continue; }

                int count = cards.Count(c => c == card);
                if (count == 5)
                {
                    return HandType.FiveOfAKind;
                }
                if (count == 4)
                {
                    type = HandType.FourOfAKind;
                }
                if (count == 3 && type == HandType.Pair)
                {
                    return HandType.FullHouse;
                }
                if (count == 2 && type == HandType.ThreeOfAKind)
                {
                    return HandType.FullHouse;
                }
                if (count == 2 && type == HandType.Pair) 
                { 
                    type = HandType.TwoPair; 
                    continue; 
                }
                if (count == 3)
                {
                    type = HandType.ThreeOfAKind;
                }
                if (count == 2)
                {
                    type = HandType.Pair;
                }
            }
            if (UseJokers)
            {
                type = (type, cards.Count(c => c == 'J')) switch
                {
                    (_, 0) => type,
                    (HandType.HighCard, 1) => HandType.Pair,
                    (HandType.Pair, 1) => HandType.ThreeOfAKind,
                    (HandType.ThreeOfAKind, 1) => HandType.FourOfAKind,
                    (HandType.FourOfAKind, 1) => HandType.FiveOfAKind,
                    (HandType.TwoPair, 1) => HandType.FullHouse,
                    (HandType.HighCard, 2) => HandType.ThreeOfAKind,
                    (HandType.Pair, 2) => HandType.FourOfAKind,
                    (HandType.ThreeOfAKind, 2) => HandType.FiveOfAKind,
                    (HandType.HighCard, 3) => HandType.FourOfAKind,
                    (HandType.Pair, 3) => HandType.FiveOfAKind,
                    _ => HandType.FiveOfAKind,
                };
            }
            return type;
        }

        public int CompareTo(Hand? other)
        {
            string cardOrder = UseJokers ? "J23456789TQKA" : "23456789TJQKA";

            if ((int)Type > (int)other!.Type)
            {
                return 1;
            }

            else if ((int)Type < (int)other!.Type)
            {
                return -1;
            }

            for (int i = 0; i < Cards.Length; i++)
            {
                if (cardOrder.IndexOf(Cards[i]) > cardOrder.IndexOf(other!.Cards[i])) return 1;
                if (cardOrder.IndexOf(Cards[i]) < cardOrder.IndexOf(other!.Cards[i])) return -1;
            }
            return 0;
        }
    }
    public enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
}

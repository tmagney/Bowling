namespace Calculator
{
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private readonly List<int> rolls = new List<int>();
        
        public void Roll(int pins)
        {
            rolls.Add(pins);

            if (pins == 10 && rolls.Count < 18 && rolls.Count%2 > 0)
            {
                rolls.Add(0);
            }
        }

        public int GetScore()
        {
            var score = 0;

            for (var i = 0; i < 10; i++)
            {
                var rollIndex = i*2;

                score += IsSpare(rollIndex)
                        ? 10 + GetSpareBonus(rollIndex)
                        : IsStrike(rollIndex)
                            ? 10 + GetStrikeBonus(rollIndex)
                            : rolls.Skip(rollIndex).Take(2).Sum();
            }

            return score;
        }

        private int GetSpareBonus(int index)
        {
            return rolls[index + 2];
        }

        private int GetStrikeBonus(int index)
        {
            return !IsStrike(index + 2)
                       ? rolls.Skip(index + 2).Take(2).Sum()
                       :  10 + rolls.Skip(index + 2).First();
        }

        public bool IsStrike(int index)
        {
            return rolls[index] == 10 && index % 2 == 0;
        }

        public bool IsSpare(int index)
        {
            return !IsStrike(index) && rolls[index] + rolls[index + 1] == 10;
        }
    }    
}

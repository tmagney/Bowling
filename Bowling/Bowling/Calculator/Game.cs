namespace Calculator
{
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private List<int> rolls = new List<int>();

        public void Roll(int pins)
        {
            rolls.Add(pins);
        }

        public int GetScore()
        {
            return rolls.Sum();
        }
    }
}
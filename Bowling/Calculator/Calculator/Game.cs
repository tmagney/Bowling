namespace Calculator
{
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private int currentFrame = 0;
        private readonly List<Frame> frames = new List<Frame>();

        public Game()
        {
            for (int i = 0; i < 10; i++)
            {
                frames.Add(new Frame {FrameNumber = i});
            }
        }

        public void Roll(int pins)
        {
            var frame = frames[currentFrame];
            frame.Rolls.Add(pins);
            
            if (frame.IsStrike || frame.Rolls.Count > 1 && frame.FrameNumber != 9)
            {
                currentFrame++;
            }
        }

        public int Score()
        {
            var score = 0;
            foreach(var frame in frames)
            {
                if (frame.IsStrike)
                {
                    score += 10 + GetStrikeBonus(frame);
                }
                else if (frame.IsSpare)
                {
                    score += 10 + GetSpareBonus(frame);
                }
                else
                {
                    score += frame.Rolls.Sum();
                }
            }

            return score;
        }     

        private int GetSpareBonus(Frame frame)
        {
            return frame.FrameNumber == 9
                       ? frame.Rolls.Last()
                       : frames[frame.FrameNumber + 1].Rolls.First();
        }

        private int GetStrikeBonus(Frame frame)
        {
            if (frame.FrameNumber == 9)
            {
                return frame.Rolls.Sum() - 10;
            }

            return frames[frame.FrameNumber + 1].IsStrike
                       ? 10 + frames[frame.FrameNumber + 2].Rolls.First()
                       : frames[frame.FrameNumber + 1].Rolls.Sum();
        }
    }

    internal class Frame
    {
        public int FrameNumber { get; set; }
        public List<int> Rolls { get; }

        public bool IsSpare => Rolls.Count == 2 && Rolls.Sum() == 10;

        public bool IsStrike => Rolls.Count == 1 && Rolls.Sum() == 10;

        public Frame()
        {
            Rolls = new List<int>();
        }
    }    
}

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

            if ((frame.IsStrike || frame.Rolls.Count > 1) && frame.FrameNumber != 9)
            {
                currentFrame++;
            }
        }

        public int GetScore()
        {
            return frames.Sum(frame => frame.IsStrike 
                ? 10 + GetStrikeBonus(frame) 
                : frame.IsSpare 
                    ? 10 + GetSpareBonus(frame) 
                    : frame.Rolls.Sum());
        }

        private int GetSpareBonus(Frame frame)
        {
            return frame.FrameNumber == 9
                       ? frame.Rolls.Last()
                       : frames[frame.FrameNumber + 1].Rolls.First();
        }

        private int GetStrikeBonus(Frame frame)
        {
            return frame.FrameNumber == 9
                       ? frame.Rolls.Sum() - 10
                       : frame.FrameNumber == 8
                             ? frames[frame.FrameNumber + 1].Rolls.Take(2).Sum()
                             : frames[frame.FrameNumber + 1].IsStrike
                                   ? 10 + frames[frame.FrameNumber + 2].Rolls.First()
                                   : frames[frame.FrameNumber + 1].Rolls.Sum();
        }
    }

    internal class Frame
    {
        public int FrameNumber { get; set; }
        public List<int> Rolls { get; }

        public bool IsSpare => !IsStrike && Rolls.Sum() == 10;

        public bool IsStrike => Rolls.First() == 10;

        public Frame()
        {
            Rolls = new List<int>();
        }
    }    
}

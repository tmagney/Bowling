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
            for(int i = 0; i < 10; i++)
            {
                var frame = frames[i];

                if (frame.IsStrike)
                {
                    score += 10 + StrikeBonus(frame);
                }
                else if (frame.IsSpare)
                {
                    score += 10 + SpareBonus(frame);
                }
                else
                {
                    score += frame.Rolls.Sum();
                }
            }

            return score;
        }     

        private int SpareBonus(Frame frame)
        {
            return frame.FrameNumber == 9
                       ? frame.Rolls.Last()
                       : frames[frame.FrameNumber + 1].Rolls.First();
        }

        private int StrikeBonus(Frame frame)
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



//public int Score()
//{
//    var score = 0;

//    for (int i = 0; i < frames.Count; i++)
//    {
//        var frame = frames[i];
//        if (frame.IsSpare)
//        {
//            var nextFrame = frames[i + 1];
//            score += frame.Rolls.Sum() + nextFrame.Rolls.First();
//        }
//        else if (frame.IsStrike)
//        {
//            score += 10;

//            var nextFrame = frames[i + 1];
//            if (nextFrame.IsStrike)
//            {
//                score += 10;
//                var thirdFrame = frames[i + 2];
//                score += thirdFrame.Rolls.First();
//            }
//            else
//            {
//                score += nextFrame.Rolls.Sum();
//            }
//            //score += frame.Rolls.Sum() + nextFrame.Rolls.Sum();
//        }
//        else
//        {
//            score += frame.Rolls.Sum();
//        }
//    }

//    return score;
//}
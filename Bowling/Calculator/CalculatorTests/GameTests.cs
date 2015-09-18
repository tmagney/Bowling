namespace CalculatorTests
{
    using Calculator;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class GameTests
    {
        private Game sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Game();
        }

        [Test]
        public void RollAllZeros_ScoresZero()
        {
            for (int i = 0; i < 20; i++)
            {
                sut.Roll(0);
            }

            sut.Score().Should().Be(0);
        }

        [Test]
        public void RollAllOnes_Scores20()
        {
            for (int i = 0; i < 20; i++)
            {
                sut.Roll(1);
            }

            sut.Score().Should().Be(20);
        }

        [Test]
        public void RollSpareAndOnes_Scores29()
        {
            RollSpare();
            for (int i = 0; i < 18; i++)
            {
                sut.Roll(1);
            }

            sut.Score().Should().Be(29);
        }

        [Test]
        public void RollStrikeAndOnes_Scores30()
        {
            sut.Roll(10);
            for (int i = 0; i < 18; i++)
            {
                sut.Roll(1);
            }

            sut.Score().Should().Be(30);
        }

        [Test]
        public void RollThreeStrikesAndOnes_Scores77()
        {
            sut.Roll(10);
            sut.Roll(10);
            sut.Roll(10);

            for (int i = 0; i < 14; i++)
            {
                sut.Roll(1);
            }

            sut.Score().Should().Be(77);
        }
        
        [Test]
        public void RollOnesThenSpareAndStrikeInTenthFrame_Scores38()
        {
            for (var i = 0; i < 18; i++)
            {
                sut.Roll(1);
            }

            RollSpare();
            sut.Roll(10);

            sut.Score().Should().Be(38);
        }

        [Test]
        public void PerfectGame_Scores300()
        {
            for (var i = 0; i < 10; i++)
            {
                sut.Roll(10);
            }
        }
        
        private void RollSpare()
        {
            sut.Roll(5);
            sut.Roll(5);    
        }
    }
}

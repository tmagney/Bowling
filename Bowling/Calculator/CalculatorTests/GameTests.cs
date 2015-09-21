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
            RollMany(20, 0);

            sut.GetScore().Should().Be(0);
        }

        [Test]
        public void RollAllOnes_Scores20()
        {
            RollMany(20, 1);
            sut.GetScore().Should().Be(20);
        }

        [Test]
        public void RollSpareAndOnes_Scores29()
        {
            RollSpare();
            RollMany(18, 1);

            sut.GetScore().Should().Be(29);
        }

        [Test]
        public void RollStrikeAndOnes_Scores30()
        {
            sut.Roll(10);
            RollMany(18, 1);

            sut.GetScore().Should().Be(30);
        }
        
        [Test]
        public void PerfectGame_Scores300()
        {
            RollMany(10, 10);
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                sut.Roll(pins);
            }
        }
        
        private void RollSpare()
        {
            sut.Roll(5);
            sut.Roll(5);    
        }
    }
}

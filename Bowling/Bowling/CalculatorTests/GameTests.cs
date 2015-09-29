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
        public void RollZeros_Scoores0()
        {
            sut.GetScore().Should().Be(0);
        }

        [Test]
        public void RollOnes_Scores20()
        {
            RollMany(20, 1);
            sut.GetScore().Should().Be(20);
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                sut.Roll(pins);
            }
        }
    }
}

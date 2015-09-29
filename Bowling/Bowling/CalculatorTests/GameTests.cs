namespace CalculatorTests
{
    using Calculator;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class GameTests
    {
        private Game game;

        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }

        [Test]
        public void RollZeros_Scoores0()
        {
            game.GetScore().Should().Be(0);
        }
    }
}

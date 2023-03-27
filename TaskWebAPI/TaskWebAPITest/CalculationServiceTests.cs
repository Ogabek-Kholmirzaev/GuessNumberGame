using TaskWebAPI.Services;

namespace TestTaskWeb
{
    public class CalculationServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(7046, 8724, "M:2; P:0")]
        [TestCase(7046, 7842, "M:0; P:2")]
        [TestCase(7046, 7640, "M:2; P:2")]
        public void CheckGuessNumber_ReturnsCorrectResult(int secretNumber, int guessNumber, string expected)
        {
            var actual = CalculationService.CheckGuessNumber(secretNumber, guessNumber);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1234, true)]
        [TestCase(9878, false)]
        [TestCase(3425, true)]
        [TestCase(9999, false)]
        [TestCase(1042, true)]
        [TestCase(1241, false)]
        [TestCase(7566, false)]
        public void IsAllDigitsDifferent_ReturnsCorrectResult(int number, bool expected)
        {
            var actual = CalculationService.IsAllDigitsDifferent(number);

            Assert.AreEqual(expected, actual);
        }
    }
}
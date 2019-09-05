using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAnSmsMessage
{
    public class WhenCallingHeaderSize
    {
        [Theory]
        [InlineAutoData("000000", 4)]
        [InlineAutoData("", 0)]
        [InlineAutoData("050003CC0202", 7)]
        [InlineAutoData("050003CC0202032401", 11)]
        internal void ThenTheAnswerIsCorrect(string testHeader, int expected)
        {
            var actual = SmsMessage.HeaderSize(testHeader);

            actual.Should().Be(expected);
        }
    }
}

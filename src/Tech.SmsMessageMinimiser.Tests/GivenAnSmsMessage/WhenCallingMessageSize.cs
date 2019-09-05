using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAnSmsMessage
{
    public class WhenCallingMessageSize
    {
        [Theory]
        [InlineAutoData("", "", 0)]
        [InlineAutoData("xxx", "", 3)]
        [InlineAutoData("xxx", "050003CC0201", 10)]
        internal void TheResultIsCorrect(string message, string header, int expected)
        {
            var testMessage = new SmsMessage(message, header);
            var actual = testMessage.MessageSize;

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineAutoData("")]
        [InlineAutoData("xxx")]
        internal void ABlankMessageAlwaysReturnsZero(string header)
        {
            var testMessage = new SmsMessage(string.Empty, header);
            var actual = testMessage.MessageSize;

            actual.Should().Be(0);
        }
    }
}

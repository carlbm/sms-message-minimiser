using FluentAssertions;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAnSmsMessage
{
    public class WhenCallingPayloadLengthForConcatenatedMessage
    {
        [Theory]
        [InlineData("", 153)]
        [InlineData("00", 152)]
        [InlineData("0011", 150)]
        [InlineData("001122", 149)]
        [InlineData("00112233", 148)]
        public void ThenTheResultIsCorrect(string header, int expected)
        {
            var actual = SmsMessage.PayloadLengthForConcatenatedMessage(header);

            actual.Should().Be(expected);
        }
    }
}

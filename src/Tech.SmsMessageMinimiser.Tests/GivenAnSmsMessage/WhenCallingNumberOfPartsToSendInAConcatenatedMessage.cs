using System.Linq;
using FluentAssertions;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAnSmsMessage
{
    public class WhenCallingNumberOfPartsToSendInAConcatenatedMessage
    {
        [Theory]
        [InlineData(20, "", 1)]
        [InlineData(160, "", 2)]
        [InlineData(154, "", 2)]
        [InlineData(153, "", 1)]
        [InlineData(305, "", 2)]
        [InlineData(306, "", 2)]
        [InlineData(307, "", 3)]
        [InlineData(459, "", 3)]
        [InlineData(460, "", 4)]
        public void ThenTheResultIsCorrect(int payloadLength, string header, int expected)
        {
            var testPayload = string.Join("", Enumerable.Repeat('x', payloadLength));
            var testMessage = new SmsMessage(testPayload, header);
            var actual = testMessage.NumberOfPartsToSendInAConcatenatedMessage;

            actual.Should().Be(expected);
        }
    }
}

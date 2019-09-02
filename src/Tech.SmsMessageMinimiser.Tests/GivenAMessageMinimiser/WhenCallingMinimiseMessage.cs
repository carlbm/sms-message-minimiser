using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAMessageMinimiser
{
    public class WhenCallingMinimiseMessage
    {
        [Theory]
        [AutoData]
        public void ThenTheResultShouldBeAnSmsMessageGroup(MessageMinimiser sut, string testMessage)
        {
            var actual = sut.MinimiseMessage(testMessage);

            actual.Should().BeOfType<SmsMessageGroup>();
        }
    }
}

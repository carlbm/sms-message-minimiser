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

        public class WithABasicMessage
        {
            [Theory]
            [InlineAutoData("a test message with a £ symbol and a € symbol", "612074657374206d6573736167652077697468206120012073796d626f6c20616e642061201b652073796d626f6c")]
            public void ThenPayloadIsUntransformed(string testMessage, string expected, MessageMinimiser sut)
            {
                var actual = sut.MinimiseMessage(testMessage);

                actual.SmsMessages[0].Payload.Should().Be(expected);
            }

            public class ThatIs161CharactersLong
            {
                [Theory]
                [InlineAutoData("a test message with more than 160 characters " +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenThereIsOnlyOnePhysicalMessage(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesToSend.Should().Be(1);
                }

                [Theory]
                [InlineAutoData("a test message with more than 160 characters " +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenTheChargeIsForTwoMessages(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesCharged.Should().Be(2);
                }
            }

            public class ThatIs306CharactersLong
            {
                [Theory]
                [InlineAutoData("a test message with more than 160 characters xxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenThereIsOnlyOnePhysicalMessage(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesToSend.Should().Be(1);
                }

                [Theory]
                [InlineAutoData("a test message with more than 160 characters xxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenTheChargeIsForTwoMessages(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesCharged.Should().Be(2);
                }
            }

            public class ThatIs307CharactersLong
            {
                [Theory]
                [InlineAutoData("a test message with more than 160 characters xxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenThereIsOnlyOnePhysicalMessage(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesToSend.Should().Be(1);
                }

                [Theory]
                [InlineAutoData("a test message with more than 160 characters xxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
                public void ThenTheChargeIsForTwoMessages(string testMessage, MessageMinimiser sut)
                {
                    var actual = sut.MinimiseMessage(testMessage);

                    actual.NumberOfMessagesCharged.Should().Be(3);
                }
            }
        }

        public class WithFrenchCharacters
        {
            [Theory]
            [InlineAutoData("c'est envoyé")]
            public void ThenTheyUseTheBasicCharacterSet(string testMessage, MessageMinimiser sut)
            {
                var actual = sut.MinimiseMessage(testMessage);

                actual.SmsMessages[0].Udh.Should().BeEmpty();
            }
        }

        public class WithABasicShiftedCharacter
        {
            [Theory]
            [InlineAutoData("a £ and a €")]
            public void ThenTheyUseTheBasicCharacterSet(string testMessage, MessageMinimiser sut)
            {
                var actual = sut.MinimiseMessage(testMessage);

                actual.SmsMessages[0].Udh.Should().BeEmpty();
            }
        }

        // Keeping this for the encoding!
        //[Theory]
        //[InlineAutoData("a test message", "0x610x200x740x650x730x740x200x6D0x650x730x730x610x670x65")]
        //public void ThenTheSingleMessageIsCorrect(string testMessage, string expected, MessageMinimiser sut)
        //{
        //    var actual = sut.MinimiseMessage(testMessage);

        //    actual.SmsMessages[0].Payload.Should().Be(expected);
        //}
    }
}

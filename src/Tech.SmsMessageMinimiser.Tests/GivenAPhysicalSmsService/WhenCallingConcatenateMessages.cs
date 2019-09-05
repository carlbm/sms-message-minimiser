using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Tech.SmsMessageMinimiser.Services;
using Xunit;

namespace Tech.SmsMessageMinimiser.Tests.GivenAPhysicalSmsService
{
    public class WhenCallingConcatenateMessages
    {
        public class UsingBasicCharacterSetMessages
        {
            public class WithASingleMessage
            {
                [Theory]
                [InlineAutoData(160, "", 1)]
                [InlineAutoData(161, "", 2)]
                [InlineAutoData(306, "", 2)]
                [InlineAutoData(307, "", 3)]
                [InlineAutoData(459, "", 3)]
                [InlineAutoData(460, "", 4)]
                internal void ThenTheResultContainsTheCorrectNumberOfMessages(
                    int testPayloadLength, string testHeader, int expected, PhysicalSmsService sut)
                {
                    var testPayload = string.Join("", Enumerable.Repeat('x', testPayloadLength));
                    var testMessage = new SmsMessage(testPayload, testHeader);
                    var actual = sut.ConcatenateMessages(new List<SmsMessage> {testMessage});

                    actual.Should().HaveCount(expected);
                }
            }
        }

        public class UsingSingleShiftCharacterSetMessages
        {
            public class WithASingleMessage
            {
                [Theory]
                [InlineAutoData(155, "03240102", 1)]
                [InlineAutoData(156, "03240102", 2)]
                [InlineAutoData(296, "03240102", 2)]
                [InlineAutoData(297, "03240102", 3)]
                [InlineAutoData(444, "03240102", 3)]
                [InlineAutoData(445, "03240102", 4)]
                internal void ThenTheResultContainsTheCorrectNumberOfMessages(
                    int testPayloadLength, string testHeader, int expected, PhysicalSmsService sut)
                {
                    var testPayload = string.Join("", Enumerable.Repeat('x', testPayloadLength));
                    var testMessage = new SmsMessage(testPayload, testHeader);
                    var actual = sut.ConcatenateMessages(new List<SmsMessage> {testMessage});

                    actual.Should().HaveCount(expected);
                }
            }
        }

        public class UsingLockingShiftCharacterSetMessages
        {
            public class WithASingleMessage
            {
                [Theory]
                [InlineAutoData(155, "03250103", 1)]
                [InlineAutoData(156, "03250103", 2)]
                [InlineAutoData(296, "03250103", 2)]
                [InlineAutoData(297, "03250103", 3)]
                [InlineAutoData(444, "03250103", 3)]
                [InlineAutoData(445, "03250103", 4)]
                internal void ThenTheResultContainsTheCorrectNumberOfMessages(
                    int testPayloadLength, string testHeader, int expected, PhysicalSmsService sut)
                {
                    var testPayload = string.Join("", Enumerable.Repeat('x', testPayloadLength));
                    var testMessage = new SmsMessage(testPayload, testHeader);
                    var actual = sut.ConcatenateMessages(new List<SmsMessage> { testMessage });

                    actual.Should().HaveCount(expected);
                }
            }
        }

        public class UsingBothLockingShiftAndSingleShiftCharacterSetMessages
        {
            public class WithASingleMessage
            {
                [Theory]
                [InlineAutoData(150, "0325010303240102", 1)]
                [InlineAutoData(151, "0325010303240102", 2)]
                [InlineAutoData(288, "0325010303240102", 2)]
                [InlineAutoData(289, "0325010303240102", 3)]
                [InlineAutoData(432, "0325010303240102", 3)]
                [InlineAutoData(433, "0325010303240102", 4)]
                internal void ThenTheResultContainsTheCorrectNumberOfMessages(
                    int testPayloadLength, string testHeader, int expected, PhysicalSmsService sut)
                {
                    var testPayload = string.Join("", Enumerable.Repeat('x', testPayloadLength));
                    var testMessage = new SmsMessage(testPayload, testHeader);
                    var actual = sut.ConcatenateMessages(new List<SmsMessage> { testMessage });

                    actual.Should().HaveCount(expected);
                }
            }
        }
    }
}

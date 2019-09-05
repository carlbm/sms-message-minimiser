using System;
using System.Collections.Generic;

namespace Tech.SmsMessageMinimiser.Services
{
    internal interface IConcatenateSmsMessages
    {
        List<SmsMessage> ConcatenateMessages(List<SmsMessage> messages);
    }

    internal class PhysicalSmsService : IConcatenateSmsMessages
    {
        public List<SmsMessage> ConcatenateMessages(List<SmsMessage> messages)
        {
            var result = new List<SmsMessage>();

            foreach (var smsMessage in messages)
            {
                var messageSize = smsMessage.MessageSize;
                if (messageSize <= 160)
                {
                    result.Add(smsMessage);
                    continue;
                }

                var csmsReferenceNumber = new Random().Next(byte.MaxValue);
                var numberOfParts = smsMessage.NumberOfPartsToSendInAConcatenatedMessage;
                var sequence = 1;
                var payloadLeftToSend = smsMessage.Payload;
                var maxPayloadLength = SmsMessage.PayloadLengthForConcatenatedMessage(smsMessage.Udh);

                while (payloadLeftToSend.Length > 0)
                {
                    var payloadLength = Math.Min(payloadLeftToSend.Length, maxPayloadLength);

                    var header = $"050003{csmsReferenceNumber:x2}{numberOfParts:x2}{sequence:x2}{smsMessage.Udh}";
                    var payload = payloadLeftToSend.Substring(0, payloadLength);
                    payloadLeftToSend = payloadLeftToSend.Substring(payloadLength);
                    result.Add(new SmsMessage(payload, header));
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tech.SmsMessageMinimiser
{
    public class SmsMessageGroup
    {
        internal SmsMessageGroup(List<SmsMessage> messages)
        {
            SmsMessages = messages;
            NumberOfMessagesCharged = CalculateNumberOfChargableMessages();
        }

        public int NumberOfMessagesToSend => SmsMessages.Count;
        public int NumberOfMessagesCharged { get; private set; }
        public List<SmsMessage> SmsMessages { get; }

        private int CalculateNumberOfChargableMessages()
        {
            var chargableMessages = 0;

            foreach (var smsMessage in SmsMessages)
            {
                var headerLength = 0;
                if (!string.IsNullOrEmpty(smsMessage.Udh) && smsMessage.Udh.Length > 2)
                {
                    // The first two characters of the header are the length of the header
                    if (int.TryParse(smsMessage.Udh.Substring(0, 2), out int result))
                    {
                        headerLength += result;
                    }
                }
                chargableMessages += CalculateNumberOfMessages(smsMessage.Payload, headerLength); 
            }

            return chargableMessages;
        }

        private static readonly char[] DoubleCharacters = { '[', ']', '\\', '^', '{', '}', '|', '~', '€' };

        private static int CalculateNumberOfMessages(string message, int headerLength)
        {
            var doubleCharacterCount = message.Count(x => DoubleCharacters.Contains(x));

            if (message == string.Empty)
            {
                return 0;
            }

            var length = (message.Length + doubleCharacterCount) / 2 + headerLength;
            if (length <= 160)
            {
                return 1;
            }

            return (int)Math.Ceiling((decimal)length / 153);
        }
    }
}

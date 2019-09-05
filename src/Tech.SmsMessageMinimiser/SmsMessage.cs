using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tech.SmsMessageMinimiser.Tests")]
namespace Tech.SmsMessageMinimiser
{
    public class SmsMessage
    {
        internal SmsMessage(string message)
        {
            Payload = message;
        }

        internal SmsMessage(string message, string udh)
        {
            Payload = message;
            Udh = udh;
        }

        public string Udh { get; } = string.Empty;
        public string Payload { get; }

        internal int NumberOfPartsToSendInAConcatenatedMessage
        {
            get
            {
                // 160 less
                // 6 7-bit parts for concatenation header
                // the existing header
                // Rounded up to a 7-bit alignment
                // is the size of the payload per message
                var payloadSize = PayloadLengthForConcatenatedMessage(Udh);

                return (int)Math.Ceiling((decimal)Payload.Length / payloadSize);
            }
        }

        private const string ConcatenatedHeaderSample = "050003CC0201";
        internal static int PayloadLengthForConcatenatedMessage(string includingHeader = null)
        {
            var sampleHeader = ConcatenatedHeaderSample + includingHeader;
            var headerSize = HeaderSize(sampleHeader);

            return 160 - headerSize;
        }

        internal int MessageSize
        {
            get
            {
                {
                    if (Payload.Length == 0)
                    {
                        return 0;
                    }

                    return Payload.Length + HeaderSize(Udh);
                }
            }
        }

        internal static int HeaderSize(string headerToCalculateWith)
        {
            var numberOfHeaderBytes = Math.Max(headerToCalculateWith.Length / 2, 0);
            var bitsInHeader = numberOfHeaderBytes * 8;
            var sevenBitAlignedSize = (int)Math.Ceiling((decimal)bitsInHeader / 7);
            return sevenBitAlignedSize;
        }
    }
}

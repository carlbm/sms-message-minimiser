using System.Collections.Generic;

namespace Tech.SmsMessageMinimiser
{
    public class SmsMessageGroup
    {
        public int NumberOfMessages => SmsMessages.Count;
        public List<SmsMessage> SmsMessages { get; } = new List<SmsMessage>();
    }
}

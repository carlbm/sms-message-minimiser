namespace Tech.SmsMessageMinimiser
{
    public sealed class SmsMessage
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
    }
}

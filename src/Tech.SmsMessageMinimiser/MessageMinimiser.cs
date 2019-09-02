namespace Tech.SmsMessageMinimiser
{
    public class MessageMinimiser
    {
        public MessageMinimiser()
        {
            
        }

        public bool AllowUtf16 { get; set; }
        public int MaximumAllowedMessages { get; set; }

        public SmsMessageGroup MinimiseMessage(string message)
        {
            return new SmsMessageGroup();
        }
    }
}

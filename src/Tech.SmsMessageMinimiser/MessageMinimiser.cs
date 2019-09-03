using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tech.SmsMessageMinimiser.GsmShiftTables;

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
            var shiftTable = new BasicCharacterSet();
            var messages = new List<SmsMessage>();

            if (message.All(x => shiftTable.Characters.ContainsKey(x)))
            {
                // All characters are accounted for
                messages.Add(new SmsMessage(message));
            }
            else
            {
                var stringBuilder = new StringBuilder();
                foreach (var character in message)
                {
                    stringBuilder.Append(shiftTable.Characters[character]);
                }
            }

            return new SmsMessageGroup(messages);
        }
    }
}

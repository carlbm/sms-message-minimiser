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
            IAmACharacterSet matchingCharacterSet = null;

            if (message.All(x => shiftTable.BasicCharacters.ContainsKey(x) ||
                                 shiftTable.ShiftedCharacters.ContainsKey(x)))
            {
                matchingCharacterSet = new BasicCharacterSet();
            }

            if (matchingCharacterSet == null)
            {
                return new SmsMessageGroup(messages);
            }

            var stringBuilder = new StringBuilder();
            foreach (var character in message)
            {
                if (matchingCharacterSet.ShiftedCharacters.ContainsKey(character))
                {
                    stringBuilder.Append(matchingCharacterSet.ShiftCharacter.ToString("x2"));
                    stringBuilder.Append(matchingCharacterSet.ShiftedCharacters[character].ToString("x2"));
                }
                else
                {
                    stringBuilder.Append(matchingCharacterSet.BasicCharacters[character].ToString("x2"));
                }
            }
            messages.Add(new SmsMessage(stringBuilder.ToString(), matchingCharacterSet.Udh));
            
            return new SmsMessageGroup(messages);
        }
    }
}

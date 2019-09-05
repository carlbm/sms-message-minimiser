using System.Collections.Generic;

namespace Tech.SmsMessageMinimiser.GsmShiftTables
{
    internal interface IAmACharacterSet
    {
        Dictionary<char, byte> ShiftedCharacters { get; }
        Dictionary<char, byte> BasicCharacters { get; }

        int Priority { get; }
        string Udh { get; }
        byte ShiftCharacter { get; }
    }
}

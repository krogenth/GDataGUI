using System;
using System.Runtime.InteropServices;

namespace GDataGUI
{
    public class itemDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        struct itemData
        {
            short id;
            byte equipType;
            byte UsableType;
            short cost;
            byte icon;
            byte equipBitflag;
            byte weaponType;
            byte spellId;
            short spellPower;
            byte elementBitflag;
            byte autoEffect;
            short autoEffectPower;
            byte parameter1;
            byte parameter2;
            byte parameter3;
            byte parameter4;
            short parameterValue1;
            short parameterValue2;
            short parameterValue3;
            short parameterValue4;
        }
    }
}
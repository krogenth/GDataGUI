using System;
using System.Runtime.InteropServices;

namespace GDataGUI
{
    public class charDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        struct charData
        {
            int startExp;
            short nextExp;
            byte level;
            byte comboCrit;     //  low 4 = combo, high 4 = crit
            short str;
            short vit;
            short wit;
            short agi;
            short sp;
            int unknown1;
            int unknown2;
            int unknown3;
            int unknown4;
            byte fireLevel;
            byte waterLevel;
            byte windLevel;
            byte earthLevel;
            byte wpn1Level;
            byte wpn2Level;
            byte wpn3Level;
            byte wpn4Level;
            byte currMp1;
            byte maxMp1;
            byte currMp2;
            byte maxMp2;
            byte currMp3;
            byte maxMp3;
            byte magicMoveRes;
            byte plaguePoisonRes;
            byte paraSleepRes;
            byte fireWaterRes;
            byte windEarthRes;
            short weaponId;
            short shieldId;
            short armourId;
            short helmetId;
            short footId;
            short accId;
            short item1;
            short item2;
            short item3;
            short item4;
            short item5;
            short item6;
            short item7;
            short item8;
            short item9;
            short item10;
            short item11;
            short item12;
            int unknwon5;
        }
    }
}
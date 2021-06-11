using System;
using System.Runtime.InteropServices;

namespace GDataGUI
{
    public class enemyDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        struct enemyData
        {
            byte id;
            byte level;
            short hp;
            short str;
            short vit;
            short wit;
            short agi;
            short unknown1;
            short exp;
            short gold;
            byte unknown2;
            byte unknown3;
            short itemId1;
            short itemId2;
            byte itemChance1;
            byte itemChance2;
            byte mp;
            byte sp;
            byte spellId1;
            byte spellId2;
            byte spellId3;
            byte spellId4;
            byte comboCount;
            byte attackRange;
            byte unknown4;
            byte unknown5;
            short knockbackResist;
            byte moveBlockResist;
            byte spellBlockResist;
            byte sleepResist;
            byte poisonResist;
            byte fireWaterResist;
            byte windEarthResist;
            short size;
            short size2;            //  always same as first size
            short unknown6;
            short unknown7;
            short unknown8;
            short unknown9;
            byte unknown10;
            byte icon;
        }
    }
}
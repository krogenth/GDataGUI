using System;
using System.Runtime.InteropServices;

namespace GDataGUI
{
    public class moveDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        struct moveData
        {
            byte id;
            byte id2;               //  should always be same as id
            short cost;
            short castTime;
            short attackIp;         //  either ip damage, or knockback
            short power;
            byte targetType;
            byte moveLevel;         //  not entirely sure, might be specifying what MP level the spell/move is
            byte exp;
            byte radius;
            byte elementBitflag;
            byte weaponBitflag;
            byte movementType;
            byte critChance;
            byte effectType;
            byte unknown1;
            byte animation;
            byte unknown2;
            byte icon;
            byte unknown3;
        }
    }
}
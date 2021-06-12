using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GDataGUI
{
    public class moveDataClass
    {
        

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveName
        {
            public string name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveEName
        {
            public string extendedName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveDescription
        {
            public string description;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveStats
        {
            public byte id;
            public byte id2;               //  should always be same as id
            public short cost;
            public short castTime;
            public short attackIp;         //  either ip damage, or knockback
            public short power;
            public byte targetType;
            public byte moveLevel;         //  not entirely sure, might be specifying what MP level the spell/move is
            public byte exp;
            public byte radius;
            public byte elementBitflag;
            public byte weaponBitflag;
            public byte movementType;
            public byte critChance;
            public byte effectType;
            public byte unknown1;
            public byte animation;
            public byte unknown2;
            public byte icon;
            public byte unknown3;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveData
        {
            public moveName name = new moveName();
            public moveEName extendedName = new moveEName();
            public moveDescription description = new moveDescription();
            public moveStats data = new moveStats();
        }

        const string windtFile = "content/FIELD/WINDT.BIN";     //  item and move/spell data
        const int moveNamePointerOffset = 0x10;
        const int moveENamePointerOffset = 0x14;
        const int moveDescriptPointerOffset = 0x18;
        const int moveDataPointerOffset = 0x1C;

        public List<moveData> moves = new List<moveData>();

        public void readMoveData()
        {
            using (BinaryReader file = new BinaryReader(File.Open(windtFile, FileMode.Open)))
            {
                file.BaseStream.Seek(moveNamePointerOffset, SeekOrigin.Begin);
                int moveNameOffset = file.ReadInt32();
                int moveENameOffset = file.ReadInt32();
                int moveDescriptOffset = file.ReadInt32();
                int moveDataOffset = file.ReadInt32();

                file.BaseStream.Position = moveNameOffset;
                for(int i = 0; file.BaseStream.Position < moveENameOffset;)
                {
                    if (file.PeekChar() == 0)
                    {
                        file.BaseStream.Position++;
                        continue;
                    }

                    moves.Add(new moveData());

                    //  ReadString will not stop at a null(0x00) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        moves[i].name.name += file.ReadChar();
                    i++;
                    file.BaseStream.Position++;
                }
            }
        }
    }
}
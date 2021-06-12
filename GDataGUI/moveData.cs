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
        public class moveRequirements
        {
            public byte type1;
            public byte typeLevel1;
            public byte type2;
            public byte typeLevel2;
            public byte type3;
            public byte typeLevel3;
            public byte charBitflag;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class moveData
        {
            public moveName name = new moveName();
            public moveEName extendedName = new moveEName();
            public moveDescription description = new moveDescription();
            public moveStats data = new moveStats();
            public moveRequirements requirements = new moveRequirements();
        }

        const string windtFile = "content/FIELD/WINDT.BIN";     //  item and move/spell data
        const int moveNamePointerOffset = 0x10;
        const int moveENamePointerOffset = 0x14;
        const int moveDescriptPointerOffset = 0x18;
        const int moveDataPointerOffset = 0x1C;
        const int moveRequirePointerOffset = 0x20;

        int moveNameOffset;
        int moveENameOffset;
        int moveDescriptOffset;
        int moveDataOffset;
        int moveRequireOffset;

        public List<moveData> moves = new List<moveData>();

        public void readMoveData()
        {
            //  entries for name section are Null byte delimited
            //  extended name and description section are Null + End of Text delimited
            using (BinaryReader file = new BinaryReader(File.Open(windtFile, FileMode.Open)))
            {
                file.BaseStream.Seek(moveNamePointerOffset, SeekOrigin.Begin);
                moveNameOffset = file.ReadInt32();
                moveENameOffset = file.ReadInt32();
                moveDescriptOffset = file.ReadInt32();
                moveDataOffset = file.ReadInt32();
                moveRequireOffset = file.ReadInt32();

                //  move stream position to beginning of move name section
                file.BaseStream.Position = moveNameOffset;
                for(int i = 0; file.BaseStream.Position < moveENameOffset;)
                {
                    if (file.PeekChar() == 0)
                    {
                        file.BaseStream.Position++;
                        continue;
                    }

                    moves.Add(new moveData());

                    //  ReadString will not stop at a Null(0x00) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        moves[i].name.name += file.ReadChar();
                    i++;
                    file.BaseStream.Position++;
                }

                //  move stream position to beginning of move extended name section
                //  all moves should have an entry, so we can now base it off that
                file.BaseStream.Position = moveENameOffset;
                foreach(moveData move in moves)
                {
                    //  check for a null byte, if there is one, advance a byte
                    if(file.PeekChar() == 0)
                        file.BaseStream.Position += 2;

                    //  ReadString will not stop at a Null(0x00) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        move.extendedName.extendedName += file.ReadChar();
                }

                //  move stream position to beginning of move description section
                file.BaseStream.Position = moveDescriptOffset;
                foreach(moveData move in moves)
                {
                    //  check for a null byte, if there is one, advance 2 bytes
                    //  description section has 0x00 0x03 between entries
                    if (file.PeekChar() == 0)
                        file.BaseStream.Position += 2;

                    //  ReadString will not stop at a Null(0x00) or End of Text(0x03) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        move.description.description += file.ReadChar();
                }

                //  move stream position to beginning of move data section
                file.BaseStream.Position = moveDataOffset;
                foreach (moveData move in moves)
                {
                    move.data.id = file.ReadByte();
                    move.data.id2 = file.ReadByte();
                    move.data.cost = file.ReadInt16();
                    move.data.castTime = file.ReadInt16();
                    move.data.attackIp = file.ReadInt16();
                    move.data.power = file.ReadInt16();
                    move.data.targetType = file.ReadByte();
                    move.data.moveLevel = file.ReadByte();
                    move.data.exp = file.ReadByte();
                    move.data.radius = file.ReadByte();
                    move.data.elementBitflag = file.ReadByte();
                    move.data.weaponBitflag = file.ReadByte();
                    move.data.movementType = file.ReadByte();
                    move.data.critChance = file.ReadByte();
                    move.data.effectType = file.ReadByte();
                    move.data.unknown1 = file.ReadByte();
                    move.data.animation = file.ReadByte();
                    move.data.unknown2 = file.ReadByte();
                    move.data.icon = file.ReadByte();
                    move.data.unknown3 = file.ReadByte();
                }

                //  move stream position to beginning of move requirements section
                //  we add the size of the struct because every section starts with an empty entry, but this is not delimited
                file.BaseStream.Position = moveRequireOffset + System.Runtime.InteropServices.Marshal.SizeOf(typeof(moveRequirements));
                foreach(moveData move in moves)
                {
                    move.requirements.type1 = file.ReadByte();
                    move.requirements.typeLevel1 = file.ReadByte();
                    move.requirements.type2 = file.ReadByte();
                    move.requirements.typeLevel2 = file.ReadByte();
                    move.requirements.type3 = file.ReadByte();
                    move.requirements.typeLevel3 = file.ReadByte();
                    move.requirements.charBitflag = file.ReadByte();
                }
            }
        }

        public void writeMoveData()
        {
            //  check how many entries are currently stored, if there are none, then we never read in anything
            if (moves.Count == 0)
                return;

            using (BinaryWriter file = new BinaryWriter(File.Open(windtFile, FileMode.Open)))
            {
                //  for writing, we already know how many entries there are, so just write them immediately

                //  move stream position to beginning of move name section
                file.BaseStream.Position = moveNameOffset;
                foreach (moveData move in moves)
                {
                    file.Write((byte)0);        //  enter delimiter first because of empty first entry
                    file.Write(move.name.name.ToCharArray());
                }

                //  move stream position to beginning of move extended name section
                file.BaseStream.Position = moveENameOffset;
                foreach(moveData move in moves)
                {
                    file.Write((byte)0);        //  same as for the names, do delimiter first because of empty entry
                    file.Write((byte)3);
                    file.Write(move.extendedName.extendedName.ToCharArray());
                }

                //  move stream position to beginning of move description section
                file.BaseStream.Position = moveDescriptOffset;
                foreach (moveData move in moves)
                {
                    file.Write((byte)0);        //  same as the extended names
                    file.Write((byte)3);
                    file.Write(move.description.description.ToCharArray());
                }

                //  move stream position to beginning of move description section
                file.BaseStream.Position = moveDataOffset;
                foreach(moveData move in moves)
                {
                    file.Write(move.data.id);
                    file.Write(move.data.id2);
                    file.Write(move.data.cost);
                    file.Write(move.data.castTime);
                    file.Write(move.data.attackIp);
                    file.Write(move.data.power);
                    file.Write(move.data.targetType);
                    file.Write(move.data.moveLevel);
                    file.Write(move.data.exp);
                    file.Write(move.data.radius);
                    file.Write(move.data.elementBitflag);
                    file.Write(move.data.weaponBitflag);
                    file.Write(move.data.movementType);
                    file.Write(move.data.critChance);
                    file.Write(move.data.effectType);
                    file.Write(move.data.unknown1);
                    file.Write(move.data.animation);
                    file.Write(move.data.unknown2);
                    file.Write(move.data.icon);
                    file.Write(move.data.unknown3);
                }

                //  move stream position to beginning of move requirements section
                //  we add the size of the struct because every section starts with an empty entry, but this has to be jumped over
                file.BaseStream.Position = moveRequireOffset + System.Runtime.InteropServices.Marshal.SizeOf(typeof(moveRequirements));
                foreach (moveData move in moves)
                {
                    file.Write(move.requirements.type1);
                    file.Write(move.requirements.typeLevel1);
                    file.Write(move.requirements.type2);
                    file.Write(move.requirements.typeLevel2);
                    file.Write(move.requirements.type3);
                    file.Write(move.requirements.typeLevel3);
                    file.Write(move.requirements.charBitflag);
                }
            }
        }
    }
}
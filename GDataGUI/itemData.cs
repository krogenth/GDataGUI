using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GDataGUI
{
    public class itemDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class itemName
        {
            public string name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class itemEName
        {
            public string extendedName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class itemDescription
        {
            public string description;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class itemStats
        {
            public short id;
            public byte equipType;
            public byte usableType;
            public short cost;
            public byte icon;
            public byte charBitflag;
            public byte weaponType;
            public byte spellId;
            public short spellPower;
            public byte elementBitflag;
            public byte autoEffect;
            public short autoEffectPower;
            public byte parameter1;
            public byte parameter2;
            public byte parameter3;
            public byte parameter4;
            public short parameterValue1;
            public short parameterValue2;
            public short parameterValue3;
            public short parameterValue4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class itemData
        {
            public itemName name = new itemName();
            public itemEName extendedName = new itemEName();
            public itemDescription description = new itemDescription();
            public itemStats data = new itemStats();
        }

        const string windtFile = "content/FIELD/WINDT.BIN";     //  item and move/spell data
        const int itemNamePointerOffset = 0x00;
        const int itemENamePointerOffset = 0x04;
        const int itemDescriptPointerOffset = 0x08;
        const int itemDataPointerOffset = 0x0C;

        public List<itemData> items = new List<itemData>();

        public void readItemData()
        {
            //  entries for name section are Null byte delimited
            //  extended name and description section are Null + End of Text delimited
            using (BinaryReader file = new BinaryReader(File.Open(windtFile, FileMode.Open)))
            {
                file.BaseStream.Seek(itemNamePointerOffset, SeekOrigin.Begin);
                int itemNameOffset = file.ReadInt32();
                int itemENameOffset = file.ReadInt32();
                int itemDescriptOffset = file.ReadInt32();
                int itemDataOffset = file.ReadInt32();

                //  move stream position to beginning of move name section
                file.BaseStream.Position = itemNameOffset;
                for (int i = 0; file.BaseStream.Position < itemENameOffset;)
                {
                    if (file.PeekChar() == 0)
                    {
                        file.BaseStream.Position++;
                        continue;
                    }

                    items.Add(new itemData());

                    //  ReadString will not stop at a Null(0x00) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        items[i].name.name += file.ReadChar();
                    i++;
                    file.BaseStream.Position++;
                }

                //  move stream position to beginning of move extended name section
                //  all moves should have an entry, so we can now base it off that
                file.BaseStream.Position = itemENameOffset;
                foreach (itemData item in items)
                {
                    //  check for a null byte, if there is one, advance a byte
                    if (file.PeekChar() == 0)
                        file.BaseStream.Position += 2;

                    //  ReadString will not stop at a Null(0x00) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        item.extendedName.extendedName += file.ReadChar();
                }

                //  move stream position to beginning of move description section
                file.BaseStream.Position = itemDescriptOffset;
                foreach (itemData item in items)
                {
                    //  check for a null byte, if there is one, advance 2 bytes
                    //  description section has 0x00 0x03 between entries
                    if (file.PeekChar() == 0)
                        file.BaseStream.Position += 2;

                    //  ReadString will not stop at a Null(0x00) or End of Text(0x03) byte, so we must look for it ourselves
                    while (file.PeekChar() != 0)
                        item.description.description += file.ReadChar();
                }

                //  move stream position to beginning of move data section
                file.BaseStream.Position = itemDataOffset;
                foreach (itemData item in items)
                {
                    /*
                    public short id;
                    public byte equipType;
                    public byte usableType;
                    public short cost;
                    public byte icon;
                    public byte equipBitflag;
                    public byte weaponType;
                    public byte spellId;
                    public short spellPower;
                    public byte elementBitflag;
                    public byte autoEffect;
                    public short autoEffectPower;
                    public byte parameter1;
                    public byte parameter2;
                    public byte parameter3;
                    public byte parameter4;
                    public short parameterValue1;
                    public short parameterValue2;
                    public short parameterValue3;
                    public short parameterValue4;
                    */
                    item.data.id = file.ReadInt16();
                    item.data.equipType = file.ReadByte();
                    item.data.usableType = file.ReadByte();
                    item.data.cost = file.ReadInt16();
                    item.data.icon = file.ReadByte();
                    item.data.charBitflag = file.ReadByte();
                    item.data.weaponType = file.ReadByte();
                    item.data.spellId = file.ReadByte();
                    item.data.spellPower = file.ReadInt16();
                    item.data.elementBitflag = file.ReadByte();
                    item.data.autoEffect = file.ReadByte();
                    item.data.autoEffectPower = file.ReadInt16();
                    item.data.parameter1 = file.ReadByte();
                    item.data.parameter2 = file.ReadByte();
                    item.data.parameter3 = file.ReadByte();
                    item.data.parameter4 = file.ReadByte();
                    item.data.parameterValue1 = file.ReadInt16();
                    item.data.parameterValue2 = file.ReadInt16();
                    item.data.parameterValue3 = file.ReadInt16();
                    item.data.parameterValue4 = file.ReadInt16();
                }
            }
        }
    }
}
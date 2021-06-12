using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GDataGUI
{
    public class charDataClass
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public class charData
        {
            public int startExp;
            public short nextExp;
            public byte level;
            public byte comboCrit;     //  low 4 = combo, high 4 = crit
            public short hp;
            public short str;
            public short vit;
            public short wit;
            public short agi;
            public short sp;
            public int unknown1;
            public int unknown2;
            public int unknown3;
            public int unknown4;
            public byte fireLevel;
            public byte waterLevel;
            public byte windLevel;
            public byte earthLevel;
            public byte wpnLevel1;
            public byte wpnLevel2;
            public byte wpnLevel3;
            public byte wpnLevel4;
            public byte currMp1;
            public byte maxMp1;
            public byte currMp2;
            public byte maxMp2;
            public byte currMp3;
            public byte maxMp3;
            public byte magicMoveRes;
            public byte plaguePoisonRes;
            public byte paraSleepRes;
            public byte confuseCritRes;
            public byte fireWaterRes;
            public byte windEarthRes;
            public short weaponId;
            public short shieldId;
            public short armourId;
            public short helmetId;
            public short footId;
            public short accId;
            public short item1;
            public short item2;
            public short item3;
            public short item4;
            public short item5;
            public short item6;
            public short item7;
            public short item8;
            public short item9;
            public short item10;
            public short item11;
            public short item12;
            public int unknown5;
        }

        const string mcharFile = "content/BIN/MCHAR.DAT";     //  character starting data
        const int charStartDataPointerOffset = 0x00;
        const int unknownDataPointerOffset = 0x04;

        int charStartDataOffset;
        int unknownDataOffset;

        public string[] charNames = {"Justin", "Feena", "Sue", "Gadwin", "Rapp", "Milda", "Guido", "Liete"};
        public List<charData> chars = new List<charData>();

        public void readCharData()
        {
            //  entries for character starting data is not delimited
            using (BinaryReader file = new BinaryReader(File.Open(mcharFile, FileMode.Open)))
            {
                file.BaseStream.Seek(charStartDataPointerOffset, SeekOrigin.Begin);
                charStartDataOffset = file.ReadInt32();
                unknownDataOffset = file.ReadInt32();

                //  move stream position to beginning of character starting data section
                file.BaseStream.Position = charStartDataOffset;
                for (int i = 0; file.BaseStream.Position < unknownDataOffset;)
                {
                    chars.Add(new charData());

                    chars[i].startExp = file.ReadInt32();
                    chars[i].nextExp = file.ReadInt16();
                    chars[i].level = file.ReadByte();
                    chars[i].comboCrit = file.ReadByte();
                    chars[i].hp = file.ReadInt16();
                    chars[i].str = file.ReadInt16();
                    chars[i].vit = file.ReadInt16();
                    chars[i].wit = file.ReadInt16();
                    chars[i].agi = file.ReadInt16();
                    chars[i].sp = file.ReadInt16();
                    chars[i].unknown1 = file.ReadInt32();
                    chars[i].unknown2 = file.ReadInt32();
                    chars[i].unknown3 = file.ReadInt32();
                    chars[i].unknown4 = file.ReadInt32();
                    chars[i].fireLevel = file.ReadByte();
                    chars[i].waterLevel = file.ReadByte();
                    chars[i].windLevel = file.ReadByte();
                    chars[i].earthLevel = file.ReadByte();
                    chars[i].wpnLevel1 = file.ReadByte();
                    chars[i].wpnLevel2 = file.ReadByte();
                    chars[i].wpnLevel3 = file.ReadByte();
                    chars[i].wpnLevel4 = file.ReadByte();
                    chars[i].currMp1 = file.ReadByte();
                    chars[i].maxMp1 = file.ReadByte();
                    chars[i].currMp2 = file.ReadByte();
                    chars[i].maxMp2 = file.ReadByte();
                    chars[i].currMp3 = file.ReadByte();
                    chars[i].maxMp3 = file.ReadByte();
                    chars[i].magicMoveRes = file.ReadByte();
                    chars[i].plaguePoisonRes = file.ReadByte();
                    chars[i].paraSleepRes = file.ReadByte();
                    chars[i].confuseCritRes = file.ReadByte();
                    chars[i].fireWaterRes = file.ReadByte();
                    chars[i].windEarthRes = file.ReadByte();
                    chars[i].weaponId = file.ReadInt16();
                    chars[i].shieldId = file.ReadInt16();
                    chars[i].armourId = file.ReadInt16();
                    chars[i].helmetId = file.ReadInt16();
                    chars[i].footId = file.ReadInt16();
                    chars[i].accId = file.ReadInt16();
                    chars[i].item1 = file.ReadInt16();
                    chars[i].item2 = file.ReadInt16();
                    chars[i].item3 = file.ReadInt16();
                    chars[i].item4 = file.ReadInt16();
                    chars[i].item5 = file.ReadInt16();
                    chars[i].item6 = file.ReadInt16();
                    chars[i].item7 = file.ReadInt16();
                    chars[i].item8 = file.ReadInt16();
                    chars[i].item9 = file.ReadInt16();
                    chars[i].item10 = file.ReadInt16();
                    chars[i].item11 = file.ReadInt16();
                    chars[i].item12 = file.ReadInt16();
                    chars[i].unknown5 = file.ReadInt32();

                    i++;
                }
            }
        }

        public void writeCharData()
        {
            //  check how many entries are currently stored, if there are none, then we never read in anything
            if (chars.Count == 0)
                return;

            using (BinaryWriter file = new BinaryWriter(File.Open(mcharFile, FileMode.Open)))
            {
                file.BaseStream.Position = charStartDataOffset;
                foreach(charData chara in chars)
                {
                    file.Write(chara.startExp);
                    file.Write(chara.nextExp);
                    file.Write(chara.level);
                    file.Write(chara.comboCrit);
                    file.Write(chara.hp);
                    file.Write(chara.str);
                    file.Write(chara.vit);
                    file.Write(chara.wit);
                    file.Write(chara.agi);
                    file.Write(chara.sp);
                    file.Write(chara.unknown1);
                    file.Write(chara.unknown2);
                    file.Write(chara.unknown3);
                    file.Write(chara.unknown4);
                    file.Write(chara.fireLevel);
                    file.Write(chara.waterLevel);
                    file.Write(chara.windLevel);
                    file.Write(chara.earthLevel);
                    file.Write(chara.wpnLevel1);
                    file.Write(chara.wpnLevel2);
                    file.Write(chara.wpnLevel3);
                    file.Write(chara.wpnLevel4);
                    file.Write(chara.currMp1);
                    file.Write(chara.maxMp1);
                    file.Write(chara.currMp2);
                    file.Write(chara.maxMp2);
                    file.Write(chara.currMp3);
                    file.Write(chara.maxMp3);
                    file.Write(chara.magicMoveRes);
                    file.Write(chara.plaguePoisonRes);
                    file.Write(chara.paraSleepRes);
                    file.Write(chara.confuseCritRes);
                    file.Write(chara.fireWaterRes);
                    file.Write(chara.windEarthRes);
                    file.Write(chara.weaponId);
                    file.Write(chara.shieldId);
                    file.Write(chara.armourId);
                    file.Write(chara.helmetId);
                    file.Write(chara.footId);
                    file.Write(chara.accId);
                    file.Write(chara.item1);
                    file.Write(chara.item2);
                    file.Write(chara.item3);
                    file.Write(chara.item4);
                    file.Write(chara.item5);
                    file.Write(chara.item6);
                    file.Write(chara.item7);
                    file.Write(chara.item8);
                    file.Write(chara.item9);
                    file.Write(chara.item10);
                    file.Write(chara.item11);
                    file.Write(chara.item12);
                    file.Write(chara.unknown5);
                }
            }
        }
    }
}
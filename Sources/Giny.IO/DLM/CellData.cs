using Giny.Core.IO;
using System;

namespace Giny.IO.DLM
{
    public class CellData
    {
        public short Id
        {
            get;
            set;
        }
        public int Speed
        {
            get;
            set;
        }

        public sbyte MapChangeData
        {
            get;
            set;
        }

        public uint MoveZone
        {
            get;
            set;
        }

        public int Losmov
        {
            get;
             set;
        }

        public int Floor
        {
            get;
            set;
        }

        public int Arrow
        {
            get;
            set;
        }

        public bool Los
        {
            get;
            set;
        }

        public bool Mov
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public bool FarmCell
        {
            get;
            set;
        }

        public bool Blue
        {
            get;
            set;
        }

        public bool Red
        {
            get;
            set;
        }

        public bool NonWalkableDuringRP
        {
            get;
            set;
        }

        public bool NonWalkableDuringFight
        {
            get;
            set;
        }
        public bool HavenBagCell
        {
            get;
            set;
        }
        public byte LinkedZone
        {
            get;
            set;
        }
        public bool Walkable()
        {
            return (this.Losmov & 1) == 1;
        }
        public bool UseTopArrow()
        {
            return (this.Arrow & 1) != 0;
        }

        public bool UseBottomArrow()
        {
            return (this.Arrow & 2) != 0;
        }

        public bool UseRightArrow()
        {
            return (this.Arrow & 4) != 0;
        }

        public bool UseLeftArrow()
        {
            return (this.Arrow & 8) != 0;
        }
        public bool HasLinkedZoneFight()
        {
            return this.Mov && !this.NonWalkableDuringFight && !this.FarmCell && !this.HavenBagCell;
        }
        public bool HasLinkedZoneRP()
        {
            return this.Mov && !this.FarmCell;
        }
        public CellData()
        {

        }
        public void Serialize(BigEndianWriter writer, sbyte mapVersion)
        {
            writer.WriteSByte((sbyte)(Floor / 10d));

            if (Floor == -1280)
            {
                return;
            }

            if (mapVersion >= 9)
            {
                Losmov = (Mov ? 0 : 1) | (NonWalkableDuringFight ? 2 : 0) | (NonWalkableDuringRP ? 4 : 0) |
                    (Los ? 0 : 8) | (Blue ? 16 : 0) | (Red ? 32 : 0) | (Visible ? 64 : 0) | (FarmCell ? 128 : 0);

                if (mapVersion >= 10)
                {
                    Losmov |= (HavenBagCell ? 256 : 0);
                }
                writer.WriteShort((short)Losmov); 
            }
            else
            {
                throw new NotImplementedException();

                /* this.Los = (this.Losmov & 2) >> 1 == 1;
                this.Mov = (this.Losmov & 1) == 1;
                this.Visible = (this.Losmov & 64) >> 6 == 1;
                this.FarmCell = (this.Losmov & 32) >> 5 == 1;
                this.Blue = (this.Losmov & 16) >> 4 == 1;
                this.Red = (this.Losmov & 8) >> 3 == 1;
                this.NonWalkableDuringRP = (this.Losmov & 128) >> 7 == 1;
                this.NonWalkableDuringFight = (this.Losmov & 4) >> 2 == 1;

                writer.WriteByte((byte)Losmov); */

            }
            writer.WriteSByte((sbyte)Speed);
            writer.WriteSByte(MapChangeData);


            if (mapVersion > 5)
            {
                writer.WriteByte((byte)MoveZone);
            }
            if (mapVersion > 10 && (this.HasLinkedZoneRP() || this.HasLinkedZoneFight()))
            {
                writer.WriteByte(LinkedZone);
            }
            if (mapVersion > 7 && mapVersion < 9)
            {
                writer.WriteSByte((sbyte)(Arrow & 15)); // not sure
            }
        }
        public CellData(BigEndianReader reader, sbyte mapVersion, short id)
        {
            this.Id = id;
            int tmpbytesv9 = 0;
            bool topArrow = false;
            bool bottomArrow = false;
            bool rightArrow = false;
            bool leftArrow = false;
            int tmpBits = 0;

            this.Floor = reader.ReadSByte() * 10;

            if (this.Floor == -1280)
            {
                return;
            }
            if (mapVersion >= 9)
            {
                tmpbytesv9 = reader.ReadShort();
                this.Losmov = tmpbytesv9;

                this.Mov = (tmpbytesv9 & 1) == 0;
                this.NonWalkableDuringFight = (tmpbytesv9 & 2) != 0;
                this.NonWalkableDuringRP = (tmpbytesv9 & 4) != 0;
                this.Los = (tmpbytesv9 & 8) == 0; // => différent de 8 ?
                this.Blue = (tmpbytesv9 & 16) != 0;
                this.Red = (tmpbytesv9 & 32) != 0;
                this.Visible = (tmpbytesv9 & 64) != 0; //  != => strictent égal?
                this.FarmCell = (tmpbytesv9 & 128) != 0;
                if (mapVersion >= 10)
                {
                    this.HavenBagCell = (tmpbytesv9 & 256) != 0;
                    topArrow = (tmpbytesv9 & 512) != 0;
                    bottomArrow = (tmpbytesv9 & 1024) != 0;
                    rightArrow = (tmpbytesv9 & 2048) != 0;
                    leftArrow = (tmpbytesv9 & 4096) != 0;
                }
                else
                {
                    topArrow = (tmpbytesv9 & 256) != 0;
                    bottomArrow = (tmpbytesv9 & 512) != 0;
                    rightArrow = (tmpbytesv9 & 1024) != 0;
                    leftArrow = (tmpbytesv9 & 2048) != 0;
                }
            }
            else
            {
                this.Losmov = reader.ReadByte();
                this.Los = (this.Losmov & 2) >> 1 == 1;
                this.Mov = (this.Losmov & 1) == 1;
                this.Visible = (this.Losmov & 64) >> 6 == 1;
                this.FarmCell = (this.Losmov & 32) >> 5 == 1;
                this.Blue = (this.Losmov & 16) >> 4 == 1;
                this.Red = (this.Losmov & 8) >> 3 == 1;
                this.NonWalkableDuringRP = (this.Losmov & 128) >> 7 == 1;
                this.NonWalkableDuringFight = (this.Losmov & 4) >> 2 == 1;
            }
            this.Speed = reader.ReadSByte();
            this.MapChangeData = reader.ReadSByte();

            if (mapVersion > 5)
            {
                this.MoveZone = reader.ReadByte();
            }
            if (mapVersion > 10 && (this.HasLinkedZoneRP() || this.HasLinkedZoneFight()))
            {
                this.LinkedZone = reader.ReadByte();
            }
            if (mapVersion > 7 && mapVersion < 9)
            {
                tmpBits = reader.ReadSByte();
                this.Arrow = 15 & tmpBits;
            }
        }
    }
}

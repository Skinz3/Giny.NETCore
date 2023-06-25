using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffectDate : ObjectEffect  
    { 
        public new const ushort Id = 616;
        public override ushort TypeId => Id;

        public short year;
        public byte month;
        public byte day;
        public byte hour;
        public byte minute;

        public ObjectEffectDate()
        {
        }
        public ObjectEffectDate(short year,byte month,byte day,byte hour,byte minute,short actionId)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (year < 0)
            {
                throw new System.Exception("Forbidden value (" + year + ") on element year.");
            }

            writer.WriteVarShort((short)year);
            if (month < 0)
            {
                throw new System.Exception("Forbidden value (" + month + ") on element month.");
            }

            writer.WriteByte((byte)month);
            if (day < 0)
            {
                throw new System.Exception("Forbidden value (" + day + ") on element day.");
            }

            writer.WriteByte((byte)day);
            if (hour < 0)
            {
                throw new System.Exception("Forbidden value (" + hour + ") on element hour.");
            }

            writer.WriteByte((byte)hour);
            if (minute < 0)
            {
                throw new System.Exception("Forbidden value (" + minute + ") on element minute.");
            }

            writer.WriteByte((byte)minute);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            year = (short)reader.ReadVarUhShort();
            if (year < 0)
            {
                throw new System.Exception("Forbidden value (" + year + ") on element of ObjectEffectDate.year.");
            }

            month = (byte)reader.ReadByte();
            if (month < 0)
            {
                throw new System.Exception("Forbidden value (" + month + ") on element of ObjectEffectDate.month.");
            }

            day = (byte)reader.ReadByte();
            if (day < 0)
            {
                throw new System.Exception("Forbidden value (" + day + ") on element of ObjectEffectDate.day.");
            }

            hour = (byte)reader.ReadByte();
            if (hour < 0)
            {
                throw new System.Exception("Forbidden value (" + hour + ") on element of ObjectEffectDate.hour.");
            }

            minute = (byte)reader.ReadByte();
            if (minute < 0)
            {
                throw new System.Exception("Forbidden value (" + minute + ") on element of ObjectEffectDate.minute.");
            }

        }


    }
}









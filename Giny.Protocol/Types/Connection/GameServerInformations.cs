using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameServerInformations  
    { 
        public const ushort Id = 7410;
        public virtual ushort TypeId => Id;

        public short id;
        public byte type;
        public bool isMonoAccount;
        public byte status;
        public byte completion;
        public bool isSelectable;
        public byte charactersCount;
        public byte charactersSlots;
        public double date;

        public GameServerInformations()
        {
        }
        public GameServerInformations(short id,byte type,bool isMonoAccount,byte status,byte completion,bool isSelectable,byte charactersCount,byte charactersSlots,double date)
        {
            this.id = id;
            this.type = type;
            this.isMonoAccount = isMonoAccount;
            this.status = status;
            this.completion = completion;
            this.isSelectable = isSelectable;
            this.charactersCount = charactersCount;
            this.charactersSlots = charactersSlots;
            this.date = date;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,isMonoAccount);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isSelectable);
            writer.WriteByte((byte)_box0);
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            writer.WriteByte((byte)type);
            writer.WriteByte((byte)status);
            writer.WriteByte((byte)completion);
            if (charactersCount < 0)
            {
                throw new System.Exception("Forbidden value (" + charactersCount + ") on element charactersCount.");
            }

            writer.WriteByte((byte)charactersCount);
            if (charactersSlots < 0)
            {
                throw new System.Exception("Forbidden value (" + charactersSlots + ") on element charactersSlots.");
            }

            writer.WriteByte((byte)charactersSlots);
            if (date < -9.00719925474099E+15 || date > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element date.");
            }

            writer.WriteDouble((double)date);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            isMonoAccount = BooleanByteWrapper.GetFlag(_box0,0);
            isSelectable = BooleanByteWrapper.GetFlag(_box0,1);
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameServerInformations.id.");
            }

            type = (byte)reader.ReadByte();
            status = (byte)reader.ReadByte();
            if (status < 0)
            {
                throw new System.Exception("Forbidden value (" + status + ") on element of GameServerInformations.status.");
            }

            completion = (byte)reader.ReadByte();
            if (completion < 0)
            {
                throw new System.Exception("Forbidden value (" + completion + ") on element of GameServerInformations.completion.");
            }

            charactersCount = (byte)reader.ReadByte();
            if (charactersCount < 0)
            {
                throw new System.Exception("Forbidden value (" + charactersCount + ") on element of GameServerInformations.charactersCount.");
            }

            charactersSlots = (byte)reader.ReadByte();
            if (charactersSlots < 0)
            {
                throw new System.Exception("Forbidden value (" + charactersSlots + ") on element of GameServerInformations.charactersSlots.");
            }

            date = (double)reader.ReadDouble();
            if (date < -9.00719925474099E+15 || date > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element of GameServerInformations.date.");
            }

        }


    }
}









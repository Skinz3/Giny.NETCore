using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyGuestInformations  
    { 
        public const ushort Id = 3541;
        public virtual ushort TypeId => Id;

        public long guestId;
        public long hostId;
        public string name;
        public EntityLook guestLook;
        public byte breed;
        public bool sex;
        public PlayerStatus status;
        public PartyEntityBaseInformation[] entities;

        public PartyGuestInformations()
        {
        }
        public PartyGuestInformations(long guestId,long hostId,string name,EntityLook guestLook,byte breed,bool sex,PlayerStatus status,PartyEntityBaseInformation[] entities)
        {
            this.guestId = guestId;
            this.hostId = hostId;
            this.name = name;
            this.guestLook = guestLook;
            this.breed = breed;
            this.sex = sex;
            this.status = status;
            this.entities = entities;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (guestId < 0 || guestId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element guestId.");
            }

            writer.WriteVarLong((long)guestId);
            if (hostId < 0 || hostId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + hostId + ") on element hostId.");
            }

            writer.WriteVarLong((long)hostId);
            writer.WriteUTF((string)name);
            guestLook.Serialize(writer);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
            writer.WriteShort((short)entities.Length);
            for (uint _i8 = 0;_i8 < entities.Length;_i8++)
            {
                (entities[_i8] as PartyEntityBaseInformation).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            PartyEntityBaseInformation _item8 = null;
            guestId = (long)reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element of PartyGuestInformations.guestId.");
            }

            hostId = (long)reader.ReadVarUhLong();
            if (hostId < 0 || hostId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + hostId + ") on element of PartyGuestInformations.hostId.");
            }

            name = (string)reader.ReadUTF();
            guestLook = new EntityLook();
            guestLook.Deserialize(reader);
            breed = (byte)reader.ReadByte();
            sex = (bool)reader.ReadBoolean();
            uint _id7 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id7);
            status.Deserialize(reader);
            uint _entitiesLen = (uint)reader.ReadUShort();
            for (uint _i8 = 0;_i8 < _entitiesLen;_i8++)
            {
                _item8 = new PartyEntityBaseInformation();
                _item8.Deserialize(reader);
                entities[_i8] = _item8;
            }

        }


    }
}









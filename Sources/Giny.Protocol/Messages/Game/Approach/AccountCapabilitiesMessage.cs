using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AccountCapabilitiesMessage : NetworkMessage  
    { 
        public  const ushort Id = 4667;
        public override ushort MessageId => Id;

        public int accountId;
        public bool tutorialAvailable;
        public int breedsVisible;
        public int breedsAvailable;
        public byte status;
        public bool canCreateNewCharacter;

        public AccountCapabilitiesMessage()
        {
        }
        public AccountCapabilitiesMessage(int accountId,bool tutorialAvailable,int breedsVisible,int breedsAvailable,byte status,bool canCreateNewCharacter)
        {
            this.accountId = accountId;
            this.tutorialAvailable = tutorialAvailable;
            this.breedsVisible = breedsVisible;
            this.breedsAvailable = breedsAvailable;
            this.status = status;
            this.canCreateNewCharacter = canCreateNewCharacter;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,tutorialAvailable);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,canCreateNewCharacter);
            writer.WriteByte((byte)_box0);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            if (breedsVisible < 0)
            {
                throw new System.Exception("Forbidden value (" + breedsVisible + ") on element breedsVisible.");
            }

            writer.WriteVarInt((int)breedsVisible);
            if (breedsAvailable < 0)
            {
                throw new System.Exception("Forbidden value (" + breedsAvailable + ") on element breedsAvailable.");
            }

            writer.WriteVarInt((int)breedsAvailable);
            writer.WriteByte((byte)status);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            tutorialAvailable = BooleanByteWrapper.GetFlag(_box0,0);
            canCreateNewCharacter = BooleanByteWrapper.GetFlag(_box0,1);
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of AccountCapabilitiesMessage.accountId.");
            }

            breedsVisible = (int)reader.ReadVarUhInt();
            if (breedsVisible < 0)
            {
                throw new System.Exception("Forbidden value (" + breedsVisible + ") on element of AccountCapabilitiesMessage.breedsVisible.");
            }

            breedsAvailable = (int)reader.ReadVarUhInt();
            if (breedsAvailable < 0)
            {
                throw new System.Exception("Forbidden value (" + breedsAvailable + ") on element of AccountCapabilitiesMessage.breedsAvailable.");
            }

            status = (byte)reader.ReadByte();
        }


    }
}









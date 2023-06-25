using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LockableStateUpdateHouseDoorMessage : LockableStateUpdateAbstractMessage  
    { 
        public new const ushort Id = 990;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;
        public bool secondHand;

        public LockableStateUpdateHouseDoorMessage()
        {
        }
        public LockableStateUpdateHouseDoorMessage(int houseId,int instanceId,bool secondHand,bool locked)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
            this.secondHand = secondHand;
            this.locked = locked;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }

            writer.WriteInt((int)instanceId);
            writer.WriteBoolean((bool)secondHand);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of LockableStateUpdateHouseDoorMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of LockableStateUpdateHouseDoorMessage.instanceId.");
            }

            secondHand = (bool)reader.ReadBoolean();
        }


    }
}









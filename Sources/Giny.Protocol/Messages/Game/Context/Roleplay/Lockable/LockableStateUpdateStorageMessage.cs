using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage  
    { 
        public new const ushort Id = 9912;
        public override ushort MessageId => Id;

        public double mapId;
        public int elementId;

        public LockableStateUpdateStorageMessage()
        {
        }
        public LockableStateUpdateStorageMessage(double mapId,int elementId,bool locked)
        {
            this.mapId = mapId;
            this.elementId = elementId;
            this.locked = locked;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element elementId.");
            }

            writer.WriteVarInt((int)elementId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of LockableStateUpdateStorageMessage.mapId.");
            }

            elementId = (int)reader.ReadVarUhInt();
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element of LockableStateUpdateStorageMessage.elementId.");
            }

        }


    }
}









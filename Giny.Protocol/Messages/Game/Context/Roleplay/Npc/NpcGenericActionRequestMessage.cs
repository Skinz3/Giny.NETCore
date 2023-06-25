using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NpcGenericActionRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6477;
        public override ushort MessageId => Id;

        public int npcId;
        public byte npcActionId;
        public double npcMapId;

        public NpcGenericActionRequestMessage()
        {
        }
        public NpcGenericActionRequestMessage(int npcId,byte npcActionId,double npcMapId)
        {
            this.npcId = npcId;
            this.npcActionId = npcActionId;
            this.npcMapId = npcMapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)npcId);
            if (npcActionId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcActionId + ") on element npcActionId.");
            }

            writer.WriteByte((byte)npcActionId);
            if (npcMapId < 0 || npcMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + npcMapId + ") on element npcMapId.");
            }

            writer.WriteDouble((double)npcMapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            npcId = (int)reader.ReadInt();
            npcActionId = (byte)reader.ReadByte();
            if (npcActionId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcActionId + ") on element of NpcGenericActionRequestMessage.npcActionId.");
            }

            npcMapId = (double)reader.ReadDouble();
            if (npcMapId < 0 || npcMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + npcMapId + ") on element of NpcGenericActionRequestMessage.npcMapId.");
            }

        }


    }
}









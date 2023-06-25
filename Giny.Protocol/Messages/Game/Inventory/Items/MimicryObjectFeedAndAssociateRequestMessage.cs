using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MimicryObjectFeedAndAssociateRequestMessage : SymbioticObjectAssociateRequestMessage  
    { 
        public new const ushort Id = 3920;
        public override ushort MessageId => Id;

        public int foodUID;
        public byte foodPos;
        public bool preview;

        public MimicryObjectFeedAndAssociateRequestMessage()
        {
        }
        public MimicryObjectFeedAndAssociateRequestMessage(int foodUID,byte foodPos,bool preview,int symbioteUID,byte symbiotePos,int hostUID,byte hostPos)
        {
            this.foodUID = foodUID;
            this.foodPos = foodPos;
            this.preview = preview;
            this.symbioteUID = symbioteUID;
            this.symbiotePos = symbiotePos;
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (foodUID < 0)
            {
                throw new System.Exception("Forbidden value (" + foodUID + ") on element foodUID.");
            }

            writer.WriteVarInt((int)foodUID);
            if (foodPos < 0 || foodPos > 255)
            {
                throw new System.Exception("Forbidden value (" + foodPos + ") on element foodPos.");
            }

            writer.WriteByte((byte)foodPos);
            writer.WriteBoolean((bool)preview);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            foodUID = (int)reader.ReadVarUhInt();
            if (foodUID < 0)
            {
                throw new System.Exception("Forbidden value (" + foodUID + ") on element of MimicryObjectFeedAndAssociateRequestMessage.foodUID.");
            }

            foodPos = (byte)reader.ReadSByte();
            if (foodPos < 0 || foodPos > 255)
            {
                throw new System.Exception("Forbidden value (" + foodPos + ") on element of MimicryObjectFeedAndAssociateRequestMessage.foodPos.");
            }

            preview = (bool)reader.ReadBoolean();
        }


    }
}









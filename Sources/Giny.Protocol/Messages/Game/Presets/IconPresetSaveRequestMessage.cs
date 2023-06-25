using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IconPresetSaveRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7054;
        public override ushort MessageId => Id;

        public short presetId;
        public byte symbolId;
        public bool updateData;

        public IconPresetSaveRequestMessage()
        {
        }
        public IconPresetSaveRequestMessage(short presetId,byte symbolId,bool updateData)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.updateData = updateData;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presetId);
            if (symbolId < 0)
            {
                throw new System.Exception("Forbidden value (" + symbolId + ") on element symbolId.");
            }

            writer.WriteByte((byte)symbolId);
            writer.WriteBoolean((bool)updateData);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = (short)reader.ReadShort();
            symbolId = (byte)reader.ReadByte();
            if (symbolId < 0)
            {
                throw new System.Exception("Forbidden value (" + symbolId + ") on element of IconPresetSaveRequestMessage.symbolId.");
            }

            updateData = (bool)reader.ReadBoolean();
        }


    }
}









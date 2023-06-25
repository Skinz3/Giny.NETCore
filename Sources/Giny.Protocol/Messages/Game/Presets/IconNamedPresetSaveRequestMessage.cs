using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IconNamedPresetSaveRequestMessage : IconPresetSaveRequestMessage  
    { 
        public new const ushort Id = 8602;
        public override ushort MessageId => Id;

        public string name;
        public byte type;

        public IconNamedPresetSaveRequestMessage()
        {
        }
        public IconNamedPresetSaveRequestMessage(string name,byte type,short presetId,byte symbolId,bool updateData)
        {
            this.name = name;
            this.type = type;
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.updateData = updateData;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of IconNamedPresetSaveRequestMessage.type.");
            }

        }


    }
}









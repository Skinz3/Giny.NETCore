using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolFightPreparationUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 146;
        public override ushort MessageId => Id;

        public byte idolSource;
        public Idol[] idols;

        public IdolFightPreparationUpdateMessage()
        {
        }
        public IdolFightPreparationUpdateMessage(byte idolSource,Idol[] idols)
        {
            this.idolSource = idolSource;
            this.idols = idols;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)idolSource);
            writer.WriteShort((short)idols.Length);
            for (uint _i2 = 0;_i2 < idols.Length;_i2++)
            {
                writer.WriteShort((short)(idols[_i2] as Idol).TypeId);
                (idols[_i2] as Idol).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            Idol _item2 = null;
            idolSource = (byte)reader.ReadByte();
            if (idolSource < 0)
            {
                throw new System.Exception("Forbidden value (" + idolSource + ") on element of IdolFightPreparationUpdateMessage.idolSource.");
            }

            uint _idolsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _idolsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<Idol>((short)_id2);
                _item2.Deserialize(reader);
                idols[_i2] = _item2;
            }

        }


    }
}









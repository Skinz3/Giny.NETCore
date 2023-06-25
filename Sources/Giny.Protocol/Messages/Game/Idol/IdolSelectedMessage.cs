using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolSelectedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2306;
        public override ushort MessageId => Id;

        public short idolId;
        public bool activate;
        public bool party;

        public IdolSelectedMessage()
        {
        }
        public IdolSelectedMessage(short idolId,bool activate,bool party)
        {
            this.idolId = idolId;
            this.activate = activate;
            this.party = party;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,activate);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,party);
            writer.WriteByte((byte)_box0);
            if (idolId < 0)
            {
                throw new System.Exception("Forbidden value (" + idolId + ") on element idolId.");
            }

            writer.WriteVarShort((short)idolId);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            activate = BooleanByteWrapper.GetFlag(_box0,0);
            party = BooleanByteWrapper.GetFlag(_box0,1);
            idolId = (short)reader.ReadVarUhShort();
            if (idolId < 0)
            {
                throw new System.Exception("Forbidden value (" + idolId + ") on element of IdolSelectedMessage.idolId.");
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolSelectErrorMessage : NetworkMessage  
    { 
        public  const ushort Id = 7051;
        public override ushort MessageId => Id;

        public byte reason;
        public short idolId;
        public bool activate;
        public bool party;

        public IdolSelectErrorMessage()
        {
        }
        public IdolSelectErrorMessage(byte reason,short idolId,bool activate,bool party)
        {
            this.reason = reason;
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
            writer.WriteByte((byte)reason);
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
            reason = (byte)reader.ReadByte();
            if (reason < 0)
            {
                throw new System.Exception("Forbidden value (" + reason + ") on element of IdolSelectSystem.ExceptionMessage.reason.");
            }

            idolId = (short)reader.ReadVarUhShort();
            if (idolId < 0)
            {
                throw new System.Exception("Forbidden value (" + idolId + ") on element of IdolSelectSystem.ExceptionMessage.idolId.");
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DebtsUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7615;
        public override ushort MessageId => Id;

        public byte action;
        public DebtInformation[] debts;

        public DebtsUpdateMessage()
        {
        }
        public DebtsUpdateMessage(byte action,DebtInformation[] debts)
        {
            this.action = action;
            this.debts = debts;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)action);
            writer.WriteShort((short)debts.Length);
            for (uint _i2 = 0;_i2 < debts.Length;_i2++)
            {
                writer.WriteShort((short)(debts[_i2] as DebtInformation).TypeId);
                (debts[_i2] as DebtInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            DebtInformation _item2 = null;
            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of DebtsUpdateMessage.action.");
            }

            uint _debtsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _debtsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<DebtInformation>((short)_id2);
                _item2.Deserialize(reader);
                debts[_i2] = _item2;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiConfirmationMessage : NetworkMessage  
    { 
        public  const ushort Id = 7202;
        public override ushort MessageId => Id;

        public long kamas;
        public long amount;
        public short rate;
        public byte action;
        public string transaction;

        public HaapiConfirmationMessage()
        {
        }
        public HaapiConfirmationMessage(long kamas,long amount,short rate,byte action,string transaction)
        {
            this.kamas = kamas;
            this.amount = amount;
            this.rate = rate;
            this.action = action;
            this.transaction = transaction;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element amount.");
            }

            writer.WriteVarLong((long)amount);
            if (rate < 0)
            {
                throw new System.Exception("Forbidden value (" + rate + ") on element rate.");
            }

            writer.WriteVarShort((short)rate);
            writer.WriteByte((byte)action);
            writer.WriteUTF((string)transaction);
        }
        public override void Deserialize(IDataReader reader)
        {
            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of HaapiConfirmationMessage.kamas.");
            }

            amount = (long)reader.ReadVarUhLong();
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element of HaapiConfirmationMessage.amount.");
            }

            rate = (short)reader.ReadVarUhShort();
            if (rate < 0)
            {
                throw new System.Exception("Forbidden value (" + rate + ") on element of HaapiConfirmationMessage.rate.");
            }

            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of HaapiConfirmationMessage.action.");
            }

            transaction = (string)reader.ReadUTF();
        }


    }
}









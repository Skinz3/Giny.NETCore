using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiBuyValidationMessage : HaapiValidationMessage  
    { 
        public new const ushort Id = 1897;
        public override ushort MessageId => Id;

        public long amount;
        public string email;

        public HaapiBuyValidationMessage()
        {
        }
        public HaapiBuyValidationMessage(long amount,string email,byte action,byte code)
        {
            this.amount = amount;
            this.email = email;
            this.action = action;
            this.code = code;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element amount.");
            }

            writer.WriteVarLong((long)amount);
            writer.WriteUTF((string)email);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            amount = (long)reader.ReadVarUhLong();
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element of HaapiBuyValidationMessage.amount.");
            }

            email = (string)reader.ReadUTF();
        }


    }
}









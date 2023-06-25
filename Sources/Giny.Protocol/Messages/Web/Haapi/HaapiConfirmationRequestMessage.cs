using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiConfirmationRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8332;
        public override ushort MessageId => Id;

        public long kamas;
        public long ogrines;
        public short rate;
        public byte action;

        public HaapiConfirmationRequestMessage()
        {
        }
        public HaapiConfirmationRequestMessage(long kamas,long ogrines,short rate,byte action)
        {
            this.kamas = kamas;
            this.ogrines = ogrines;
            this.rate = rate;
            this.action = action;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
            if (ogrines < 0 || ogrines > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + ogrines + ") on element ogrines.");
            }

            writer.WriteVarLong((long)ogrines);
            if (rate < 0)
            {
                throw new System.Exception("Forbidden value (" + rate + ") on element rate.");
            }

            writer.WriteVarShort((short)rate);
            writer.WriteByte((byte)action);
        }
        public override void Deserialize(IDataReader reader)
        {
            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of HaapiConfirmationRequestMessage.kamas.");
            }

            ogrines = (long)reader.ReadVarUhLong();
            if (ogrines < 0 || ogrines > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + ogrines + ") on element of HaapiConfirmationRequestMessage.ogrines.");
            }

            rate = (short)reader.ReadVarUhShort();
            if (rate < 0)
            {
                throw new System.Exception("Forbidden value (" + rate + ") on element of HaapiConfirmationRequestMessage.rate.");
            }

            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of HaapiConfirmationRequestMessage.action.");
            }

        }


    }
}









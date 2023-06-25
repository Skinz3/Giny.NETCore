using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DebtsDeleteMessage : NetworkMessage  
    { 
        public  const ushort Id = 9246;
        public override ushort MessageId => Id;

        public byte reason;
        public double[] debts;

        public DebtsDeleteMessage()
        {
        }
        public DebtsDeleteMessage(byte reason,double[] debts)
        {
            this.reason = reason;
            this.debts = debts;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)reason);
            writer.WriteShort((short)debts.Length);
            for (uint _i2 = 0;_i2 < debts.Length;_i2++)
            {
                if (debts[_i2] < 0 || debts[_i2] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + debts[_i2] + ") on element 2 (starting at 1) of debts.");
                }

                writer.WriteDouble((double)debts[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val2 = double.NaN;
            reason = (byte)reader.ReadByte();
            if (reason < 0)
            {
                throw new System.Exception("Forbidden value (" + reason + ") on element of DebtsDeleteMessage.reason.");
            }

            uint _debtsLen = (uint)reader.ReadUShort();
            debts = new double[_debtsLen];
            for (uint _i2 = 0;_i2 < _debtsLen;_i2++)
            {
                _val2 = (double)reader.ReadDouble();
                if (_val2 < 0 || _val2 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of debts.");
                }

                debts[_i2] = (double)_val2;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightTackledMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 3891;
        public override ushort MessageId => Id;

        public double[] tacklersIds;

        public GameActionFightTackledMessage()
        {
        }
        public GameActionFightTackledMessage(double[] tacklersIds,short actionId,double sourceId)
        {
            this.tacklersIds = tacklersIds;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)tacklersIds.Length);
            for (uint _i1 = 0;_i1 < tacklersIds.Length;_i1++)
            {
                if (tacklersIds[_i1] < -9.00719925474099E+15 || tacklersIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + tacklersIds[_i1] + ") on element 1 (starting at 1) of tacklersIds.");
                }

                writer.WriteDouble((double)tacklersIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            base.Deserialize(reader);
            uint _tacklersIdsLen = (uint)reader.ReadUShort();
            tacklersIds = new double[_tacklersIdsLen];
            for (uint _i1 = 0;_i1 < _tacklersIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9.00719925474099E+15 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of tacklersIds.");
                }

                tacklersIds[_i1] = (double)_val1;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EmotePlayMassiveMessage : EmotePlayAbstractMessage  
    { 
        public new const ushort Id = 6236;
        public override ushort MessageId => Id;

        public double[] actorIds;

        public EmotePlayMassiveMessage()
        {
        }
        public EmotePlayMassiveMessage(double[] actorIds,short emoteId,double emoteStartTime)
        {
            this.actorIds = actorIds;
            this.emoteId = emoteId;
            this.emoteStartTime = emoteStartTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)actorIds.Length);
            for (uint _i1 = 0;_i1 < actorIds.Length;_i1++)
            {
                if (actorIds[_i1] < -9.00719925474099E+15 || actorIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + actorIds[_i1] + ") on element 1 (starting at 1) of actorIds.");
                }

                writer.WriteDouble((double)actorIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            base.Deserialize(reader);
            uint _actorIdsLen = (uint)reader.ReadUShort();
            actorIds = new double[_actorIdsLen];
            for (uint _i1 = 0;_i1 < _actorIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9.00719925474099E+15 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of actorIds.");
                }

                actorIds[_i1] = (double)_val1;
            }

        }


    }
}









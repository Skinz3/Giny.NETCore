using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntShowLegendaryUIMessage : NetworkMessage  
    { 
        public  const ushort Id = 9173;
        public override ushort MessageId => Id;

        public short[] availableLegendaryIds;

        public TreasureHuntShowLegendaryUIMessage()
        {
        }
        public TreasureHuntShowLegendaryUIMessage(short[] availableLegendaryIds)
        {
            this.availableLegendaryIds = availableLegendaryIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)availableLegendaryIds.Length);
            for (uint _i1 = 0;_i1 < availableLegendaryIds.Length;_i1++)
            {
                if (availableLegendaryIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + availableLegendaryIds[_i1] + ") on element 1 (starting at 1) of availableLegendaryIds.");
                }

                writer.WriteVarShort((short)availableLegendaryIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _availableLegendaryIdsLen = (uint)reader.ReadUShort();
            availableLegendaryIds = new short[_availableLegendaryIdsLen];
            for (uint _i1 = 0;_i1 < _availableLegendaryIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of availableLegendaryIds.");
                }

                availableLegendaryIds[_i1] = (short)_val1;
            }

        }


    }
}









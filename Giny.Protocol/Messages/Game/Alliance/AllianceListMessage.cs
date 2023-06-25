using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceListMessage : NetworkMessage  
    { 
        public  const ushort Id = 8557;
        public override ushort MessageId => Id;

        public AllianceFactSheetInformations[] alliances;

        public AllianceListMessage()
        {
        }
        public AllianceListMessage(AllianceFactSheetInformations[] alliances)
        {
            this.alliances = alliances;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)alliances.Length);
            for (uint _i1 = 0;_i1 < alliances.Length;_i1++)
            {
                (alliances[_i1] as AllianceFactSheetInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AllianceFactSheetInformations _item1 = null;
            uint _alliancesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _alliancesLen;_i1++)
            {
                _item1 = new AllianceFactSheetInformations();
                _item1.Deserialize(reader);
                alliances[_i1] = _item1;
            }

        }


    }
}









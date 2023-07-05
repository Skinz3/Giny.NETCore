using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MapInformationsRequestMessage : NetworkMessage
    {
        public const ushort Id = 9293;
        public override ushort MessageId => Id;

        public double mapId;

        public MapInformationsRequestMessage()
        {
        }
        public MapInformationsRequestMessage(double mapId)
        {
            this.mapId = mapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of MapInformationsRequestMessage.mapId.");
            }

        }

    }
}



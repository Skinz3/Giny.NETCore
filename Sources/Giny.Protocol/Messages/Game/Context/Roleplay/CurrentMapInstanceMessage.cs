using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CurrentMapInstanceMessage : CurrentMapMessage
    {
        public new const ushort Id = 4130;
        public override ushort MessageId => Id;

        public double instantiatedMapId;

        public CurrentMapInstanceMessage()
        {
        }
        public CurrentMapInstanceMessage(double instantiatedMapId, double mapId)
        {
            this.instantiatedMapId = instantiatedMapId;
            this.mapId = mapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (instantiatedMapId < 0 || instantiatedMapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + instantiatedMapId + ") on element instantiatedMapId.");
            }

            writer.WriteDouble((double)instantiatedMapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            instantiatedMapId = (double)reader.ReadDouble();
            if (instantiatedMapId < 0 || instantiatedMapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + instantiatedMapId + ") on element of CurrentMapInstanceMessage.instantiatedMapId.");
            }

        }

    }
}



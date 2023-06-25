using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightStartMessage : NetworkMessage  
    { 
        public  const ushort Id = 8247;
        public override ushort MessageId => Id;

        public Idol[] idols;

        public GameFightStartMessage()
        {
        }
        public GameFightStartMessage(Idol[] idols)
        {
            this.idols = idols;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)idols.Length);
            for (uint _i1 = 0;_i1 < idols.Length;_i1++)
            {
                (idols[_i1] as Idol).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            Idol _item1 = null;
            uint _idolsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _idolsLen;_i1++)
            {
                _item1 = new Idol();
                _item1.Deserialize(reader);
                idols[_i1] = _item1;
            }

        }


    }
}









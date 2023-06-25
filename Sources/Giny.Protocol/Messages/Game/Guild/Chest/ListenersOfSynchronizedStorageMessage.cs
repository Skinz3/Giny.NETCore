using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ListenersOfSynchronizedStorageMessage : NetworkMessage  
    { 
        public  const ushort Id = 8019;
        public override ushort MessageId => Id;

        public string[] players;

        public ListenersOfSynchronizedStorageMessage()
        {
        }
        public ListenersOfSynchronizedStorageMessage(string[] players)
        {
            this.players = players;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)players.Length);
            for (uint _i1 = 0;_i1 < players.Length;_i1++)
            {
                writer.WriteUTF((string)players[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            string _val1 = null;
            uint _playersLen = (uint)reader.ReadUShort();
            players = new string[_playersLen];
            for (uint _i1 = 0;_i1 < _playersLen;_i1++)
            {
                _val1 = (string)reader.ReadUTF();
                players[_i1] = (string)_val1;
            }

        }


    }
}









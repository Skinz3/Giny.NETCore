using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaPlayerBehavioursMessage : NetworkMessage  
    { 
        public  const ushort Id = 2244;
        public override ushort MessageId => Id;

        public string[] flags;
        public string[] sanctions;
        public int banDuration;

        public GameRolePlayArenaPlayerBehavioursMessage()
        {
        }
        public GameRolePlayArenaPlayerBehavioursMessage(string[] flags,string[] sanctions,int banDuration)
        {
            this.flags = flags;
            this.sanctions = sanctions;
            this.banDuration = banDuration;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)flags.Length);
            for (uint _i1 = 0;_i1 < flags.Length;_i1++)
            {
                writer.WriteUTF((string)flags[_i1]);
            }

            writer.WriteShort((short)sanctions.Length);
            for (uint _i2 = 0;_i2 < sanctions.Length;_i2++)
            {
                writer.WriteUTF((string)sanctions[_i2]);
            }

            if (banDuration < 0)
            {
                throw new System.Exception("Forbidden value (" + banDuration + ") on element banDuration.");
            }

            writer.WriteInt((int)banDuration);
        }
        public override void Deserialize(IDataReader reader)
        {
            string _val1 = null;
            string _val2 = null;
            uint _flagsLen = (uint)reader.ReadUShort();
            flags = new string[_flagsLen];
            for (uint _i1 = 0;_i1 < _flagsLen;_i1++)
            {
                _val1 = (string)reader.ReadUTF();
                flags[_i1] = (string)_val1;
            }

            uint _sanctionsLen = (uint)reader.ReadUShort();
            sanctions = new string[_sanctionsLen];
            for (uint _i2 = 0;_i2 < _sanctionsLen;_i2++)
            {
                _val2 = (string)reader.ReadUTF();
                sanctions[_i2] = (string)_val2;
            }

            banDuration = (int)reader.ReadInt();
            if (banDuration < 0)
            {
                throw new System.Exception("Forbidden value (" + banDuration + ") on element of GameRolePlayArenaPlayerBehavioursMessage.banDuration.");
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapRunningFightDetailsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7296;
        public override ushort MessageId => Id;

        public short fightId;
        public GameFightFighterLightInformations[] attackers;
        public GameFightFighterLightInformations[] defenders;

        public MapRunningFightDetailsMessage()
        {
        }
        public MapRunningFightDetailsMessage(short fightId,GameFightFighterLightInformations[] attackers,GameFightFighterLightInformations[] defenders)
        {
            this.fightId = fightId;
            this.attackers = attackers;
            this.defenders = defenders;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteShort((short)attackers.Length);
            for (uint _i2 = 0;_i2 < attackers.Length;_i2++)
            {
                writer.WriteShort((short)(attackers[_i2] as GameFightFighterLightInformations).TypeId);
                (attackers[_i2] as GameFightFighterLightInformations).Serialize(writer);
            }

            writer.WriteShort((short)defenders.Length);
            for (uint _i3 = 0;_i3 < defenders.Length;_i3++)
            {
                writer.WriteShort((short)(defenders[_i3] as GameFightFighterLightInformations).TypeId);
                (defenders[_i3] as GameFightFighterLightInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            GameFightFighterLightInformations _item2 = null;
            uint _id3 = 0;
            GameFightFighterLightInformations _item3 = null;
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of MapRunningFightDetailsMessage.fightId.");
            }

            uint _attackersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _attackersLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>((short)_id2);
                _item2.Deserialize(reader);
                attackers[_i2] = _item2;
            }

            uint _defendersLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _defendersLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>((short)_id3);
                _item3.Deserialize(reader);
                defenders[_i3] = _item3;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameRolePlayAttackMonsterRequestMessage : NetworkMessage
    {
        public const ushort Id = 850;
        public override ushort MessageId => Id;

        public double monsterGroupId;

        public GameRolePlayAttackMonsterRequestMessage()
        {
        }
        public GameRolePlayAttackMonsterRequestMessage(double monsterGroupId)
        {
            this.monsterGroupId = monsterGroupId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (monsterGroupId < -9007199254740992 || monsterGroupId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + monsterGroupId + ") on element monsterGroupId.");
            }

            writer.WriteDouble((double)monsterGroupId);
        }
        public override void Deserialize(IDataReader reader)
        {
            monsterGroupId = (double)reader.ReadDouble();
            if (monsterGroupId < -9007199254740992 || monsterGroupId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + monsterGroupId + ") on element of GameRolePlayAttackMonsterRequestMessage.monsterGroupId.");
            }

        }

    }
}



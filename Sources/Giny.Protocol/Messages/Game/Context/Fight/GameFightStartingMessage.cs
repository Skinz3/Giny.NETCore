using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameFightStartingMessage : NetworkMessage
    {
        public const ushort Id = 1962;
        public override ushort MessageId => Id;

        public byte fightType;
        public short fightId;
        public double attackerId;
        public double defenderId;
        public bool containsBoss;
        public int[] monsters;

        public GameFightStartingMessage()
        {
        }
        public GameFightStartingMessage(byte fightType, short fightId, double attackerId, double defenderId, bool containsBoss, int[] monsters)
        {
            this.fightType = fightType;
            this.fightId = fightId;
            this.attackerId = attackerId;
            this.defenderId = defenderId;
            this.containsBoss = containsBoss;
            this.monsters = monsters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)fightType);
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            if (attackerId < -9007199254740992 || attackerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + attackerId + ") on element attackerId.");
            }

            writer.WriteDouble((double)attackerId);
            if (defenderId < -9007199254740992 || defenderId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + defenderId + ") on element defenderId.");
            }

            writer.WriteDouble((double)defenderId);
            writer.WriteBoolean((bool)containsBoss);
            writer.WriteShort((short)monsters.Length);
            for (uint _i6 = 0; _i6 < monsters.Length; _i6++)
            {
                writer.WriteInt((int)monsters[_i6]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val6 = 0;
            fightType = (byte)reader.ReadByte();
            if (fightType < 0)
            {
                throw new System.Exception("Forbidden value (" + fightType + ") on element of GameFightStartingMessage.fightType.");
            }

            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameFightStartingMessage.fightId.");
            }

            attackerId = (double)reader.ReadDouble();
            if (attackerId < -9007199254740992 || attackerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + attackerId + ") on element of GameFightStartingMessage.attackerId.");
            }

            defenderId = (double)reader.ReadDouble();
            if (defenderId < -9007199254740992 || defenderId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + defenderId + ") on element of GameFightStartingMessage.defenderId.");
            }

            containsBoss = (bool)reader.ReadBoolean();
            uint _monstersLen = (uint)reader.ReadUShort();
            monsters = new int[_monstersLen];
            for (uint _i6 = 0; _i6 < _monstersLen; _i6++)
            {
                _val6 = (int)reader.ReadInt();
                monsters[_i6] = (int)_val6;
            }

        }

    }
}



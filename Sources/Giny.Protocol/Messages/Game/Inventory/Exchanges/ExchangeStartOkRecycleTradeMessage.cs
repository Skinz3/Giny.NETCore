using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeStartOkRecycleTradeMessage : NetworkMessage
    {
        public const ushort Id = 8048;
        public override ushort MessageId => Id;

        public short percentToPrism;
        public short percentToPlayer;
        public int[] adjacentSubareaPossessed;
        public int[] adjacentSubareaUnpossessed;

        public ExchangeStartOkRecycleTradeMessage()
        {
        }
        public ExchangeStartOkRecycleTradeMessage(short percentToPrism, short percentToPlayer, int[] adjacentSubareaPossessed, int[] adjacentSubareaUnpossessed)
        {
            this.percentToPrism = percentToPrism;
            this.percentToPlayer = percentToPlayer;
            this.adjacentSubareaPossessed = adjacentSubareaPossessed;
            this.adjacentSubareaUnpossessed = adjacentSubareaUnpossessed;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (percentToPrism < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPrism + ") on element percentToPrism.");
            }

            writer.WriteShort((short)percentToPrism);
            if (percentToPlayer < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPlayer + ") on element percentToPlayer.");
            }

            writer.WriteShort((short)percentToPlayer);
            writer.WriteShort((short)adjacentSubareaPossessed.Length);
            for (uint _i3 = 0; _i3 < adjacentSubareaPossessed.Length; _i3++)
            {
                if (adjacentSubareaPossessed[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + adjacentSubareaPossessed[_i3] + ") on element 3 (starting at 1) of adjacentSubareaPossessed.");
                }

                writer.WriteInt((int)adjacentSubareaPossessed[_i3]);
            }

            writer.WriteShort((short)adjacentSubareaUnpossessed.Length);
            for (uint _i4 = 0; _i4 < adjacentSubareaUnpossessed.Length; _i4++)
            {
                if (adjacentSubareaUnpossessed[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + adjacentSubareaUnpossessed[_i4] + ") on element 4 (starting at 1) of adjacentSubareaUnpossessed.");
                }

                writer.WriteInt((int)adjacentSubareaUnpossessed[_i4]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val3 = 0;
            uint _val4 = 0;
            percentToPrism = (short)reader.ReadShort();
            if (percentToPrism < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPrism + ") on element of ExchangeStartOkRecycleTradeMessage.percentToPrism.");
            }

            percentToPlayer = (short)reader.ReadShort();
            if (percentToPlayer < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPlayer + ") on element of ExchangeStartOkRecycleTradeMessage.percentToPlayer.");
            }

            uint _adjacentSubareaPossessedLen = (uint)reader.ReadUShort();
            adjacentSubareaPossessed = new int[_adjacentSubareaPossessedLen];
            for (uint _i3 = 0; _i3 < _adjacentSubareaPossessedLen; _i3++)
            {
                _val3 = (uint)reader.ReadInt();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of adjacentSubareaPossessed.");
                }

                adjacentSubareaPossessed[_i3] = (int)_val3;
            }

            uint _adjacentSubareaUnpossessedLen = (uint)reader.ReadUShort();
            adjacentSubareaUnpossessed = new int[_adjacentSubareaUnpossessedLen];
            for (uint _i4 = 0; _i4 < _adjacentSubareaUnpossessedLen; _i4++)
            {
                _val4 = (uint)reader.ReadInt();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of adjacentSubareaUnpossessed.");
                }

                adjacentSubareaUnpossessed[_i4] = (int)_val4;
            }

        }

    }
}



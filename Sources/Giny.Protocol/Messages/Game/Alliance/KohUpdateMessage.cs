using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class KohUpdateMessage : NetworkMessage
    {
        public const ushort Id = 8374;
        public override ushort MessageId => Id;

        public KohAllianceInfo[] kohAllianceInfo;
        public int startingAvaTimestamp;
        public double nextTickTime;

        public KohUpdateMessage()
        {
        }
        public KohUpdateMessage(KohAllianceInfo[] kohAllianceInfo, int startingAvaTimestamp, double nextTickTime)
        {
            this.kohAllianceInfo = kohAllianceInfo;
            this.startingAvaTimestamp = startingAvaTimestamp;
            this.nextTickTime = nextTickTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)kohAllianceInfo.Length);
            for (uint _i1 = 0; _i1 < kohAllianceInfo.Length; _i1++)
            {
                (kohAllianceInfo[_i1] as KohAllianceInfo).Serialize(writer);
            }

            if (startingAvaTimestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + startingAvaTimestamp + ") on element startingAvaTimestamp.");
            }

            writer.WriteInt((int)startingAvaTimestamp);
            if (nextTickTime < 0 || nextTickTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + nextTickTime + ") on element nextTickTime.");
            }

            writer.WriteDouble((double)nextTickTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            KohAllianceInfo _item1 = null;
            uint _kohAllianceInfoLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _kohAllianceInfoLen; _i1++)
            {
                _item1 = new KohAllianceInfo();
                _item1.Deserialize(reader);
                kohAllianceInfo[_i1] = _item1;
            }

            startingAvaTimestamp = (int)reader.ReadInt();
            if (startingAvaTimestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + startingAvaTimestamp + ") on element of KohUpdateMessage.startingAvaTimestamp.");
            }

            nextTickTime = (double)reader.ReadDouble();
            if (nextTickTime < 0 || nextTickTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + nextTickTime + ") on element of KohUpdateMessage.nextTickTime.");
            }

        }

    }
}



using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightJoinMessage : NetworkMessage  
    { 
        public  const ushort Id = 3603;
        public override ushort MessageId => Id;

        public bool isTeamPhase;
        public bool canBeCancelled;
        public bool canSayReady;
        public bool isFightStarted;
        public short timeMaxBeforeFightStart;
        public byte fightType;

        public GameFightJoinMessage()
        {
        }
        public GameFightJoinMessage(bool isTeamPhase,bool canBeCancelled,bool canSayReady,bool isFightStarted,short timeMaxBeforeFightStart,byte fightType)
        {
            this.isTeamPhase = isTeamPhase;
            this.canBeCancelled = canBeCancelled;
            this.canSayReady = canSayReady;
            this.isFightStarted = isFightStarted;
            this.timeMaxBeforeFightStart = timeMaxBeforeFightStart;
            this.fightType = fightType;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,isTeamPhase);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,canBeCancelled);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,canSayReady);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,isFightStarted);
            writer.WriteByte((byte)_box0);
            if (timeMaxBeforeFightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + timeMaxBeforeFightStart + ") on element timeMaxBeforeFightStart.");
            }

            writer.WriteShort((short)timeMaxBeforeFightStart);
            writer.WriteByte((byte)fightType);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            isTeamPhase = BooleanByteWrapper.GetFlag(_box0,0);
            canBeCancelled = BooleanByteWrapper.GetFlag(_box0,1);
            canSayReady = BooleanByteWrapper.GetFlag(_box0,2);
            isFightStarted = BooleanByteWrapper.GetFlag(_box0,3);
            timeMaxBeforeFightStart = (short)reader.ReadShort();
            if (timeMaxBeforeFightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + timeMaxBeforeFightStart + ") on element of GameFightJoinMessage.timeMaxBeforeFightStart.");
            }

            fightType = (byte)reader.ReadByte();
            if (fightType < 0)
            {
                throw new System.Exception("Forbidden value (" + fightType + ") on element of GameFightJoinMessage.fightType.");
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaRegistrationWarningMessage : NetworkMessage  
    { 
        public  const ushort Id = 9396;
        public override ushort MessageId => Id;

        public int battleMode;

        public GameRolePlayArenaRegistrationWarningMessage()
        {
        }
        public GameRolePlayArenaRegistrationWarningMessage(int battleMode)
        {
            this.battleMode = battleMode;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)battleMode);
        }
        public override void Deserialize(IDataReader reader)
        {
            battleMode = (int)reader.ReadInt();
            if (battleMode < 0)
            {
                throw new System.Exception("Forbidden value (" + battleMode + ") on element of GameRolePlayArenaRegistrationWarningMessage.battleMode.");
            }

        }


    }
}









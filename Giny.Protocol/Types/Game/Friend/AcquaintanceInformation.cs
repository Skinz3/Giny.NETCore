using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AcquaintanceInformation : AbstractContactInformations  
    { 
        public new const ushort Id = 7533;
        public override ushort TypeId => Id;

        public byte playerState;

        public AcquaintanceInformation()
        {
        }
        public AcquaintanceInformation(byte playerState,int accountId,AccountTagInformation accountTag)
        {
            this.playerState = playerState;
            this.accountId = accountId;
            this.accountTag = accountTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)playerState);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerState = (byte)reader.ReadByte();
            if (playerState < 0)
            {
                throw new System.Exception("Forbidden value (" + playerState + ") on element of AcquaintanceInformation.playerState.");
            }

        }


    }
}









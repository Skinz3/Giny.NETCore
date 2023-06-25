using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorMovementMessage : NetworkMessage  
    { 
        public  const ushort Id = 4091;
        public override ushort MessageId => Id;

        public byte movementType;
        public TaxCollectorBasicInformations basicInfos;
        public long playerId;
        public string playerName;

        public TaxCollectorMovementMessage()
        {
        }
        public TaxCollectorMovementMessage(byte movementType,TaxCollectorBasicInformations basicInfos,long playerId,string playerName)
        {
            this.movementType = movementType;
            this.basicInfos = basicInfos;
            this.playerId = playerId;
            this.playerName = playerName;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)movementType);
            basicInfos.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
        }
        public override void Deserialize(IDataReader reader)
        {
            movementType = (byte)reader.ReadByte();
            if (movementType < 0)
            {
                throw new System.Exception("Forbidden value (" + movementType + ") on element of TaxCollectorMovementMessage.movementType.");
            }

            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of TaxCollectorMovementMessage.playerId.");
            }

            playerName = (string)reader.ReadUTF();
        }


    }
}









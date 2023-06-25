using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorMovement  
    { 
        public const ushort Id = 3801;
        public virtual ushort TypeId => Id;

        public byte movementType;
        public TaxCollectorBasicInformations basicInfos;
        public long playerId;
        public string playerName;

        public TaxCollectorMovement()
        {
        }
        public TaxCollectorMovement(byte movementType,TaxCollectorBasicInformations basicInfos,long playerId,string playerName)
        {
            this.movementType = movementType;
            this.basicInfos = basicInfos;
            this.playerId = playerId;
            this.playerName = playerName;
        }
        public virtual void Serialize(IDataWriter writer)
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
        public virtual void Deserialize(IDataReader reader)
        {
            movementType = (byte)reader.ReadByte();
            if (movementType < 0)
            {
                throw new System.Exception("Forbidden value (" + movementType + ") on element of TaxCollectorMovement.movementType.");
            }

            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of TaxCollectorMovement.playerId.");
            }

            playerName = (string)reader.ReadUTF();
        }


    }
}









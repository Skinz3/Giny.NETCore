using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayFightRequestCanceledMessage : NetworkMessage  
    { 
        public  const ushort Id = 9101;
        public override ushort MessageId => Id;

        public short fightId;
        public double sourceId;
        public double targetId;

        public GameRolePlayFightRequestCanceledMessage()
        {
        }
        public GameRolePlayFightRequestCanceledMessage(short fightId,double sourceId,double targetId)
        {
            this.fightId = fightId;
            this.sourceId = sourceId;
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element sourceId.");
            }

            writer.WriteDouble((double)sourceId);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayFightRequestCanceledMessage.fightId.");
            }

            sourceId = (double)reader.ReadDouble();
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element of GameRolePlayFightRequestCanceledMessage.sourceId.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameRolePlayFightRequestCanceledMessage.targetId.");
            }

        }


    }
}









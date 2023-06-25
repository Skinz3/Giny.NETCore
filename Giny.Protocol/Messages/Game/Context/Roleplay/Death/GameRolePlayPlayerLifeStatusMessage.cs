using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayPlayerLifeStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 8586;
        public override ushort MessageId => Id;

        public byte state;
        public double phenixMapId;

        public GameRolePlayPlayerLifeStatusMessage()
        {
        }
        public GameRolePlayPlayerLifeStatusMessage(byte state,double phenixMapId)
        {
            this.state = state;
            this.phenixMapId = phenixMapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)state);
            if (phenixMapId < 0 || phenixMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + phenixMapId + ") on element phenixMapId.");
            }

            writer.WriteDouble((double)phenixMapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of GameRolePlayPlayerLifeStatusMessage.state.");
            }

            phenixMapId = (double)reader.ReadDouble();
            if (phenixMapId < 0 || phenixMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + phenixMapId + ") on element of GameRolePlayPlayerLifeStatusMessage.phenixMapId.");
            }

        }


    }
}









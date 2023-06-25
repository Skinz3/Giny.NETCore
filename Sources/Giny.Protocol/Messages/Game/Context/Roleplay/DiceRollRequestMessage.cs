using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DiceRollRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6852;
        public override ushort MessageId => Id;

        public int dice;
        public int faces;
        public byte channel;

        public DiceRollRequestMessage()
        {
        }
        public DiceRollRequestMessage(int dice,int faces,byte channel)
        {
            this.dice = dice;
            this.faces = faces;
            this.channel = channel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dice < 0)
            {
                throw new System.Exception("Forbidden value (" + dice + ") on element dice.");
            }

            writer.WriteVarInt((int)dice);
            if (faces < 0)
            {
                throw new System.Exception("Forbidden value (" + faces + ") on element faces.");
            }

            writer.WriteVarInt((int)faces);
            writer.WriteByte((byte)channel);
        }
        public override void Deserialize(IDataReader reader)
        {
            dice = (int)reader.ReadVarUhInt();
            if (dice < 0)
            {
                throw new System.Exception("Forbidden value (" + dice + ") on element of DiceRollRequestMessage.dice.");
            }

            faces = (int)reader.ReadVarUhInt();
            if (faces < 0)
            {
                throw new System.Exception("Forbidden value (" + faces + ") on element of DiceRollRequestMessage.faces.");
            }

            channel = (byte)reader.ReadByte();
            if (channel < 0)
            {
                throw new System.Exception("Forbidden value (" + channel + ") on element of DiceRollRequestMessage.channel.");
            }

        }


    }
}









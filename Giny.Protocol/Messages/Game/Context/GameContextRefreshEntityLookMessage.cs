using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextRefreshEntityLookMessage : NetworkMessage  
    { 
        public  const ushort Id = 7169;
        public override ushort MessageId => Id;

        public double id;
        public EntityLook look;

        public GameContextRefreshEntityLookMessage()
        {
        }
        public GameContextRefreshEntityLookMessage(double id,EntityLook look)
        {
            this.id = id;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            look.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameContextRefreshEntityLookMessage.id.");
            }

            look = new EntityLook();
            look.Deserialize(reader);
        }


    }
}









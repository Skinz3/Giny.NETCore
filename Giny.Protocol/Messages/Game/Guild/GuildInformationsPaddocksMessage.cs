using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInformationsPaddocksMessage : NetworkMessage  
    { 
        public  const ushort Id = 1386;
        public override ushort MessageId => Id;

        public byte nbPaddockMax;
        public PaddockContentInformations[] paddocksInformations;

        public GuildInformationsPaddocksMessage()
        {
        }
        public GuildInformationsPaddocksMessage(byte nbPaddockMax,PaddockContentInformations[] paddocksInformations)
        {
            this.nbPaddockMax = nbPaddockMax;
            this.paddocksInformations = paddocksInformations;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (nbPaddockMax < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPaddockMax + ") on element nbPaddockMax.");
            }

            writer.WriteByte((byte)nbPaddockMax);
            writer.WriteShort((short)paddocksInformations.Length);
            for (uint _i2 = 0;_i2 < paddocksInformations.Length;_i2++)
            {
                (paddocksInformations[_i2] as PaddockContentInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            PaddockContentInformations _item2 = null;
            nbPaddockMax = (byte)reader.ReadByte();
            if (nbPaddockMax < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPaddockMax + ") on element of GuildInformationsPaddocksMessage.nbPaddockMax.");
            }

            uint _paddocksInformationsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _paddocksInformationsLen;_i2++)
            {
                _item2 = new PaddockContentInformations();
                _item2.Deserialize(reader);
                paddocksInformations[_i2] = _item2;
            }

        }


    }
}









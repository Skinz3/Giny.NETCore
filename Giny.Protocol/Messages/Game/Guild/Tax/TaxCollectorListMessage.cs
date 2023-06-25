using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorListMessage : AbstractTaxCollectorListMessage  
    { 
        public new const ushort Id = 2693;
        public override ushort MessageId => Id;

        public byte nbcollectorMax;
        public TaxCollectorFightersInformation[] fightersInformations;
        public byte infoType;

        public TaxCollectorListMessage()
        {
        }
        public TaxCollectorListMessage(byte nbcollectorMax,TaxCollectorFightersInformation[] fightersInformations,byte infoType,TaxCollectorInformations[] informations)
        {
            this.nbcollectorMax = nbcollectorMax;
            this.fightersInformations = fightersInformations;
            this.infoType = infoType;
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (nbcollectorMax < 0)
            {
                throw new System.Exception("Forbidden value (" + nbcollectorMax + ") on element nbcollectorMax.");
            }

            writer.WriteByte((byte)nbcollectorMax);
            writer.WriteShort((short)fightersInformations.Length);
            for (uint _i2 = 0;_i2 < fightersInformations.Length;_i2++)
            {
                (fightersInformations[_i2] as TaxCollectorFightersInformation).Serialize(writer);
            }

            writer.WriteByte((byte)infoType);
        }
        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorFightersInformation _item2 = null;
            base.Deserialize(reader);
            nbcollectorMax = (byte)reader.ReadByte();
            if (nbcollectorMax < 0)
            {
                throw new System.Exception("Forbidden value (" + nbcollectorMax + ") on element of TaxCollectorListMessage.nbcollectorMax.");
            }

            uint _fightersInformationsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _fightersInformationsLen;_i2++)
            {
                _item2 = new TaxCollectorFightersInformation();
                _item2.Deserialize(reader);
                fightersInformations[_i2] = _item2;
            }

            infoType = (byte)reader.ReadByte();
            if (infoType < 0)
            {
                throw new System.Exception("Forbidden value (" + infoType + ") on element of TaxCollectorListMessage.infoType.");
            }

        }


    }
}









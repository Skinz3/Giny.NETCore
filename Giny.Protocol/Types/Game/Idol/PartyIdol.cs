using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyIdol : Idol  
    { 
        public new const ushort Id = 4985;
        public override ushort TypeId => Id;

        public long[] ownersIds;

        public PartyIdol()
        {
        }
        public PartyIdol(long[] ownersIds,short id,short xpBonusPercent,short dropBonusPercent)
        {
            this.ownersIds = ownersIds;
            this.id = id;
            this.xpBonusPercent = xpBonusPercent;
            this.dropBonusPercent = dropBonusPercent;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ownersIds.Length);
            for (uint _i1 = 0;_i1 < ownersIds.Length;_i1++)
            {
                if (ownersIds[_i1] < 0 || ownersIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + ownersIds[_i1] + ") on element 1 (starting at 1) of ownersIds.");
                }

                writer.WriteVarLong((long)ownersIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            base.Deserialize(reader);
            uint _ownersIdsLen = (uint)reader.ReadUShort();
            ownersIds = new long[_ownersIdsLen];
            for (uint _i1 = 0;_i1 < _ownersIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadVarUhLong();
                if (_val1 < 0 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of ownersIds.");
                }

                ownersIds[_i1] = (long)_val1;
            }

        }


    }
}









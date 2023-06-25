using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ActivitySuggestionsMessage : NetworkMessage  
    { 
        public  const ushort Id = 8161;
        public override ushort MessageId => Id;

        public short[] lockedActivitiesIds;
        public short[] unlockedActivitiesIds;

        public ActivitySuggestionsMessage()
        {
        }
        public ActivitySuggestionsMessage(short[] lockedActivitiesIds,short[] unlockedActivitiesIds)
        {
            this.lockedActivitiesIds = lockedActivitiesIds;
            this.unlockedActivitiesIds = unlockedActivitiesIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)lockedActivitiesIds.Length);
            for (uint _i1 = 0;_i1 < lockedActivitiesIds.Length;_i1++)
            {
                if (lockedActivitiesIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + lockedActivitiesIds[_i1] + ") on element 1 (starting at 1) of lockedActivitiesIds.");
                }

                writer.WriteVarShort((short)lockedActivitiesIds[_i1]);
            }

            writer.WriteShort((short)unlockedActivitiesIds.Length);
            for (uint _i2 = 0;_i2 < unlockedActivitiesIds.Length;_i2++)
            {
                if (unlockedActivitiesIds[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + unlockedActivitiesIds[_i2] + ") on element 2 (starting at 1) of unlockedActivitiesIds.");
                }

                writer.WriteVarShort((short)unlockedActivitiesIds[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _lockedActivitiesIdsLen = (uint)reader.ReadUShort();
            lockedActivitiesIds = new short[_lockedActivitiesIdsLen];
            for (uint _i1 = 0;_i1 < _lockedActivitiesIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of lockedActivitiesIds.");
                }

                lockedActivitiesIds[_i1] = (short)_val1;
            }

            uint _unlockedActivitiesIdsLen = (uint)reader.ReadUShort();
            unlockedActivitiesIds = new short[_unlockedActivitiesIdsLen];
            for (uint _i2 = 0;_i2 < _unlockedActivitiesIdsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of unlockedActivitiesIds.");
                }

                unlockedActivitiesIds[_i2] = (short)_val2;
            }

        }


    }
}









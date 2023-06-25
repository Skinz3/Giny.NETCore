using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayNpcQuestFlag  
    { 
        public const ushort Id = 3057;
        public virtual ushort TypeId => Id;

        public short[] questsToValidId;
        public short[] questsToStartId;

        public GameRolePlayNpcQuestFlag()
        {
        }
        public GameRolePlayNpcQuestFlag(short[] questsToValidId,short[] questsToStartId)
        {
            this.questsToValidId = questsToValidId;
            this.questsToStartId = questsToStartId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)questsToValidId.Length);
            for (uint _i1 = 0;_i1 < questsToValidId.Length;_i1++)
            {
                if (questsToValidId[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + questsToValidId[_i1] + ") on element 1 (starting at 1) of questsToValidId.");
                }

                writer.WriteVarShort((short)questsToValidId[_i1]);
            }

            writer.WriteShort((short)questsToStartId.Length);
            for (uint _i2 = 0;_i2 < questsToStartId.Length;_i2++)
            {
                if (questsToStartId[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + questsToStartId[_i2] + ") on element 2 (starting at 1) of questsToStartId.");
                }

                writer.WriteVarShort((short)questsToStartId[_i2]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _questsToValidIdLen = (uint)reader.ReadUShort();
            questsToValidId = new short[_questsToValidIdLen];
            for (uint _i1 = 0;_i1 < _questsToValidIdLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of questsToValidId.");
                }

                questsToValidId[_i1] = (short)_val1;
            }

            uint _questsToStartIdLen = (uint)reader.ReadUShort();
            questsToStartId = new short[_questsToStartIdLen];
            for (uint _i2 = 0;_i2 < _questsToStartIdLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of questsToStartId.");
                }

                questsToStartId[_i2] = (short)_val2;
            }

        }


    }
}









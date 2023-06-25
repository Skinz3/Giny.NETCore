using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class Achievement  
    { 
        public const ushort Id = 3209;
        public virtual ushort TypeId => Id;

        public short id;
        public AchievementObjective[] finishedObjective;
        public AchievementStartedObjective[] startedObjectives;

        public Achievement()
        {
        }
        public Achievement(short id,AchievementObjective[] finishedObjective,AchievementStartedObjective[] startedObjectives)
        {
            this.id = id;
            this.finishedObjective = finishedObjective;
            this.startedObjectives = startedObjectives;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            writer.WriteShort((short)finishedObjective.Length);
            for (uint _i2 = 0;_i2 < finishedObjective.Length;_i2++)
            {
                (finishedObjective[_i2] as AchievementObjective).Serialize(writer);
            }

            writer.WriteShort((short)startedObjectives.Length);
            for (uint _i3 = 0;_i3 < startedObjectives.Length;_i3++)
            {
                (startedObjectives[_i3] as AchievementStartedObjective).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            AchievementObjective _item2 = null;
            AchievementStartedObjective _item3 = null;
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of Achievement.id.");
            }

            uint _finishedObjectiveLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _finishedObjectiveLen;_i2++)
            {
                _item2 = new AchievementObjective();
                _item2.Deserialize(reader);
                finishedObjective[_i2] = _item2;
            }

            uint _startedObjectivesLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _startedObjectivesLen;_i3++)
            {
                _item3 = new AchievementStartedObjective();
                _item3.Deserialize(reader);
                startedObjectives[_i3] = _item3;
            }

        }


    }
}









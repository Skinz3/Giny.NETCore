using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class QuestObjectiveInformations  
    { 
        public const ushort Id = 8836;
        public virtual ushort TypeId => Id;

        public short objectiveId;
        public bool objectiveStatus;
        public string[] dialogParams;

        public QuestObjectiveInformations()
        {
        }
        public QuestObjectiveInformations(short objectiveId,bool objectiveStatus,string[] dialogParams)
        {
            this.objectiveId = objectiveId;
            this.objectiveStatus = objectiveStatus;
            this.dialogParams = dialogParams;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (objectiveId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectiveId + ") on element objectiveId.");
            }

            writer.WriteVarShort((short)objectiveId);
            writer.WriteBoolean((bool)objectiveStatus);
            writer.WriteShort((short)dialogParams.Length);
            for (uint _i3 = 0;_i3 < dialogParams.Length;_i3++)
            {
                writer.WriteUTF((string)dialogParams[_i3]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            string _val3 = null;
            objectiveId = (short)reader.ReadVarUhShort();
            if (objectiveId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectiveId + ") on element of QuestObjectiveInformations.objectiveId.");
            }

            objectiveStatus = (bool)reader.ReadBoolean();
            uint _dialogParamsLen = (uint)reader.ReadUShort();
            dialogParams = new string[_dialogParamsLen];
            for (uint _i3 = 0;_i3 < _dialogParamsLen;_i3++)
            {
                _val3 = (string)reader.ReadUTF();
                dialogParams[_i3] = (string)_val3;
            }

        }


    }
}









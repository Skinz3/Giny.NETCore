using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class InteractiveElement  
    { 
        public const ushort Id = 2235;
        public virtual ushort TypeId => Id;

        public int elementId;
        public int elementTypeId;
        public InteractiveElementSkill[] enabledSkills;
        public InteractiveElementSkill[] disabledSkills;
        public bool onCurrentMap;

        public InteractiveElement()
        {
        }
        public InteractiveElement(int elementId,int elementTypeId,InteractiveElementSkill[] enabledSkills,InteractiveElementSkill[] disabledSkills,bool onCurrentMap)
        {
            this.elementId = elementId;
            this.elementTypeId = elementTypeId;
            this.enabledSkills = enabledSkills;
            this.disabledSkills = disabledSkills;
            this.onCurrentMap = onCurrentMap;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element elementId.");
            }

            writer.WriteInt((int)elementId);
            writer.WriteInt((int)elementTypeId);
            writer.WriteShort((short)enabledSkills.Length);
            for (uint _i3 = 0;_i3 < enabledSkills.Length;_i3++)
            {
                writer.WriteShort((short)(enabledSkills[_i3] as InteractiveElementSkill).TypeId);
                (enabledSkills[_i3] as InteractiveElementSkill).Serialize(writer);
            }

            writer.WriteShort((short)disabledSkills.Length);
            for (uint _i4 = 0;_i4 < disabledSkills.Length;_i4++)
            {
                writer.WriteShort((short)(disabledSkills[_i4] as InteractiveElementSkill).TypeId);
                (disabledSkills[_i4] as InteractiveElementSkill).Serialize(writer);
            }

            writer.WriteBoolean((bool)onCurrentMap);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            InteractiveElementSkill _item3 = null;
            uint _id4 = 0;
            InteractiveElementSkill _item4 = null;
            elementId = (int)reader.ReadInt();
            if (elementId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementId + ") on element of InteractiveElement.elementId.");
            }

            elementTypeId = (int)reader.ReadInt();
            uint _enabledSkillsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _enabledSkillsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<InteractiveElementSkill>((short)_id3);
                _item3.Deserialize(reader);
                enabledSkills[_i3] = _item3;
            }

            uint _disabledSkillsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _disabledSkillsLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<InteractiveElementSkill>((short)_id4);
                _item4.Deserialize(reader);
                disabledSkills[_i4] = _item4;
            }

            onCurrentMap = (bool)reader.ReadBoolean();
        }


    }
}









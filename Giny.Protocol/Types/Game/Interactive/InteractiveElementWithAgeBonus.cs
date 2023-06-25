using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class InteractiveElementWithAgeBonus : InteractiveElement  
    { 
        public new const ushort Id = 2318;
        public override ushort TypeId => Id;

        public short ageBonus;

        public InteractiveElementWithAgeBonus()
        {
        }
        public InteractiveElementWithAgeBonus(short ageBonus,int elementId,int elementTypeId,InteractiveElementSkill[] enabledSkills,InteractiveElementSkill[] disabledSkills,bool onCurrentMap)
        {
            this.ageBonus = ageBonus;
            this.elementId = elementId;
            this.elementTypeId = elementTypeId;
            this.enabledSkills = enabledSkills;
            this.disabledSkills = disabledSkills;
            this.onCurrentMap = onCurrentMap;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (ageBonus < -1 || ageBonus > 1000)
            {
                throw new System.Exception("Forbidden value (" + ageBonus + ") on element ageBonus.");
            }

            writer.WriteShort((short)ageBonus);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ageBonus = (short)reader.ReadShort();
            if (ageBonus < -1 || ageBonus > 1000)
            {
                throw new System.Exception("Forbidden value (" + ageBonus + ") on element of InteractiveElementWithAgeBonus.ageBonus.");
            }

        }


    }
}









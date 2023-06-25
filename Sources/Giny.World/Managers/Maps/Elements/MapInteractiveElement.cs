using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Records.Maps;

namespace Giny.World.Managers.Maps.Elements
{
    public class MapInteractiveElement : MapElement
    {
        public MapInteractiveElement(MapInstance mapInstance, InteractiveElementRecord record) : base(record, mapInstance)
        {

        }

        public InteractiveElement GetInteractiveElement(Character character)
        {
            InteractiveElementSkill[] skills = new InteractiveElementSkill[]
            {
                new InteractiveElementSkill((int)Record.Skill.SkillId, (int)Record.Skill.Id)
            };

            if (character.SkillsAllowed.Contains(Record.Skill.Record))
            {
                return new InteractiveElement((int)Record.Identifier, (int)Record.Skill.Type, skills, new InteractiveElementSkill[0], true);
            }
            else
            {
                return new InteractiveElement((int)Record.Identifier, (int)Record.Skill.Type, new InteractiveElementSkill[0], skills, true);
            }
        }
    }
}

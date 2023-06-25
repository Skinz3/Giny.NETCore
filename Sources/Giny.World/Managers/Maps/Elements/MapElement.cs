using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Records.Maps;
using System.Linq;

namespace Giny.World.Managers.Maps.Elements
{
    public abstract class MapElement : IGenericActionParameter
    {
        public InteractiveElementRecord Record
        {
            get;
            private set;
        }

        public GenericActionEnum ActionIdentifier => Record.Skill.ActionIdentifier;

        public string Param1 => Record.Skill.Param1;

        public string Param2 => Record.Skill.Param2;

        public string Param3 => Record.Skill.Param3;

        public string Criteria => Record.Skill.Criteria;

        protected MapInstance MapInstance
        {
            get;
            private set;
        }

        public MapElement(InteractiveElementRecord record, MapInstance mapInstance)
        {
            this.Record = record;
            this.MapInstance = mapInstance;
        }

        public virtual bool CanUse(Character character)
        {
            return true;
            /* short[] zone = new Square(0, 1).GetCells(this.Record.CellId, character.Map);
            return zone.Length == 0 || zone.Contains(character.Record.CellId); */
        }
    }
}

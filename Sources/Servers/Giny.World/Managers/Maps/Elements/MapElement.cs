using Giny.World.Managers.Criterias;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Records.Maps;
using System.Linq;

namespace Giny.World.Managers.Maps.Elements
{
    public abstract class MapElement : IGenericAction
    {
        public InteractiveElementRecord Record
        {
            get;
            private set;
        }

        protected MapInstance MapInstance
        {
            get;
            private set;
        }
        public GenericActionEnum ActionIdentifier
        {
            get => Record.Skill.ActionIdentifier;
            set => Record.Skill.ActionIdentifier = value;
        }
        public string Param1
        {
            get => Record.Skill.Param1;
            set => Record.Skill.Param1 = value;
        }
        public string Param2
        {
            get => Record.Skill.Param2;
            set => Record.Skill.Param2 = value;
        }
        public string Param3
        {
            get => Record.Skill.Param3;
            set => Record.Skill.Param3 = value;
        }
        public string Criteria
        {
            get => Record.Skill.Criteria;
            set => Record.Skill.Criteria = value;
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

using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Maps.Npcs;
using Giny.World.Records.Maps;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Npcs
{
    public class Npc : Entity
    {
        public override string Name
        {
            get
            {
                return Template.Name;
            }
        }
        public NpcSpawnRecord SpawnRecord
        {
            get;
            set;
        }
        public NpcRecord Template
        {
            get
            {
                return SpawnRecord.Template;
            }
        }

        private long m_Id;

        public override long Id
        {
            get
            {
                return m_Id;
            }
        }

        public override short CellId
        {
            get
            {
                return SpawnRecord.CellId;
            }
            set
            {
                SpawnRecord.CellId = value;
                SpawnRecord.UpdateNow();
            }
        }

        public override DirectionsEnum Direction
        {
            get
            {
                return SpawnRecord.Direction;
            }
            set
            {
                SpawnRecord.Direction = value;
                SpawnRecord.UpdateNow();
            }
        }

        private ServerEntityLook? m_customLook;

        public override ServerEntityLook Look
        {
            get
            {
                return m_customLook == null ? Template.Look : m_customLook;
            }
            set
            {
                m_customLook = value;
            }
        }

        public Npc(NpcSpawnRecord spawnRecord, MapRecord map) : base(map)
        {
            this.SpawnRecord = spawnRecord;
            this.m_Id = this.Map.Instance.PopNextNPEntityId();
        }

        public void InteractWith(Character character, NpcActionsEnum actionType)
        {
            if (character.Busy)
                return;

            var actions = SpawnRecord.Actions.Where(x => x.Action == actionType).Where(x => CriteriaExpression.Eval(x.Criteria, character.Client));

            if (actions.Count() > 1)
            {
                character.ReplyWarning("Multiple actions (" + actionType + ") for npc " + SpawnRecord.Id);
                return;
            }

            NpcActionRecord action = actions.FirstOrDefault();

            if (action != null)
            {
                NpcsManager.Instance.HandleNpcAction(character, this, action);
            }
            else
            {
                character.ReplyWarning("No (" + actionType + ") action linked to this npc...(" + SpawnRecord.Id + ")");
            }
        }
        public override GameRolePlayActorInformations GetActorInformations()
        {
            return new GameRolePlayNpcInformations()
            {
                contextualId = Id,
                disposition = new EntityDispositionInformations(CellId, (byte)Direction),
                look = Look.ToEntityLook(),
                npcId = (short)Template.Id,
                sex = Template.Sex,
                specialArtworkId = 0
            };

        }
        public void SetId(long id)
        {
            m_Id = id;
        }

        public override string ToString()
        {
            return "Npc (" + Name + ") (Id:" + SpawnRecord.Id + " Record Id:" + Template.Id + ") (CellId:" + CellId + ")";
        }
    }
}

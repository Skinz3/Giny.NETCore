using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps.Npcs
{
    public class NpcsManager : Singleton<NpcsManager>
    {
        private Dictionary<NpcActionsEnum, MethodInfo> m_actionsHandlers = new Dictionary<NpcActionsEnum, MethodInfo>();

        [StartupInvoke("Npcs Manager", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var method in typeof(NpcActions).GetMethods())
            {
                var attribute = method.GetCustomAttribute<NpcActionHandlerAttribute>();

                if (attribute != null)
                {
                    m_actionsHandlers.Add(attribute.Action, method);
                }
            }
            SpawnNpcs();
        }

        public void SpawnNpcs()
        {
            foreach (var map in MapRecord.GetMaps())
            {
                foreach (var npcSpawnRecord in NpcSpawnRecord.GetNpcsOnMap((int)map.Id))
                {
                    map.Instance.AddEntity(new Npc(npcSpawnRecord, map));
                }
            }
        }
        public void HandleNpcAction(Character character, Npc npc, NpcActionRecord actionRecord)
        {
            if (m_actionsHandlers.ContainsKey(actionRecord.Action))
            {
                m_actionsHandlers[actionRecord.Action].Invoke(null, new object[] { character, npc, actionRecord });
            }
            else
            {
                character.ReplyWarning("Unhandled NpcAction " + actionRecord);
            }
        }
        public void MoveNpc(long spawnRecordId, int newMapId, short newCellId, DirectionsEnum newDirection)
        {
            var newMap = MapRecord.GetMap(newMapId);
            var previousMap = MapRecord.GetMap(NpcSpawnRecord.GetMapId((int)spawnRecordId));
            Npc npc = previousMap.Instance.GetEntity<Npc>(x => x.SpawnRecord.Id == spawnRecordId);

            previousMap.Instance.RemoveEntity(npc.Id);

            npc.SpawnRecord.CellId = newCellId;
            npc.SpawnRecord.Direction = newDirection;
            npc.SpawnRecord.MapId = newMapId;
            npc.SetId(newMap.Instance.PopNextNPEntityId());
            npc.SpawnRecord.UpdateInstantElement();

            previousMap.Instance.RemoveEntity(npc.Id);
            newMap.Instance.AddEntity(npc);

            newMap.Instance.Reload();
            previousMap.Instance.Reload();
        }
        public void RemoveNpc(long spawnRecordId)
        {
            NpcSpawnRecord spawnRecord = NpcSpawnRecord.GetNpcSpawnRecord(spawnRecordId);
            IEnumerable<NpcActionRecord> npcActions = NpcActionRecord.GetNpcActions(spawnRecord.Id);

            NpcReplyRecord.GetNpcReplies().Where(x => x.NpcSpawnId == spawnRecordId).RemoveInstantElements();

            spawnRecord.RemoveInstantElement();
            npcActions.RemoveInstantElements();

            MapRecord map = MapRecord.GetMap(spawnRecord.MapId);
            Npc npc = map.Instance.GetEntity<Npc>(x => x.SpawnRecord.Id == spawnRecordId);
            map.Instance.RemoveEntity(npc.Id);
            map.Instance.Reload();
        }

        public void ReloadNpcs()
        {
            DatabaseManager.Instance.Reload<NpcSpawnRecord>();
            DatabaseManager.Instance.Reload<NpcReplyRecord>();
            DatabaseManager.Instance.Reload<NpcActionRecord>();

            NpcSpawnRecord.Initialize();

            foreach (var map in MapRecord.GetMaps())
            {
                foreach (var npc in map.Instance.GetEntities<Npc>())
                {
                    map.Instance.RemoveEntity(npc.Id);
                }
                map.Instance.Reload();
            }

            NpcsManager.Instance.SpawnNpcs();

        }

        public void AddNpc(int mapId, short cellId, DirectionsEnum direction, short templateId)
        {
            var targetMap = MapRecord.GetMap(mapId);

            NpcRecord record = NpcRecord.GetNpcRecord(templateId);

            var npcSpawnId = NpcSpawnRecord.PopNextId();

            NpcSpawnRecord spawnRecord = new NpcSpawnRecord()
            {
                CellId = cellId,
                Direction = direction,
                Id = npcSpawnId,
                MapId = mapId,
                TemplateId = templateId,
                Template = record,
                Actions = new List<NpcActionRecord>(),
            };

            spawnRecord.AddInstantElement();

            Npc npc = new Npc(spawnRecord, targetMap);

            targetMap.Instance.AddEntity(npc);
        }

        [Useless]
        public void AddNpcShop(int mapId, short cellId, DirectionsEnum direction, short templateId, int itemType)
        {
            var targetMap = MapRecord.GetMap(mapId);

            NpcRecord record = NpcRecord.GetNpcRecord(templateId);

            if (!record.Actions.Contains((byte)(NpcActionsEnum.BUYSELL)))
            {
                Logger.Write("Unable to create vendor with a non vendor npc.", Channels.Warning);
                return;
            }
            string itemList = string.Join(",", ItemRecord.GetItems().Where(x => x.TypeId == itemType).Select(x => x.Id));

            var npcSpawnId = NpcSpawnRecord.PopNextId();

            NpcActionRecord actionRecord = new NpcActionRecord();
            actionRecord.Action = NpcActionsEnum.BUYSELL;
            actionRecord.Param1 = itemList;
            actionRecord.Param2 = "0";
            actionRecord.Id = NpcActionRecord.PopNextId();
            actionRecord.NpcSpawnId = npcSpawnId;

            NpcSpawnRecord spawnRecord = new NpcSpawnRecord()
            {
                CellId = cellId,
                Direction = direction,
                Id = npcSpawnId,
                MapId = mapId,
                TemplateId = templateId,
                Template = record,
                Actions = new List<NpcActionRecord>() { actionRecord },
            };

            spawnRecord.AddInstantElement();
            actionRecord.AddInstantElement();

            Npc npc = new Npc(spawnRecord, targetMap);
            targetMap.Instance.AddEntity(npc);
        }
    }
    public class NpcActionHandlerAttribute : Attribute
    {
        public NpcActionsEnum Action
        {
            get;
            set;
        }
        public NpcActionHandlerAttribute(NpcActionsEnum action)
        {
            this.Action = action;
        }
    }
}

using Giny.Core.Extensions;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Items;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Maps.Npcs;
using Giny.World.Managers.Maps.Paths;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Managers.Monsters;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using Giny.World.Records.Items;
using Giny.World.Records.Jobs;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Tinsel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.World.Managers.Chat
{
    class ChatCommands
    {
        [ChatCommand("help", ServerRoleEnum.Player)]
        public static void HelpCommand(WorldClient client)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Available commands:" + "\n");

            foreach (var command in ChatCommandsManager.Instance.GetCommandsAttribute())
            {
                if (command.RequiredRole <= client.Account.Role)
                {
                    sb.Append("-" + command.Name + "\n");
                }
            }

            client.Character.ReplyWarning(sb.ToString());
        }
        [ChatCommand("infos", ServerRoleEnum.Administrator)]
        public static void InfosCommand(WorldClient client)
        {
            foreach (var onlineClient in WorldServer.Instance.GetOnlineClients())
            {
                client.Character.Reply("-" + onlineClient.Character.Name);
            }

            client.Character.Reply("Max Connected : " + WorldServer.Instance.MaximumClients);
            client.Character.Reply("Ips : " + WorldServer.Instance.GetOnlineClients().DistinctBy(x => x.Ip).Count());
            client.Character.Reply("Connected : " + WorldServer.Instance.GetOnlineClients().Count());

        }
        [ChatCommand("items", ServerRoleEnum.Administrator)]
        public static void ReloadItemsCommand(WorldClient client)
        {
            ItemsManager.Instance.Reload();
            client.Character.Reply("Items reloaded.");
        }
        [ChatCommand("bonta", ServerRoleEnum.Player)]
        public static void BontaCommand(WorldClient client)
        {
            const int targetMapId = 147768;

            if (client.Character.Map.Id == targetMapId)
            {
                return;
            }
            if (client.Character.HasParty)
            {
                foreach (var member in client.Character.Party.Members.Values)
                {
                    member.Teleport(targetMapId);
                }
            }
            else
            {
                client.Character.Teleport(targetMapId);
            }
        }

        [ChatCommand("npcs", ServerRoleEnum.Administrator)]
        public static void ReloadNpcs(WorldClient client)
        {
            NpcsManager.Instance.ReloadNpcs();
            client.Character.ReplyWarning("Npcs reloaded.");
        }
        [ChatCommand("event", ServerRoleEnum.Player)] // in module
        public static void EventCommand(WorldClient client)
        {
            client.Character.Teleport(196086788);
        }
        [ChatCommand("look", ServerRoleEnum.Administrator)]
        public static void LookCommand(WorldClient client, string lookStr)
        {
            var look = EntityLookManager.Instance.Parse(System.Web.HttpUtility.HtmlDecode(lookStr));
            client.Character.Look = look;
            client.Character.RefreshActorOnMap();
        }
        [ChatCommand("sun", ServerRoleEnum.Administrator)]
        public static void AddSunCommand(WorldClient client, int elementId, int mapId, short cellId)
        {
            if (MapsManager.Instance.AddInteractiveSkill(client.Character.Map, elementId, GenericActionEnum.Teleport,
                (InteractiveTypeEnum)0, SkillTypeEnum.USE114, mapId.ToString(), cellId.ToString()))
            {
                client.Character.Reply("Sun added on element " + elementId);
            }
            else
            {
                client.Character.ReplyWarning("Unable to add element");

            }
        }

        [ChatCommand("craft", ServerRoleEnum.Administrator)]
        public static void CraftCommand(WorldClient client, int elementId, int skillType)
        {
            var skill = SkillRecord.GetSkill((SkillTypeEnum)skillType);

            if (MapsManager.Instance.AddInteractiveSkill(client.Character.Map, elementId, GenericActionEnum.Craft,
                (InteractiveTypeEnum)skill.InteractiveTypeId,
              (SkillTypeEnum)skill.Id))
            {
                client.Character.Reply("Craft table added element " + elementId);
            }
            else
            {
                client.Character.ReplyWarning("Unable to add element");

            }
        }
        [ChatCommand("jobxp", ServerRoleEnum.Administrator)]
        public static void AddJobXp(WorldClient client, long amount)
        {
            foreach (var job in client.Character.Record.Jobs)
            {
                client.Character.AddJobExp(job.JobId, amount);
            }
        }
        [ChatCommand("rmin", ServerRoleEnum.Administrator)]
        public static void RemoveInteractiveCommand(WorldClient client, int elementId)
        {
            var element = client.Character.Map.GetElementRecord(elementId);


            if (element == null)
            {
                client.Character.ReplyWarning("Unable to find element : " + elementId);
                return;
            }
            if (element.Skill == null)
            {
                client.Character.ReplyWarning("Unable to find skill : " + elementId);
                return;
            }
            element.Skill.RemoveInstantElement();
            element.Skill = null;

            client.Character.Map.Instance.Reload();
        }
        [ChatCommand("smith", ServerRoleEnum.Administrator)]
        public static void SmithCommand(WorldClient client, int elementId, int skillType)
        {
            var skill = SkillRecord.GetSkill((SkillTypeEnum)skillType);

            if (MapsManager.Instance.AddInteractiveSkill(client.Character.Map, elementId, GenericActionEnum.Smithmagic, (InteractiveTypeEnum)skill.InteractiveTypeId,
              (SkillTypeEnum)skill.Id))
            {
                client.Character.Reply("Smith table added element " + elementId);
            }
            else
            {
                client.Character.ReplyWarning("Unable to add element");

            }
        }

        [ChatCommand("monsters", ServerRoleEnum.Administrator)]
        public static void SpawnMonstersCommand(WorldClient source, string monsters)
        {
            IEnumerable<MonsterRecord> records = monsters.Split(',').Select(x => MonsterRecord.GetMonsterRecord(short.Parse(x)));
            MonstersManager.Instance.AddFixedMonsterGroup(source.Character.Map.Instance, source.Character.CellId, records.ToArray());
            source.Character.Reply("Monsters spawned.");
        }
        [ChatCommand("elements", ServerRoleEnum.Administrator)]
        public static void ElementsCommand(WorldClient source)
        {
            InteractiveElementRecord[] elements = source.Character.Map.Elements.ToArray();

            if (elements.Count() == 0)
            {
                source.Character.Reply("No Elements on Map...");
                return;
            }

            var colors = CollectionsExtensions.RandomColors(elements.Length);

            source.Character.DebugClearHighlightCells();

            for (int i = 0; i < elements.Count(); i++)
            {
                var ele = elements[i];
                var cell = source.Character.Map.GetCell(ele.CellId);

                if (cell != null)
                {
                    source.Character.DebugHighlightCells(colors[i], new CellRecord[] { cell });
                    source.Character.Reply("Id: " + ele.Identifier + " Cell:" + ele.CellId + " Bones:" + ele.BonesId + " Gfx:" + ele.GfxId, colors[i]);
                }
            }
        }
        [ChatCommand("rdmap", ServerRoleEnum.Administrator)]
        public static void TeleportToRandomMapInSubarea(WorldClient client, short subareaId)
        {
            client.Character.Teleport(MapRecord.GetMaps().Where(x => x.Subarea.Id == subareaId).Random());
        }
        [ChatCommand("map", ServerRoleEnum.Administrator)]
        public static void MapCommand(WorldClient client)
        {
            client.Character.Reply(client.Character.Map.Instance.ToString(), Color.CornflowerBlue);
        }
        [ChatCommand("addnpcshop", ServerRoleEnum.Administrator)]
        public static void AddNpcCommand(WorldClient client, short templateId, int itemType)
        {
            NpcsManager.Instance.AddNpcShop((int)client.Character.Map.Id, client.Character.CellId, client.Character.Direction, templateId, itemType);
        }
        [ChatCommand("addnpc", ServerRoleEnum.Administrator)]
        public static void AddNpcCommand(WorldClient client, short templateId)
        {
            NpcsManager.Instance.AddNpc((int)client.Character.Map.Id, client.Character.CellId, client.Character.Direction, templateId);
        }
        [ChatCommand("mvnpc", ServerRoleEnum.Administrator)]
        public static void MoveNpcCommand(WorldClient client, long spawnId)
        {
            NpcsManager.Instance.MoveNpc(spawnId, (int)client.Character.Map.Id, client.Character.CellId, client.Character.Direction);
        }
        [ChatCommand("rmnpc", ServerRoleEnum.Administrator)]
        public static void RemoveNpcCommand(WorldClient client, long spawnId)
        {
            NpcsManager.Instance.RemoveNpc(spawnId);
        }
        [ChatCommand("spawn", ServerRoleEnum.Player)]
        public static void SpawnCommmand(WorldClient client)
        {
            client.Character.SpawnPoint();
        }
        [ChatCommand("start", ServerRoleEnum.Player)]
        public static void StartCommand(WorldClient client)
        {
            client.Character.Teleport(ConfigFile.Instance.SpawnMapId, ConfigFile.Instance.SpawnCellId);
        }
        [ChatCommand("itemlist", ServerRoleEnum.Administrator)]
        public static void ItemListCommand(WorldClient client, short id)
        {
            var items = ItemRecord.GetItems().Where(x => x.TypeId == id).OrderBy(x => x.Level);

            client.Character.Reply(string.Join(",", items.Select(x => x.Id.ToString())));
        }
        [ChatCommand("title", ServerRoleEnum.GamemasterPadawan)]
        public static void AddTitleCommand(WorldClient client, short id)
        {
            if (TitleRecord.Exists(id))
            {
                client.Character.LearnTitle(id);
            }
            else
            {
                client.Character.ReplyWarning("This title do not exists.");
            }
        }
        [ChatCommand("ornament", ServerRoleEnum.GamemasterPadawan)]
        public static void AddOrnamentCommand(WorldClient client, short id)
        {
            if (OrnamentRecord.Exists(id))
            {
                client.Character.LearnOrnament(id, true);
            }
            else
            {
                client.Character.ReplyWarning("This ornament do not exists.");
            }
        }
        [ChatCommand("party", ServerRoleEnum.Administrator)]
        public static void TeleportPartyCommand(WorldClient client)
        {
            if (!client.Character.HasParty)
            {
                client.Character.ReplyWarning("No party.");
            }

            foreach (var member in client.Character.Party.Members.Values)
            {
                member.Teleport(client.Character.Map.Id);
            }
        }
        [ChatCommand("addzaap", ServerRoleEnum.Administrator)]
        public static void AddZaapCommand(WorldClient client, int elementId, int zoneId)
        {
            TeleportersManager.Instance.AddDestination(
                TeleporterTypeEnum.TELEPORTER_ZAAP,
                InteractiveTypeEnum.ZAAP16,
                GenericActionEnum.Zaap,
                client.Character.Map,
               client.Character.Map.GetElementRecord(elementId),
                zoneId);
        }
        [ChatCommand("addzaapi", ServerRoleEnum.Administrator)]
        public static void AddZaapiCommand(WorldClient client, int elementId)
        {
            TeleportersManager.Instance.AddDestination(
                TeleporterTypeEnum.TELEPORTER_SUBWAY,
                InteractiveTypeEnum.ZAAPI106,
                GenericActionEnum.Zaapi,
                client.Character.Map,
               client.Character.Map.GetElementRecord(elementId),
                client.Character.Map.Subarea.AreaId);

            client.Character.Reply("Zaapi added.");
        }
        [ChatCommand("nocollide", ServerRoleEnum.Administrator)]
        public static void NoCollideCommand(WorldClient client)
        {
            List<MapObstacle> obstacles = new List<MapObstacle>();

            for (short i = 0; i < 560; i++)
            {
                obstacles.Add(new MapObstacle(i, 1));
            }
            client.Send(new MapObstacleUpdateMessage(obstacles.ToArray()));
            client.Character.Reply("No collide.", true);
        }
        [ChatCommand("level", ServerRoleEnum.GamemasterPadawan)]
        public static void LevelCommand(WorldClient client, short newLevel)
        {
            if (newLevel <= client.Character.Level)
            {
                client.Character.ReplyWarning("New level must be superior to <b>" + client.Character.Level + "</b>");
                return;
            }
            client.Character.SetExperience(ExperienceManager.Instance.GetCharacterXPForLevel(newLevel));
        }
        [ChatCommand("exp", ServerRoleEnum.GamemasterPadawan)]
        public static void AddExpCommand(WorldClient client, long amount)
        {
            client.Character.AddExperience(amount);
        }
        [ChatCommand("notif", ServerRoleEnum.Administrator)]
        public static void NotifCommand(WorldClient client, string message)
        {
            foreach (var target in WorldServer.Instance.GetOnlineClients())
            {
                target.Character.DisplayNotification("Serveur : " + message);
            }
        }
        [ChatCommand("go", ServerRoleEnum.GamemasterPadawan)]
        public static void TeleportCommand(WorldClient client, int mapId)
        {
            client.Character.Teleport(mapId);
        }
        [ChatCommand("item", ServerRoleEnum.GamemasterPadawan)]
        public static void AddItemCommand(WorldClient client, short itemId, int quantity)
        {
            client.Character.Inventory.AddItem(itemId, quantity, true);
            client.Character.NotifyItemGained(itemId, quantity);
        }
        [ChatCommand("relative", ServerRoleEnum.Administrator)]
        public static void RelativeMapCommand(WorldClient client)
        {
            var maps = MapRecord.GetMaps(client.Character.Map.Position.Point).ToList();

            int index = maps.IndexOf(client.Character.Map);

            if (maps.Count > index + 1)
                client.Character.Teleport((int)maps[index + 1].Id);
            else
                client.Character.Teleport((int)maps[0].Id);
        }
        [ChatCommand("goto", ServerRoleEnum.GamemasterPadawan)]
        public static void GotoCommand(WorldClient client, string targetName)
        {
            var target = WorldServer.Instance.GetOnlineClient(x => x.Character.Name == targetName);

            if (target != null)
            {
                client.Character.Teleport((int)target.Character.Map.Id, target.Character.CellId);
            }
            else
            {
                client.Character.ReplyWarning("<b>" + targetName + "</b> is not connected.");
            }
        }
        [ChatCommand("spell", ServerRoleEnum.GamemasterPadawan)]
        public static void LearnSpell(WorldClient client, short spellId)
        {
            client.Character.LearnSpell(spellId, true);
        }
        [ChatCommand("kamas", ServerRoleEnum.GamemasterPadawan)]
        public static void AddKamas(WorldClient client, long amount)
        {
            client.Character.AddKamas(amount);
            client.Character.OnKamasGained(amount);
        }
        [ChatCommand("doom", ServerRoleEnum.Administrator)]
        public static void LeafCommand(WorldClient client)
        {
            if (client.Character.Fighting)
            {
                client.Character.Fighter.EnemyTeam.KillTeam();
            }

            client.Character.Fighter.Fight.CheckFightEnd();
        }

        [ChatCommand("itemset", ServerRoleEnum.Administrator)]
        public static void ItemSetCommand(WorldClient client, int id)
        {
            ItemSetRecord set = ItemSetRecord.GetItemSet(id);

            if (set == null)
            {
                client.Character.ReplyWarning("Unable to find set : " + id);
            }
            else
            {
                foreach (var itemId in set.Items)
                {
                    client.Character.Inventory.AddItem(itemId, 1);
                }

                client.Character.Reply("Item set added.");
            }
        }
        [ChatCommand("restore", ServerRoleEnum.Administrator)]
        public static void RestoreCharacter(WorldClient client, string targetName)
        {
            var target = WorldServer.Instance.GetOnlineClient(x => x.Character.Name == targetName);

            if (target != null)
            {
                foreach (var item in target.Character.Inventory.GetEquipedItems())
                {
                    target.Character.Inventory.SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
                }

                foreach (KeyValuePair<CharacteristicEnum, DetailedCharacteristic> stat in target.Character.Stats.GetCharacteristics<DetailedCharacteristic>())
                {
                    stat.Value.Objects = 0;
                }

                target.Character.RefreshStats();
            }
            client.Character.Reply("Target restored.");
        }
        [ChatCommand("test", ServerRoleEnum.Administrator)]
        public static void TestCommand(WorldClient client)
        {
            var stats = client.Character.Stats;



            client.Character.RefreshStats();
            return;

            var item = client.Character.Inventory.GetEquipedItems().FirstOrDefault(x => x.Record.TypeEnum == ItemTypeEnum.RING);

            client.Character.Inventory.Unequip(item.PositionEnum);

            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 40));

            item.UpdateElement();


            client.Character.Inventory.OnItemModified(item);
            return;
            IEnumerable<MonsterRecord> records = MonsterRecord.GetMonsterRecords().Where(x => x.IsBoss == true).Shuffle().Take(8);
            MonstersManager.Instance.AddFixedMonsterGroup(client.Character.Map.Instance, client.Character.CellId, records.ToArray());
        }

        [ChatCommand("restoreitem", ServerRoleEnum.Administrator)]
        public static void RestoreItemCommand(WorldClient client, short itemId)
        {
            foreach (var item in CharacterItemRecord.GetCharacterItems())
            {
                if (item.GId == itemId)
                {
                    item.Effects = item.Record.Effects.Generate();
                    item.UpdateElement();

                    if (WorldServer.Instance.IsOnline(item.CharacterId) && item is CharacterItemRecord)
                    {
                        var target = WorldServer.Instance.GetOnlineClient(x => x.Character.Id == item.CharacterId);
                        target.Character.Inventory.OnItemModified(item);

                        client.Character.Reply("Update " + item.Record + ". Owner : " + target.Character.Name);
                    }
                }
            }

            foreach (var item in BankItemRecord.GetBankItems())
            {
                if (item.GId == itemId)
                {
                    item.Effects = item.Record.Effects.Generate();
                    item.UpdateElement();
                }
            }

            foreach (var item in BidShopItemRecord.GetItems())
            {
                if (item.GId == itemId)
                {
                    item.Effects = item.Record.Effects.Generate();
                    item.UpdateElement();
                }
            }

            foreach (var item in MerchantItemRecord.GetMerchantItems())
            {
                if (item.GId == itemId)
                {
                    item.Effects = item.Record.Effects.Generate();
                    item.UpdateElement();
                }
            }

            client.Character.Reply("Item restored.");
        }

    }
}

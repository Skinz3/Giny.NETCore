using Giny.Core.Network.Messages;
using Giny.Core.Pool;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Maps.Elements;
using Giny.World.Managers.Monsters;
using Giny.World.Managers.Skills;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Entities.Merchants;
using Giny.Core.Time;
using Giny.Core.Extensions;
using System.Collections.Concurrent;
using Giny.Core.DesignPattern;

namespace Giny.World.Managers.Maps.Instances
{
    public abstract class MapInstance : INetworkEntity
    {
        public int MonsterGroupCount
        {
            get
            {
                return GetEntities<MonsterGroup>().Count();
            }
        }

        public int CharactersCount
        {
            get
            {
                return GetEntities<Character>().Count();
            }
        }

        [WIP("thread safe..")]
        private List<MapElement> m_elements = new List<MapElement>();

        private ConcurrentDictionary<long, Entity> m_entities = new ConcurrentDictionary<long, Entity>();

        private ActionTimer m_monsterSpawner;

        private ConcurrentDictionary<int, Fight> m_fights = new ConcurrentDictionary<int, Fight>();

        private ReversedUniqueIdProvider m_npIdPopper = new ReversedUniqueIdProvider(0);

        private UniqueIdProvider m_dropItemIdPopper = new UniqueIdProvider(0);

        public int PopNextDropItemId()
        {
            return m_dropItemIdPopper.Pop();
        }
        public MapRecord Record
        {
            get;
            private set;
        }

        public bool Mute = false;

        public MapInstance(MapRecord record)
        {
            this.Record = record;
            this.m_elements = new List<MapElement>(Record.Elements.Where(x => x.Skill != null).Select(x => x.GetMapElement(this)).ToList());
            InitializeSpawnCycle();
        }
        private void InitializeSpawnCycle()
        {
            if (Record.IsDungeonMap && Record.MonsterRoom.MonsterIds.Count > 0)
            {
                this.m_monsterSpawner = new ActionTimer(Record.MonsterRoom.GetRespawnInterval(), SpawnDungeonGroup, true);
            }
            else
            {
                this.m_monsterSpawner = new ActionTimer(MonstersManager.MonsterSpawningPoolInterval, SpawnMonsterGroup, true);
            }
        }
        private void SpawnDungeonGroup()
        {
            if (MonsterGroupCount == 0)
            {
                MonstersManager.Instance.SpawnDungeonGroup(Record);
            }
        }
        private void SpawnMonsterGroup()
        {
            if (this.MonsterGroupCount < MonstersManager.MaxGroupPerMap)
            {
                AsyncRandom rd = new AsyncRandom();

                if (Record.Subarea.Monsters.Length > 0)
                    MonstersManager.Instance.SpawnMonsterGroup(this.Record, rd);
            }
        }

        public bool MonsterGroupExists(MonsterGroup monsterGroup)
        {
            foreach (var group in GetEntities<MonsterGroup>())
            {
                if (group.GetMonsters().All(x => monsterGroup.GetMonsters().Select(monster => monster.Record.Id).Contains(x.Record.Id)))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reload()
        {
            this.m_elements = new List<MapElement>(Record.Elements.Where(x => x.Skill != null).Select(x => x.GetMapElement(this)).ToList());

            foreach (var character in GetEntities<Character>())
            {
                character.Client.Send(GetMapComplementaryInformationsDataMessage(character));
            }

            InitializeSpawnCycle();
        }

        public void AddEntity(Entity entity)
        {
            if (!m_entities.ContainsKey(entity.Id))
            {
                var informations = entity.GetActorInformations();
                Send(new GameRolePlayShowActorMessage(informations));

                m_entities.TryAdd(entity.Id, entity);
                OnEntitiesUpdated();
            }
        }
        private void OnEntitiesUpdated()
        {
            if (Record.CanSpawnMonsters || (Record.IsDungeonMap && Record.MonsterRoom.MonsterIds.Count > 0))
            {
                if (CharactersCount == 0)
                    this.m_monsterSpawner.Pause();
                else if (!m_monsterSpawner.Started)
                    this.m_monsterSpawner.Start();

                m_monsterSpawner.Interval = MonstersManager.MonsterSpawningPoolInterval * (MonsterGroupCount + 1);
            }
        }
        public void RemoveEntity(long entityId)
        {
            Entity result = null;

            if (m_entities.TryRemove(entityId, out result))
            {
                this.Send(new GameContextRemoveElementMessage(result.Id));
                OnEntitiesUpdated();
            }
        }
        public Entity[] GetEntities()
        {
            return m_entities.Values.ToArray();
        }
        public T[] GetEntities<T>() where T : Entity
        {
            return m_entities.Values.OfType<T>().ToArray();
        }
        public T GetEntity<T>(long id) where T : Entity
        {
            if (!m_entities.ContainsKey(id))
            {
                return null;
            }
            return m_entities[id] as T;
        }

        public void AddFight(Fight fight)
        {
            if (m_fights.TryAdd(fight.Id, fight))
            {

                if (fight.ShowBlades)
                    Send(new GameRolePlayShowChallengeMessage(fight.GetFightCommonInformations()));

                SendMapFightCount();
            }
        }

        public Fight GetFight(short fightId)
        {
            Fight result = null;
            m_fights.TryGetValue(fightId, out result);
            return result;
        }

        public void RemoveFight(Fight fight)
        {
            Fight result = null;

            m_fights.TryRemove(fight.Id, out result);

            RemoveBlades(fight);

            SendMapFightCount();
        }
        public void RemoveBlades(Fight fight)
        {
            if (fight.ShowBlades)
                Send(new GameRolePlayRemoveChallengeMessage((short)fight.Id));
        }

        public void SendMapFightCount()
        {
            foreach (var character in GetEntities<Character>())
            {
                SendMapFightCount(character.Client);
            }

        }
        public void SendMapFightCount(WorldClient client)
        {
            client.Send(new MapFightCountMessage((short)m_fights.Count));
        }

        public bool IsCellFree(short cellId, short exclude)
        {
            foreach (var entity in GetEntities<Entity>())
            {
                if (entity.CellId == exclude)
                {
                    continue;
                }
                if (entity.CellId == cellId)
                    return false;
            }
            return true;
        }

        public bool IsCellFree(short cellId)
        {
            foreach (var entity in GetEntities<Entity>())
            {
                if (entity.CellId == cellId)
                    return false;
            }
            return true;
        }

        public CellRecord FindMonsterGroupCell()
        {
            return Record.Cells.Where(x => x.IsValidFightCell() && IsCellFree(x.Id)).Random();
        }
        public short? GetNearEntityCell(CellRecord cellRecord)
        {
            var point = cellRecord.Point.GetNearPoints().Shuffle().FirstOrDefault(x => Record.IsCellWalkable(x.CellId) && IsCellFree(x.CellId));

            if (point != null)
            {
                return point.CellId;
            }
            else
            {
                return null;
            }
        }

        public T GetEntity<T>(Func<T, bool> predicate) where T : Entity
        {
            return (T)m_entities.Values.OfType<T>().FirstOrDefault(predicate);
        }
        public Entity GetEntity(long id)
        {
            return GetEntity<Entity>(id);
        }

        public void EntityTalk(Entity entity, string message)
        {
            Send(new EntityTalkMessage((double)entity.Id, 4, new string[] { message }));
        }

        public void SendMapComplementary(WorldClient client)
        {
            client.Send(GetMapComplementaryInformationsDataMessage(client.Character));
        }
        protected GameRolePlayActorInformations[] GetGameRolePlayActorsInformations()
        {
            return m_entities.Values.Select(x => x.GetActorInformations()).ToArray();
        }
        protected MapObstacle[] GetMapObstacles()
        {
            return new MapObstacle[0];
        }
        protected HouseInformations[] GetHousesInformations()
        {
            return new HouseInformations[0];
        }
        protected bool HasAgressiveMonsters()
        {
            return false;
        }
        public abstract MapComplementaryInformationsDataMessage GetMapComplementaryInformationsDataMessage(Character character);

        public long PopNextNPEntityId()
        {
            return (long)m_npIdPopper.Pop();
        }

        public void Send(NetworkMessage message)
        {
            foreach (var character in GetEntities<Character>())
            {
                character.Client.Send(message);
            }
        }
        
        public void ToggleMute()
        {
            Mute = !Mute;
        }
        public MapElement GetElement<T>(int identifier)
        {
            return (MapElement)m_elements.FirstOrDefault(x => x.Record.Identifier == identifier);
        }
        public IEnumerable<T> GetElements<T>() where T : MapElement
        {
            return m_elements.OfType<T>();
        }
        protected FightStartingPositions GetFightStartingPositions()
        {
            return new FightStartingPositions(Record.RedCells.Select(x => x.Id).ToArray(), Record.BlueCells.Select(x => x.Id).ToArray());
        }
        protected InteractiveElement[] GetInteractiveElements(Character character)
        {
            return GetElements<MapInteractiveElement>().Select(x => x.GetInteractiveElement(character)).ToArray(); // todo create mapinteractiveelement and mapstatedelement
        }
        protected StatedElement[] GetStatedElements()
        {
            return GetElements<MapStatedElement>().Select(x => x.GetStatedElement()).ToArray();
        }
        protected FightCommonInformations[] GetFightsCommonInformations()
        {
            return m_fights.Values.Where(x => !x.Started).Select(x => x.GetFightCommonInformations()).ToArray();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var entity in m_entities.Values)
            {
                sb.Append(entity.ToString() + "\n");
            }
            return sb.ToString();
        }
        /// <summary>
        /// yet, i dont think more then one skill by element is effective for a private server
        /// </summary>
        public void UseInteractive(Character character, int elemId, int skillInstanceUid)
        {
            var element = this.GetElement<MapElement>(elemId);

            if (element != null && element.CanUse(character) && !character.Busy)
            {
                if (element != null)
                {
                    if (GenericActionsManager.Instance.IsHandled(element))
                    {
                        bool canMove = element.Record.Skill.Record.ParentBonesIds.Count == 0; /* Should be working */

                        short duration = canMove ? (short)0 : SkillsManager.SKILL_DURATION; /* Duration should be related to job level (its not a const) */

                        character.SendMap(new InteractiveUsedMessage(character.Id, elemId, (short)element.Record.Skill.SkillId, duration, canMove));
                        GenericActionsManager.Instance.Handle(character, element);
                    }
                    else
                    {
                        character.Client.Send(new InteractiveUseErrorMessage(elemId, skillInstanceUid));
                    }
                }
                else
                {
                    character.Client.Send(new InteractiveUseErrorMessage(elemId, skillInstanceUid));
                }
            }
            else
            {
                character.Client.Send(new InteractiveUseErrorMessage(elemId, skillInstanceUid));
            }
        }

        public bool IsMerchantLimitReached()
        {
            return GetEntities<CharacterMerchant>().Count() >= ConfigFile.Instance.MaxMerchantPerMap;
        }

        /* public void RemoveDropItem(DropItem dropItem)
         {
             m_droppedItems.Remove(dropItem);
             m_dropItemIdPopper.Push(dropItem.Id);
             this.Send(new ObjectGroundRemovedMessage(dropItem.CellId));

         }

         public DropItem GetDroppedItem(ushort cellId)
         {
             return m_droppedItems.FirstOrDefault(x => x.CellId == cellId);
         }

         public DropItem[] GetDroppedItems()
         {
             return m_droppedItems.ToArray();
         }

         public void OnElementsUpdated()
         {
             foreach (var character in GetEntities<Character>())
             {
                 var elements = Array.ConvertAll(m_interactiveElements.ToArray(), x => x.GetInteractiveElement(character));

                 foreach (var element in elements)
                 {
                     character.Client.Send(new InteractiveElementUpdatedMessage(element));
                 }
             }
         } */
    }
}

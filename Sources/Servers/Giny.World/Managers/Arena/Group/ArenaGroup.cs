using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.World.Managers.Arena.Member;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Records.Arena;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Arena.Group
{
    public class ArenaGroup
    {
        public const ushort LEVEL_SHIFT = 30;

        public CharacterInventoryPositionEnum[] NoPositions = new CharacterInventoryPositionEnum[]
        {
        CharacterInventoryPositionEnum.INVENTORY_POSITION_ENTITY,
        CharacterInventoryPositionEnum.ACCESSORY_POSITION_SHIELD,
        };

        private PvpArenaTypeEnum arenaType;
        protected ArenaMemberCollection blueTeam { get; private set; }
        protected ArenaMemberCollection redTeam { get; private set; }

        public ArenaGroup(PvpArenaTypeEnum type)
        {
            arenaType = type;
            blueTeam = new ArenaMemberCollection(this);
            redTeam = new ArenaMemberCollection(this);
        }

        public PvpArenaTypeEnum Type => arenaType;

        public virtual ushort RequestDuration
        {
            get
            {
                return 30;
            }
        }

        public bool CanChallenge(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            if (Empty)
            {
                return true;
            }

            double averageLevel = CalculateAverageLevel();
            return character.Level > averageLevel - LEVEL_SHIFT && character.Level < averageLevel + LEVEL_SHIFT;
        }

        private double CalculateAverageLevel()
        {
            var allMembers = blueTeam.GetMembers().Concat(redTeam.GetMembers()).ToList();
            if (!allMembers.Any())
            {
                return 0;
            }

            double totalLevel = allMembers.Sum(member => member.Character.Level);
            return totalLevel / allMembers.Count;
        }

        public bool Ready
        {
            get
            {
                return blueTeam.IsFull && redTeam.IsFull;
            }
        }

        public bool Accepted
        {
            get
            {
                return blueTeam.Accepted && redTeam.Accepted;
            }
        }

        public bool Empty
        {
            get
            {
                return blueTeam.Empty && redTeam.Empty;
            }
        }


        public void AddCharacter(Character character)
        {
            var team = SelectTeam();
            team.AddMember(this, character);
        }

        public void RemoveCharacter(Character character)
        {
            if (blueTeam.RemoveMember(character))
            {
                return;
            }

            if (redTeam.RemoveMember(character))
            {
                return;
            }

            throw new InvalidOperationException("Character not found in any team.");
        }

        private ArenaMemberCollection SelectTeam()
        {
            // Select the team with fewer or equal members
            return blueTeam.GetMembers().Count() <= redTeam.GetMembers().Count() ? blueTeam : redTeam;
        }

        public ArenaMember[] GetAllMembers()
        {
            return blueTeam.GetMembers().Concat(redTeam.GetMembers()).ToArray();
        }


        public void StartFighting()
        {
            ForEach(x => x.Character.PreviousRoleplayMapId = (int?)x.Character.Record.MapId);
            ForEach(x => x.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_STARTING_FIGHT));

            MapRecord map = ArenaMapRecord.GetArenaMap();

            foreach (var member in blueTeam.GetMembers())
            {
                member.Character.Teleport(map.Id);
                CheckIntegrity(member);

            }
            foreach (var member in redTeam.GetMembers())
            {
                member.Character.Teleport(map.Id);
                CheckIntegrity(member);
            }
            FightArena fight = FightManager.Instance.CreateFightArena(map);

            foreach (var member in blueTeam.GetMembers())
            {
                fight.BlueTeam.AddFighter(member.Character.CreateFighter(fight.BlueTeam));
            }

            foreach (var member in redTeam.GetMembers())
            {
                fight.RedTeam.AddFighter(member.Character.CreateFighter(fight.RedTeam));
            }


            fight.StartPlacement();
            this.Dispose();
        }

        public void CheckIntegrity(ArenaMember member)
        {
            foreach (var noPos in NoPositions)
            {
                if (member.Character.Inventory.Unequip(noPos))
                {
                    member.Character.OnItemUnequipedArena();
                }
            }

        }

        public bool ContainsIp(string ip)
        {
            return GetAllMembers().Any(x => x.Character.Client.Ip == ip);
        }
        public void Send(NetworkMessage message)
        {
            ForEach(x => x.Send(message));
        }
        public void ForEach(Action<ArenaMember> action)
        {
            foreach (var member in blueTeam.GetMembers())
            {
                action(member);
            }
            foreach (var member in redTeam.GetMembers())
            {
                action(member);
            }
        }
        public void Dispose()
        {
            ArenaManager.Instance.ArenaGroups.Remove(this);
            this.redTeam = null;
            this.blueTeam = null;
        }
    }

}

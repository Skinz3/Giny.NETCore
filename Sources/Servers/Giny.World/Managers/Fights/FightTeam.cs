using Giny.Core.Extensions;
using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Maps;
using Giny.World.Network;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights
{
    public class FightTeam : INetworkEntity
    {
        public TeamEnum TeamId
        {
            get;
            private set;
        }

        public AlignmentSideEnum Side
        {
            get;
            private set;
        }

        public TeamTypeEnum Type
        {
            get;
            private set;
        }

        private List<Fighter> Fighters = new List<Fighter>();

        public Fighter Leader => Fighters.Count > 0 ? Fighters.First() : null;

        public FightTeam EnemyTeam => this == Fight.RedTeam ? Fight.BlueTeam : Fight.RedTeam;

        public IEnumerable<CellRecord> PlacementCells
        {
            get;
            set;
        }
        public Fight Fight
        {
            get;
            set;
        }

        public CellRecord BladesCell
        {
            get;
            set;
        }

        public FightTeamOptions Options
        {
            get;
            set;
        }

        public int Alives
        {
            get
            {
                return Fighters.FindAll(x => x.Alive).Count;
            }
        }

        public bool LeaderAsParty
        {
            get
            {
                return (Leader is CharacterFighter) && ((CharacterFighter)Leader).Character.HasParty;
            }
        }

        public FightTeam(TeamEnum id, IEnumerable<CellRecord> placementCells, AlignmentSideEnum side, TeamTypeEnum teamtype)
        {
            this.Side = side;
            this.Type = teamtype;
            this.TeamId = id;
            this.PlacementCells = placementCells;
            this.Options = new FightTeamOptions(this);
        }
        public void Send(NetworkMessage message)
        {
            foreach (var fighter in GetFighters<CharacterFighter>(false).Where(x => !x.Disconnected))
            {
                fighter.Character.Client.Send(message);
            }
        }



        public T GetFighter<T>(Func<T, bool> predicate) where T : Fighter
        {
            return GetFighters<T>().FirstOrDefault(predicate);
        }
        public void OnFighters<T>(Action<T> action, bool aliveOnly = true) where T : Fighter
        {
            foreach (var fighter in GetFighters<T>(aliveOnly))
            {
                action(fighter);
            }
        }
        public IEnumerable<T> GetFighters<T>(bool aliveOnly = true) where T : Fighter
        {
            return aliveOnly ? Fighters.OfType<T>().Where(x => x.Alive) : Fighters.OfType<T>();
        }
        public IEnumerable<Fighter> GetFighters(bool aliveOnly = true)
        {
            return aliveOnly ? Fighters.Where(x => x.Alive) : Fighters;
        }
        public Fighter[] GetDeads()
        {
            return Fighters.FindAll(x => !x.Alive).ToArray();
        }
        public void KillTeam()
        {
            foreach (var target in GetFighters<Fighter>())
            {
                target.Stats.LifePoints = 0;
                target.Die(target);
            }

        }
        public IEnumerable<Portal> GetPortals()
        {
            return Fight.GetMarks<Portal>().Where(x => x.Source.Team == this);
        }

        public void AddFighter(Fighter fighter)
        {
            fighter.Fight = Fight;
            fighter.Initialize();
            this.Fighters.Add(fighter);

            if (!Fight.Started)
                fighter.OnJoined();

            Fight.OnFighterJoined(fighter);
        }

        public void ShowCell(CharacterFighter source, short cellId)
        {
            this.Send(new ShowCellMessage(source.Id, cellId));
        }
        public CellRecord GetPlacementCell()
        {
            return PlacementCells.Where(cell => !Fighters.Any(fighter => fighter.Cell.Id == cell.Id)).Random();
        }
        public void RemoveFighter(Fighter fighter)
        {
            Fighters.Remove(fighter);

            Fight.Send(new GameFightRemoveTeamMemberMessage((short)Fight.Id, (byte)TeamId, fighter.Id));

            Fight.UpdateTeams();
        }

        public void Update()
        {
            var msg = new GameFightUpdateTeamMessage((short)Fight.Id, GetFightTeamInformations());
            Fight.Send(msg);

            if (Fight.ShowBlades && !Fight.Started)
                Fight.Map.Instance.Send(msg);
        }
        public bool IsPlacementCellsFree(int count)
        {
            return PlacementCells.Count(x => !Fighters.Any(fighter => fighter.Cell.Id == x.Id)) >= count;
        }

        public int GetFightersCount(bool aliveOnly = true)
        {
            return GetFighters<Fighter>(aliveOnly).Count();
        }
        public bool AreAllReady()
        {
            return Fighters.All(x => x.IsReady);
        }
        /*public bool CanSpawnPortal()
        {
            return GetAllPortals().Length < 4;
        }
        public Portal[] GetAllPortals()
        {
            return Fight.Marks.OfType<Portal>().Where(x => x.Source.Team == this).ToArray();
        }
        public int GetActivePortalCount()
        {
            return GetAllPortals().Count(x => x.Active);
        }
        public void RemoveFirstPortal(Fighter source)
        {
            var portal = GetAllPortals().FirstOrDefault();

            if (portal != null)
            {
                Fight.RemoveMark(source, portal);
            }
        } */
        public FightTeamInformations GetFightTeamInformations()
        {
            List<FightTeamMemberInformations> members = new List<FightTeamMemberInformations>();
            Fighters.ForEach(x => members.Add(x.GetFightTeamMemberInformations()));
            return new FightTeamInformations()
            {
                leaderId = Leader == null ? 0 : Leader.Id,
                nbWaves = 0,
                teamId = (byte)TeamId,
                teamMembers = members.ToArray(),
                teamSide = (byte)Side,
                teamTypeId = (byte)Type,
            };
        }
        public FightTeamLightInformations GetFightTeamLightInformations()
        {
            return new FightTeamLightInformations()
            {
                hasAllianceMember = false,
                hasFriend = false,
                hasGroupMember = false,
                hasGuildMember = false,
                hasMyTaxCollector = false,
                leaderId = Leader == null ? 0 : Leader.Id,
                meanLevel = GetTeamLevel(),
                nbWaves = 0,
                teamId = (byte)TeamId,
                teamMembersCount = (byte)Fighters.Count,
                teamSide = (byte)Side,
                teamTypeId = (byte)Type,
            };
        }

        public Fighter CloserFighter(Fighter source)
        {
            return GetFighters<Fighter>(true).OrderByDescending(x => x.GetMPDistance(source)).LastOrDefault();
        }
        public Fighter[] CloserFighters(Fighter source)
        {
            return GetFighters<Fighter>(true).OrderByDescending(x => x.GetMPDistance(source)).ToArray();
        }
        public Fighter LastDead()
        {
            return Fighters.FindAll(x => !x.Alive).OrderByDescending(x => x.DeathTime).FirstOrDefault();
        }
        public int GetTeamLevel()
        {
            return Fighters.Sum(x => x.Level);
        }

    }
}



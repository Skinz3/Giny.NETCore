using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Handlers.Roleplay.Maps.Paths;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Monsters;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Maps
{
    class MapsHandler
    {
        [WIP("check areaId")]
        [MessageHandler]
        public static void HandleFriendJoinRequestMessage(FriendJoinRequestMessage message, WorldClient client)
        {
            //  client.Character.Teleport(target.Character.Record.MapId, target.Character.Map.Instance.GetNearEntityCell(target.Character.GetCell()));
        }
        [MessageHandler]
        public static void HandleGameMapChangeOriantation(GameMapChangeOrientationRequestMessage message, WorldClient client)
        {
            client.Character.SetDirection((DirectionsEnum)message.direction);
        }
        [MessageHandler]
        public static void HandleEmotePlayRequestMessage(EmotePlayRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting && !client.Character.Collecting)
                client.Character.PlayEmote(message.emoteId);
        }
        [MessageHandler]
        public static void HandleMapInformationsRequestMessage(MapInformationsRequestMessage message, WorldClient client)
        {
            if (client.Character.Record.MapId == message.mapId)
            {
                client.Character.Map = MapRecord.GetMap((int)message.mapId);

                if (client.Character.Map == null)
                {
                    client.Character.SpawnPoint();
                    client.Character.Reply("Unknown Map...(" + message.mapId + ")");
                    return;
                }

                client.Character.OnEnterMap();
                client.Character.NoMove();

            }
        }

        [MessageHandler]
        public static void HandleGameMapMovementRequestMessage(GameMapMovementRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                if (client.Character.Fighter.Fight.StartAcknowledged)
                {
                    List<short> path = PathReader.FightMove(PathReader.ReturnDispatchedCells(message.keyMovements)).Keys.ToList();

                    IEnumerable<CellRecord> cells = path.Select(x => client.Character.Map.GetCell(x));
                    client.Character.Fighter.Move(cells.ToList());
                }
            }
            else
            {

                if (!client.Character.ChangeMap && client.Character.Map.Id == message.mapId)//&& !client.Character.Collecting)
                    client.Character.MoveOnMap(message.keyMovements);
                else
                    client.Character.NoMove();
            }
        }

        [MessageHandler]
        public static void HandleMapMovementConfirm(GameMapMovementConfirmMessage message, WorldClient client)
        {
            if (client.Character.IsMoving)
            {
                client.Character.EndMove();
                client.Send(new BasicNoOperationMessage());
            }
        }
        [MessageHandler]
        public static void HandleMapMovementCancel(GameMapMovementCancelMessage message, WorldClient client)
        {
            client.Character.CancelMove(message.cellId);
        }
        [MessageHandler]
        public static void HandleGameRolePlayAttackMonsterRequest(GameRolePlayAttackMonsterRequestMessage message, WorldClient client)
        {
            if (client.Character.Map != null)
            {
                MonsterGroup group = client.Character.Map.Instance.GetEntity<MonsterGroup>((long)message.monsterGroupId);

                if (group != null && client.Character.CellId == group.CellId)
                {
                    if (client.Character.Map.BlueCells.Count >= group.MonsterCount && client.Character.Map.RedCells.Count() >= client.Character.FighterCount)
                    {
                        client.Character.Map.Instance.RemoveEntity(group.Id);

                        CellRecord cell = client.Character.Map.GetCell(group.CellId);

                        FightPvM fight = FightManager.Instance.CreateFightPvM(client.Character, group, client.Character.Map, cell);

                        foreach (var monsterFighter in group.CreateFighters(fight.BlueTeam))
                        {
                            fight.BlueTeam.AddFighter(monsterFighter);
                        }

                        fight.RedTeam.AddFighter(client.Character.CreateFighter(fight.RedTeam));

                        fight.StartPlacement();

                    }
                    else
                    {
                        client.Character.ReplyWarning("Invalid map placements. Unable to fight.");
                    }
                }
                else
                {
                    client.Character.NoMove();
                }
            }
        }
        [MessageHandler]
        public static void HandleGameRolePlayPlayerFightFriendlyAnswer(GameRolePlayPlayerFightFriendlyAnswerMessage message, WorldClient client)
        {
            if (client.Character.IsInRequest() && client.Character.HasRequestBoxOpen<DualRequest>())
            {
                if (message.accept)
                {
                    client.Character.RequestBox.Accept();
                }
                else
                {
                    if (client.Character == client.Character.RequestBox.Target)
                    {
                        client.Character.RequestBox.Deny();
                    }
                    else
                    {
                        client.Character.RequestBox.Cancel();
                    }
                }
            }
        }
        [MessageHandler]
        public static void HandleGameRolePlayPlayerFightRequest(GameRolePlayPlayerFightRequestMessage message, WorldClient client)
        {
            Character target = client.Character.Map.Instance.GetEntity<Character>(message.targetId);

            if (target != null)
            {
                if (message.friendly)
                {
                    FighterRefusedReasonEnum fighterRefusedReasonEnum = client.Character.CanRequestFight(target);

                    if (fighterRefusedReasonEnum != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                    {
                        client.Send(new ChallengeFightJoinRefusedMessage(client.Character.Id, (byte)fighterRefusedReasonEnum));
                    }
                    else
                    {
                        target.OpenRequestBox(new DualRequest(client.Character, target));
                    }
                }
            }
        }
        [MessageHandler]
        public static void HandleChangeMap(ChangeMapMessage message, WorldClient client)
        {
            MapScrollEnum scrollType = MapScrollEnum.UNDEFINED;
            if (client.Character.Map.LeftMap == message.mapId)
                scrollType = MapScrollEnum.LEFT;
            if (client.Character.Map.RightMap == message.mapId)
                scrollType = MapScrollEnum.RIGHT;
            if (client.Character.Map.BottomMap == message.mapId)
                scrollType = MapScrollEnum.BOTTOM;
            if (client.Character.Map.TopMap == message.mapId)
                scrollType = MapScrollEnum.TOP;

            if (scrollType != MapScrollEnum.UNDEFINED)
            {
                int teleportMapId = MapsManager.Instance.GetNeighbourMapId(client.Character.Map, scrollType);

                MapPoint cellPoint = new MapPoint(MapsManager.Instance.GetNeighbourCellId(client.Character.Record.CellId, scrollType));

                client.Character.Record.Direction = MapsManager.Instance.GetScrollDirection(scrollType);

                MapRecord teleportedMap = MapRecord.GetMap(teleportMapId);

                if (teleportedMap != null)
                {
                    if (!cellPoint.IsInMap() || !teleportedMap.IsCellWalkable(cellPoint.CellId))
                    {
                        cellPoint = new MapPoint(MapsManager.Instance.FindNearMapBorder(teleportedMap, scrollType, cellPoint));
                    }

                    if (!cellPoint.IsInMap())
                    {
                        cellPoint = teleportedMap.RandomWalkableCell().Point;
                    }

                    client.Character.Teleport(teleportMapId, cellPoint.CellId);
                }
                else
                {
                    client.Character.ReplyError("Unknown map.");
                }
            }
            else
            {
                client.Character.ReplyError("Unknown map transition.");
            }
        }
    }
}

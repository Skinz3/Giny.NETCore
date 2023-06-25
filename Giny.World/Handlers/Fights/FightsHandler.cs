using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Giny.World.Managers.Fights;
using System.Threading.Tasks;
using Giny.World.Managers.Fights.Fighters;
using Giny.Protocol.Enums;
using Giny.World.Records.Spells;
using Giny.World.Managers.Spells;

namespace Giny.World.Handlers.Fights
{
    class FightsHandler
    {
        [MessageHandler]
        public static void HandleGameActionFightCastRequestMessage(GameActionFightCastOnTargetRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            Fighter target = client.Character.Fighter.Fight.GetFighter<Fighter>(x => x.Id == (long)message.targetId);

            if (target != null)
            {
                client.Character.Fighter.CastSpell(message.spellId, target.Cell.Id);
            }
        }
        [MessageHandler]
        public static void HandleGameActionFightCastRequestMessage(GameActionFightCastRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            client.Character.Fighter.CastSpell(message.spellId, message.cellId);
        }
        [MessageHandler]
        public static void HandleGameContextQuitMessage(GameContextQuitMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.Fighter.Leave(true);
        }
        [MessageHandler]
        public static void HandleGameFightPlacementPositionRequestMessage(GameFightPlacementPositionRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting && !client.Character.Fighter.Fight.Started && !client.Character.Fighter.IsReady && client.Character.Fighter.Fight.IsCellFree(client.Character.Map.GetCell(message.cellId)))
            {
                client.Character.Fighter.ModifyPlacement((short)message.cellId);
            }
        }
        [MessageHandler]
        public static void HandleGameFightPlacementSwapPositionsRequestMessage(GameFightPlacementSwapPositionsRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting && !client.Character.Fighter.IsReady && !client.Character.Fighter.Fight.Started)
            {
                var target = client.Character.Fighter.Fight.GetFighter<Fighter>(x => x.Id == message.requestedId);

                if (target.Cell.Id == message.cellId)
                {
                    client.Character.Fighter.SwapPlacementPosition(target);
                }
            }
        }

        [MessageHandler]
        public static void HandleGameFightJoinRequestMessage(GameFightJoinRequestMessage message, WorldClient client)
        {
            Fight fight = client.Character.Map.Instance.GetFight(message.fightId);

            if (fight != null && !client.Character.Fighting)
            {
                fight.Join(client.Character, message.fighterId);
            }
        }
        [MessageHandler]
        public static void HandleGameFightReady(GameFightReadyMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.Fighter.ToggleReady(message.isReady);
        }
        [MessageHandler]
        public static void HandleGameGameContextKickMessage(GameContextKickMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                Fighter target = client.Character.Fighter.Fight.GetFighter<Fighter>(x => x.Id == message.targetId);

                if (target != null)
                {
                    target.Kick(client.Character.Fighter);
                }
            }
        }
        [MessageHandler]
        public static void HandleShowCellRequestMessage(ShowCellRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                client.Character.Fighter.Team.ShowCell(client.Character.Fighter, message.cellId);
            }
        }
        [MessageHandler]
        public static void HandleFightOptionToggle(GameFightOptionToggleMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.Fighter.Team.Options.ToggleOption((FightOptionsEnum)message.option);
        }


        [MessageHandler]
        public static void HandleGameActionAcknowledgementMessage(GameActionAcknowledgementMessage message, WorldClient client)
        {
            if (message.valid && client.Character.Fighting && client.Character.Fighter.IsFighterTurn)
            {
                client.Character.Fighter.Fight.SequenceManager.AcknowledgeAction(client.Character.Fighter, message.actionId);
            }
        }
        [MessageHandler]
        public static void HandleGameFightTurnFinishMessage(GameFightTurnFinishMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
                return;

            client.Character.Fighter.PassTurn();
        }
        [MessageHandler]
        public static void HandleGameFightTurnReadyMessage(GameFightTurnReadyMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
                return;

            client.Character.Fighter.ToggleTurnReady(message.isReady);
        }
    }
}

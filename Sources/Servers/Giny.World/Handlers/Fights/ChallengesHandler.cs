using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Fights
{
    public class ChallengesHandler
    {
        [MessageHandler]
        public static void HandleChallengeBonusChoiceMessage(ChallengeBonusChoiceMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            client.Character.Fighter.ChallengeBonus = (ChallengeBonusEnum)message.challengeBonus;
            client.Send(new ChallengeBonusChoiceSelectedMessage(message.challengeBonus));
        }

        [MessageHandler]
        public static void HandleChallengeModSelectMessage(ChallengeModSelectMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }
            client.Character.Fighter.ChallengeMod = (ChallengeModEnum)message.challengeMod;

            client.Send(new ChallengeModSelectedMessage(message.challengeMod));
        }

        [MessageHandler]
        public static void HandleChallengeTargetsRequestMessage(ChallengeTargetsRequestMessage message, WorldClient client)
        {
            // client.Send(new ChallengeTargetsMessage(new ChallengeInformation(message.challengeId, new ChallengeTargetInformation[] { new ChallengeTargetInformation(-1, 255) },
            //   50, 20, 0)));
        }

        [MessageHandler]
        public static void HandleChallengeSelectionMessage(ChallengeSelectionMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            var fight = client.Character.Fighter.Fight as FightPvM;

            if (fight == null)
            {
                return;
            }



            client.Character.Fighter.Team.Send(new ChallengeSelectedMessage(
                new ChallengeInformation(message.challengeId, new ChallengeTargetInformation[0],
                50, 50, 3)));
        }

        [MessageHandler]
        public static void HandleChallengeValidateMessage(ChallengeValidateMessage message, WorldClient client)
        {

            if (!client.Character.Fighting)
            {
                return;
            }

            var fight = client.Character.Fighter.Fight as FightPvM;

            if (fight == null)
            {
                return;
            }


            fight.Challenges.ValidateChallenge(message.challengeId);



        }

        [MessageHandler]
        public static void HandleChallengeReadyMessage(ChallengeReadyMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            var fight = client.Character.Fighter.Fight as FightPvM;

            if (fight == null)
            {
                return;
            }

            client.Character.Fighter.ChallengeMod = (ChallengeModEnum)message.challengeMod;

            fight.Challenges.DisplayChallengeProposal();

        }
    }
}

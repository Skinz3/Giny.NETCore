using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Challenges;
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
            if (!client.Character.Fighting || client.Character.Fighter.Team.Leader != client.Character.Fighter.Team.Leader)
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

            if (client.Character.Fighter.Fight.Started)
            {
                return;
            }



            if (client.Character.Fighter.Fight is FightPvM fightPvM)
            {

                if (client.Character.Fighter.IsTeamLeader())
                {
                    fightPvM.Challenges.ChallengeMod = (ChallengeModEnum)message.challengeMod;
                }


                client.Character.Fighter.Team.Send(new ChallengeModSelectedMessage((byte)fightPvM.Challenges.ChallengeMod));
            }

        }

        [MessageHandler]
        public static void HandleChallengeTargetsRequestMessage(ChallengeTargetsRequestMessage message, WorldClient client)
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

            Challenge? challenge = fight.Challenges.GetActiveChallenge(message.challengeId);

            if (challenge == null)
            {
                return;
            }

            var targets = challenge.GetTargets();

            if (targets.Count() > 0)
            {
                client.Send(new ChallengeTargetsMessage(challenge.GetChallengeInformation()));
            }

        }

        [MessageHandler]
        public static void HandleChallengeSelectionMessage(ChallengeSelectionMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return;
            }

            if (client.Character.Fighter != client.Character.Fighter.Team.Leader)
            {
                return;
            }

            var fight = client.Character.Fighter.Fight as FightPvM;

            if (fight == null)
            {
                return;
            }

            var challenge = fight.Challenges.GetProposalChallenge(message.challengeId);

            if (challenge != null)
            {
                client.Character.Fighter.Team.Send(new ChallengeSelectedMessage(challenge.GetChallengeInformation()));
            }
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

            if (client.Character.Fighter != client.Character.Fighter.Team.Leader)
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



            if (client.Character.Fighter != client.Character.Fighter.Team.Leader)
            {
                return;
            }

            if (client.Character.Fighter.Fight is FightPvM fightPvM)
            {
                fightPvM.Challenges.ChallengeMod = (ChallengeModEnum)message.challengeMod;

                if (fightPvM.Challenges.ChallengeMod == ChallengeModEnum.CHALLENGE_CHOICE)
                {
                    fightPvM.Challenges.DisplayChallengeProposal();

                }
            }
        }
    }
}

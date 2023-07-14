using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
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
            client.Send(new ChallengeBonusChoiceSelectedMessage(30));
        }

        [MessageHandler]
        public static void HandleChallengeModSelectMessage(ChallengeModSelectMessage message, WorldClient client)
        {

        }

        [MessageHandler]
        public static void HandleChallengeTargetsRequestMessage(ChallengeTargetsRequestMessage message, WorldClient client)
        {
            var challenges = new ChallengeInformation[2];

            challenges[0] = new ChallengeInformation(1, new ChallengeTargetInformation[] { new ChallengeTargetInformation(-1, 255) },
                50, 50, 0);

            challenges[1] = new ChallengeInformation(3, new ChallengeTargetInformation[] { new ChallengeTargetInformation(-1, 255) },
              50, 50, 0);

            client.Send(new ChallengeProposalMessage(challenges, 30d));
        }
    }
}

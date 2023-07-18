using Giny.Core.Extensions;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    public class FightChallenges
    {
        /// <summary>
        /// in seconds
        /// </summary>
        public const short ChallengeSelectionTime = 5;
        public Fight Fight
        {
            get;
            private set;
        }
        private List<Challenge> ActiveChallenges
        {
            get;
            set;
        }

        private List<ChallengeProposal> ChallengeProposals
        {
            get;
            set;
        }


        private int ProposalIndex
        {
            get;
            set;
        }

        public FightChallenges(Fight fight)
        {
            this.Fight = fight;
            ActiveChallenges = new List<Challenge>();
            ProposalIndex = 0;


        }
        public FightTeam GetTeamChallenged()
        {
            return Fight.GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER);
        }


        private int GetChallengeCount()
        {
            return Fight.Map.IsDungeonMap ? 2 : 1;
        }

        public void DisplayChallengeProposal()
        {
            short added = (short)(ChallengeSelectionTime * ChallengeProposals.Count);

            Fight.AddPlacementDelay(added);

            List<ChallengeInformation> challengeInformations = new List<ChallengeInformation>();

            foreach (var proposal in ChallengeProposals)
            {
                challengeInformations.AddRange(proposal.GetChallengeInformations());
            }

            FightTeam targetTeam = GetTeamChallenged();

            targetTeam.Send(new ChallengeProposalMessage(ChallengeProposals[ProposalIndex].GetChallengeInformations().ToArray(), added));
        }


        private void DisplayChallengeNumber()
        {
            FightTeam targetTeam = GetTeamChallenged();
            targetTeam.Send(new ChallengeNumberMessage(ChallengeProposals.Count));
        }

        public void OnWinnersDetermined()
        {
            foreach (var challenge in ActiveChallenges)
            {
                challenge.OnWinnersDetermined();
            }
        }

        public double GetChallengesDropRatioBonus()
        {
            return ActiveChallenges.Where(x => x.Success).Sum(x => x.DropBonusRatio);
        }
        public double GetChallengesExpRatioBonus()
        {
            return ActiveChallenges.Where(x => x.Success).Sum(x => x.XpBonusRatio);
        }

        public void OnFightStart()
        {
            var leader = GetTeamChallenged().Leader as CharacterFighter;

            if (leader.ChallengeMod == ChallengeModEnum.CHALLENGE_RANDOM
                || ChallengeProposals.Any(x => x.Selected == null))
            {
                RandomizeChallenges();
            }
            ActiveChallenges = ChallengeProposals.Select(x => x.Selected).ToList();

            foreach (var challenge in ActiveChallenges)
            {
                challenge.BindEvents();
            }
        }
        public void OnPlacementStarted()
        {
            ChallengeProposals = ChallengesManager.Instance.CreateChallengeProposals(GetTeamChallenged(), GetChallengeCount());
            DisplayChallengeNumber();
        }

        public void ValidateChallenge(int challengeId)
        {
            ChallengeProposals[ProposalIndex].ValidateChallenge(challengeId);

            FightTeam targetTeam = GetTeamChallenged();
            targetTeam.Send(new ChallengeAddMessage(ChallengeProposals[ProposalIndex].Selected.GetChallengeInformation()));


            if (ProposalIndex < ChallengeProposals.Count - 1)
            {
                ProposalIndex++;
                DisplayChallengeProposal();
            }

        }

        public void RandomizeChallenges()
        {
            FightTeam targetTeam = GetTeamChallenged();

            Random random = new Random();

            foreach (var proposal in this.ChallengeProposals)
            {
                proposal.Selected = proposal.Challenges.Random(random);

                targetTeam.Send(new ChallengeAddMessage(proposal.Selected.GetChallengeInformation()));
            }
        }

        public Challenge? GetActiveChallenge(int id)
        {
            return ActiveChallenges.FirstOrDefault(x => x.Id == id);
        }

        public Challenge? GetProposalChallenge(int challengeId)
        {
            foreach (var proposal in ChallengeProposals)
            {
                var challenge = proposal.Challenges.FirstOrDefault(x => x.Id == challengeId);

                if (challenge != null)
                {
                    return challenge;
                }
            }
            return null;
        }
    }
}

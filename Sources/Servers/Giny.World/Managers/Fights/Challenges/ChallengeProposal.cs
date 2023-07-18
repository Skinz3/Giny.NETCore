using Giny.Protocol.Types;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    public class ChallengeProposal
    {
        public int Index
        {
            get;
            private set;
        }

        public List<Challenge> Challenges
        {
            get;
            set;
        } = new List<Challenge>();

        public Challenge Selected
        {
            get;
            set;
        }

        public bool Consistent => Challenges.Count == 2;


        public bool IsChallengeCompatible(ChallengeRecord record)
        {
            foreach (var challenge in Challenges)
            {
                if (challenge.Record == record || challenge.Record.IncompatiblesChallenges.Contains(record.Id))
                {
                    return false;
                }
            }

            return true;
        }

        public void ValidateChallenge(int challengeId)
        {
            Selected = Challenges.FirstOrDefault(x => x.Id == challengeId);
        }

        internal IEnumerable<ChallengeInformation> GetChallengeInformations()
        {
            return Challenges.Select(x => x.GetChallengeInformation());
        }

        public ChallengeProposal(int index)
        {
            Index = index;
        }
    }
}

using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Time;
using Giny.World.Modules;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    public class ChallengesManager : Singleton<ChallengesManager>
    {
        private Dictionary<ChallengeRecord, Type> m_challenges = new Dictionary<ChallengeRecord, Type>();

        [StartupInvoke("Challenges", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var type in AssemblyCore.GetTypes())
            {
                ChallengeAttribute attribute = type.GetCustomAttribute<ChallengeAttribute>();

                if (attribute != null)
                {
                    var challenge = ChallengeRecord.GetChallenge(attribute.Id);

                    if (challenge == null)
                    {
                        Logger.Write("Challenge " + attribute.Id + " have unknown challenge record. (skipped)", Channels.Warning);
                        continue;
                    }
                    this.m_challenges.Add(challenge, type);
                }
            }
        }


        public List<ChallengeProposal> CreateChallengeProposals(FightTeam team, int count)
        {
            Random random = new Random();

            List<Challenge> challenges = new List<Challenge>();

            foreach (var template in m_challenges)
            {
                Challenge challenge = (Challenge)Activator.CreateInstance(template.Value, new object[] { template.Key, team });

                if (challenge.IsValid())
                {
                    challenges.Add(challenge);
                }

            }


            List<ChallengeProposal> proposals = new List<ChallengeProposal>();

            for (int i = 0; i < count; i++)
            {
                proposals.Add(new ChallengeProposal(i));
            }


            foreach (var proposal in proposals)
            {

                foreach (var challenge in challenges.Shuffle())
                {
                    if (proposals.Where(x => x != proposal).All(x => x.IsChallengeCompatible(challenge.Record)))
                    {
                        challenge.Initialize();
                        proposal.Challenges.Add(challenge);
                    }

                    if (proposal.Consistent)
                    {
                        break;
                    }
                }

            }


            if (!proposals.All(x => x.Consistent))
            {
                Logger.Write("Unable to compute consistant challenge proposals...", Channels.Warning);
            }

            return proposals;
        }

    }

    public class ChallengeAttribute : Attribute
    {
        public int Id
        {
            get;
            private set;
        }

        public ChallengeAttribute(int id)
        {
            this.Id = id;
        }
    }
}
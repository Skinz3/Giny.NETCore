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
                    this.m_challenges.Add(ChallengeRecord.GetChallenge(attribute.Id), type);
                }
            }
        }
        public List<Challenge> CreateChallenges(FightTeam team, int count)
        {
            List<Challenge> results = new List<Challenge>();

            AsyncRandom random = new AsyncRandom();

            for (int i = 0; i < count; i++)
            {
                var pair = m_challenges.ToList().FindAll(x => CanAddChallenge(results, x.Key)).Random();

                if (pair.Value != null)
                {
                    Challenge challenge = (Challenge)Activator.CreateInstance(pair.Value, new object[] { pair.Key, team });

                    if (challenge.IsValid())
                    {
                        results.Add(challenge);
                    }
                    else
                        i--;
                }
            }
            return results;
        }
        private bool CanAddChallenge(List<Challenge> results, ChallengeRecord challenge)
        {
            return !results.Any(x => challenge.IncompatiblesChallenges.Contains((short)x.Id)) && !results.Any(x => x.Id == challenge.Id);
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

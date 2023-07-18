using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Challenges
{
    [Table("challenges")]
    [D2OClass("Challenge")]
    public class ChallengeRecord : ITable
    {
        [Container]
        private static Dictionary<long, ChallengeRecord> Challenges = new Dictionary<long, ChallengeRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("descriptionId")]
        public string Description
        {
            get;
            set;
        }
        [D2OField("incompatibleChallenges")]
        public List<long> IncompatiblesChallenges
        {
            get;
            set;
        }

        [D2OField("activationCriterion")]
        public string ActivationCriterion
        {
            get;
            set;
        }


        [D2OField("completionCriterion")]
        public string CompletionCriterion
        {
            get;
            set;
        }

        [D2OField("targetMonsterId")]
        public int TargetMonsterId
        {
            get;
            set;
        }

        [D2OField("categoryId")]
        public int CategoryId
        {
            get;
            set;
        }

        [Update]
        public long InitialSpellLevelId
        {
            get;
            set;
        }
        public static ChallengeRecord GetChallenge(int id)
        {
            Challenges.TryGetValue(id, out var challenge);
            return challenge;
        }
    }
}

using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    public abstract class Challenge
    {
        public abstract double XpBonusRatio
        {
            get;
        }

        public abstract double DropBonusRatio
        {
            get;
        }

        public short Id => (short)Record.Id;

        private ChallengeRecord Record
        {
            get;
            set;
        }

        protected List<Fighter> ConcernedFighters
        {
            get;
            private set;
        }

        protected Fight Fight => Team.Fight;

        protected FightTeam Team
        {
            get;
            set;
        }

        protected ChallengeResultEnum Result
        {
            get;
            set;
        }

        public bool Success => Result == ChallengeResultEnum.Success;

        public Challenge(ChallengeRecord record, FightTeam team)
        {
            this.Team = team;
            this.Record = record;
            this.Result = ChallengeResultEnum.Unknown;
            this.ConcernedFighters = GetConcernedFighters().ToList();
            this.BindEvents();
        }

        public abstract IEnumerable<Fighter> GetConcernedFighters();

        public abstract void BindEvents();

        public abstract void UnbindEvents();

        public void OnWinnersDetermined()
        {
            if (Result == ChallengeResultEnum.Unknown)
            {
                if (Team == Fight.Winners)
                    OnChallengeResulted(ChallengeResultEnum.Success);
                else
                    OnChallengeResulted(ChallengeResultEnum.Failed);
            }
        }

        protected virtual void OnChallengeResulted(ChallengeResultEnum result)
        {
            this.Result = result;
            Fight.Send(new ChallengeResultMessage()
            {
                challengeId = Id,
                success = Success,
            });
            UnbindEvents();
        }

        public virtual bool IsValid()
        {
            return true;
        }

        protected virtual void OnChallengeTargetUpdated()
        {
            Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
        }
        protected void OnTargetUpdated()
        {
            Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
        }

        public virtual long GetTargetId()
        {
            return 0;
        }
        public virtual short GetTargetedCellId()
        {
            return 0;
        }
    }
}

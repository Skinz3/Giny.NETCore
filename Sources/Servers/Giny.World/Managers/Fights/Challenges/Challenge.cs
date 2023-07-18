using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
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

        public ChallengeRecord Record
        {
            get;
            private set;
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

        protected ChallengeStateEnum State
        {
            get;
            set;
        }

        public bool Success => State == ChallengeStateEnum.CHALLENGE_COMPLETED;

        public Challenge(ChallengeRecord record, FightTeam team)
        {
            this.Team = team;
            this.Record = record;
            this.State = ChallengeStateEnum.CHALLENGE_RUNNING;

        }
        public virtual void Initialize()
        {
            this.ConcernedFighters = GetConcernedFighters().ToList();
        }

        public abstract IEnumerable<Fighter> GetConcernedFighters();

        public abstract void BindEvents();

        public abstract void UnbindEvents();

        public void OnWinnersDetermined()
        {
            if (State == ChallengeStateEnum.CHALLENGE_RUNNING)
            {
                if (Team == Fight.Winners)
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_COMPLETED);
                else
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
            }
        }

        protected virtual void OnChallengeResulted(ChallengeStateEnum result)
        {
            this.State = result;
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

        public ChallengeInformation GetChallengeInformation()
        {
            return new ChallengeInformation(Id, new ChallengeTargetInformation[0], (int)(DropBonusRatio * 100d),
                (int)(XpBonusRatio * 100d), (byte)State);
        }

        protected virtual void OnChallengeTargetUpdated()
        {
            // Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
        }
        protected void OnTargetUpdated()
        {
            //  Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
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

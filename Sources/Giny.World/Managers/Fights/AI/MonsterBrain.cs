using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class MonsterBrain
    {
        private AIFighter Fighter
        {
            get;
            set;
        }

        private SummonAction SummonAction
        {
            get;
            set;
        }
        private MarkAction MarkAction
        {
            get;
            set;
        }
        private BuffAction BuffAction
        {
            get;
            set;
        }
        private HealAction HealAction
        {
            get;
            set;
        }
        private MoveToTarget MoveToTarget
        {
            get;
            set;
        }
        private CastOnEnemyAction CastOnEnemyAction
        {
            get;
            set;
        }
        private FleeAction FleeAction
        {
            get;
            set;
        }
        public MonsterBrain(AIFighter fighter)
        {
            this.Fighter = fighter;
            this.SummonAction = new SummonAction(Fighter);
            this.MarkAction = new MarkAction(Fighter);
            this.BuffAction = new BuffAction(Fighter);
            this.HealAction = new HealAction(Fighter);
            this.MoveToTarget = new MoveToTarget(Fighter);
            this.CastOnEnemyAction = new CastOnEnemyAction(Fighter);
            this.FleeAction = new FleeAction(Fighter);
        }


        public void Play()
        {
            SummonAction.Execute();
            MarkAction.Execute();
            BuffAction.Execute();
            HealAction.Execute();
            CastOnEnemyAction.Execute();
            MoveToTarget.Execute();
            BuffAction.Execute();
            FleeAction.Execute();
        }
    }
}
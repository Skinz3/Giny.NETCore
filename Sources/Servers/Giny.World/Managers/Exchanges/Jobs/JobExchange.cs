using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Items.Collections;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges.Jobs
{
    public abstract class JobExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.CRAFT;

        private const int CountDefault = 1;

        public const int CountLimit = 5000;

        protected int Count
        {
            get;
            set;
        }
        protected SkillRecord Skill
        {
            get;
            private set;
        }
        protected CharacterJob CharacterJob
        {
            get;
            private set;
        }
        protected JobItemCollection Items
        {
            get;
            private set;
        }

        public JobExchange(Character character, SkillRecord skill) : base(character)
        {
            this.Skill = skill;

            if (skill != null)
                this.CharacterJob = character.GetJob(skill.ParentJobId);
            this.Items = new JobItemCollection(character);
            this.Count = CountDefault;
        }
        public abstract void SetCount(int count);

        protected void ResetCount()
        {
            this.Count = 1;
            Character.Client.Send(new ExchangeCraftCountModifiedMessage(Count));
        }
        
        public override void MoveItem(int uid, int quantity)
        {
            Items.MoveItem(uid, quantity);
        }
        public override void Open()
        {
            Character.Client.Send(new ExchangeStartOkCraftWithInformationMessage()
            {
                skillId = (short)Skill.Id,
            });
        }
        public override void MoveItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }

        public override void MoveKamas(long quantity)
        {
            throw new NotImplementedException();
        }

        public override void OnNpcGenericAction(NpcActionsEnum action)
        {
            throw new NotImplementedException();
        }
        public override void ModifyItemPriced(int objectUID, int quantity, long price)
        {
            throw new NotImplementedException();
        }
    }
}

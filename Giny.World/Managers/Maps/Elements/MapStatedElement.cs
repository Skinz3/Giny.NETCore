using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Giny.Core.Time;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Managers.Skills;
using Giny.World.Records.Characters;
using Giny.World.Records.Maps;
using Timer = System.Timers.Timer;

namespace Giny.World.Managers.Maps.Elements
{
    public class MapStatedElement : MapInteractiveElement
    {
        /// <summary>
        /// Exprimé en secondes.
        /// </summary>
        public const int GrowInterval = 120;

        /// <summary>
        /// Représente le timer d'utilisation de l'élement.
        /// </summary>
        private Timer OnUsedCallback;

        /// <summary>
        /// Représente le timer de repousse de l'élement.
        /// </summary>
        private ActionTimer GrowCallback;

        /// <summary>
        /// BonesId de l'élement lié au skill.
        /// </summary>
        public int BonesId
        {
            get
            {
                return Record.BonesId;
            }
        }
        /// <summary>
        /// Personnage collecteur.
        /// </summary>
        private Character Character
        {
            get;
            set;
        }
        /// <summary>
        /// Skill utilisé.
        /// </summary>
        protected InteractiveSkillRecord Skill
        {
            get
            {
                return Record.Skill;
            }
        }
        /// <summary>
        /// Etat actuel de l'élement.
        /// </summary>
        private StatedElementState State
        {
            get;
            set;
        }

        public MapStatedElement(MapInstance mapInstance, InteractiveElementRecord record) : base(mapInstance, record)
        {
        }
        public StatedElement GetStatedElement()
        {
            return new StatedElement()
            {
                elementCellId = Record.CellId,
                elementId = Record.Identifier,
                elementState = (int)State,
                onCurrentMap = true,
            };
        }
        public void Use(Character character)
        {
            this.Character = character;

            CharacterJob job = Character.GetJob(Skill.Record.ParentJobId);

            if (job != null && job.Level < Skill.Record.MinLevel)
            {
                return;
            }

            if (State == StatedElementState.Active)
            {
                this.Character.Collecting = true;
                this.UpdateState(StatedElementState.Used);
                this.GrowCallback = new ActionTimer(GrowInterval * 1000, Grow, false);
                OnUsedCallback = new Timer(SkillsManager.SKILL_DURATION * 100);
                OnUsedCallback.Elapsed += OnUsedCallback_Elapsed;
                OnUsedCallback.Start();
            }
        }

        private void OnUsedCallback_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnUsedCallback.Dispose();
            Character.Client.Send(new InteractiveUseEndedMessage(Record.Identifier, (short)Skill.Id));
            this.Collect();
            UpdateState(StatedElementState.Unactive);
            GrowCallback.Start();
        }
        public void Collect()
        {
            if (State == StatedElementState.Used)
            {
                CharacterJob job = Character.GetJob(Skill.Record.ParentJobId);

                int quantity = JobFormulas.Instance.GetCollectedItemQuantity(job != null ? job.Level : 1, Skill.Record);

                this.Character.Inventory.AddItem(Skill.Record.GatheredRessourceItem, quantity);
                this.Character.Client.Send(new ObtainedItemMessage(Skill.Record.GatheredRessourceItem, quantity));

                if (job != null)
                {
                    this.Character.AddJobExp(Skill.Record.ParentJobId, (long)((5 * Skill.Record.MinLevel) * ConfigFile.Instance.JobRate));
                }
                this.Character.Collecting = false;
                this.Character = null;
            }
        }
        private void UpdateState(StatedElementState state)
        {
            this.State = state;
            MapInstance.Send(new StatedElementUpdatedMessage(GetStatedElement()));
        }
        public void Grow()
        {
            if (State == StatedElementState.Unactive)
            {
                UpdateState(StatedElementState.Active);
            }
        }
        public override bool CanUse(Character character)
        {
            return base.CanUse(character) && State == StatedElementState.Active;
        }
    }
}

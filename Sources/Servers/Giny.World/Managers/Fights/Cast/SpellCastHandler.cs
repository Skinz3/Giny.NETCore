using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public abstract class SpellCastHandler
    {
        public SpellCast Cast
        {
            get;
            private set;
        }

        protected SpellEffectHandler[] Handlers
        {
            get;
            set;
        }

        private List<Telefrag> GeneratedTelefrags
        {
            get;
            set;
        }

        public bool IsTelefraged(Fighter actor)
        {
            return GeneratedTelefrags.Any(x => x.Source == actor || x.Target == actor);
        }

        public IEnumerable<SpellEffectHandler> GetEffectHandlers()
        {
            return Handlers;
        }

        public void AddTelefrag(Telefrag telefrag)
        {
            GeneratedTelefrags.Add(telefrag);
        }
        public bool Initialized => m_initialized;

        protected bool m_initialized = false;

        public SpellCastHandler(SpellCast cast)
        {
            this.Cast = cast;
            this.GeneratedTelefrags = new List<Telefrag>();
        }

        public abstract bool Initialize();

        public abstract bool Execute();

        public bool RevealsInvisible()
        {
            return Handlers.Any(x => x.RevealsInvisible());
        }
    }
}

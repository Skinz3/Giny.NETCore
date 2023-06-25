using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Effects;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public class DefaultSpellCastHandler : SpellCastHandler
    {
        public DefaultSpellCastHandler(SpellCast cast) : base(cast)
        {

        }
        public override bool Initialize()
        {
            var random = new AsyncRandom();

            var effects = Cast.IsCriticalHit ? Cast.Spell.Level.CriticalEffects : Cast.Spell.Level.Effects;

            var handlers = new List<SpellEffectHandler>();

            var groups = effects.GroupBy(x => x.Group);
            double totalRandSum = effects.Sum(entry => entry.Random);
            var randGroup = random.NextDouble();
            var stopRandGroup = false;

            foreach (var groupEffects in groups)
            {
                double randSum = groupEffects.Sum(entry => entry.Random);

                if (randSum > 0)
                {
                    if (stopRandGroup)
                        continue;

                    if (randGroup > randSum / totalRandSum)
                    {
                        randGroup -= randSum / totalRandSum;
                        continue;
                    }

                    stopRandGroup = true;
                }

                var randEffect = random.NextDouble();
                var stopRandEffect = false;

                foreach (var effect in groupEffects)
                {
                    if (groups.Count() <= 1)
                    {
                        if (effect.Random > 0)
                        {
                            if (stopRandEffect)
                                continue;

                            if (randEffect > effect.Random / randSum)
                            {
                                randEffect -= effect.Random / randSum;
                                continue;
                            }

                            stopRandEffect = true;
                        }
                    }

                    var handler = SpellEffectManager.Instance.GetSpellEffectHandler(effect, this);

                    if (handler != null)
                    {
                        if (!handler.CanApply())
                        {
                            return false;
                        }

                        handlers.Add(handler);
                    }
                    else
                    {
                        Cast.Source.Fight.Warn("Unknown effect handler " + effect.EffectEnum);
                    }
                }
            }


            Handlers = handlers.ToArray();
            m_initialized = true;

            return true;
        }
        /*
         * 'overriddenTargets' parameter is specified in particular cases !
         *  (Glyph triggers for exemple)
         *  You can trust Effect.TargetMask ! 
         */
        public override bool Execute()
        {
            if (!m_initialized)
            {
                if (!Initialize())
                {
                    return false;
                }
            }


            SpellEffectHandler[] handlers = OrderHandlers().ToArray();

            foreach (var handler in handlers)
            {
                try
                {
                    handler.SetTriggerToken(Cast.Token);
                    handler.Execute();
                }
                catch (Exception ex)
                {
                    Cast.Source.Fight.Warn("Unable to cast effect " + handler.Effect.EffectEnum);
                    Logger.Write(ex, Channels.Critical);
                }
            }

            return true;
        }

        protected virtual IEnumerable<SpellEffectHandler> OrderHandlers()
        {
            return Handlers;
        }
        protected IEnumerable<SpellEffectHandler> OrderByEffects(params EffectsEnum[] effects)
        {
            return Handlers.OrderBy(x => Array.IndexOf(effects, x.Effect.EffectEnum));
        }

    }
}

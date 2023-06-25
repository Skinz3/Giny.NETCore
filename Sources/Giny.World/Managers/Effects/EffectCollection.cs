using Giny.Core.Time;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Records.Items;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects
{
    [ProtoContract]
    public class EffectCollection : IEnumerable<Effect>
    {
        private static EffectsEnum[] DiceEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_CastSpell_1175,
            EffectsEnum.Effect_DamageNeutral,
            EffectsEnum.Effect_DamageFire,
            EffectsEnum.Effect_DamageAir,
            EffectsEnum.Effect_DamageEarth,
            EffectsEnum.Effect_DamageWater,
            EffectsEnum.Effect_StealHPWater,
            EffectsEnum.Effect_StealHPEarth,
            EffectsEnum.Effect_StealHPAir,
            EffectsEnum.Effect_StealHPFire,
            EffectsEnum.Effect_StealHPNeutral,
            EffectsEnum.Effect_RemoveAP,
            EffectsEnum.Effect_RemainingFights,
            EffectsEnum.Effect_StealKamas,
            EffectsEnum.Effect_HealHP_108,
        };

        [ProtoMember(1)]
        private List<Effect> Effects
        {
            get;
            set;
        }

        public bool IsAssociated => Exists(EffectsEnum.Effect_LivingObjectId) || Exists(EffectsEnum.Effect_Apparence_Wrapper) || Exists(EffectsEnum.Effect_Appearance);

        public EffectCollection()
        {
            this.Effects = new List<Effect>();
        }
        public EffectCollection(IEnumerable<Effect> effects)
        {
            this.Effects = effects.ToList();
        }

        public EffectCollection Generate(bool perfect = false)
        {
            EffectCollection result = new EffectCollection();

            AsyncRandom random = new AsyncRandom();

            foreach (var effect in this.OfType<EffectDice>())
            {
                if (DiceEffects.Contains(effect.EffectEnum))
                {
                    result.Add((EffectDice)effect.Clone());
                }
                else
                {
                    result.Add(effect.Generate(random, perfect));
                }
            }

            return result;
        }

        public ObjectEffect[] GetObjectEffects()
        {
            ObjectEffect[] effects = new ObjectEffect[Effects.Count];
            for (int i = 0; i < Effects.Count; i++)
            {
                effects[i] = Effects[i].GetObjectEffect();
            }
            return effects;
        }
        public T GetFirst<T>() where T : Effect
        {
            return Effects.OfType<T>().FirstOrDefault();
        }
        public T Get<T>(Predicate<Effect> predicate) where T : Effect
        {
            return (T)Effects.Find(predicate);
        }
        public T GetFirst<T>(EffectsEnum effectEnum) where T : Effect
        {
            return (T)Effects.FirstOrDefault(x => x.EffectEnum == effectEnum);
        }
        public IEnumerable<T> OfType<T>() where T : Effect
        {
            return Effects.OfType<T>();
        }
        public void Add(Effect effect)
        {
            Effects.Add(effect);
        }
        public bool Exists(EffectsEnum effect)
        {
            return Effects.Any(x => x.EffectEnum == effect);
        }
        public bool Exists<T>() where T : Effect
        {
            return Effects.OfType<T>().Count() > 0;
        }
        public void Clear()
        {
            Effects.Clear();
        }
        public void Remove(Effect effect)
        {
            Effects.Remove(effect);
        }
        public void RemoveAll(EffectsEnum effectsEnum)
        {
            Effects.RemoveAll(x => x.EffectEnum == effectsEnum);
        }

        public EffectCollection Clone()
        {
            EffectCollection result = new EffectCollection();

            foreach (var effect in Effects)
            {
                result.Add((Effect)effect.Clone());
            }
            return result;
        }

        public bool SequenceEqual(EffectCollection effects)
        {
            return Effects.SequenceEqual(effects);
        }

        public IEnumerator<Effect> GetEnumerator()
        {
            return Effects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Effects.GetEnumerator();
        }

        public Effect this[int i]
        {
            get => this.Effects[i];
            set => this.Effects[i] = value;
        }


    }
}

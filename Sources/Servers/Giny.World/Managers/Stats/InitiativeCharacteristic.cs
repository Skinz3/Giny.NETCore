using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public class InitiativeCharacteristic : DetailedCharacteristic
    {
        private EntityStats Stats
        {
            get;
            set;
        }


        public InitiativeCharacteristic()
        {

        }

        public static new InitiativeCharacteristic New(int @base)
        {
            throw new InvalidOperationException("Cannot create initiative characteristic from base.");
        }

        public static InitiativeCharacteristic Zero()
        {
            return new InitiativeCharacteristic();
        }

        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicDetailed(Base + GetNaturalInitiative(), Additional, Objects, 0, Context, (short)characteristic);
        }

        private int GetNaturalInitiative()
        {
            var totalContext = Total() + Context;

            if (ContextualLimit && Limit.HasValue)
            {
                totalContext = totalContext > Limit.Value ? Limit.Value : totalContext;
            }

            var diff = totalContext - (Base + Additional + Objects + Context);

            return diff;
        }

        private int GetTotalInitiative()
        {
            var initiative = Base + Additional + Objects;
            double num1 = Stats.Total() + initiative;
            double num2 = Stats.LifePoints / (double)Stats.MaxLifePoints;
            double value = num1 * num2;
            value = value > 0 ? value : 0;
            return (int)value;
        }
        public override Characteristic Clone()
        {
            return new InitiativeCharacteristic()
            {
                Stats = Stats,
                Additional = Additional,
                Base = Base,
                Objects = Objects,
            };
        }
        public void Bind(EntityStats stats)
        {
            this.Stats = stats;
        }
        public override int Total()
        {
            var total = GetTotalInitiative();

            if (!Limit.HasValue)
            {
                return total;
            }
            return total > Limit.Value ? Limit.Value : total;
        }
    }
}

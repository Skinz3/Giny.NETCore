
using Giny.Core;
using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Records.Challenges;
using Giny.World.Records.Effects;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Challenges
{
    public class ChallengeSpells
    {
        private static SpellLevelRecord FindTreeSpellLevel(List<SpellLevelRecord> childs)
        {
            foreach (var child in childs)
            {
                var parents = SpellLevelRecord.GetLevelsCastingSpell(child.SpellId, child.Grade);

                if (parents.Count == 0)
                {
                    return childs.First();
                }

                else
                {
                    return FindTreeSpellLevel(parents);
                }

            }

            return null;
        }
        public static void Patch()
        {
            Logger.Write("Patching challenges spells...");

            foreach (var level in SpellLevelRecord.GetSpellLevels())
            {
                foreach (var effect in level.Effects.OfType<EffectDice>())
                {
                    if (effect.EffectEnum == EffectsEnum.Effect_ValidateChallenge || effect.EffectEnum == EffectsEnum.Effect_InvalidateChallenge)
                    {
                        var challengeRecord = ChallengeRecord.GetChallenge(effect.Value);

                        if (challengeRecord == null)
                        {
                            Logger.Write("Unable to find challenge " + effect.Value, Channels.Warning);
                            continue;
                        }

                        if (challengeRecord.InitialSpellLevelId != 0)
                        {
                            continue;
                        }

                        var initialLevel = FindTreeSpellLevel(new List<SpellLevelRecord>() { level });

                        challengeRecord.InitialSpellLevelId = initialLevel.Id;
                        challengeRecord.UpdateInstantElement();

                        Logger.Write($"Find initial spell level for challenge '{challengeRecord.Name}'");

                    }
                }
            }
        }
    }
}

using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    public class MarksManager : Singleton<MarksManager>
    {
        public Color GetMarkColor(SpellEnum spellId)
        {
            switch (spellId)
            {
                case SpellEnum.REPULSION_GLYPH:
                    return Color.FromArgb(102, 0, 102);
                /*
                 * Mot marquant 
                 * Glyphe Lapino
                 * Marque de Régénération
                 */

                case SpellEnum.STRIKING_GLYPH:
                case SpellEnum.FRIENDSHIP_WORD13197:
                case SpellEnum.REGENERATING_MARK:
                    return Color.DeepPink;

                case SpellEnum.CAWWOT:
                    return Color.White;
                /* Piège Répulsif
                 * Piège de Dérive
                 * Fosse commune */
                case SpellEnum.MASS_GRAVE14314:
                case SpellEnum.REPELLING_TRAP12914:
                case SpellEnum.DRIFT_TRAP:
                    return Color.FromArgb(10849205);

                /* Piège a Fragmentation
                 * Piège Sournois */
                case SpellEnum.FRAGMENTATION_TRAP:
                case SpellEnum.TRICKY_TRAP12906:
                    return Color.FromArgb(12128795);
                /*
                 * Piège de masse
                 * Piège Scélérat */
                case SpellEnum.MASS_TRAP12920:
                case SpellEnum.SICKRAT_TRAP: // pas sur id
                    return Color.FromArgb(5911580);

                /*
                 * Piège Fangeux
                 * Calamité */
                case SpellEnum.MIRY_TRAP:
                case SpellEnum.CALAMITY12950:
                    return Color.FromArgb(4228004);
                /*
                 * Piège Mortel
                 * Piège Funeste */
                case SpellEnum.LETHAL_TRAP12921:
                case SpellEnum.MALEVOLENT_TRAP:
                    return Color.FromArgb(0);

                case SpellEnum.MIST12930:
                    return Color.FromArgb(4149784);
                /*
                 * Piège Insidieux
                 * Piège Insidieux (Glyphe)
                 */
                case SpellEnum.INSIDIOUS_TRAP12926:
                case SpellEnum.INSIDIOUS_TRAP:
                    return Color.FromArgb(3222918);

                /*
                 * Piège d'Immobilisation
                 */
                case SpellEnum.PARALYSING_TRAP12910:
                    return Color.FromArgb(2258204);

                /*
                 * Runes Huppermage 
                 */
                case SpellEnum.EARTH_RUNE: // a revoir, pas sur id
                    return Color.Brown;
                case SpellEnum.FIRE_RUNE13687:
                    return Color.Red;
                case SpellEnum.WATER_RUNE: // a revoir, pas sur id
                    return Color.Blue;
                case SpellEnum.AIR_RUNE: // a revoir, pas sur id
                    return Color.Green;


                /*
                * Glyphes Féca
                */
                case SpellEnum.AGGRESSIVE_GLYPH12992:
                    return Color.LimeGreen;
                case SpellEnum.BURNING_GLYPH12985:
                    return Color.OrangeRed;
                case SpellEnum.PARALYSING_GLYPH12990:
                    return Color.CornflowerBlue;
                case SpellEnum.BLINDING_GLYPH: // pas sur id
                    return Color.SaddleBrown;


                case SpellEnum.LOAD_HEADICE: // glyphe korriandre
                    return Color.White;
            }

            return Color.FromArgb(0);
        }
    }
}

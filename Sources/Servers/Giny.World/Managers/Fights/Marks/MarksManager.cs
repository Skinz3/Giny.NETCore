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
        public Color GetMarkColor(short spellId)
        {
            switch (spellId)
            {
                case 24391: // Étendard (Forgelance)
                    return Color.FromArgb(5718180);


                case 12988: // Glyphe de répulsion
                    return Color.FromArgb(102, 0, 102);


                case 13221: // Mot marquant 
                case 13197: // Glyphe Lapino
                case 13211: // Marque de Régénération
                    return Color.DeepPink;

                case 367: // Cawote
                    return Color.White;

                case 14314: // Piège Répulsif
                case 12914: // Piège de Dérive
                case 12942: // Fosse commune
                    return Color.FromArgb(10849205);


                case 12941: // Piège a Fragmentation
                case 12906: // Piège Sournois
                    return Color.FromArgb(12128795);


                case 12920: // Piège de masse
                case 12931: // Piège Scélérat
                    return Color.FromArgb(5911580);


                case 12916: // Piège Fangeux
                case 12950: // Calamité
                    return Color.FromArgb(4228004);

                case 12921: // Piège Mortel
                case 12948: // Piège Funeste
                    return Color.FromArgb(0);

                case 12930: // Brume
                    return Color.FromArgb(4149784);

                case 12926: // Piège Insidieux
                case 12918: // Piège Insidieux (Glyphe)
                    return Color.FromArgb(3222918);

                case 12910: // Piège d'Immobilisation
                    return Color.FromArgb(2258204);


                case 6191:  // Rune terre
                    return Color.Brown;
                case 13687: // Rune feu
                    return Color.Red;
                case 5837:  // Rune eau
                    return Color.Blue;
                case 6190: //  Rune air
                    return Color.Green;


                case 12992: // Glyphe agressif
                    return Color.LimeGreen;
                case 12985: // Glyphe Enflammé
                    return Color.OrangeRed;
                case 12990: // Glyphe Optique
                    return Color.CornflowerBlue;
                case 12987: // Glyphe d'Aveuglement
                    return Color.SaddleBrown;


                case 2699: // Glyphe korriandre
                    return Color.White;
            }

            return Color.FromArgb(0);
        }
    }
}

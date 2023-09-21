using Giny.Core;
using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.World.Records.Breeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Breeds
{
    public class BreedGraves
    {
        private static Dictionary<BreedEnum, short> GraveBones = new Dictionary<BreedEnum, short>()
        {
            { BreedEnum.Feca,2384 },
            { BreedEnum.Osamodas,2380 },
            { BreedEnum.Enutrof,2373 },
            { BreedEnum.Sram,2376 },
            { BreedEnum.Xelor,2386 },
            { BreedEnum.Ecaflip,2378 },
            { BreedEnum.Eniripsa,2383 },
            { BreedEnum.Iop,2374 },
            { BreedEnum.Cra,2372 },
            { BreedEnum.Sadida,2381 },
            { BreedEnum.Sacrieur,2379 },
            { BreedEnum.Pandawa , 2375 },
            { BreedEnum.Roublard ,2382 },
            { BreedEnum.Zobal , 2377 },
            { BreedEnum.Steamer, 2385 },
            { BreedEnum.Eliotrope, 3091 },
            { BreedEnum.Huppermage, 3376 },
            { BreedEnum.Ouginak, 3940 },
            { BreedEnum.Forgelance, 7254 },
        };

        public static void Patch()
        {
            Logger.Write("Patching breed graves...");

            foreach (var breed in BreedRecord.GetBreeds())
            {
                if (!GraveBones.ContainsKey(breed.BreedEnum))
                {
                    Logger.Write("Unable to find grave bones for breed " + breed.BreedEnum, Channels.Warning);
                    continue;
                }

                breed.GraveBonesId = GraveBones[breed.BreedEnum];
                breed.UpdateNow();
            }
        }
    }
}

using Giny.IO.D2OClasses;
using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public class InitialSpellHandler
    {
        /// <summary>
        /// Le client ne contenant pas les informations sur les sorts intiaux des classes
        /// obligé de distinguer les cas ici.
        /// </summary>
        public static void Execute(CharacterFighter fighter)
        {
            /*
             * Maîtrise des invocations
             */
            if (fighter.Character.HasSpell(18646))
            {
                fighter.ExecuteSpell(21976, 1, fighter.Cell);
            }
            switch (fighter.Breed)
            {
                case BreedEnum.Ouginak:
                    Ouginak(fighter);
                    break;

                case BreedEnum.Roublard:
                    Roublard(fighter);
                    break;

                case BreedEnum.Ecaflip:
                    Ecaflip(fighter);

                    break;
                case BreedEnum.Huppermage:
                    Huppermage(fighter);

                    break;
                case BreedEnum.Pandawa:
                    Pandawa(fighter);

                    break;
                case BreedEnum.Sram:
                    Sram(fighter);

                    break;
                case BreedEnum.Iop:
                    Iop(fighter);
                    break;

                case BreedEnum.Cra:
                    Cra(fighter);
                    break;

                case BreedEnum.Zobal:
                    Zobal(fighter);
                    break;

                case BreedEnum.Osamodas:
                    Osamodas(fighter);
                    break;

                case BreedEnum.Sadida:
                    Sadida(fighter);
                    break;

                case BreedEnum.Sacrieur:
                    Sacrieur(fighter);
                    break;

                case BreedEnum.Forgelance:
                    Forgelance(fighter);
                    break;

                case BreedEnum.Eniripsa:
                    Eniripsa(fighter);
                    break;

                case BreedEnum.Enutrof:
                    Enutrof(fighter);
                    break;

                case BreedEnum.Eliotrope:
                    Eliotrope(fighter);
                    break;
            }
        }

        private static void Eliotrope(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(14631, 1, fighter.Cell);


            CharacterSpell treeSpell = fighter.Character.GetSpellByBase(14574)!;

            /*
             * Portail flexible
             */
            if (!treeSpell.Variant)
            {
                fighter.ExecuteSpell(24955, treeSpell.GetGrade(fighter.Character), fighter.Cell);
            }
            else
            {
                fighter.ExecuteSpell(24956, treeSpell.GetGrade(fighter.Character), fighter.Cell);
            }

        }
        private static void Enutrof(CharacterFighter fighter)
        {
            /*
             * Tourbière
             */
            if (fighter.HasSpell(13358))
            {
                fighter.ExecuteSpell(23603, 1, fighter.Cell);
            }

            /* 
             * Les doigts d'énutrof
             */

            fighter.ExecuteSpell(14330, 1, fighter.Cell);
        }
        private static void Ouginak(CharacterFighter fighter)
        {
            /*
             * Cerbère
             */
            if (fighter.HasSpell(13758))
            {
                fighter.ExecuteSpell(25726, 1, fighter.Cell);
            }
            //fighter.ExecuteSpell(13745, 1, fighter.Cell);
        }
        private static void Roublard(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(20488, 1, fighter.Cell);
        }
        private static void Ecaflip(CharacterFighter fighter)
        {
            /*
             * Mésaventure
             */
            if (fighter.Character.HasSpell(12879))
            {
                fighter.ExecuteSpell(17019, 1, fighter.Cell);
            }

            /*
             * Tromperie
             */
            if (fighter.Character.HasSpell(12881))
            {
                fighter.ExecuteSpell(17020, 1, fighter.Cell);
            }
        }
        private static void Huppermage(CharacterFighter fighter)
        {
            /*
             * Torrent Arcanique
             */
            if (fighter.Character.HasSpell(14342))
            {
                fighter.ExecuteSpell(23960, 1, fighter.Cell);
            }

            /* 
             * Création
             */
            if (fighter.Character.HasSpell(13727))
            {
                fighter.ExecuteSpell(23959, 1, fighter.Cell);
            }


            /* 
             * La rune de l'Huppermage
             */
            fighter.ExecuteSpell(21985, 1, fighter.Cell);
        }
        private static void Pandawa(CharacterFighter fighter)
        {
            /*
             * Chopine de pandawa
             */
            fighter.ExecuteSpell(12830, 1, fighter.Cell);
        }
        private static void Iop(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(21981, 1, fighter.Cell);
        }
        private static void Cra(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(21977, 1, fighter.Cell);
        }
        private static void Osamodas(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(13991, 1, fighter.Cell);
        }
        private static void Zobal(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(18633, 1, fighter.Cell);
        }
        private static void Sacrieur(CharacterFighter fighter)
        {
            /*
             * Transposition
             */
            if (fighter.HasSpell(12736))
            {
                fighter.ExecuteSpell(25188, 1, fighter.Cell);
            }

            /*
             * Attirance
             */

            if (fighter.HasSpell(12735))
            {
                fighter.ExecuteSpell(25187, 1, fighter.Cell);
            }

            fighter.ExecuteSpell(12718, 1, fighter.Cell);
        }

        private static void Forgelance(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(24387, 1, fighter.Cell);
        }
        private static void Eniripsa(CharacterFighter fighter)
        {
            fighter.ExecuteSpell(25810, 1, fighter.Cell);
        }

        private static void Sram(CharacterFighter fighter)
        {
            /*
            * Chausse-trappe
            */
            if (fighter.Character.HasSpell(12932))
            {
                fighter.ExecuteSpell(25104, 1, fighter.Cell);
            }

            /*
            * Perfidie
            */
            if (fighter.Character.HasSpell(12949))
            {
                fighter.ExecuteSpell(25103, 1, fighter.Cell);
            }


            /*
             * Injection toxique
             */
            if (fighter.Character.HasSpell(12940))
            {
                fighter.ExecuteSpell(25105, 1, fighter.Cell);
            }

            /*
             * Marque mortuaire
             */
            if (fighter.Character.HasSpell(14313))
            {
                fighter.ExecuteSpell(25106, 1, fighter.Cell);
            }


            /*
            * Comploteur
            */
            if (fighter.Character.HasSpell(12936))
            {
                fighter.ExecuteSpell(25102, 1, fighter.Cell);
            }


            /* L'ombre de sram */
            fighter.ExecuteSpell(21978, 1, fighter.Cell);
        }

        private static void Sadida(CharacterFighter fighter)
        {
            /*
             * Arbre ou Arbre Feuillu
             */
            CharacterSpell treeSpell = fighter.Character.GetSpellByBase(13519)!;

            if (!treeSpell.Variant)
            {
                fighter.ExecuteSpell(24418, treeSpell.GetGrade(fighter.Character), fighter.Cell);
            }
            else
            {
                fighter.ExecuteSpell(24417, treeSpell.GetGrade(fighter.Character), fighter.Cell);
            }

            /*
             * Folle transmutée
             */
            if (fighter.Character.HasSpell(13515))
            {
                fighter.ExecuteSpell(25510, 1, fighter.Cell);
            }


            /*
             * Bloqueuse transmutée
             */
            if (fighter.Character.HasSpell(13526))
            {
                fighter.ExecuteSpell(25511, 1, fighter.Cell);
            }

            /*
            * Sacrifiée transmutée
            */
            if (fighter.Character.HasSpell(13522))
            {
                fighter.ExecuteSpell(25512, 1, fighter.Cell);
            }

            /*
           * Gonflable transmutée
           */
            if (fighter.Character.HasSpell(13523))
            {
                fighter.ExecuteSpell(25513, 1, fighter.Cell);
            }

            /*
           * Sacrifiée transmutée
           */
            if (fighter.Character.HasSpell(13520))
            {
                fighter.ExecuteSpell(25514, 1, fighter.Cell);
            }


            /*
             * Soulier du sadida
             */
            fighter.ExecuteSpell(14377, 1, fighter.Cell);

        }
    }
}

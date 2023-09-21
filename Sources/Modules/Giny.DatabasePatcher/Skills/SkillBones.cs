using Giny.ORM;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Skills
{
    class SkillBones
    {
        private static Dictionary<long, int[]> SkillsBonesIds = new Dictionary<long, int[]>()
        {
            {45 , new int[]{ 660 }}, //  Blé
            {46,  new int[] {661 }}, // Houblon
            { 296, new int[] { 660 }}, // Blé
            { 50,  new int[] { 662 }}, // Lin
            { 300, new int[] { 662 }}, // Lin
            { 54,  new int[] { 663 }}, // Chanvre
            { 37,  new int[] { 654 }}, // Erable
            { 102, new int[] { 4938,224 }}, // Eau potable
            { 6,   new int[] { 650,3715 }}, // Frene
            { 57,  new int[] { 701 }}, // Frène
            { 53,  new int[] { 664 }}, // Orge
            { 68,  new int[] { 3212 }}, // Ortie
            { 124, new int[] { 1018 }}, // Gougeon
            { 35,  new int[] { 659 }}, // Orme
            { 24, new int[] { 1081 }}, // Fer
            { 69, new int[] { 3213 }}, // Sauge
            { 40, new int[] { 652 }}, // Noyer
            { 39, new int[] { 651 }}, // Chataignier
            { 125 ,new int[] { 1019 }}, // Truite
            { 56, new int[] { 1078 }}, // Manganèse
            { 55,new int[] { 1077 }}, // Etin
            { 26,new int[] { 4921 ,1074}}, // Bronze
            { 25,new int[] { 4920, 1075 }}, // Cuivre
            { 298,new int[] { 4918 }}, // Fer
            { 28 ,new int[] { 1063 }}, // Kobalte
            { 52 ,new int[] { 665 }}, // Seigle
            { 31, new int[] { 1073 }}, // Bauxite
            { 192,new int[] { 1290 }}, // Obsidienne
            { 30,new int[] { 1079 }}, // Or
            { 344 ,new int[] { 3555 }}, // Salikrone
            { 341,new int[] { 3554 }}, // Quisnoa
            { 347,new int[] { 3552 }}, // Patelle
            { 342,new int[] { 3553 }}, // Ecume de mer
            { 343 ,new int[] { 3551 }}, // Bois d'Aquajou
            { 33,new int[] { 655 }}, // Bois d'If
            { 10 ,new int[] { 653 }}, // Bois de Chêne
            { 139,new int[] { 681 }}, // Bois de Bombu
            { 141 ,new int[] { 682 }}, // Bois d'Oliviolet
            { 154 ,new int[] { 685 }}, // Bois de Bambou
            { 41 ,new int[] { 656 }}, // Bois de meurisier
            { 306 ,new int[] { 3222 }}, // Noisetier
            { 34,new int[] { 657 }}, // Ebène
            { 38,new int[] { 658 }}, // Charme
            { 174,new int[] { 689 }}, // Kaliptus
            { 155,new int[] { 686 }}, // Bambou sombre
            { 158, new int[] { 1029 }}, // Bambou sacré
            { 190 ,new int[] { 1289 }}, // Bois de Tremble
            { 74, new int[] { 680 }}, // Edelweiss
            { 237 ,new int[]{ 2345 } }, // Cawotte Fraiche
            { 338 ,new int[]{ 677 } }, // Trefle a 4 feuiles
            { 73,new int[]{ 679 } }, // Orchidée
            {58,new int[]{ 667} }, // Malt
            { 307,new int[]{ 3227, 3228 } }, // Mais
            { 308,new int[]{ 3234 } }, // Millet
            {304,new int[]{ 3223 } }, // Belladone
            {160,new int[]{ 684 } }, // Graine de Pandouille
            {159,new int[]{ 5866 } }, // Riz
            {161,new int[]{ 2202 } }, // Dolomite
            {162,new int[]{2209,1080} }, // Silicate
            {191,new int[]{ 1245 } }, // Frostiz
            {303,new int[]{ 3230 } }, // Ginseng
            {188,new int[]{ 1288 } }, // Perce-Neige
            {325,new int[]{ 1839 } }, // Bar Ricain  plusieurs ressources, même bones...
            {319,new int[]{ 1021 } }, // Anguille plusieurs ressources, même bones...
            {72,new int[]{ 678 } }, // Menthe Sauvage

        };

        public static void Patch()
        {
            foreach (var skillRecord in SkillRecord.GetSkills())
            {
                if (SkillsBonesIds.ContainsKey(skillRecord.Id))
                {
                    skillRecord.ParentBonesIds = SkillsBonesIds[skillRecord.Id].ToList();
                }

                skillRecord.UpdateNow();
            }
        }
    }
}

using Giny.Core;
using Giny.ORM;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Monsters
{
    public class Dungeons
    {
        public static void Patch()
        {
            Logger.Write("Spawning dungeon monsters...");

            foreach (var value in DungeonRecord.GetDungeonRecords())
            {
                value.Rooms.Clear();
            }

            DungeonRecord dungeon = null;

            /* Grange du Tournesol Affamé */

            dungeon = DungeonRecord.GetDungeon(8);
            /* Tournesol Sauvage,Pissenlit Diabolique,Gardienne Champêtre,Épouvanteur,Tournesol Sauvage,Épouvanteur,Gardienne Champêtre,Pissenlit Diabolique*/
            dungeon.Rooms.Add(190449664, new MonsterRoom(10, 48, 79, 4821, 4820, 48, 4820, 4821, 79));
            /* Tournesol Sauvage,Rose Démoniaque,Gardienne Champêtre,Épouvanteur,Épouvanteur,Gardienne Champêtre,Rose Démoniaque,Tournesol Sauvage*/
            dungeon.Rooms.Add(190448640, new MonsterRoom(10, 48, 78, 4821, 4820, 4820, 4821, 78, 48));
            /* Tournesol Sauvage,Tournesol Sauvage,Rose Démoniaque,Épouvanteur,Rose Démoniaque,Pissenlit Diabolique,Tournesol Sauvage,Épouvanteur*/
            dungeon.Rooms.Add(190316544, new MonsterRoom(10, 48, 48, 78, 4820, 78, 79, 48, 4820));
            /* Tournesol Sauvage,Pissenlit Diabolique,Rose Démoniaque,Épouvanteur,Pissenlit Diabolique,Tournesol Sauvage,Épouvanteur,Rose Démoniaque*/
            dungeon.Rooms.Add(190317568, new MonsterRoom(10, 48, 79, 78, 4820, 79, 48, 4820, 78));
            /* Tournesol Affamé,Gardienne Champêtre,Rose Démoniaque,Pissenlit Diabolique,Gardienne Champêtre,Gardienne Champêtre,Rose Démoniaque,Pissenlit Diabolique*/
            dungeon.Rooms.Add(190318592, new MonsterRoom(10, 799, 4821, 78, 79, 4821, 4821, 78, 79));
            dungeon.Rooms.Add(190318594, new MonsterRoom(10));

            /* Centre du Labyrinthe du Minotoror */

            dungeon = DungeonRecord.GetDungeon(4);
            /* Gamino,Déminoboule,Minotoror,Mominotor,Gamino,Serpiplume,Scaratos,Scaratos*/
            dungeon.Rooms.Add(34473474, new MonsterRoom(10, 668, 832, 121, 831, 668, 829, 830, 830));
            dungeon.Rooms.Add(34473476, new MonsterRoom(10));

            /* Cour du Bouftou Royal */

            dungeon = DungeonRecord.GetDungeon(1);
            /* Boufton Blanc,Bouftou,Chef de Guerre Bouftou,Boufton Noir,Chef de Guerre Bouftou,Bouftou,Boufton Blanc,Boufton Noir*/
            dungeon.Rooms.Add(121373185, new MonsterRoom(10, 134, 101, 148, 149, 148, 101, 134, 149));
            /* Boufton Blanc,Chef de Guerre Bouftou,Bouftou Noir,Boufton Noir,Boufton Noir,Bouftou Noir,Chef de Guerre Bouftou,Boufton Blanc*/
            dungeon.Rooms.Add(121374209, new MonsterRoom(10, 134, 148, 4822, 149, 149, 4822, 148, 134));
            /* Boufton Blanc,Bouftou,Bouftou Noir,Boufton Noir,Boufton Blanc,Boufton Noir,Bouftou Noir,Bouftou*/
            dungeon.Rooms.Add(121375233, new MonsterRoom(10, 134, 101, 4822, 149, 134, 149, 4822, 101));
            /* Boufton Blanc,Bouftou,Chef de Guerre Bouftou,Boufton Noir,Bouftou,Boufton Blanc,Boufton Noir,Chef de Guerre Bouftou*/
            dungeon.Rooms.Add(121373187, new MonsterRoom(10, 134, 101, 148, 149, 101, 134, 149, 148));
            /* Bouftou Royal,Bouftou Noir,Chef de Guerre Bouftou,Bouftou,Bouftou Noir,Bouftou Noir,Chef de Guerre Bouftou,Bouftou*/
            dungeon.Rooms.Add(121374211, new MonsterRoom(10, 147, 4822, 148, 101, 4822, 4822, 148, 101));
            dungeon.Rooms.Add(121375235, new MonsterRoom(10));

            /* Domaine Ancestral */

            dungeon = DungeonRecord.GetDungeon(9);
            /* Abrakne Sombre,Abraknyde Sombre,Abraknyde Sombre,Abraknyde Vénérable,Abrakne Sombre,Abrakne Sombre,Abraknyde Sombre,Abraknyde Vénérable*/
            dungeon.Rooms.Add(149684224, new MonsterRoom(10, 651, 253, 253, 254, 651, 651, 253, 254));
            /* Abrakne Sombre,Araknotron,Araknotron,Abraknyde Vénérable,Abrakne Sombre,Araknotron,Abraknyde Vénérable,Abraknyde Vénérable*/
            dungeon.Rooms.Add(149685248, new MonsterRoom(10, 651, 917, 917, 254, 651, 917, 254, 254));
            /* Abraknyde Sombre,Araknotron,Abraknyde Vénérable,Abraknyde Vénérable,Abraknyde Sombre,Abraknyde Sombre,Araknotron,Abraknyde Vénérable*/
            dungeon.Rooms.Add(149686272, new MonsterRoom(10, 253, 917, 254, 254, 253, 253, 917, 254));
            /* Abrakne Sombre,Abraknyde Sombre,Araknotron,Abraknyde Vénérable,Abrakne Sombre,Abraknyde Sombre,Araknotron,Abraknyde Vénérable*/
            dungeon.Rooms.Add(149687296, new MonsterRoom(10, 651, 253, 917, 254, 651, 253, 917, 254));
            /* Abraknyde Ancestral,Abrakne Sombre,Abraknyde Sombre,Araknotron,Abrakne Sombre,Abraknyde Sombre,Araknotron,Araknotron*/
            dungeon.Rooms.Add(149688320, new MonsterRoom(10, 173, 651, 253, 917, 651, 253, 917, 917));
            dungeon.Rooms.Add(149689344, new MonsterRoom(10));

            /* Clairière du Chêne Mou */

            dungeon = DungeonRecord.GetDungeon(10);
            /* Abrakne Sombre Irascible,Araknotron Irascible,Araknotron Irascible,Abraknyde Sombre Irascible,Abraknyde Sombre Irascible,Abrakne Sombre Irascible,Abrakne Sombre Irascible,Araknotron Irascible*/
            dungeon.Rooms.Add(149423104, new MonsterRoom(10, 992, 990, 990, 991, 991, 992, 992, 990));
            /* Branche Soignante,Abrakne Sombre Irascible,Araknotron Irascible,Araknotron Irascible,Abraknyde Sombre Irascible,Branche Soignante,Abrakne Sombre Irascible,Abraknyde Sombre Irascible*/
            dungeon.Rooms.Add(149424128, new MonsterRoom(10, 260, 992, 990, 990, 991, 260, 992, 991));
            /* Branche Invocatrice,Abrakne Sombre Irascible,Abrakne Sombre Irascible,Araknotron Irascible,Abraknyde Sombre Irascible,Branche Invocatrice,Araknotron Irascible,Abraknyde Sombre Irascible*/
            dungeon.Rooms.Add(149425152, new MonsterRoom(10, 258, 992, 992, 990, 991, 258, 990, 991));
            /* Branche Soignante,Branche Invocatrice,Abrakne Sombre Irascible,Araknotron Irascible,Branche Invocatrice,Abraknyde Sombre Irascible,Araknotron Irascible,Abrakne Sombre Irascible*/
            dungeon.Rooms.Add(149426176, new MonsterRoom(10, 260, 258, 992, 990, 258, 991, 990, 992));
            /* Chêne Mou,Branche Soignante,Branche Invocatrice,Abraknyde Sombre Irascible,Abraknyde Sombre Irascible,Branche Soignante,Branche Invocatrice,Abraknyde Sombre Irascible*/
            dungeon.Rooms.Add(149427200, new MonsterRoom(10, 257, 260, 258, 991, 991, 260, 258, 991));
            dungeon.Rooms.Add(149428224, new MonsterRoom(10));

            /* Clos des Blops */

            dungeon = DungeonRecord.GetDungeon(11);
            /* Blopignon,Blop Reinette,Blop Coco,Tronkoblop,Tronkoblop,Blopignon,Blop Reinette,Blop Coco*/
            dungeon.Rooms.Add(166986752, new MonsterRoom(10, 1182, 276, 273, 1183, 1183, 1182, 276, 273));
            /* Tronkoblop,Blop Indigo,Blop Griotte,Blopignon,Blopignon,Tronkoblop,Blop Indigo,Blop Griotte*/
            dungeon.Rooms.Add(166987776, new MonsterRoom(10, 1183, 274, 275, 1182, 1182, 1183, 274, 275));
            /* Gloutoblop,Tronkoblop,Blopignon,Blop Coco,Gloutoblop,Blop Reinette,Blop Indigo,Blop Griotte*/
            dungeon.Rooms.Add(166988800, new MonsterRoom(10, 1181, 1183, 1182, 273, 1181, 276, 274, 275));
            /* Gloutoblop,Gloutoblop,Blopignon,Tronkoblop,Gloutoblop,Gloutoblop,Blopignon,Tronkoblop*/
            dungeon.Rooms.Add(166989824, new MonsterRoom(10, 1181, 1181, 1182, 1183, 1181, 1181, 1182, 1183));
            dungeon.Rooms.Add(166990848, new MonsterRoom(10));
            /* Blop Indigo Royal,Blopignon,Tronkoblop,Blop Indigo,Blopignon,Tronkoblop,Blopignon,Tronkoblop*/
            dungeon.Rooms.Add(166985730, new MonsterRoom(10, 1186, 1182, 1183, 274, 1182, 1183, 1182, 1183));
            /* Blop Griotte Royal,Blopignon,Tronkoblop,Blop Griotte,Blopignon,Tronkoblop,Blopignon,Tronkoblop*/
            dungeon.Rooms.Add(166986754, new MonsterRoom(10, 1185, 1182, 1183, 275, 1182, 1183, 1182, 1183));
            /* Blop Reinette Royal,Blopignon,Tronkoblop,Blop Reinette,Blopignon,Tronkoblop,Blopignon,Tronkoblop*/
            dungeon.Rooms.Add(166987778, new MonsterRoom(10, 1187, 1182, 1183, 276, 1182, 1183, 1182, 1183));
            /* Blop Coco Royal,Blopignon,Tronkoblop,Blop Coco,Blopignon,Tronkoblop,Blopignon,Tronkoblop*/
            dungeon.Rooms.Add(166988802, new MonsterRoom(10, 1184, 1182, 1183, 273, 1182, 1183, 1182, 1183));
            /* Blop Multicolore Royal,Blop Reinette Royal,Blop Indigo Royal,Blop Griotte Royal,Blop Coco Royal,Tronkoblop,Tronkoblop,Blopignon*/
            dungeon.Rooms.Add(166989826, new MonsterRoom(10, 1188, 1187, 1186, 1185, 1184, 1183, 1183, 1182));

            /* Donjon des Bworks */

            dungeon = DungeonRecord.GetDungeon(13);
            /* Bwork Archer,Bwork Archer,Bwork Archer,Bwork Archer,Bwork Archer,Bwork Archer,Bwork Archer,Bwork Archer*/
            dungeon.Rooms.Add(104595969, new MonsterRoom(10, 74, 74, 74, 74, 74, 74, 74, 74));
            /* Bwork,Bwork Mage,Bwork Mage,Bwork Mage,Bwork,Bwork Mage,Bwork Mage,Bwork Mage*/
            dungeon.Rooms.Add(104596993, new MonsterRoom(10, 876, 44, 44, 44, 876, 44, 44, 44));
            /* Bwork,Bwork,Bwork,Bwork Mage,Bwork,Bwork,Bwork,Bwork Mage*/
            dungeon.Rooms.Add(104598017, new MonsterRoom(10, 876, 876, 876, 44, 876, 876, 876, 44));
            /* Bwork,Bwork Mage,Bwork Mage,Bwork Archer,Bwork,Bwork,Bwork Mage,Bwork Archer*/
            dungeon.Rooms.Add(104595971, new MonsterRoom(10, 876, 44, 44, 74, 876, 876, 44, 74));
            /* Bwork,Bwork Mage,Bworkette,Bwork Archer,Bwork,Bwork Mage,Bwork Archer,Bwork Archer*/
            dungeon.Rooms.Add(104596995, new MonsterRoom(10, 876, 44, 792, 74, 876, 44, 74, 74));
            dungeon.Rooms.Add(104598019, new MonsterRoom(10));
            dungeon.Rooms.Add(104599041, new MonsterRoom(10));
            dungeon.Rooms.Add(104599043, new MonsterRoom(10));

            /* Grotte du Bworker */

            dungeon = DungeonRecord.GetDungeon(14);
            /* Bwork Élémental d'Eau,Bwork Élémental d'Air,Bwork Élémental de Terre,Bwork Élémental de Feu,Bwork Élémental de Feu,Bwork Élémental d'Eau,Bwork Élémental d'Air,Bwork Élémental de Terre*/
            dungeon.Rooms.Add(104333825, new MonsterRoom(10, 485, 486, 487, 484, 484, 485, 486, 487));
            /* Bwork Élémental de Feu,Bwork Élémental de Terre,Cybwork,Mama Bwork,Bwork Élémental de Terre,Cybwork,Mama Bwork,Bwork Élémental de Feu*/
            dungeon.Rooms.Add(104334849, new MonsterRoom(10, 484, 487, 488, 479, 487, 488, 479, 484));
            /* Bwork Élémental d'Eau,Bwork Élémental d'Air,Cybwork,Mama Bwork,Bwork Élémental d'Eau,Bwork Élémental d'Air,Mama Bwork,Cybwork*/
            dungeon.Rooms.Add(104335873, new MonsterRoom(10, 485, 486, 488, 479, 485, 486, 479, 488));
            /* Bwork Élémental d'Eau,Bwork Élémental d'Air,Bwork Élémental de Terre,Mama Bwork,Bwork Élémental d'Air,Bwork Élémental de Terre,Bwork Élémental d'Eau,Mama Bwork*/
            dungeon.Rooms.Add(104336897, new MonsterRoom(10, 485, 486, 487, 479, 486, 487, 485, 479));
            /* Bworker,Bwork Élémental de Feu,Cybwork,Mama Bwork,Cybwork,Cybwork,Bwork Élémental de Feu,Mama Bwork*/
            dungeon.Rooms.Add(104333827, new MonsterRoom(10, 478, 484, 488, 479, 488, 488, 484, 479));
            dungeon.Rooms.Add(104334851, new MonsterRoom(10));

            /* Terrier du Wa Wabbit */

            dungeon = DungeonRecord.GetDungeon(17);
            /* Black Wo Wabbit,Black Wo Wabbit,Tiwobot,Wobot,Black Wo Wabbit,Black Wo Wabbit,Tiwobot,Wobot Kiafin*/
            dungeon.Rooms.Add(116654593, new MonsterRoom(10, 3461, 3461, 3474, 181, 3461, 3461, 3474, 3462));
            /* Wobot Kiafin,Wobot Kiafin,Tiwobot,Blanc Pa Wabbit,Wobot Kiafin,Wobot Kiafin,Tiwobot,Black Wo Wabbit*/
            dungeon.Rooms.Add(116655617, new MonsterRoom(10, 3462, 3462, 3474, 3463, 3462, 3462, 3474, 3461));
            /* Wobot,Wobot,Tiwobot,Wobot Kiafin,Wobot,Wobot,Tiwobot,Blanc Pa Wabbit*/
            dungeon.Rooms.Add(116656641, new MonsterRoom(10, 181, 181, 3474, 3462, 181, 181, 3474, 3463));
            /* Blanc Pa Wabbit,Blanc Pa Wabbit,Tiwobot,Black Wo Wabbit,Blanc Pa Wabbit,Blanc Pa Wabbit,Tiwobot,Wobot*/
            dungeon.Rooms.Add(116657665, new MonsterRoom(10, 3463, 3463, 3474, 3461, 3463, 3463, 3474, 181));
            /* Wa Wobot,Black Wo Wabbit,Wobot Kiafin,Tiwobot,Wobot Kiafin,Black Wo Wabbit,Wobot,Blanc Pa Wabbit*/
            dungeon.Rooms.Add(116654595, new MonsterRoom(10, 3460, 3461, 3462, 3474, 3462, 3461, 181, 3463));
            dungeon.Rooms.Add(116655619, new MonsterRoom(10));

            /* Château Ensablé */

            dungeon = DungeonRecord.GetDungeon(19);
            /* Pichon Orange,Pichon Orange,Pichon Blanc,Pichon Vert,Pichon Orange,Pichon Blanc,Pichon Vert,Pichon Bleu*/
            dungeon.Rooms.Add(193725440, new MonsterRoom(10, 920, 920, 922, 923, 920, 922, 923, 921));
            /* Pichon Bleu,Pichon Bleu,Pichon Kloune,Pichon Vert,Pichon Bleu,Pichon Kloune,Pichon Vert,Pichon Orange*/
            dungeon.Rooms.Add(193726464, new MonsterRoom(10, 921, 921, 924, 923, 921, 924, 923, 920));
            /* Pichon Kloune,Pichon Kloune,Pichon Blanc,Pichon Vert,Pichon Kloune,Pichon Blanc,Pichon Vert,Pichon Orange*/
            dungeon.Rooms.Add(193727488, new MonsterRoom(10, 924, 924, 922, 923, 924, 922, 923, 920));
            /* Pichon Vert,Pichon Vert,Pichon Bleu,Pichon Blanc,Pichon Vert,Pichon Bleu,Pichon Blanc,Pichon Orange*/
            dungeon.Rooms.Add(193728512, new MonsterRoom(10, 923, 923, 921, 922, 923, 921, 922, 920));
            /* Mob l'Éponge,Pichon Kloune,Pichon Blanc,Pichon Orange,Pichon Vert,Pichon Kloune,Pichon Vert,Pichon Bleu*/
            dungeon.Rooms.Add(193729536, new MonsterRoom(10, 928, 924, 922, 920, 923, 924, 923, 921));
            dungeon.Rooms.Add(193730560, new MonsterRoom(10));

            /* Donjon des Forgerons */

            dungeon = DungeonRecord.GetDungeon(21);
            dungeon.Rooms.Add(87294465, new MonsterRoom(10));
            /* Bandit du clan des Roublards,Bandit du clan des Roublards,Boulanger Sombre,Boulanger Sombre,Bandit du clan des Roublards,Bandit du clan des Roublards,Boulanger Sombre,Boulanger Sombre*/
            dungeon.Rooms.Add(87295489, new MonsterRoom(10, 155, 155, 153, 153, 155, 155, 153, 153));
            /* Mineur Sombre,Bandit du clan des Roublards,Bandit du clan des Roublards,Boulanger Sombre,Mineur Sombre,Bandit du clan des Roublards,Bandit du clan des Roublards,Boulanger Sombre*/
            dungeon.Rooms.Add(87296513, new MonsterRoom(10, 118, 155, 155, 153, 118, 155, 155, 153));
            dungeon.Rooms.Add(87297537, new MonsterRoom(10));
            dungeon.Rooms.Add(87294467, new MonsterRoom(10));
            dungeon.Rooms.Add(87295491, new MonsterRoom(10));
            dungeon.Rooms.Add(87294469, new MonsterRoom(10));
            dungeon.Rooms.Add(87295493, new MonsterRoom(10));
            dungeon.Rooms.Add(87296517, new MonsterRoom(10));
            dungeon.Rooms.Add(87294471, new MonsterRoom(10));
            dungeon.Rooms.Add(87296515, new MonsterRoom(10));
            dungeon.Rooms.Add(87297541, new MonsterRoom(10));

            /* Gelaxième Dimension */

            dungeon = DungeonRecord.GetDungeon(24);
            /* Gelée Fraise,Gelée Fraise,Gelée Fraise,Gelée Fraise,Gelée Fraise,Gelée Fraise,Gelée Fraise,Gelée Fraise*/
            dungeon.Rooms.Add(98566657, new MonsterRoom(10, 57, 57, 57, 57, 57, 57, 57, 57));
            /* Gelée Fraise,Gelée Bleue,Gelée Bleue,Gelée Bleue,Gelée Fraise,Gelée Bleue,Gelée Bleue,Gelée Bleue*/
            dungeon.Rooms.Add(98567681, new MonsterRoom(10, 57, 55, 55, 55, 57, 55, 55, 55));
            /* Gelée Citron,Gelée Citron,Gelée Fraise,Gelée Bleue,Gelée Citron,Gelée Citron,Gelée Fraise,Gelée Bleue*/
            dungeon.Rooms.Add(98566659, new MonsterRoom(10, 429, 429, 57, 55, 429, 429, 57, 55));
            /* Gelée Citron,Gelée Menthe,Gelée Menthe,Gelée Bleue,Gelée Citron,Gelée Menthe,Gelée Menthe,Gelée Bleue*/
            dungeon.Rooms.Add(98567683, new MonsterRoom(10, 429, 56, 56, 55, 429, 56, 56, 55));
            /* Gelée Royale Bleue,Gelée Royale Citron,Gelée Royale Fraise,Gelée Royale Menthe,Gelée Bleue,Gelée Citron,Gelée Fraise,Gelée Menthe*/
            dungeon.Rooms.Add(98568707, new MonsterRoom(10, 58, 430, 86, 85, 55, 429, 57, 56));
            dungeon.Rooms.Add(98568705, new MonsterRoom(10));

            /* Grotte Hesque */

            dungeon = DungeonRecord.GetDungeon(25);
            /* Palmifleur Passaoh,Palmifleur Morito,Palmifleur Malibout,Palmifleur Kouraçao,Crustorail Kouraçao,Crustorail Passaoh,Crustorail Morito,Crustorail Malibout*/
            dungeon.Rooms.Add(5243139, new MonsterRoom(10, 1066, 1067, 1065, 1064, 1060, 1062, 1063, 1061));
            /* Palmifleur Malibout,Crustorail Passaoh,Crustorail Morito,Crustorail Malibout,Palmifleur Morito,Palmifleur Passaoh,Crustorail Passaoh,Crustorail Morito*/
            dungeon.Rooms.Add(5244416, new MonsterRoom(10, 1065, 1062, 1063, 1061, 1067, 1066, 1062, 1063));
            /* Palmifleur Passaoh,Palmifleur Kouraçao,Palmifleur Morito,Palmifleur Malibout,Corailleur,Corailleur,Corailleur,Corailleur*/
            dungeon.Rooms.Add(5242883, new MonsterRoom(10, 1066, 1064, 1067, 1065, 1022, 1022, 1022, 1022));
            /* Corailleur,Corailleur,Palmifleur Kouraçao,Crustorail Kouraçao,Corailleur,Corailleur,Corailleur,Corailleur*/
            dungeon.Rooms.Add(5244419, new MonsterRoom(10, 1022, 1022, 1064, 1060, 1022, 1022, 1022, 1022));
            /* Corailleur,Corailleur Magistral,Palmifleur Malibout,Palmifleur Morito,Palmifleur Passaoh,Palmifleur Kouraçao,Crustorail Malibout,Crustorail Kouraçao*/
            dungeon.Rooms.Add(5242886, new MonsterRoom(10, 1022, 1027, 1065, 1067, 1066, 1064, 1061, 1060));
            dungeon.Rooms.Add(5244422, new MonsterRoom(10));

            /* Village Kanniboul */

            dungeon = DungeonRecord.GetDungeon(27);
            /* Kanniboul Ark,Kanniboul Eth,Kanniboul Ark,Kanniboul Tam,Kanniboul Jav,Kanniboul Eth,Kanniboul Jav,Kanniboul Sarbak*/
            dungeon.Rooms.Add(157548544, new MonsterRoom(10, 211, 212, 211, 4151, 213, 212, 213, 214));
            /* Kanniboul Sarbak,Kanniboul Sarbak,Kanniboul Eth,Kanniboul Ark,Kanniboul Sarbak,Kanniboul Tam,Kanniboul Tam,Kanniboul Jav*/
            dungeon.Rooms.Add(157549568, new MonsterRoom(10, 214, 214, 212, 211, 214, 4151, 4151, 213));
            /* Kanniboul Jav,Kanniboul Jav,Kanniboul Sarbak,Kanniboul Eth,Kanniboul Jav,Kanniboul Sarbak,Kanniboul Ark,Kanniboul Tam*/
            dungeon.Rooms.Add(157550592, new MonsterRoom(10, 213, 213, 214, 212, 213, 214, 211, 4151));
            /* Kanniboul Eth,Kanniboul Eth,Kanniboul Jav,Kanniboul Tam,Kanniboul Eth,Kanniboul Jav,Kanniboul Ark,Kanniboul Sarbak*/
            dungeon.Rooms.Add(157551616, new MonsterRoom(10, 212, 212, 213, 4151, 212, 213, 211, 214));
            /* Kanniboul Tam,Kanniboul Jav,Kanniboul Ebil,Kanniboul Sarbak,Kanniboul Eth,Kanniboul Eth,Kanniboul Tam,Kanniboul Ark*/
            dungeon.Rooms.Add(157552640, new MonsterRoom(10, 4151, 213, 2960, 214, 212, 212, 4151, 211));
            dungeon.Rooms.Add(157553664, new MonsterRoom(10));

            /* Canopée du Kimbo */

            dungeon = DungeonRecord.GetDungeon(28);
            /* Abrakleur Clair,Abrakleur Clair,Meupette,Poolay,Abrakleur Clair,Meupette,Poolay,Poolay*/
            dungeon.Rooms.Add(21495808, new MonsterRoom(10, 1047, 1047, 1046, 1043, 1047, 1046, 1043, 1043));
            /* Kaskargo,Poolay,Poolay,Meupette,Kaskargo,Poolay,Poolay,Meupette*/
            dungeon.Rooms.Add(21499904, new MonsterRoom(10, 1044, 1043, 1043, 1046, 1044, 1043, 1043, 1046));
            /* Bitouf Aérien,Bitouf Aérien,Poolay,Meupette,Bitouf Aérien,Bitouf Aérien,Poolay,Meupette*/
            dungeon.Rooms.Add(21497856, new MonsterRoom(10, 1020, 1020, 1043, 1046, 1020, 1020, 1043, 1046));
            /* Abrakleur Clair,Kaskargo,Bitouf Aérien,Meupette,Abrakleur Clair,Kaskargo,Poolay,Meupette*/
            dungeon.Rooms.Add(21496834, new MonsterRoom(10, 1047, 1044, 1020, 1046, 1047, 1044, 1043, 1046));
            /* Kaskargo,Abrakleur Clair,Bitouf Aérien,Kimbo,Kaskargo,Bitouf Aérien,Poolay,Meupette*/
            dungeon.Rooms.Add(21497858, new MonsterRoom(10, 1044, 1047, 1020, 1045, 1044, 1020, 1043, 1046));
            dungeon.Rooms.Add(21498882, new MonsterRoom(10));

            /* Nid du Kwakwa */

            dungeon = DungeonRecord.GetDungeon(32);
            /* Kwak de Glace,Kwak de Glace,Bwak de Glace,Kwakere de Glace,Kwak de Glace,Kwak de Glace,Bwak de Glace,Kwakere de Glace*/
            dungeon.Rooms.Add(64749568, new MonsterRoom(10, 84, 84, 3282, 271, 84, 84, 3282, 271));
            /* Kwak de Vent,Kwak de Vent,Bwak de Vent,Kwakere de Vent,Kwak de Vent,Kwak de Vent,Bwak de Vent,Kwakere de Vent*/
            dungeon.Rooms.Add(64750592, new MonsterRoom(10, 81, 81, 3281, 272, 81, 81, 3281, 272));
            /* Kwak de Terre,Kwak de Terre,Bwak de Terre,Kwakere de Terre,Kwak de Terre,Kwak de Terre,Bwak de Terre,Kwakere de Terre*/
            dungeon.Rooms.Add(64751616, new MonsterRoom(10, 235, 235, 266, 270, 235, 235, 266, 270));
            /* Kwak de Flamme,Kwak de Flamme,Bwak de Flamme,Kwakere de Flamme,Kwak de Flamme,Kwak de Flamme,Bwak de Flamme,Kwakere de Flamme*/
            dungeon.Rooms.Add(64752640, new MonsterRoom(10, 83, 83, 3283, 269, 83, 83, 3283, 269));
            /* Kwak de Vent,Kwak de Glace,Kwak de Terre,Kwakwa,Kwak de Vent,Kwak de Glace,Kwak de Terre,Kwak de Flamme*/
            dungeon.Rooms.Add(64753664, new MonsterRoom(10, 81, 84, 235, 2995, 81, 84, 235, 83));
            dungeon.Rooms.Add(64754688, new MonsterRoom(10));

            /* Laboratoire de Brumen Tinctorias */

            dungeon = DungeonRecord.GetDungeon(35);
            /* Croc Gland,Scorbute,Crowneille,Kolérat,Crowneille,Scorbute,Croc Gland,Kolérat*/
            dungeon.Rooms.Add(176947200, new MonsterRoom(10, 299, 298, 3096, 300, 3096, 298, 299, 300));
            /* Croc Gland,Crowneille,Macien,Kolérat,Kolérat,Macien,Crowneille,Croc Gland*/
            dungeon.Rooms.Add(176948224, new MonsterRoom(10, 299, 3096, 301, 300, 300, 301, 3096, 299));
            /* Croc Gland,Scorbute,Macien,Kolérat,Croc Gland,Kolérat,Macien,Scorbute*/
            dungeon.Rooms.Add(176949248, new MonsterRoom(10, 299, 298, 301, 300, 299, 300, 301, 298));
            /* Croc Gland,Scorbute,Crowneille,Kolérat,Scorbute,Croc Gland,Kolérat,Crowneille*/
            dungeon.Rooms.Add(176950272, new MonsterRoom(10, 299, 298, 3096, 300, 298, 299, 300, 3096));
            /* Nelween,Crowneille,Macien,Scorbute,Macien,Macien,Crowneille,Scorbute*/
            dungeon.Rooms.Add(176951296, new MonsterRoom(10, 3100, 3096, 301, 298, 301, 301, 3096, 298));
            dungeon.Rooms.Add(176952320, new MonsterRoom(10));

            /* Cale de l'Arche d'Otomaï */

            dungeon = DungeonRecord.GetDungeon(39);
            /* Canondorf,Nakunbra,Boomba,Le Flib,Nakunbra,Canondorf,Barbroussa,Sparo*/
            dungeon.Rooms.Add(22282240, new MonsterRoom(10, 231, 229, 228, 1050, 229, 231, 1049, 1048));
            /* Canondorf,Barbroussa,Sparo,Le Flib,Nakunbra,Sparo,Le Flib,Le Flib*/
            dungeon.Rooms.Add(22283264, new MonsterRoom(10, 231, 1049, 1048, 1050, 229, 1048, 1050, 1050));
            /* Nakunbra,Barbroussa,Sparo,Le Flib,Canondorf,Barbroussa,Sparo,Le Flib*/
            dungeon.Rooms.Add(22282242, new MonsterRoom(10, 229, 1049, 1048, 1050, 231, 1049, 1048, 1050));
            /* Barbroussa,Barbroussa,Sparo,Le Flib,Barbroussa,Barbroussa,Sparo,Le Flib*/
            dungeon.Rooms.Add(22283266, new MonsterRoom(10, 1049, 1049, 1048, 1050, 1049, 1049, 1048, 1050));
            /* Barbroussa,Gourlo le Terrible,Sparo,Le Flib,Barbroussa,Sparo,Sparo,Le Flib*/
            dungeon.Rooms.Add(22284290, new MonsterRoom(10, 1049, 1051, 1048, 1050, 1049, 1048, 1048, 1050));
            dungeon.Rooms.Add(22285314, new MonsterRoom(10));

            /* Goulet du Rasboul */

            dungeon = DungeonRecord.GetDungeon(41);
            /* Kilibriss,Craqueleur Poli,Craqueleur Poli,Craqueboule Poli,Kilibriss,Craqueleur Poli,Craqueboule Poli,Craqueboule Poli*/
            dungeon.Rooms.Add(22808576, new MonsterRoom(10, 1070, 1025, 1025, 1026, 1070, 1025, 1026, 1026));
            /* Kilibriss,Craqueleur Poli,Mufafah,Craqueboule Poli,Kilibriss,Kilibriss,Mufafah,Mufafah*/
            dungeon.Rooms.Add(22806530, new MonsterRoom(10, 1070, 1025, 1068, 1026, 1070, 1070, 1068, 1068));
            /* Bitouf des Plaines,Craqueleur Poli,Kido,Craqueleur Poli,Bitouf des Plaines,Bitouf des Plaines,Bitouf des Plaines,Kido*/
            dungeon.Rooms.Add(22807554, new MonsterRoom(10, 1019, 1025, 1069, 1025, 1019, 1019, 1019, 1069));
            /* Kilibriss,Bitouf des Plaines,Kido,Mufafah,Kilibriss,Bitouf des Plaines,Kido,Mufafah*/
            dungeon.Rooms.Add(22808578, new MonsterRoom(10, 1070, 1019, 1069, 1068, 1070, 1019, 1069, 1068));
            /* Kilibriss,Silf le Rasboul Majeur,Bitouf des Plaines,Kido,Kilibriss,Craqueleur Poli,Mufafah,Craqueleur Poli*/
            dungeon.Rooms.Add(22809602, new MonsterRoom(10, 1070, 1071, 1019, 1069, 1070, 1025, 1068, 1025));
            dungeon.Rooms.Add(22810626, new MonsterRoom(10));

            /* Donjon des Rats de Bonta */

            dungeon = DungeonRecord.GetDungeon(42);
            /* Capoei Rat,Capoei Rat,Scélée Rate,Scélée Rate,Capoei Rat,Capoei Rat,Scélée Rate,Scélée Rate*/
            dungeon.Rooms.Add(27000832, new MonsterRoom(10, 942, 942, 938, 938, 942, 942, 938, 938));
            /* Capoei Rat,Capoei Rat,Capoei Rat,Scélée Rate,Capoei Rat,Capoei Rat,Capoei Rat,Scélée Rate*/
            dungeon.Rooms.Add(27003904, new MonsterRoom(10, 942, 942, 942, 938, 942, 942, 942, 938));
            /* Chak Rat,Capoei Rat,Capoei Rat,Scélée Rate,Capoei Rat,Capoei Rat,Capoei Rat,Scélée Rate*/
            dungeon.Rooms.Add(27001858, new MonsterRoom(10, 941, 942, 942, 938, 942, 942, 942, 938));
            /* Chak Rat,Chak Rat,Capoei Rat,Scélée Rate,Chak Rat,Chak Rat,Capoei Rat,Scélée Rate*/
            dungeon.Rooms.Add(27003906, new MonsterRoom(10, 941, 941, 942, 938, 941, 941, 942, 938));
            /* Chak Rat,Capoei Rat,Rat Blanc,Scélée Rate,Chak Rat,Capoei Rat,Capoei Rat,Scélée Rate*/
            dungeon.Rooms.Add(27000836, new MonsterRoom(10, 941, 942, 940, 938, 941, 942, 942, 938));
            dungeon.Rooms.Add(27001860, new MonsterRoom(10));

            /* Donjon des Rats de Brâkmar */

            dungeon = DungeonRecord.GetDungeon(43);
            /* Rat Plapla,Rat Plapla,Rat Sio,Rat Sio,Rat Plapla,Rat Plapla,Rat Sio,Rat Sio*/
            dungeon.Rooms.Add(40108544, new MonsterRoom(10, 935, 935, 937, 937, 935, 935, 937, 937));
            /* Rat Plapla,Rat Plapla,Rat Plapla,Rat Sio,Rat Plapla,Rat Plapla,Rat Plapla,Rat Sio*/
            dungeon.Rooms.Add(40109568, new MonsterRoom(10, 935, 935, 935, 937, 935, 935, 935, 937));
            /* Rat Li,Rat Plapla,Rat Plapla,Rat Sio,Rat Plapla,Rat Plapla,Rat Plapla,Rat Sio*/
            dungeon.Rooms.Add(40111616, new MonsterRoom(10, 936, 935, 935, 937, 935, 935, 935, 937));
            /* Rat Li,Rat Li,Rat Plapla,Rat Sio,Rat Li,Rat Li,Rat Plapla,Rat Sio*/
            dungeon.Rooms.Add(40110594, new MonsterRoom(10, 936, 936, 935, 937, 936, 936, 935, 937));
            /* Rat Li,Rat Plapla,Rat Noir,Rat Sio,Rat Li,Rat Plapla,Rat Plapla,Rat Sio*/
            dungeon.Rooms.Add(40110085, new MonsterRoom(10, 936, 935, 939, 937, 936, 935, 935, 937));
            dungeon.Rooms.Add(40110087, new MonsterRoom(10));

            /* Donjon des Rats du Château d'Amakna */

            dungeon = DungeonRecord.GetDungeon(44);
            /* Rat Colleur,Rat Masseur,Rat Goûtant,Ramane,Rat Colleur,Rat Masseur,Rat Goûtant,Ramane*/
            dungeon.Rooms.Add(102760961, new MonsterRoom(10, 3348, 3347, 3346, 3345, 3348, 3347, 3346, 3345));
            /* Rat Colleur,Rat Masseur,Rat Goûtant,Ramane,Rat Colleur,Rat Pine,Rat Fraîchi*/
            dungeon.Rooms.Add(102761985, new MonsterRoom(10, 3348, 3347, 3346, 3345, 3348, 3349, 3350));
            /* Rat Botteur,Rat Caille,Rat Fraîchi,Rat Pine,Rat Pine,Rat Fraîchi,Ramane,Ramane*/
            dungeon.Rooms.Add(102763009, new MonsterRoom(10, 3352, 3351, 3350, 3349, 3349, 3350, 3345, 3345));
            /* Rat Botteur,Rat Caille,Rat Caille,Rat Botteur,Rat Botteur,Rat Caille,Rat Botteur,Rat Caille*/
            dungeon.Rooms.Add(102764033, new MonsterRoom(10, 3352, 3351, 3351, 3352, 3352, 3351, 3352, 3351));
            /* Rat Blanc,Rat Noir,Sphincter Cell,Ramane,Rat Caille,Rat Botteur,Rat Pine,Rat Fraîchi*/
            dungeon.Rooms.Add(102760963, new MonsterRoom(10, 940, 939, 943, 3345, 3351, 3352, 3349, 3350));
            dungeon.Rooms.Add(102761987, new MonsterRoom(10));

            /* Donjon des Scarafeuilles */

            dungeon = DungeonRecord.GetDungeon(45);
            dungeon.Rooms.Add(94109696, new MonsterRoom(10));
            /* Scarafeuille Blanc,Scarafeuille Bleu,Scarafeuille Bleu,Scarafeuille Immature,Scarafeuille Blanc,Scarafeuille Bleu,Scarafeuille Bleu,Scarafeuille Immature*/
            dungeon.Rooms.Add(94110720, new MonsterRoom(10, 241, 198, 198, 798, 241, 198, 198, 798));
            /* Scarafeuille Rouge,Scarafeuille Vert,Scarafeuille Vert,Scarafeuille Immature,Scarafeuille Rouge,Scarafeuille Vert,Scarafeuille Rouge,Scarafeuille Immature*/
            dungeon.Rooms.Add(94111744, new MonsterRoom(10, 194, 240, 240, 798, 194, 240, 194, 798));
            /* Scarafeuille Bleu,Scarafeuille Rouge,Scarafeuille Vert,Scarafeuille Blanc,Scarafeuille Vert,Scarafeuille Rouge,Scarafeuille Bleu,Scarafeuille Blanc*/
            dungeon.Rooms.Add(94112768, new MonsterRoom(10, 198, 194, 240, 241, 240, 194, 198, 241));
            /* Scarafeuille Noir,Scarafeuille Blanc,Scarafeuille Rouge,Scarafeuille Immature,Scarafeuille Noir,Scarafeuille Vert,Scarafeuille Bleu,Scarafeuille Immature*/
            dungeon.Rooms.Add(94113792, new MonsterRoom(10, 795, 241, 194, 798, 795, 240, 198, 798));
            /* Scarabosse Doré,Scarafeuille Noir,Scarafeuille Blanc,Scarafeuille Bleu,Scarafeuille Noir,Scarafeuille Noir,Scarafeuille Vert,Scarafeuille Rouge*/
            dungeon.Rooms.Add(94115840, new MonsterRoom(10, 797, 795, 241, 198, 795, 795, 240, 194));
            dungeon.Rooms.Add(94116864, new MonsterRoom(10));

            /* Repaire de Skeunk */

            dungeon = DungeonRecord.GetDungeon(46);
            /* Diamantine,Warko Violet,Warko Violet,Koalak Farouche,Koalak Farouche*/
            dungeon.Rooms.Add(107481088, new MonsterRoom(10, 681, 745, 745, 756, 756));
            /* Émeraude,Poupée Émeraude,Poupée Émeraude,Poupée Émeraude,Poupée Émeraude,Poupée Émeraude,Poupée Émeraude*/
            dungeon.Rooms.Add(107482112, new MonsterRoom(10, 673, 674, 674, 674, 674, 674, 674));
            /* Rubise,Fauchalak,Warko Violet,Koalak Sanguin,Guerrier Koalak*/
            dungeon.Rooms.Add(107483136, new MonsterRoom(10, 677, 783, 745, 744, 749));
            /* Saphira,Poupée Affamée,Koalak Sanguin,Koalak Sanguin,Guerrier Koalak,Guerrier Koalak*/
            dungeon.Rooms.Add(107484160, new MonsterRoom(10, 675, 676, 744, 744, 749, 749));
            /* Skeunk,Émeraude,Saphira,Rubise,Diamantine,Poupée Affamée*/
            dungeon.Rooms.Add(107485184, new MonsterRoom(10, 780, 673, 675, 677, 681, 676));
            dungeon.Rooms.Add(107486208, new MonsterRoom(10));

            /* Donjon des Squelettes */

            dungeon = DungeonRecord.GetDungeon(47);
            /* Rib,Rib,Rib,Chafer Primitif,Rib,Rib,Rib,Chafer Primitif*/
            dungeon.Rooms.Add(87033344, new MonsterRoom(10, 108, 108, 108, 3240, 108, 108, 108, 3240));
            /* Chafer Primitif,Chafer Primitif,Rib,Chafer Fantassin,Chafer Primitif,Rib,Chafer Fantassin,Chafer Fantassin*/
            dungeon.Rooms.Add(87034368, new MonsterRoom(10, 3240, 3240, 108, 396, 3240, 108, 396, 396));
            /* Chafer Draugr,Chafer Primitif,Chafer Fantassin,Chafer Invisible,Chafer Primitif,Chafer Invisible,Chafer Invisible,Chafer Invisible*/
            dungeon.Rooms.Add(87035392, new MonsterRoom(10, 3239, 3240, 396, 110, 3240, 110, 110, 110));
            /* Chafer Draugr,Chafer Primitif,Chafer Fantassin,Chafer,Chafer Draugr,Chafer Primitif,Chafer Fantassin,Chafer*/
            dungeon.Rooms.Add(87032322, new MonsterRoom(10, 3239, 3240, 396, 54, 3239, 3240, 396, 54));
            /* Chafer Rōnin,Chafer Draugr,Chafer Draugr,Chafer Draugr,Rib,Chafer Fantassin,Chafer,Chafer Invisible*/
            dungeon.Rooms.Add(87033346, new MonsterRoom(10, 3238, 3239, 3239, 3239, 108, 396, 54, 110));
            dungeon.Rooms.Add(87034370, new MonsterRoom(10));

            /* Donjon des Tofus */

            dungeon = DungeonRecord.GetDungeon(48);
            /* Tofukaz,Tofu Noir,Tofu Noir,Tofu,Tofukaz,Tofu Noir,Tofu Noir,Tofu*/
            dungeon.Rooms.Add(96338946, new MonsterRoom(10, 806, 796, 796, 43, 806, 796, 796, 43));
            /* Tofukaz,Tofukaz,Tofu Noir,Tofu Noir,Tofukaz,Tofukaz,Tofu Noir,Tofu Noir*/
            dungeon.Rooms.Add(96206848, new MonsterRoom(10, 806, 806, 796, 796, 806, 806, 796, 796));
            /* Tofoune,Tofu Ventripotent,Tofu,Tofoune,Tofoune,Tofoune,Tofu*/
            dungeon.Rooms.Add(96207874, new MonsterRoom(10, 804, 808, 43, 804, 804, 804, 43));
            /* Tofoune,Tofu Ventripotent,Tofukaz,Tofoune,Tofukaz,Tofu Noir,Tofu Noir*/
            dungeon.Rooms.Add(96208898, new MonsterRoom(10, 804, 808, 806, 804, 806, 796, 796));
            /* Tofoune,Tofu Ventripotent,Batofu,Tofoune,Tofukaz,Tofu Noir,Tofu*/
            dungeon.Rooms.Add(96209922, new MonsterRoom(10, 804, 808, 800, 804, 806, 796, 43));
            dungeon.Rooms.Add(96210946, new MonsterRoom(10));

            /* Tofulailler Royal */

            dungeon = DungeonRecord.GetDungeon(50);
            /* Tofutoflamme,Tofutoflamme,Vilain Petit Tofu,Vilain Petit Tofu,Tofutoflamme,Tofutoflamme,Vilain Petit Tofu,Vilain Petit Tofu*/
            dungeon.Rooms.Add(96338948, new MonsterRoom(10, 3300, 3300, 3299, 3299, 3300, 3300, 3299, 3299));
            /* Tofuzmo,Tofuzmo,Tofutoflamme,Vilain Petit Tofu,Tofuzmo,Tofuzmo,Tofutoflamme,Vilain Petit Tofu*/
            dungeon.Rooms.Add(96338950, new MonsterRoom(10, 807, 807, 3300, 3299, 807, 807, 3300, 3299));
            /* Tofubine,Tofutoflamme,Tofuzmo,Vilain Petit Tofu,Tofubine,Tofuzmo,Tofutoflamme,Vilain Petit Tofu*/
            dungeon.Rooms.Add(96207878, new MonsterRoom(10, 3301, 3300, 807, 3299, 3301, 807, 3300, 3299));
            /* Tofu Dodu,Tofu Dodu,Tofubine,Tofuzmo,Tofu Dodu,Tofu Dodu,Tofubine,Tofuzmo*/
            dungeon.Rooms.Add(96208902, new MonsterRoom(10, 3302, 3302, 3301, 807, 3302, 3302, 3301, 807));
            /* Tofu Royal,Tofu Dodu,Tofubine,Tofutoflamme,Tofu Dodu,Tofubine,Tofutoflamme,Vilain Petit Tofu*/
            dungeon.Rooms.Add(96209926, new MonsterRoom(10, 382, 3302, 3301, 3300, 3302, 3301, 3300, 3299));
            dungeon.Rooms.Add(96210950, new MonsterRoom(10));

            /* Laboratoire du Tynril */

            dungeon = DungeonRecord.GetDungeon(51);
            /* Fécorce,Brouture,Nerbe,Chiendent,Fécorce,Brouture,Nerbe,Chiendent*/
            dungeon.Rooms.Add(89391104, new MonsterRoom(10, 1075, 1073, 1074, 1076, 1075, 1073, 1074, 1076));
            /* Floribonde,Brouture,Nerbe,Chiendent,Floribonde,Brouture,Nerbe,Chiendent*/
            dungeon.Rooms.Add(89392128, new MonsterRoom(10, 1029, 1073, 1074, 1076, 1029, 1073, 1074, 1076));
            /* Bitouf Sombre,Bitouf Sombre,Floribonde,Fécorce,Bitouf Sombre,Bitouf Sombre,Floribonde,Fécorce*/
            dungeon.Rooms.Add(89391106, new MonsterRoom(10, 1041, 1041, 1029, 1075, 1041, 1041, 1029, 1075));
            /* Abrakleur Sombre,Abrakleur Sombre,Bitouf Sombre,Floribonde,Abrakleur Sombre,Bitouf Sombre,Bitouf Sombre,Floribonde*/
            dungeon.Rooms.Add(89394178, new MonsterRoom(10, 1077, 1077, 1041, 1029, 1077, 1041, 1041, 1029));
            /* Tynril Déconcerté,Tynril Consterné,Tynril Ahuri,Tynril Perfide*/
            dungeon.Rooms.Add(89392130, new MonsterRoom(10, 1085, 1072, 1087, 1086));
            dungeon.Rooms.Add(89393154, new MonsterRoom(10));

            /* Château du Wa Wabbit */

            dungeon = DungeonRecord.GetDungeon(52);
            /* Black Wabbit,Wabbit,Black Tiwabbit,Tiwabbit,Black Wabbit,Wabbit,Tiwabbit,Tiwabbit Kiafin*/
            dungeon.Rooms.Add(116392448, new MonsterRoom(10, 65, 64, 68, 96, 65, 64, 96, 72));
            /* Black Wabbit,Wabbit,Wabbit,Tiwabbit Kiafin,Black Wabbit,Wabbit,Wabbit,Tiwabbit Kiafin*/
            dungeon.Rooms.Add(116393472, new MonsterRoom(10, 65, 64, 64, 72, 65, 64, 64, 72));
            /* Wo Wabbit,Black Wabbit,Black Wabbit,Wabbit,Wo Wabbit,Black Wabbit,Wabbit,Wabbit*/
            dungeon.Rooms.Add(116394496, new MonsterRoom(10, 97, 65, 65, 64, 97, 65, 64, 64));
            /* Grand Pa Wabbit,Wo Wabbit,Black Wabbit,Wabbit,Grand Pa Wabbit,Grand Pa Wabbit,Wo Wabbit,Wo Wabbit*/
            dungeon.Rooms.Add(116395520, new MonsterRoom(10, 99, 97, 65, 64, 99, 99, 97, 97));
            /* Wa Wabbit,Grand Pa Wabbit,Grand Pa Wabbit,Wo Wabbit,Grand Pa Wabbit,Grand Pa Wabbit,Wo Wabbit,Wo Wabbit*/
            dungeon.Rooms.Add(116392450, new MonsterRoom(10, 180, 99, 99, 97, 99, 99, 97, 97));
            dungeon.Rooms.Add(116393474, new MonsterRoom(10));

            /* Salle du Minotot */

            dungeon = DungeonRecord.GetDungeon(53);
            /* Minotot,Mominotor,Minotoror,Déminoboule,Gamino,Gamino,Scaratos,Scaratos*/
            dungeon.Rooms.Add(34472450, new MonsterRoom(10, 827, 831, 121, 832, 668, 668, 830, 830));

            /* Serre du Royalmouth */

            dungeon = DungeonRecord.GetDungeon(54);
            /* Boufmouth,Boufmouth,Bouftonmouth,Bouftonmouth,Boufmouth,Boufmouth,Bouftonmouth,Bouftonmouth*/
            dungeon.Rooms.Add(55050240, new MonsterRoom(10, 2850, 2850, 2853, 2853, 2850, 2850, 2853, 2853));
            /* Boufmouth de guerre,Boufmouth,Bouftonmouth,Bouftonmouth,Boufmouth de guerre,Boufmouth,Boufmouth,Bouftonmouth*/
            dungeon.Rooms.Add(55050242, new MonsterRoom(10, 2851, 2850, 2853, 2853, 2851, 2850, 2850, 2853));
            /* Boufmouth légendaire,Boufmouth de guerre,Boufmouth,Bouftonmouth,Boufmouth légendaire,Boufmouth de guerre,Boufmouth,Bouftonmouth*/
            dungeon.Rooms.Add(55052290, new MonsterRoom(10, 2852, 2851, 2850, 2853, 2852, 2851, 2850, 2853));
            /* Boufmouth légendaire,Boufmouth de guerre,Boufmouth de guerre,Boufmouth,Boufmouth légendaire,Boufmouth de guerre,Boufmouth de guerre,Boufmouth*/
            dungeon.Rooms.Add(55052288, new MonsterRoom(10, 2852, 2851, 2851, 2850, 2852, 2851, 2851, 2850));
            /* Boufmouth légendaire,Royalmouth,Boufmouth de guerre,Boufmouth,Boufmouth légendaire,Boufmouth de guerre,Boufmouth,Bouftonmouth*/
            dungeon.Rooms.Add(55053312, new MonsterRoom(10, 2852, 2854, 2851, 2850, 2852, 2851, 2850, 2853));
            dungeon.Rooms.Add(55054336, new MonsterRoom(10));

            /* Excavation du Mansot Royal */

            dungeon = DungeonRecord.GetDungeon(55);
            /* Shamansot,Mamansot,Mansobèse,Timansot,Shamansot,Mamansot,Mansobèse,Timansot*/
            dungeon.Rooms.Add(56098816, new MonsterRoom(10, 2855, 2857, 2856, 2849, 2855, 2857, 2856, 2849));
            /* Mamansot,Mansobèse,Timansot,Timansot,Mamansot,Mansobèse,Timansot,Timansot*/
            dungeon.Rooms.Add(56099840, new MonsterRoom(10, 2857, 2856, 2849, 2849, 2857, 2856, 2849, 2849));
            /* Fu Mansot,Shamansot,Shamansot,Mansobèse,Fu Mansot,Shamansot,Shamansot,Mansobèse*/
            dungeon.Rooms.Add(56100864, new MonsterRoom(10, 2858, 2855, 2855, 2856, 2858, 2855, 2855, 2856));
            /* Fu Mansot,Shamansot,Mamansot,Mamansot,Fu Mansot,Fu Mansot,Shamansot,Mamansot*/
            dungeon.Rooms.Add(56101888, new MonsterRoom(10, 2858, 2855, 2857, 2857, 2858, 2858, 2855, 2857));
            /* Mansot Royal,Fu Mansot,Shamansot,Mamansot,Fu Mansot,Mamansot,Mansobèse,Timansot*/
            dungeon.Rooms.Add(56102912, new MonsterRoom(10, 2848, 2858, 2855, 2857, 2858, 2857, 2856, 2849));
            dungeon.Rooms.Add(56103936, new MonsterRoom(10));

            /* Épave du Grolandais violent */

            dungeon = DungeonRecord.GetDungeon(56);
            /* Fantimonier,Fantimonier,Fancrôme,Fancrôme,Fantimonier,Fancrôme,Fancrôme,Fancrôme*/
            dungeon.Rooms.Add(56360960, new MonsterRoom(10, 2881, 2881, 2874, 2874, 2881, 2874, 2874, 2874));
            /* Harpirate,Fantimonier,Fantômat,Fantomalamère,Harpirate,Fantimonier,Fantômat,Fancrôme*/
            dungeon.Rooms.Add(56361984, new MonsterRoom(10, 2878, 2881, 2880, 2875, 2878, 2881, 2880, 2874));
            /* Harpirate,Harpirate,Fantomalamère,Fantomalamère,Harpirate,Harpirate,Fantomalamère,Fantomalamère*/
            dungeon.Rooms.Add(56363008, new MonsterRoom(10, 2878, 2878, 2875, 2875, 2878, 2878, 2875, 2875));
            /* Vigie pirate,Vigie pirate,Fantômat,Fantômat,Vigie pirate,Vigie pirate,Fantômat,Fantômat*/
            dungeon.Rooms.Add(56364032, new MonsterRoom(10, 2876, 2876, 2880, 2880, 2876, 2876, 2880, 2880));
            /* Ben le Ripate,Vigie pirate,Harpirate,Fantimonier,Fantimonier,Fantômat,Fantomalamère,Fancrôme*/
            dungeon.Rooms.Add(56365056, new MonsterRoom(10, 2877, 2876, 2878, 2881, 2881, 2880, 2875, 2874));
            dungeon.Rooms.Add(56365058, new MonsterRoom(10));

            /* Hypogée de l'Obsidiantre */

            dungeon = DungeonRecord.GetDungeon(57);
            /* Crapeur,Mofette,Fumrirolle,Atomystique,Crapeur,Mofette,Fumrirolle,Atomystique*/
            dungeon.Rooms.Add(57149697, new MonsterRoom(10, 2865, 2867, 2868, 2866, 2865, 2867, 2868, 2866));
            /* Mofette,Mofette,Fumrirolle,Fumrirolle,Mofette,Mofette,Fumrirolle,Fumrirolle*/
            dungeon.Rooms.Add(57151233, new MonsterRoom(10, 2867, 2867, 2868, 2868, 2867, 2867, 2868, 2868));
            /* Mofette,Atomystique,Atomystique,Fumrirolle,Mofette,Atomystique,Atomystique,Fumrirolle*/
            dungeon.Rooms.Add(57152769, new MonsterRoom(10, 2867, 2866, 2866, 2868, 2867, 2866, 2866, 2868));
            /* Crapeur,Mofette,Fumrirolle,Solfataré,Crapeur,Crapeur,Crapeur,Solfataré*/
            dungeon.Rooms.Add(57154305, new MonsterRoom(10, 2865, 2867, 2868, 2869, 2865, 2865, 2865, 2869));
            /* Obsidiantre,Mofette,Fumrirolle,Solfataré,Crapeur,Mofette,Fumrirolle,Atomystique*/
            dungeon.Rooms.Add(57155841, new MonsterRoom(10, 2924, 2867, 2868, 2869, 2865, 2867, 2868, 2866));
            dungeon.Rooms.Add(57157377, new MonsterRoom(10));

            /* Antre du Korriandre */

            dungeon = DungeonRecord.GetDungeon(59);
            /* Abrazif,Abrazif,Mérulette,Mérulette,Abrazif,Abrazif,Mérulette,Mérulette*/
            dungeon.Rooms.Add(62915584, new MonsterRoom(10, 2898, 2898, 2900, 2900, 2898, 2898, 2900, 2900));
            /* Fongeur,Fongeur,Dramanite,Dramanite,Fongeur,Dramanite,Dramanite,Dramanite*/
            dungeon.Rooms.Add(62916608, new MonsterRoom(10, 2897, 2897, 2895, 2895, 2897, 2895, 2895, 2895));
            /* Mérulette,Mérulette,Fistulor,Fistulor,Mérulette,Fistulor,Fistulor,Fistulor*/
            dungeon.Rooms.Add(62917632, new MonsterRoom(10, 2900, 2900, 2896, 2896, 2900, 2896, 2896, 2896));
            /* Fongeur,Fongeur,Abrazif,Mérulette,Fongeur,Fongeur,Abrazif,Mérulette*/
            dungeon.Rooms.Add(62918656, new MonsterRoom(10, 2897, 2897, 2898, 2900, 2897, 2897, 2898, 2900));
            /* Korriandre,Fongeur,Mérulette,Dramanite,Fongeur,Abrazif,Mérulette,Fistulor*/
            dungeon.Rooms.Add(62919680, new MonsterRoom(10, 2968, 2897, 2900, 2895, 2897, 2898, 2900, 2896));
            dungeon.Rooms.Add(62920704, new MonsterRoom(10));

            /* Cavernes du Kolosso */

            dungeon = DungeonRecord.GetDungeon(60);
            /* Blérice,Blérice,Blérom,Wolvero,Blérice,Blérice,Blérice,Croleur*/
            dungeon.Rooms.Add(61998084, new MonsterRoom(10, 2966, 2966, 2884, 2885, 2966, 2966, 2966, 2886));
            /* Blérom,Blérom,Blérom,Croleur,Blérom,Blérom,Blérom,Wolvero*/
            dungeon.Rooms.Add(61998082, new MonsterRoom(10, 2884, 2884, 2884, 2886, 2884, 2884, 2884, 2885));
            /* Blérom,Blérauve,Blérauve,Wolvero,Croleur,Blérauve,Blérauve,Blérauve*/
            dungeon.Rooms.Add(61998338, new MonsterRoom(10, 2884, 2883, 2883, 2885, 2886, 2883, 2883, 2883));
            /* Blérom,Croleur,Fleuro,Fleuro,Wolvero,Fleuro,Fleuro,Fleuro*/
            dungeon.Rooms.Add(61998340, new MonsterRoom(10, 2884, 2886, 2965, 2965, 2885, 2965, 2965, 2965));
            /* Kolosso,Professeur Xa,Blérom,Croleur,Blérice,Blérauve,Wolvero,Fleuro*/
            dungeon.Rooms.Add(61868036, new MonsterRoom(10, 2986, 2992, 2884, 2886, 2966, 2883, 2885, 2965));
            dungeon.Rooms.Add(61869060, new MonsterRoom(10));

            /* Antichambre des Gloursons */

            dungeon = DungeonRecord.GetDungeon(61);
            dungeon.Rooms.Add(62130696, new MonsterRoom(10));
            /* Glourmand,Boulglours,Apériglours,Gloursaya,Glourmand,Boulglours,Apériglours,Gloursaya*/
            dungeon.Rooms.Add(62131720, new MonsterRoom(10, 2987, 2862, 2861, 2863, 2987, 2862, 2861, 2863));
            /* Glourmand,Apériglours,Apériglours,Gloursaya,Glourmand,Apériglours,Apériglours,Apériglours*/
            dungeon.Rooms.Add(62132744, new MonsterRoom(10, 2987, 2861, 2861, 2863, 2987, 2861, 2861, 2861));
            /* Glouragan,Glouragan,Boulglours,Glourmand,Glouragan,Glouragan,Glouragan,Boulglours*/
            dungeon.Rooms.Add(62133768, new MonsterRoom(10, 2988, 2988, 2862, 2987, 2988, 2988, 2988, 2862));
            /* Meliglours,Meliglours,Glourmand,Glourmand,Meliglours,Meliglours,Glourmand,Glourmand*/
            dungeon.Rooms.Add(62134792, new MonsterRoom(10, 2989, 2989, 2987, 2987, 2989, 2989, 2987, 2987));
            /* Glourséleste,Meliglours,Glouragan,Glourmand,Boulglours,Apériglours,Apériglours,Gloursaya*/
            dungeon.Rooms.Add(62135816, new MonsterRoom(10, 2864, 2989, 2988, 2987, 2862, 2861, 2861, 2863));
            dungeon.Rooms.Add(62136840, new MonsterRoom(10));

            /* Donjon de la mine de Sakaï */

            dungeon = DungeonRecord.GetDungeon(62);
            /* Gobus,Ouilleur,Sapeur,Gobosteur,Gobus,Ouilleur,Sapeur,Gobosteur*/
            dungeon.Rooms.Add(57934593, new MonsterRoom(10, 2941, 2934, 2933, 2932, 2941, 2934, 2933, 2932));
            /* Perkü,Sapeur,Sapeur,Gobosteur,Sapeur,Sapeur,Gobosteur,Gobosteur*/
            dungeon.Rooms.Add(57935617, new MonsterRoom(10, 2936, 2933, 2933, 2932, 2933, 2933, 2932, 2932));
            /* Courtilieur,Marôdeur,Ouilleur,Gobosteur,Courtilieur,Marôdeur,Ouilleur,Gobosteur*/
            dungeon.Rooms.Add(57936641, new MonsterRoom(10, 2937, 2935, 2934, 2932, 2937, 2935, 2934, 2932));
            /* Courtilieur,Courtilieur,Perkü,Perkü,Courtilieur,Courtilieur,Perkü,Perkü*/
            dungeon.Rooms.Add(57937665, new MonsterRoom(10, 2937, 2937, 2936, 2936, 2937, 2937, 2936, 2936));
            /* Grolloum,Gobus,Perkü,Marôdeur,Courtilieur,Ouilleur,Sapeur,Gobosteur*/
            dungeon.Rooms.Add(57938689, new MonsterRoom(10, 2942, 2941, 2936, 2935, 2937, 2934, 2933, 2932));
            dungeon.Rooms.Add(57939713, new MonsterRoom(10));

            /* Potager d'Halouine */

            dungeon = DungeonRecord.GetDungeon(66);
            /* Champêtrouille,Lanverne,Chauffe-Soutrille,Chauffe-Soutrille,Champêtrouille,Lanverne,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101188608, new MonsterRoom(10, 3309, 3310, 3311, 3311, 3309, 3310, 3310, 3311));
            /* Cauchemarakne,Lanverne,Lanverne,Chauffe-Soutrille,Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101189632, new MonsterRoom(10, 3308, 3310, 3310, 3311, 3308, 3309, 3310, 3311));
            /* Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille,Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101190656, new MonsterRoom(10, 3308, 3309, 3310, 3311, 3308, 3309, 3310, 3311));
            /* Dévhorreur,Cauchemarakne,Champêtrouille,Lanverne,Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101191680, new MonsterRoom(10, 3307, 3308, 3309, 3310, 3308, 3309, 3310, 3311));
            /* Cauchemarakne,Champêtrouille,Lanverne,Halouine,Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101193730, new MonsterRoom(10, 3308, 3309, 3310, 3306, 3308, 3309, 3310, 3311));
            /* Cauchemarakne,Champêtrouille,Lanverne,Halouine,Cauchemarakne,Champêtrouille,Lanverne,Chauffe-Soutrille*/
            dungeon.Rooms.Add(101193728, new MonsterRoom(10, 3308, 3309, 3310, 3306, 3308, 3309, 3310, 3311));

            /* Transporteur de Sylargh */

            dungeon = DungeonRecord.GetDungeon(67);
            /* Kanimate,Kanimate,Brikoglours,Mansordide,Kanimate,Kanimate,Brikoglours,Mérulor*/
            dungeon.Rooms.Add(110100480, new MonsterRoom(10, 3404, 3404, 3405, 3406, 3404, 3404, 3405, 3408));
            /* Brikoglours,Brikoglours,Mansordide,Mérulor,Brikoglours,Brikoglours,Mansordide,Kanimate*/
            dungeon.Rooms.Add(110101504, new MonsterRoom(10, 3405, 3405, 3406, 3408, 3405, 3405, 3406, 3404));
            /* Mansordide,Mansordide,Brikoglours,Mécanofoux,Mansordide,Mansordide,Brikoglours,Kanimate*/
            dungeon.Rooms.Add(110103552, new MonsterRoom(10, 3406, 3406, 3405, 3407, 3406, 3406, 3405, 3404));
            /* Mécanofoux,Mécanofoux,Brikoglours,Mansordide,Mécanofoux,Mécanofoux,Brikoglours,Kanimate*/
            dungeon.Rooms.Add(110100482, new MonsterRoom(10, 3407, 3407, 3405, 3406, 3407, 3407, 3405, 3404));
            /* Sylargh,Brikoglours,Mansordide,Mécanofoux,Mérulor,Brikoglours,Mérulor,Kanimate*/
            dungeon.Rooms.Add(110101506, new MonsterRoom(10, 3409, 3405, 3406, 3407, 3408, 3405, 3408, 3404));
            dungeon.Rooms.Add(110102530, new MonsterRoom(10));

            /* Salons privés de Klime */

            dungeon = DungeonRecord.GetDungeon(68);
            /* Harrogant,Harrogant,Grodruche,Peunch,Harrogant,Harrogant,Grodruche,Empaillé*/
            dungeon.Rooms.Add(110362624, new MonsterRoom(10, 3379, 3379, 3380, 3381, 3379, 3379, 3380, 3382));
            /* Grodruche,Grodruche,Peunch,Empaillé,Grodruche,Grodruche,Peunch,Cuirboule*/
            dungeon.Rooms.Add(110363648, new MonsterRoom(10, 3380, 3380, 3381, 3382, 3380, 3380, 3381, 3383));
            /* Cuirboule,Cuirboule,Empaillé,Peunch,Cuirboule,Cuirboule,Empaillé,Grodruche*/
            dungeon.Rooms.Add(110364672, new MonsterRoom(10, 3383, 3383, 3382, 3381, 3383, 3383, 3382, 3380));
            /* Peunch,Peunch,Cuirboule,Grodruche,Peunch,Peunch,Cuirboule,Empaillé*/
            dungeon.Rooms.Add(110362626, new MonsterRoom(10, 3381, 3381, 3383, 3380, 3381, 3381, 3383, 3382));
            /* Klime,Cuirboule,Empaillé,Grodruche,Cuirboule,Empaillé,Harrogant,Peunch*/
            dungeon.Rooms.Add(110363650, new MonsterRoom(10, 3384, 3383, 3382, 3380, 3383, 3382, 3379, 3381));
            dungeon.Rooms.Add(110364674, new MonsterRoom(10));

            /* Forgefroide de Missiz Frizz */

            dungeon = DungeonRecord.GetDungeon(69);
            /* Ventrublion,Ventrublion,Karkanik,Verglasseur,Ventrublion,Ventrublion,Karkanik,Stalak*/
            dungeon.Rooms.Add(109838849, new MonsterRoom(10, 3385, 3385, 3387, 3389, 3385, 3385, 3387, 3386));
            /* Stalak,Stalak,Karkanik,Frimar,Stalak,Stalak,Karkanik,Ventrublion*/
            dungeon.Rooms.Add(109839873, new MonsterRoom(10, 3386, 3386, 3387, 3390, 3386, 3386, 3387, 3385));
            /* Karkanik,Karkanik,Ventrublion,Stalak,Karkanik,Karkanik,Karkanik,Ventrublion*/
            dungeon.Rooms.Add(109840897, new MonsterRoom(10, 3387, 3387, 3385, 3386, 3387, 3387, 3387, 3385));
            /* Frimar,Frimar,Karkanik,Verglasseur,Frimar,Frimar,Karkanik,Ventrublion*/
            dungeon.Rooms.Add(109841921, new MonsterRoom(10, 3390, 3390, 3387, 3389, 3390, 3390, 3387, 3385));
            /* Missiz Frizz,Karkanik,Stalak,Verglasseur,Stalak,Karkanik,Frimar,Ventrublion*/
            dungeon.Rooms.Add(109840899, new MonsterRoom(10, 3391, 3387, 3386, 3389, 3386, 3387, 3390, 3385));
            dungeon.Rooms.Add(109841923, new MonsterRoom(10));

            /* Laboratoire de Nileza */

            dungeon = DungeonRecord.GetDungeon(70);
            /* Nessil,Nessil,Dodox,Krakal,Nessil,Nessil,Dodox,Termystique*/
            dungeon.Rooms.Add(109576705, new MonsterRoom(10, 3392, 3392, 3394, 3393, 3392, 3392, 3394, 3395));
            /* Termystique,Termystique,Dodox,Krakal,Termystique,Termystique,Dodox,Dodox*/
            dungeon.Rooms.Add(109577729, new MonsterRoom(10, 3395, 3395, 3394, 3393, 3395, 3395, 3394, 3394));
            /* Drosérâle,Drosérâle,Krakal,Dodox,Drosérâle,Drosérâle,Krakal,Termystique*/
            dungeon.Rooms.Add(109578753, new MonsterRoom(10, 3396, 3396, 3393, 3394, 3396, 3396, 3393, 3395));
            /* Dodox,Dodox,Krakal,Termystique,Dodox,Dodox,Dodox,Krakal*/
            dungeon.Rooms.Add(109579777, new MonsterRoom(10, 3394, 3394, 3393, 3395, 3394, 3394, 3394, 3393));
            /* Nileza,Dodox,Drosérâle,Nessil,Krakal,Krakal,Dodox,Termystique*/
            dungeon.Rooms.Add(109576707, new MonsterRoom(10, 3397, 3394, 3396, 3392, 3393, 3393, 3394, 3395));
            dungeon.Rooms.Add(109577731, new MonsterRoom(10));

            /* Aquadôme de Merkator */

            dungeon = DungeonRecord.GetDungeon(73);
            /* Krabouilleur,Krabouilleur,Harpo,Pikoleur,Krabouilleur,Pikoleur,Harpo,Eskoglyphe*/
            dungeon.Rooms.Add(119276033, new MonsterRoom(10, 3542, 3542, 3538, 3535, 3542, 3535, 3538, 3543));
            /* Eskoglyphe,Eskoglyphe,Harpo,Cyclophandre,Eskoglyphe,Cyclophandre,Harpo,Krabouilleur*/
            dungeon.Rooms.Add(119276035, new MonsterRoom(10, 3543, 3543, 3538, 3544, 3543, 3544, 3538, 3542));
            /* Cyclophandre,Cyclophandre,Harpo,Pikoleur,Cyclophandre,Pikoleur,Harpo,Krabouilleur*/
            dungeon.Rooms.Add(119277057, new MonsterRoom(10, 3544, 3544, 3538, 3535, 3544, 3535, 3538, 3542));
            /* Harpo,Harpo,Pikoleur,Eskoglyphe,Harpo,Eskoglyphe,Pikoleur,Krabouilleur*/
            dungeon.Rooms.Add(119277059, new MonsterRoom(10, 3538, 3538, 3535, 3543, 3538, 3543, 3535, 3542));
            /* Merkator,Harpo,Cyclophandre,Eskoglyphe,Harpo,Cyclophandre,Pikoleur,Krabouilleur*/
            dungeon.Rooms.Add(119278081, new MonsterRoom(10, 3534, 3538, 3544, 3543, 3538, 3544, 3535, 3542));
            dungeon.Rooms.Add(119278083, new MonsterRoom(10));

            /* Pyramide d'Ombre */

            dungeon = DungeonRecord.GetDungeon(74);
            /* Panterreur,Panterreur,Sombléro,Brutopak,Panterreur,Brutopak,Sombléro,Noctulule*/
            dungeon.Rooms.Add(123207680, new MonsterRoom(10, 3567, 3567, 3570, 3568, 3567, 3568, 3570, 3566));
            /* Noctulule,Noctulule,Sombléro,Caznoar,Noctulule,Caznoar,Sombléro,Panterreur*/
            dungeon.Rooms.Add(123208704, new MonsterRoom(10, 3566, 3566, 3570, 3569, 3566, 3569, 3570, 3567));
            /* Caznoar,Caznoar,Sombléro,Brutopak,Caznoar,Panterreur,Sombléro,Panterreur*/
            dungeon.Rooms.Add(123209728, new MonsterRoom(10, 3569, 3569, 3570, 3568, 3569, 3567, 3570, 3567));
            /* Sombléro,Sombléro,Brutopak,Noctulule,Sombléro,Panterreur,Brutopak,Panterreur*/
            dungeon.Rooms.Add(123210752, new MonsterRoom(10, 3570, 3570, 3568, 3566, 3570, 3567, 3568, 3567));
            /* Ombre,Caznoar,Sombléro,Noctulule,Caznoar,Sombléro,Brutopak,Panterreur*/
            dungeon.Rooms.Add(123212800, new MonsterRoom(10, 3446, 3569, 3570, 3566, 3569, 3570, 3568, 3567));
            dungeon.Rooms.Add(123213824, new MonsterRoom(10));

            /* Grotte de Kanigroula */

            dungeon = DungeonRecord.GetDungeon(75);
            /* Orfélin,Kaniblou,Panthègros,Félygiène,Kaniblou,Orfélin,Félygiène,Panthègros*/
            dungeon.Rooms.Add(125829635, new MonsterRoom(10, 3562, 3561, 3560, 3559, 3561, 3562, 3559, 3560));
            /* Kanihilan,Orfélin,Panthègros,Félygiène,Kanihilan,Orfélin,Panthègros,Félygiène*/
            dungeon.Rooms.Add(125830657, new MonsterRoom(10, 3557, 3562, 3560, 3559, 3557, 3562, 3560, 3559));
            /* Kanihilan,Kaniblou,Orfélin,Félygiène,Kanihilan,Kaniblou,Félygiène,Félygiène*/
            dungeon.Rooms.Add(125830659, new MonsterRoom(10, 3557, 3561, 3562, 3559, 3557, 3561, 3559, 3559));
            /* Kanihilan,Kaniblou,Panthègros,Félygiène,Kanihilan,Kaniblou,Panthègros,Panthègros*/
            dungeon.Rooms.Add(125831681, new MonsterRoom(10, 3557, 3561, 3560, 3559, 3557, 3561, 3560, 3560));
            /* Kanihilan,Kanigroula,Orfélin,Kaniblou,Kanihilan,Orfélin,Orfélin,Kaniblou*/
            dungeon.Rooms.Add(125831683, new MonsterRoom(10, 3557, 3556, 3562, 3561, 3557, 3562, 3562, 3561));
            dungeon.Rooms.Add(125832707, new MonsterRoom(10));

            /* Volière de la Haute Truche */

            dungeon = DungeonRecord.GetDungeon(80);
            /* Truchideur,Truchtine,Truchon,Truchon,Truchideur,Truchideur,Truchtine,Truchon*/
            dungeon.Rooms.Add(132907008, new MonsterRoom(10, 3621, 3622, 3623, 3623, 3621, 3621, 3622, 3623));
            /* Gruche,Truchideur,Truchideur,Truchon,Gruche,Truchideur,Truchon,Truchon*/
            dungeon.Rooms.Add(132908032, new MonsterRoom(10, 3619, 3621, 3621, 3623, 3619, 3621, 3623, 3623));
            /* Gruche,Truchmuche,Truchtine,Truchon,Gruche,Truchmuche,Truchtine,Truchon*/
            dungeon.Rooms.Add(132909056, new MonsterRoom(10, 3619, 3620, 3622, 3623, 3619, 3620, 3622, 3623));
            /* Gruche,Truchmuche,Truchideur,Truchtine,Gruche,Truchmuche,Truchideur,Truchtine*/
            dungeon.Rooms.Add(132910080, new MonsterRoom(10, 3619, 3620, 3621, 3622, 3619, 3620, 3621, 3622));
            /* Haute Truche,Gruche,Truchmuche,Truchtine,Gruche,Truchmuche,Truchmuche,Truchtine*/
            dungeon.Rooms.Add(132911104, new MonsterRoom(10, 3618, 3619, 3620, 3622, 3619, 3620, 3620, 3622));
            dungeon.Rooms.Add(132912128, new MonsterRoom(10));

            /* Antre de la Reine Nyée */

            dungeon = DungeonRecord.GetDungeon(89);
            /* Gargantûl,Saltik,Gargantûl,Arapex,Dardalaine,Saltik,Dardalaine,Néfileuse*/
            dungeon.Rooms.Add(149160960, new MonsterRoom(10, 3995, 3994, 3995, 3991, 3992, 3994, 3992, 3993));
            /* Néfileuse,Néfileuse,Saltik,Gargantûl,Néfileuse,Arapex,Arapex,Dardalaine*/
            dungeon.Rooms.Add(149161984, new MonsterRoom(10, 3993, 3993, 3994, 3995, 3993, 3991, 3991, 3992));
            /* Dardalaine,Dardalaine,Néfileuse,Saltik,Dardalaine,Néfileuse,Gargantûl,Arapex*/
            dungeon.Rooms.Add(149163008, new MonsterRoom(10, 3992, 3992, 3993, 3994, 3992, 3993, 3995, 3991));
            /* Saltik,Saltik,Dardalaine,Arapex,Saltik,Dardalaine,Gargantûl,Néfileuse*/
            dungeon.Rooms.Add(149164032, new MonsterRoom(10, 3994, 3994, 3992, 3991, 3994, 3992, 3995, 3993));
            /* Arapex,Dardalaine,Reine Nyée,Néfileuse,Saltik,Saltik,Arapex,Gargantûl*/
            dungeon.Rooms.Add(149165056, new MonsterRoom(10, 3991, 3992, 3996, 3993, 3994, 3994, 3991, 3995));
            dungeon.Rooms.Add(149166080, new MonsterRoom(10));

            /* Crypte de Kardorim */

            dungeon = DungeonRecord.GetDungeon(90);
            /* Sergent Chafer,Chafer Furtif,Chafer Débutant,Chafer Débutant,Chafer Piquier,Chafer Piquier,Chafer Furtif,Chafer Éclaireur*/
            dungeon.Rooms.Add(152829952, new MonsterRoom(10, 4050, 4047, 4046, 4046, 4049, 4049, 4047, 4048));
            /* Chafer Éclaireur,Chafer Éclaireur,Chafer Furtif,Chafer Débutant,Sergent Chafer,Sergent Chafer,Chafer Piquier,Chafer Éclaireur*/
            dungeon.Rooms.Add(152830976, new MonsterRoom(10, 4048, 4048, 4047, 4046, 4050, 4050, 4049, 4048));
            /* Chafer Piquier,Chafer Piquier,Chafer Éclaireur,Chafer Furtif,Chafer Piquier,Sergent Chafer,Chafer Éclaireur,Chafer Débutant*/
            dungeon.Rooms.Add(152832000, new MonsterRoom(10, 4049, 4049, 4048, 4047, 4049, 4050, 4048, 4046));
            /* Chafer Piquier,Sergent Chafer,Chafer Piquier,Chafer Éclaireur,Sergent Chafer,Chafer Furtif,Chafer Furtif,Chafer Débutant*/
            dungeon.Rooms.Add(152833024, new MonsterRoom(10, 4049, 4050, 4049, 4048, 4050, 4047, 4047, 4046));
            /* Kardorim,Sergent Chafer,Chafer Piquier,Chafer Éclaireur,Sergent Chafer,Chafer Furtif,Chafer Furtif,Chafer Débutant*/
            dungeon.Rooms.Add(152834048, new MonsterRoom(10, 4051, 4050, 4049, 4048, 4050, 4047, 4047, 4046));
            dungeon.Rooms.Add(152835072, new MonsterRoom(10));

            /* Arbre de Moon */

            dungeon = DungeonRecord.GetDungeon(92);
            /* Fourbasse,Gloutovore,Fourbasse,Domoizelle,Dostrogo,Gloutovore,Dostrogo,Trukikol*/
            dungeon.Rooms.Add(157286400, new MonsterRoom(10, 217, 216, 217, 4163, 4162, 216, 4162, 215));
            /* Trukikol,Trukikol,Gloutovore,Fourbasse,Trukikol,Domoizelle,Domoizelle,Dostrogo*/
            dungeon.Rooms.Add(157287424, new MonsterRoom(10, 215, 215, 216, 217, 215, 4163, 4163, 4162));
            /* Dostrogo,Dostrogo,Trukikol,Gloutovore,Dostrogo,Trukikol,Fourbasse,Domoizelle*/
            dungeon.Rooms.Add(157288448, new MonsterRoom(10, 4162, 4162, 215, 216, 4162, 215, 217, 4163));
            /* Gloutovore,Gloutovore,Dostrogo,Domoizelle,Gloutovore,Dostrogo,Fourbasse,Trukikol*/
            dungeon.Rooms.Add(157289472, new MonsterRoom(10, 216, 216, 4162, 4163, 216, 4162, 217, 215));
            /* Domoizelle,Dostrogo,Moon,Trukikol,Gloutovore,Gloutovore,Domoizelle,Fourbasse*/
            dungeon.Rooms.Add(157290496, new MonsterRoom(10, 4163, 4162, 226, 215, 216, 216, 4163, 217));
            dungeon.Rooms.Add(157291520, new MonsterRoom(10));

            /* Cimetière des Mastodontes */

            dungeon = DungeonRecord.GetDungeon(100);
            /* Fennex,Fennex,Ouroboulos,Léolhyène,Fennex,Ouroboulos,Scordion Bleu,Léolhyène*/
            dungeon.Rooms.Add(174326272, new MonsterRoom(10, 4616, 4616, 4620, 4619, 4616, 4620, 4615, 4619));
            /* Scordion Bleu,Scordion Bleu,Boulépique,Léolhyène,Scordion Bleu,Boulépique,Fennex,Léolhyène*/
            dungeon.Rooms.Add(174327296, new MonsterRoom(10, 4615, 4615, 4617, 4619, 4615, 4617, 4616, 4619));
            /* Boulépique,Boulépique,Ouroboulos,Léolhyène,Boulépique,Ouroboulos,Fennex,Léolhyène*/
            dungeon.Rooms.Add(174328320, new MonsterRoom(10, 4617, 4617, 4620, 4619, 4617, 4620, 4616, 4619));
            /* Ouroboulos,Scordion Bleu,Léolhyène,Léolhyène,Ouroboulos,Scordion Bleu,Fennex,Léolhyène*/
            dungeon.Rooms.Add(174329344, new MonsterRoom(10, 4620, 4615, 4619, 4619, 4620, 4615, 4616, 4619));
            /* Ouroboulos,Fennex,Léolhyène,Mantiscore,Boulépique,Boulépique,Scordion Bleu,Léolhyène*/
            dungeon.Rooms.Add(174330368, new MonsterRoom(10, 4620, 4616, 4619, 4621, 4617, 4617, 4615, 4619));
            dungeon.Rooms.Add(174331392, new MonsterRoom(10));

            /* Caverne d'El Piko */

            dungeon = DungeonRecord.GetDungeon(101);
            /* Cactana,Cactiflore,Cactoblongo,Pampactus,Cactoblongo,Cactiflore,Cactana,Pampactus*/
            dungeon.Rooms.Add(174064128, new MonsterRoom(10, 4605, 4604, 4606, 4607, 4606, 4604, 4605, 4607));
            /* Cactana,Cactoblongo,Lévito,Pampactus,Pampactus,Lévito,Cactoblongo,Cactana*/
            dungeon.Rooms.Add(174065664, new MonsterRoom(10, 4605, 4606, 4608, 4607, 4607, 4608, 4606, 4605));
            /* Cactana,Cactiflore,Lévito,Pampactus,Cactana,Pampactus,Lévito,Cactiflore*/
            dungeon.Rooms.Add(174067200, new MonsterRoom(10, 4605, 4604, 4608, 4607, 4605, 4607, 4608, 4604));
            /* Cactana,Cactiflore,Cactoblongo,Pampactus,Cactiflore,Cactana,Pampactus,Cactoblongo*/
            dungeon.Rooms.Add(174068736, new MonsterRoom(10, 4605, 4604, 4606, 4607, 4604, 4605, 4607, 4606));
            /* Lévito,Cactoblongo,Cactiflore,El Piko,Lévito,Lévito,Cactoblongo,Cactiflore*/
            dungeon.Rooms.Add(174070272, new MonsterRoom(10, 4608, 4606, 4604, 4609, 4608, 4608, 4606, 4604));
            dungeon.Rooms.Add(174071808, new MonsterRoom(10));

            /* Boyau du Père Ver */

            dungeon = DungeonRecord.GetDungeon(102);
            /* Morsquale,Masticroc,Cycloporth,Trémorse,Trémorse,Morsquale,Masticroc,Cycloporth*/
            dungeon.Rooms.Add(176030208, new MonsterRoom(10, 4730, 4729, 4731, 4728, 4728, 4730, 4729, 4731));
            /* Pikténia,Trémorse,Masticroc,Cycloporth,Cycloporth,Pikténia,Trémorse,Masticroc*/
            dungeon.Rooms.Add(175899136, new MonsterRoom(10, 4727, 4728, 4729, 4731, 4731, 4727, 4728, 4729));
            /* Cycloporth,Pikténia,Morsquale,Masticroc,Masticroc,Cycloporth,Pikténia,Morsquale*/
            dungeon.Rooms.Add(175900160, new MonsterRoom(10, 4731, 4727, 4730, 4729, 4729, 4731, 4727, 4730));
            /* Masticroc,Cycloporth,Trémorse,Morsquale,Morsquale,Masticroc,Cycloporth,Trémorse*/
            dungeon.Rooms.Add(175901184, new MonsterRoom(10, 4729, 4731, 4728, 4730, 4730, 4729, 4731, 4728));
            /* Père Ver,Pikténia,Morsquale,Trémorse,Pikténia,Pikténia,Trémorse,Morsquale*/
            dungeon.Rooms.Add(175902208, new MonsterRoom(10, 4726, 4727, 4730, 4728, 4727, 4727, 4728, 4730));
            dungeon.Rooms.Add(175903232, new MonsterRoom(10));

            /* Chapiteau des Magik Riktus */

            dungeon = DungeonRecord.GetDungeon(105);
            /* Pirolienne,Roukouto,Graboule,Tivelo,Graboule,Roukouto,Pirolienne,Tivelo*/
            dungeon.Rooms.Add(181665792, new MonsterRoom(10, 4863, 4864, 4865, 4862, 4865, 4864, 4863, 4862));
            /* Pirolienne,Roukouto,Bozoteur,Tivelo,Tivelo,Bozoteur,Roukouto,Pirolienne*/
            dungeon.Rooms.Add(181666816, new MonsterRoom(10, 4863, 4864, 4861, 4862, 4862, 4861, 4864, 4863));
            /* Pirolienne,Graboule,Bozoteur,Tivelo,Pirolienne,Tivelo,Graboule,Bozoteur*/
            dungeon.Rooms.Add(181667840, new MonsterRoom(10, 4863, 4865, 4861, 4862, 4863, 4862, 4865, 4861));
            /* Pirolienne,Roukouto,Graboule,Tivelo,Roukouto,Pirolienne,Tivelo,Graboule*/
            dungeon.Rooms.Add(181668864, new MonsterRoom(10, 4863, 4864, 4865, 4862, 4864, 4863, 4862, 4865));
            /* Choudini,Bozoteur,Roukouto,Graboule,Bozoteur,Bozoteur,Graboule,Roukouto*/
            dungeon.Rooms.Add(181669888, new MonsterRoom(10, 4860, 4861, 4864, 4865, 4861, 4861, 4865, 4864));
            dungeon.Rooms.Add(181670912, new MonsterRoom(10));

            /* Brasserie du roi Dazak */

            dungeon = DungeonRecord.GetDungeon(110);
            /* Kasrok,Kasrok,Tanklume,Boufbos,Vatenbière,Kasrok,Boufbos,Tanklume*/
            dungeon.Rooms.Add(195035136, new MonsterRoom(10, 5317, 5317, 5314, 5315, 5318, 5317, 5315, 5314));
            /* Vatenbière,Vatenbière,Tanklume,Barbélier,Vatenbière,Barbélier,Tanklume,Kasrok*/
            dungeon.Rooms.Add(195036160, new MonsterRoom(10, 5318, 5318, 5314, 5316, 5318, 5316, 5314, 5317));
            /* Barbélier,Barbélier,Tanklume,Boufbos,Barbélier,Boufbos,Tanklume,Kasrok*/
            dungeon.Rooms.Add(195037184, new MonsterRoom(10, 5316, 5316, 5314, 5315, 5316, 5315, 5314, 5317));
            /* Tanklume,Tanklume,Boufbos,Barbélier,Tanklume,Barbélier,Boufbos,Kasrok*/
            dungeon.Rooms.Add(195038208, new MonsterRoom(10, 5314, 5314, 5315, 5316, 5314, 5316, 5315, 5317));
            /* Dazak Martegel,Tanklume,Kasrok,Boufbos,Barbélier,Barbélier,Tanklume,Vatenbière*/
            dungeon.Rooms.Add(195039232, new MonsterRoom(10, 5319, 5314, 5317, 5315, 5316, 5316, 5314, 5318));
            dungeon.Rooms.Add(195040256, new MonsterRoom(10));

            /* Arbre de Mort */

            dungeon = DungeonRecord.GetDungeon(117);
            /* Gangredogue,Gangredogue,Nheur'Gueule,Dolid,Gangredogue,Gangredogue,Nheur'Gueule,Dolid*/
            dungeon.Rooms.Add(202637312, new MonsterRoom(10, 6031, 6031, 6029, 6028, 6031, 6031, 6029, 6028));
            /* Dolid,Nheur'Gueule,Tentaclaque,Pistilangue,Dolid,Nheur'Gueule,Tentaclaque,Pistilangue*/
            dungeon.Rooms.Add(202638336, new MonsterRoom(10, 6028, 6029, 6030, 6027, 6028, 6029, 6030, 6027));
            /* Nheur'Gueule,Dolid,Gangredogue,Tentaclaque,Nheur'Gueule,Dolid,Gangredogue,Tentaclaque*/
            dungeon.Rooms.Add(202639360, new MonsterRoom(10, 6029, 6028, 6031, 6030, 6029, 6028, 6031, 6030));
            /* Pistilangue,Tentaclaque,Nheur'Gueule,Nheur'Gueule,Pistilangue,Tentaclaque,Nheur'Gueule,Nheur'Gueule*/
            dungeon.Rooms.Add(202640384, new MonsterRoom(10, 6027, 6030, 6029, 6029, 6027, 6030, 6029, 6029));
            /* Nheur'Gueule,Dolid,Pistilangue,Corruption,Nheur'Gueule,Dolid,Tentaclaque,Tentaclaque*/
            dungeon.Rooms.Add(202641408, new MonsterRoom(10, 6029, 6028, 6027, 6026, 6029, 6028, 6030, 6030));
            dungeon.Rooms.Add(202642432, new MonsterRoom(10));

            



            foreach (var value in DungeonRecord.GetDungeonRecords())
            {
                value.UpdateInstantElement();
            }

        }
    }
}

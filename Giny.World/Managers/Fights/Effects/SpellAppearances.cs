using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects
{
    public class SpellAppearances
    {
        [SpellAppearance(0)]
        public static void DefaultAppearence(Fighter fighter, ref ServerEntityLook look)
        {
            look = fighter.BaseLook.Clone();
        }
        [SpellAppearance(729)]
        public static void MomificationLook(Fighter fighter, ref ServerEntityLook look)
        {
            if (!look.IsRiding)
            {
                look.SetBones(113);
            }
            else
            {
                look.ActorLook.SetBones(1068);
            }
        }
        [SpellAppearance(106)]
        public static void CowardLook(Fighter fighter, ref ServerEntityLook look)
        {
            if (!look.IsRiding)
            {
                look.SetBones(1576);
            }

            if (fighter.Sex)
            {
                look.AddSkin(1450);
            }
            else
            {
                look.AddSkin(1449);
            }
        }

        [SpellAppearance(105)]
        public static void PsychopathLook(Fighter fighter, ref ServerEntityLook look)
        {
            if (!look.IsRiding)
                look.SetBones(1575);

            if (fighter.Sex)
            {
                look.AddSkin(1448);
            }
            else
            {
                look.AddSkin(1443);
            }
        }

        [SpellAppearance(1318)]
        public static void SentinelLook(Fighter fighter, ref ServerEntityLook look)
        {
            if (look.IsRiding)
            {
                look = look.ActorLook;
            }
            look.SetBones(4321);
        }
        [SpellAppearance(1260)]
        public static void OuginakDogLook(Fighter fighter, ref ServerEntityLook look)
        {
            IEnumerable<int> colors = look.Colors;
            look = EntityLookManager.Instance.CreateLookFromBones(3906, 150);
            look.SetColors(colors);
        }
        [SpellAppearance(1177)]
        public static void SadidaTree(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(3164, 80);
        }
        [SpellAppearance(1171)]
        public static void SadidaLifeTree(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(3166, 80);
        }
        /*
         * Scaphandre
         */
        [SpellAppearance(1035)]
        public static void SteamerDivingSuit(Fighter fighter, ref ServerEntityLook look)
        {
            look.AddSkin(1955);
        }

        [SpellAppearance(1326)]
        public static void BambouPandawa(Fighter fighter,ref ServerEntityLook look)
        {
            look.SetBones(4576);
        }
        [SpellAppearance(667)]
        public static void DrunkedPandawa(Fighter fighter, ref ServerEntityLook look)
        {
            if (look.IsRiding)
            {
                look.SetBones(1084);
            }
            else
            {
                look.SetBones(44);
            }
        }
        [SpellAppearance(1335)]
        public static void OsamodasToad(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(4811, 110);
        }
        [SpellAppearance(1236)]
        public static void OsamodasBouftou(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(3670, 60);
        }
        [SpellAppearance(1235)]
        public static void OsamodasTofu(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(3669, 130);
        }
        [SpellAppearance(1234)]
        public static void OsamodasDragon(Fighter fighter, ref ServerEntityLook look)
        {
            look = EntityLookManager.Instance.CreateLookFromBones(3716, 150);
        }
    }
}

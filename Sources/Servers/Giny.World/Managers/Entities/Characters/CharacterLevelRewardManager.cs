using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters
{
    public class CharacterLevelRewardManager : Singleton<CharacterLevelRewardManager>
    {
        public const byte EMOTE_100 = 22;

        public const byte EMOTE_OMEGA_100 = 171;
        public const byte EMOTE_OMEGA_200 = 172;
        public const byte EMOTE_OMEGA_300 = 173;
        public const byte EMOTE_OMEGA_400 = 174;
        public const byte EMOTE_OMEGA_500 = 175;

        public const byte ORNAMENT_100 = 13;
        public const byte ORNAMENT_160 = 14;
        public const byte ORNAMENT_200 = 15;

        public const byte ORNAMENT_OMEGA_25 = 111;
        public const byte ORNAMENT_OMEGA_50 = 112;
        public const byte ORNAMENT_OMEGA_75 = 113;
        public const byte ORNAMENT_OMEGA_125 = 114;
        public const byte ORNAMENT_OMEGA_150 = 115;
        public const byte ORNAMENT_OMEGA_175 = 116;
        public const byte ORNAMENT_OMEGA_225 = 117;
        public const byte ORNAMENT_OMEGA_250 = 118;
        public const byte ORNAMENT_OMEGA_275 = 119;
        public const byte ORNAMENT_OMEGA_325 = 120;
        public const byte ORNAMENT_OMEGA_350 = 121;
        public const byte ORNAMENT_OMEGA_375 = 122;
        public const byte ORNAMENT_OMEGA_425 = 123;
        public const byte ORNAMENT_OMEGA_450 = 124;
        public const byte ORNAMENT_OMEGA_475 = 125;


        public void OnCharacterLevelUp(Character character, short oldLevel, short newLevel)
        {
            if (oldLevel < 100 && newLevel >= 100)
            {
                character.Stats.ActionPoints.Base += 1;
            }

            AddRewardEmote(character, oldLevel, newLevel, 100, EMOTE_100);
            AddRewardOrnament(character, oldLevel, newLevel, 100, ORNAMENT_100);

            AddRewardOrnament(character, oldLevel, newLevel, 160, ORNAMENT_160);
            AddRewardOrnament(character, oldLevel, newLevel, 200, ORNAMENT_200);

            AddRewardEmote(character, oldLevel, newLevel, 300, EMOTE_OMEGA_100);
            AddRewardEmote(character, oldLevel, newLevel, 400, EMOTE_OMEGA_200);
            AddRewardEmote(character, oldLevel, newLevel, 500, EMOTE_OMEGA_300);
            AddRewardEmote(character, oldLevel, newLevel, 600, EMOTE_OMEGA_400);
            AddRewardEmote(character, oldLevel, newLevel, 700, EMOTE_OMEGA_500);


            AddRewardOrnament(character, oldLevel, newLevel, 225, ORNAMENT_OMEGA_25);
            AddRewardOrnament(character, oldLevel, newLevel, 250, ORNAMENT_OMEGA_50);
            AddRewardOrnament(character, oldLevel, newLevel, 275, ORNAMENT_OMEGA_75);

            AddRewardOrnament(character, oldLevel, newLevel, 325, ORNAMENT_OMEGA_125);
            AddRewardOrnament(character, oldLevel, newLevel, 350, ORNAMENT_OMEGA_150);
            AddRewardOrnament(character, oldLevel, newLevel, 375, ORNAMENT_OMEGA_175);

            AddRewardOrnament(character, oldLevel, newLevel, 425, ORNAMENT_OMEGA_225);
            AddRewardOrnament(character, oldLevel, newLevel, 450, ORNAMENT_OMEGA_250);
            AddRewardOrnament(character, oldLevel, newLevel, 475, ORNAMENT_OMEGA_275);

        }

        private void AddRewardEmote(Character character, short oldLevel, short newLevel, short level, byte value)
        {
            if (oldLevel < level && newLevel >= level)
            {
                character.LearnEmote(value);
            }
        }
        private void AddRewardOrnament(Character character, short oldLevel, short newLevel, short level, byte value)
        {
            if (oldLevel < level && newLevel >= level)
            {
                character.LearnOrnament(value, character.CharacterLoadingComplete);
            }
        }
    }
}

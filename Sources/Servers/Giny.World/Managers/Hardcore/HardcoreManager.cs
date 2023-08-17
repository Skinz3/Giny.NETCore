using Giny.Core.DesignPattern;
using Giny.IO.D2OClasses;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Achievements;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Spells;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Hardcore
{
    /// </summary>
    public class HardcoreManager : Singleton<HardcoreManager>
    {
        /// <summary>
        /// Put in BreedRecord, and patch in DatabasePatcher module.
        /// </summary>
        private Dictionary<BreedEnum, short> GraveBones = new Dictionary<BreedEnum, short>()
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
            { BreedEnum.Huppermage, 0 }, // ?
            { BreedEnum.Ouginak, 0 }, // ?
            { BreedEnum.Forgelance, 0 }, // ?
        };

        private const int ReplayLevel = 1;

        public void OnCharacterLooseFight(CharacterFighter fighter)
        {
            GameServerTypeEnum serverType = WorldServer.Instance.GetServerType();

            if (serverType != GameServerTypeEnum.SERVER_TYPE_EPIC
                && serverType != GameServerTypeEnum.SERVER_TYPE_HARDCORE)
            {
                return;
            }

            if (!IsFightAppliable(fighter.Fight, serverType))
            {
                return;
            }


            Character character = fighter.Character;

            KillCharacter(character);
        }

        private bool IsFightAppliable(Fight fight, GameServerTypeEnum serverType)
        {
            if (serverType == GameServerTypeEnum.SERVER_TYPE_EPIC)
            {
                return fight is FightPvM;
            }
            else
            {
                /*
                 * Not implemented
                 */
                return false;
            }
        }
        private void KillCharacter(Character character)
        {
            short bonesId = GraveBones[character.Breed.BreedEnum];

            character.Record.ContextualLook = EntityLookManager.Instance.CreateLookFromBones(bonesId);

            character.Record.HardcoreInformations.DeathState = HardcoreOrEpicDeathStateEnum.DEATH_STATE_DEAD;

            character.Record.HardcoreInformations.DeathCount++;

            if (character.Level > character.Record.HardcoreInformations.DeathMaxLevel)
            {
                character.Record.HardcoreInformations.DeathMaxLevel = character.Level;
            }


            character.Client.Send(new GameRolePlayGameOverMessage());
        }


        public void ReplayCharacter(CharacterRecord record)
        {
            long experience = ExperienceManager.Instance.GetCharacterXPForLevel(ReplayLevel);

            record.HardcoreInformations.DeathState = HardcoreOrEpicDeathStateEnum.DEATH_STATE_ALIVE;

            record.Experience = experience;
            record.Achievements = new List<CharacterAchievement>();
            record.ActiveOrnamentId = 0;
            record.ActiveTitleId = 0;
            record.ContextualLook = null;
            record.Spells = new List<CharacterSpell>();
            record.Stats = EntityStats.New(ReplayLevel);
            record.Jobs = CharacterJob.New();
            record.MapId = ConfigFile.Instance.SpawnMapId;
            record.CellId = ConfigFile.Instance.SpawnCellId;

            CharacterItemRecord.RemoveCharacterItems(record.Id);

            record.UpdateElement();
        }
    }
}

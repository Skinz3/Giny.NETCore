using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
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
using Giny.World.Managers.Shortcuts;
using Giny.World.Managers.Spells;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using Giny.World.Records.Breeds;
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
            character.Inventory.UnequipAll();

            character.Record.ContextualLook = EntityLookManager.Instance.CreateLookFromBones(character.Breed.GraveBonesId);

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
            record.Shortcuts = new List<CharacterShortcut>();
            record.Stats = EntityStats.New(ReplayLevel);
            record.Jobs = CharacterJob.New();
            record.MapId = ConfigManager<WorldConfig>.Instance.SpawnMapId;
            record.CellId = ConfigManager<WorldConfig>.Instance.SpawnCellId;

            CharacterItemRecord.RemoveCharacterItems(record.Id);

            record.UpdateElement();
        }

    }
}

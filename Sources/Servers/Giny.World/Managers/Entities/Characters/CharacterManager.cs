using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Messages;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Guilds;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Breeds;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private static readonly Regex CharacterNameRule = new Regex("^[A-Z][a-z]{2,9}(?:-[A-Za-z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled);

        public CharacterCreationResult CanCreateCharacter(CharacterCreationRequestMessage message, WorldClient client)
        {
            if (WorldServer.Instance.GetServerStatus() != ServerStatusEnum.ONLINE || !IPCManager.Instance.Connected || client.InGame)
            {
                return new CharacterCreationResult(CharacterCreationResultEnum.ERR_NO_REASON);
            }
            if (!ConfigManager<WorldConfig>.Instance.AllowedBreeds.Contains((PlayableBreedEnum)message.breed))
            {
                return new CharacterCreationResult(CharacterCreationResultEnum.ERR_NOT_ALLOWED);
            }
            if (client.Characters.Count >= client.Account.CharacterSlots)
            {
                return new CharacterCreationResult(CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS);
            }
            if (CharacterRecord.NameExist(message.name))
            {
                return new CharacterCreationResult(CharacterCreationResultEnum.ERR_INVALID_NAME, (byte)NameComplianceResultEnum.ERROR_NAME_ALREADY_EXISTS);
            }
            if (!CharacterNameRule.IsMatch(message.name))
            {
                return new CharacterCreationResult(CharacterCreationResultEnum.ERR_INVALID_NAME, (byte)NameComplianceResultEnum.ERROR_BAD_CHAR);
            }

            return new CharacterCreationResult(CharacterCreationResultEnum.OK);
        }
        [Annotation("constant checking")]
        public void DeleteCharacter(CharacterRecord character)
        {
            character.RemoveNow();

            CharacterItemRecord.RemoveCharacterItems(character.Id);

            if (character.GuildId != 0)
            {
                GuildsManager.Instance.OnCharacterDeleted(character);
            }
        }
        public CharacterRecord CreateCharacter(long id, string name, int accountId, byte breedId, bool sex, short cosmeticId, int[] colors)
        {
            BreedRecord breedRecord = BreedRecord.GetBreed(breedId);
            ServerEntityLook look = BreedManager.Instance.GetBreedLook(breedRecord, sex, cosmeticId, colors);
            CharacterRecord record = CharacterRecord.Create(id, name, accountId, look, breedId, cosmeticId, sex);
            Logger.Write("Character " + record.Name + " created", Channels.Log);
            return record;



        }
    }
}

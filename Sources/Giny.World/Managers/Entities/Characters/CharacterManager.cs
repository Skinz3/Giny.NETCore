using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Messages;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Merchants;
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
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private const string VOWELS = "aeiouy";
        private const string CONSONANTS = "bcdfghjklmnpqrstvwxz";

        public static char[] UnauthorizedNameContent = new char[] { '(', ')', '[', '{', '}', ']', '\'', ':', '<', '>', '?', '!' };

        private char RandomVowel(Random rand)
        {
            return VOWELS[rand.Next(0, VOWELS.Length - 1)];
        }
        private char RandomConsonant(Random rand)
        {
            return CONSONANTS[rand.Next(0, CONSONANTS.Length - 1)];
        }
        private char GetChar(bool vowel, Random rand)
        {
            return vowel ? RandomVowel(rand) : RandomConsonant(rand);
        }
        public string GenerateName()
        {
            var rand = new Random();
            var namelen = rand.Next(5, 10);
            var name = string.Empty;

            var vowel = rand.Next(0, 2) == 0;
            name += GetChar(vowel, rand).ToString(CultureInfo.InvariantCulture).ToUpper();
            vowel = !vowel;

            for (var i = 0; i < namelen - 1; i++)
            {
                name += GetChar(vowel, rand);
                vowel = !vowel;
            }
            return name;
        }
        public CharacterCreationResultEnum CanCreateCharacter(CharacterCreationRequestMessage message, WorldClient client)
        {
            if (WorldServer.Instance.Status != ServerStatusEnum.ONLINE || !IPCManager.Instance.Connected || client.InGame)
            {
                return CharacterCreationResultEnum.ERR_NO_REASON;
            }
            if (client.Characters.Count >= client.Account.CharacterSlots)
            {
                return CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS;
            }
            if (CharacterRecord.NameExist(message.name))
            {
                client.Send(new CharacterCreationResultMessage((byte)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                return CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS;
            }
            if (client.Account.Role < ServerRoleEnum.Moderator)
            {
                foreach (var value in message.name)
                {
                    if (UnauthorizedNameContent.Contains(value))
                    {
                        return CharacterCreationResultEnum.ERR_INVALID_NAME;
                    }
                }
            }
            if (message.name.Contains(" "))
            {
                return CharacterCreationResultEnum.ERR_INVALID_NAME;
            }

            return CharacterCreationResultEnum.OK;
        }
        [WIP("constant checking")]
        public void DeleteCharacter(CharacterRecord character)
        {
            character.RemoveInstantElement();

            MerchantRecord record = MerchantRecord.RemoveMerchant(character.Id);

            if (record != null)
            {
                MerchantsManager.Instance.RemoveMerchant(record);
            }

            MerchantItemRecord.RemoveMerchantItems(character.Id);
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

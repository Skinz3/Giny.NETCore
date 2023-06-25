using Giny.Core;
using Giny.Core.Extensions;
using Giny.Core.Network.Messages;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Characters.HumanOptions;
using Giny.World.Managers.Entities.Look;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Breeds;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Approach
{
    class CharacterHandler
    {
        [MessageHandler]
        public static void HandleCharacterNameSuggestionRequestMessage(CharacterNameSuggestionRequestMessage message, WorldClient client)
        {
            client.Send(new CharacterNameSuggestionSuccessMessage(CharacterManager.Instance.GenerateName()));
        }
        [MessageHandler]
        public static void HandleCharacterCreationRequestMessage(CharacterCreationRequestMessage message, WorldClient client)
        {
            var canCreateCharacter = CharacterManager.Instance.CanCreateCharacter(message, client);

            if (canCreateCharacter != CharacterCreationResultEnum.OK)
            {
                client.Send(new CharacterCreationResultMessage((byte)canCreateCharacter));
                return;
            }

            long nextId = CharacterRecord.NextId();

            IPCManager.Instance.SendRequest(new IPCCharacterCreationRequestMessage(client.Account.Id, nextId),
            delegate (IPCCharacterCreationResultMessage result)
            {
                if (!result.succes)
                {
                    client.Send(new CharacterCreationResultMessage((byte)CharacterCreationResultEnum.ERR_NO_REASON));
                    return;
                }

                client.Send(new CharacterCreationResultMessage((byte)CharacterCreationResultEnum.OK));
                CreateCharacter(message, client, nextId);
            },
            delegate ()
            {
                client.Send(new CharacterCreationResultMessage((byte)CharacterCreationResultEnum.ERR_NO_REASON));
            });
        }
        static void CreateCharacter(CharacterCreationRequestMessage message, WorldClient client, long id)
        {
            CharacterRecord newCharacter = CharacterManager.Instance.CreateCharacter(id, message.name, client.Account.Id, message.breed, message.sex, message.cosmeticId, message.colors);

            client.Character = new Character(client, newCharacter);
            client.Character.OnLevelChanged(1, (short)(client.Character.Level - 1));
            BreedManager.Instance.LearnBreedSpells(client.Character);
            newCharacter.AddInstantElement();
            client.Character.JustCreated = true;
            ProcessSelection(client);
        }
        [MessageHandler]
        public static void HandleCharacterListRequestMessage(CharactersListRequestMessage message, WorldClient client)
        {
            client.SendCharactersList();

            if (client.Characters.Any(x => x.IsInFight))
            {
                client.Character = new Character(client, client.Characters.First(x => x.IsInFight));
                ProcessSelection(client);
            }
        }
        [MessageHandler]
        public static void HandleCharacterFirstSelectionMessage(CharacterFirstSelectionMessage message, WorldClient client) // TODO ADD TUTORIAL EFFECTS
        {
            client.Send(new CharacterSelectedErrorMessage());
            client.SendCharactersList();
            return;

            var record = client.GetCharacter(message.id);

            if (record != null)
            {
                client.Character = new Character(client, record);
                ProcessSelection(client);
            }
            else
            {
                client.SendCharactersList();
            }
        }

        [MessageHandler]
        public static void HandleCharacterCanBeCreatedRequestMessage(CharacterCanBeCreatedRequestMessage message, WorldClient client)
        {
            client.Send(new CharacterCanBeCreatedResultMessage(client.Characters.Count < client.Account.CharacterSlots));
        }

        [MessageHandler]
        public static void HandleCharacterDeletionRequestMessage(CharacterDeletionRequestMessage message, WorldClient client)
        {
            var character = client.GetCharacter(message.characterId);

            if (WorldServer.Instance.Status != ServerStatusEnum.ONLINE || character == null || !IPCManager.Instance.Connected || client.InGame)
            {
                client.Send(new CharacterDeletionErrorMessage((byte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
                return;
            }

            IPCManager.Instance.SendRequest(new IPCCharacterDeletionRequestMessage(client.Account.Id, character.Id),
            delegate (IPCCharacterDeletionResultMessage result)
            {
                if (!result.succes)
                {
                    client.Send(new CharacterDeletionErrorMessage((byte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
                    return;
                }

                client.Characters.Remove(character);
                CharacterManager.Instance.DeleteCharacter(character);
                client.Send(new CharacterCreationResultMessage((byte)CharacterCreationResultEnum.OK));
                client.SendCharactersList();
            },
            delegate ()
            {
                client.Send(new CharacterDeletionErrorMessage((byte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
            });
        }
        [MessageHandler]
        public static void HandleCharacterSelectionMessage(CharacterSelectionMessage message, WorldClient client)
        {
            CharacterRecord character = client.GetCharacter(message.id);

            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            client.Character = new Character(client, character);
            ProcessSelection(client);
        }
        private static void ProcessSelection(WorldClient client)
        {
            client.Send(new CharacterSelectedSuccessMessage(client.Character.Record.GetCharacterBaseInformations(),
               false));
            client.Send(new NotificationListMessage(new int[] { 2147483647 }));
            client.Send(new CharacterCapabilitiesMessage(4095));
            client.Send(new SequenceNumberRequestMessage());

            /*
             * -- Do not change order --
             */
            client.Character.RefreshJobs();
            client.Character.RefreshSpells();
            client.Character.RefreshGuild();
            client.Character.RefreshEmotes();
            client.Character.Inventory.Refresh();
            client.Character.RefreshShortcuts();
            client.Character.CreateHumanOptions();
            client.Character.RefreshArenaInfos();
            client.Character.SendServerExperienceModificator();
            client.Character.CheckMerchantState();
            client.Character.OnCharacterLoadingComplete();
        }

    }
}

using Giny.Core;
using Giny.Core.Extensions;
using Giny.Core.IO.Configuration;
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
using Giny.World.Managers.Hardcore;
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
            client.Send(new CharacterNameSuggestionSuccessMessage(StringExtensions.GenerateName()));
        }
        [MessageHandler]
        public static void HandleCharacterCreationRequestMessage(CharacterCreationRequestMessage message, WorldClient client)
        {
            var canCreateCharacter = CharacterManager.Instance.CanCreateCharacter(message, client);

            if (client.Account.Role > ServerRoleEnum.Player)
            {
                message.name = $"[{message.name}]";
            }

            if (canCreateCharacter.Result != CharacterCreationResultEnum.OK)
            {
                client.Send(new CharacterCreationResultMessage((byte)canCreateCharacter.Result, canCreateCharacter.Reason));
                return;
            }


            long nextId = CharacterRecord.NextId();

            IPCManager.Instance.SendRequest(new IPCCharacterCreationRequestMessage(client.Account.Id, nextId),
             (IPCCharacterCreationResultMessage result) =>
            {
                if (!result.succes)
                {
                    client.Send(new CharacterCreationResultMessage(0, (byte)CharacterCreationResultEnum.ERR_NO_REASON));
                    return;
                }

                client.Send(new CharacterCreationResultMessage(0, (byte)CharacterCreationResultEnum.OK));
                CreateCharacter(message, client, nextId);
            },
            () =>
            {
                client.Send(new CharacterCreationResultMessage(0, (byte)CharacterCreationResultEnum.ERR_NO_REASON));
            });
        }
        static void CreateCharacter(CharacterCreationRequestMessage message, WorldClient client, long id)
        {
            CharacterRecord newCharacter = CharacterManager.Instance.CreateCharacter(id, message.name, client.Account.Id, message.breed, message.sex, message.cosmeticId, message.colors);



            client.Character = new Character(client, newCharacter);
            client.Character.OnLevelChanged(1, (short)(client.Character.Level - 1));
            BreedManager.Instance.LearnBreedSpells(client.Character);
            newCharacter.AddInstantElement();
            client.Character.JustCreatedOrReplayed = true;
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
        public static void HandleCharacterDeletionPrepareRequestMessage(CharacterDeletionPrepareRequestMessage message, WorldClient client)
        {
            var character = client.GetCharacter(message.characterId);

            if (character != null)
            {
                client.Send(new CharacterDeletionPrepareMessage(character.Id, character.Name, string.Empty, false));
            }
        }
        [MessageHandler]
        public static void HandleCharacterDeletionRequestMessage(CharacterDeletionRequestMessage message, WorldClient client)
        {
            var character = client.GetCharacter(message.characterId);

            if (WorldServer.Instance.GetServerStatus() != ServerStatusEnum.ONLINE || character == null || !IPCManager.Instance.Connected || client.InGame)
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
            if (WorldServer.Instance.GetServerStatus() != ServerStatusEnum.ONLINE || !IPCManager.Instance.Connected || client.InGame)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            CharacterRecord record = client.GetCharacter(message.id);

            if (record == null || record.HardcoreInformations.DeathState == HardcoreOrEpicDeathStateEnum.DEATH_STATE_DEAD)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            if (!ConfigManager<WorldConfig>.Instance.AllowedBreeds.Contains((PlayableBreedEnum)record.BreedId))
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            client.Character = new Character(client, record);
            ProcessSelection(client);
        }

        [MessageHandler]
        public static void HandleCharacterReplayRequest(CharacterReplayRequestMessage message, WorldClient client)
        {
            if (WorldServer.Instance.GetServerStatus() != ServerStatusEnum.ONLINE || !IPCManager.Instance.Connected || client.InGame)
            {
                return;
            }

            CharacterRecord record = client.GetCharacter(message.characterId);

            if (record == null)
            {
                return;
            }

            if (record.HardcoreInformations.DeathState != HardcoreOrEpicDeathStateEnum.DEATH_STATE_DEAD)
            {
                return;
            }

            if (!WorldServer.Instance.IsEpicOrHardcore())
            {
                return;
            }

            HardcoreManager.Instance.ReplayCharacter(record);


            client.Character = new Character(client, record);
            client.Character.JustCreatedOrReplayed = true;
            client.Character.OnLevelChanged(1, (short)(client.Character.Level - 1));
            BreedManager.Instance.LearnBreedSpells(client.Character);

            ProcessSelection(client);
        }
        private static void ProcessSelection(WorldClient client)
        {

            client.Send(new NotificationListMessage(new int[] { 2147483647 }));


            client.Send(new CharacterSelectedSuccessMessage(client.Character.Record.GetCharacterBaseInformations(false),
               false));

            client.Send(new CharacterCapabilitiesMessage(4095));
            client.Send(new SequenceNumberRequestMessage());



            /*
             * -- Do not change order --
             */
            client.Character.RefreshAchievements();
            client.Character.RefreshJobs();
            client.Character.RefreshSpells();
            client.Character.RefreshGuild();
            client.Character.RefreshEmotes();
            client.Character.Inventory.Refresh();
            client.Character.RefreshShortcuts();
            client.Character.CreateHumanOptions();
            client.Character.RefreshArenaInfos();
            client.Character.SendKnownZaapList();
            client.Character.SendServerExperienceModificator();
            client.Character.OnCharacterLoadingComplete();
        }

    }
}

using Giny.Auth.Network;
using Giny.Auth.Records;
using Giny.Auth.Network;
using Giny.Core.Network.Messages;
using Giny.Protocol.IPC.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Auth.Network.IPC;
using Giny.Protocol.IPC.Types;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.Auth.Managers;
using Giny.ORM;
using Giny.Core;

namespace Giny.Auth.Handlers
{
    public class IPCHandler
    {
        [MessageHandler]
        public static void HandleHandshakeMessage(HandshakeMessage message, IPCClient client)
        {
            if (IPCServer.Instance.AddClient(message.serverId, client))
            {
                client.WorldServerRecord = WorldServerRecord.GetWorldServer(message.serverId);
            }
        }
        public static void OnServerStatusUpdated(WorldServerRecord server)
        {
            foreach (var authClient in AuthServer.Instance.GetClients()) // ServerStatusUpdateMessage instead
            {
                authClient.Send(new ServerStatusUpdateMessage(server.GetServerInformations(authClient)));
            }
        }
        [MessageHandler]
        public static void HandleUpdateServerStatusMessage(Protocol.IPC.Messages.IPCServerStatusUpdateMessage message, IPCClient client)
        {
            if (client.WorldServerRecord != null)
            {
                client.WorldServerRecord.Status = message.status;
                OnServerStatusUpdated(client.WorldServerRecord);
            }
        }
        [MessageHandler]
        public static void HandleAccountRequestMessage(AccountRequestMessage message, IPCClient client)
        {
            AccountRecord accountRecord = TicketsManager.Instance.RetreiveAccount(message.ticket);

            AccountMessage resultMessage = null;

            if (accountRecord == null)
            {
                resultMessage = new AccountMessage(null);
            }
            else
            {
                resultMessage = new AccountMessage(accountRecord.ToAccount());
            }

            client.SendIPCAnswer(resultMessage, message);
        }

        [MessageHandler]
        public static void HandleIPCCharacterCreationRequestMessage(IPCCharacterCreationRequestMessage message, IPCClient client)
        {
            if (WorldCharacterRecord.Exist(message.characterId, client.WorldServerRecord.Id))
            {
                client.SendIPCAnswer(new IPCCharacterCreationResultMessage(false), message);
                return;
            }

            WorldCharacterRecord record = new WorldCharacterRecord()
            {
                AccountId = message.accountId,
                CharacterId = message.characterId,
                ServerId = client.WorldServerRecord.Id,
                Id = WorldCharacterRecord.NextId(),
            };
            record.AddNow();

            client.SendIPCAnswer(new IPCCharacterCreationResultMessage(true), message);
        }
        [MessageHandler]
        public static void HandleCharacterDeletionRequestMessage(IPCCharacterDeletionRequestMessage message, IPCClient client)
        {
            if (!WorldCharacterRecord.Exist(message.characterId, client.WorldServerRecord.Id))
            {
                client.SendIPCAnswer(new IPCCharacterDeletionResultMessage(false), message);
            }

            var worldCharacterRecord = WorldCharacterRecord.Get(message.accountId, message.characterId);
            worldCharacterRecord.RemoveNow();

            client.SendIPCAnswer(new IPCCharacterDeletionResultMessage(true), message);
        }

        [MessageHandler]
        public static void HandleResetWorldRequestMessage(ResetWorldRequestMessage message, IPCClient client)
        {
            try
            {
                WorldCharacterRecord.Get(client.WorldServerRecord.Id).RemoveNow();
                client.SendIPCAnswer(new ResetWorldResultMessage(true), message);
            }
            catch (Exception ex)
            {
                Logger.Write("Unable to reset world :" + ex.Message, Channels.Critical);
                client.SendIPCAnswer(new ResetWorldResultMessage(false), message);
            }
        }
    }
}
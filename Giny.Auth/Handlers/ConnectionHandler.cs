using Giny.Auth.Managers;
using Giny.Auth.Network;
using Giny.Auth.Network.IPC;
using Giny.Auth.Records;
using Giny.Core;
using Giny.Core.Cryptography;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Core.Network.Messages;
using Giny.Core.Time;
using Giny.ORM;
using Giny.Protocol;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.Auth.Handlers
{
    class ConnectionHandler
    {
        public const int MAX_NICKNAME_LENGTH = 15;

        [MessageHandler]
        public static void HandleServerSelectionMessage(ServerSelectionMessage message, AuthClient client)
        {
            ProcessServerSelection(client, message.serverId);
        }
        private static void ProcessServerSelection(AuthClient client, short serverId)
        {
            if (client.Account.LastSelectedServerId != serverId)
            {
                client.Account.LastSelectedServerId = serverId;
                client.Account.UpdateInstantElement();
            }

            var ipcClient = IPCServer.Instance.GetClient(serverId);

            if (ipcClient == null)
            {
                client.OnSelectedServerRefused(serverId, ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_DUE_TO_STATUS,
                     ServerStatusEnum.OFFLINE);
                client.SendServerList();
                return;
            }
            else if (ipcClient.WorldServerRecord.Status != ServerStatusEnum.ONLINE)
            {
                client.OnSelectedServerRefused(ipcClient.WorldServerRecord.Id, ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_DUE_TO_STATUS,
                       ipcClient.WorldServerRecord.Status);
                client.SendServerList();
                return;

            }
            if (ipcClient.WorldServerRecord.MonoAccount)
            {
                IPCServer.Instance.SendRequest(ipcClient, new IsIpConnectedRequestMessage(client.Ip),
                delegate (IsIpConnectedMessage ipcMessage)
                {
                    if (ipcMessage.connected)
                    {
                        client.OnSelectedServerRefused(ipcClient.WorldServerRecord.Id, ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_MONOACCOUNT_ONLY,
                      ServerStatusEnum.FULL);
                        client.SendServerList();
                        return;
                    }
                    else
                    {
                        SelectServer(client, ipcClient);
                    }
                },
                delegate ()
                {
                    client.OnSelectedServerRefused(ipcClient.WorldServerRecord.Id, ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_DUE_TO_STATUS,
                         ipcClient.WorldServerRecord.Status);
                    client.SendServerList();

                });
            }
            else
            {
                SelectServer(client, ipcClient);
            }


        }
        private static void SelectServer(AuthClient client, IPCClient worldServer)
        {
            client.GenerateTicket();
            TicketsManager.Instance.PushAccount(client.Ticket, client.Account);
            client.Send(new SelectedServerDataMessage(worldServer.WorldServerRecord.Id, worldServer.WorldServerRecord.Host,
                new short[] { worldServer.WorldServerRecord.Port }, true, client.EncryptTicket()));
            client.Disconnect();
        }
        [MessageHandler]
        public static void HandleNicknameChoiceRequestMessage(NicknameChoiceRequestMessage message, AuthClient client)
        {
            if (client.Account == null)
            {
                return;
            }
            if (client.Account.Username.ToLower() == message.nickname.ToLower())
            {
                client.OnNicknameRefusedMessage(NicknameRefusedReasonEnum.SAME_AS_LOGIN);
                return;
            }
            if (message.nickname.Length > MAX_NICKNAME_LENGTH || message.nickname == string.Empty ||
                !Regex.Match(message.nickname, @"^[a-zA-Z_$][a-zA-Z_$0-9]*$").Success)
            {
                client.OnNicknameRefusedMessage(NicknameRefusedReasonEnum.INVALID_NICK);
                return;
            }
            if (AccountRecord.NicknameExist(message.nickname))
            {
                client.OnNicknameRefusedMessage(NicknameRefusedReasonEnum.ALREADY_USED);
                return;
            }
            try
            {
                client.Account.Nickname = message.nickname;
                client.Account.UpdateInstantElement();
                client.Send(new NicknameAcceptedMessage());
            }
            catch (Exception ex)
            {
                Logger.Write("Unable to set Nickname to " + client.Account.Username + ": " + ex, Channels.Critical);
                client.OnNicknameRefusedMessage(NicknameRefusedReasonEnum.UNKNOWN_NICK_ERROR);
                return;
            }

            Login(client, false);
        }
        [MessageHandler]
        public static void HandleIdentificationMessage(IdentificationMessage message, AuthClient client)
        {
            client.IdentificationMessage = message;
            ConnectionQueue.AddToQueue(client);
        }

        public static void ProcessIdentification(AuthClient client)
        {
            string username = null;
            string password = null;

            using (BigEndianReader reader = new BigEndianReader(client.IdentificationMessage.credentials))
            {
                username = reader.ReadUTF();
                password = reader.ReadUTF();
                Logger.Write("User try to log in Username : " + username + " Password :" + password);
                client.AesKey = reader.ReadBytes(32);
            }

            AccountRecord account = AccountRecord.ReadAccount(username);

            if (account == null || account.Password != password)
            {
                client.OnIdentificationFailed(IdentificationFailureReasonEnum.WRONG_CREDENTIALS);
                client.Disconnect();
                return;
            }
            else if (account.Banned)
            {
                client.OnIdentificationFailed(IdentificationFailureReasonEnum.BANNED);
                client.Disconnect();
                return;
            }
            client.Account = account;
            client.Characters = WorldCharacterRecord.Get(client.Account.Id);

            if (string.IsNullOrEmpty(client.Account.Nickname))
            {
                client.Send(new NicknameRegistrationMessage());
                return;
            }

            Login(client, client.IdentificationMessage.autoconnect);
        }

        private static void Login(AuthClient client, bool autoconnect)
        {
            if (!client.Account.IPs.Contains(client.Ip))
            {
                client.Account.IPs.Add(client.Ip);
                client.Account.UpdateInstantElement();
            }

            AuthClient loggedClient = AuthServer.Instance.GetClient(client.Account.Id);

            bool wasConnected = false;

            if (loggedClient != null)
            {
                wasConnected = true;
                loggedClient.Disconnect();
            }

            if (client.Account.LastSelectedServerId != 0)
            {
                IPCClient worldServerClient = IPCServer.Instance.GetClient(client.Account.LastSelectedServerId);

                if (worldServerClient != null && worldServerClient.Connected)
                {
                    IPCServer.Instance.SendRequest(worldServerClient, new DisconnectClientRequestMessage(client.Account.Id),
                    delegate (DisconnectClientResultMessage message)
                    {
                        ProcessLoginServerStep(client, autoconnect, message.sucess || wasConnected);
                    },
                    delegate ()
                    {
                        client.OnIdentificationFailed(IdentificationFailureReasonEnum.SERVICE_UNAVAILABLE);

                    });
                }
                else
                {
                    ProcessLoginServerStep(client, autoconnect, wasConnected);
                }
            }
            else
            {
                ProcessLoginServerStep(client, autoconnect, wasConnected);
            }
        }
        private static void ProcessLoginServerStep(AuthClient client, bool autoconnect, bool wasConnected)
        {
            AuthServer.Instance.AddClient(client);

            client.OnIdentificationSuccess(wasConnected);

            if (!autoconnect)
            {
                client.SendServerList();
                return;
            }
            else
            {
                if (client.Account.LastSelectedServerId != 0 && IPCServer.Instance.IsWorldConnected(client.Account.LastSelectedServerId))
                {
                    ProcessServerSelection(client, client.Account.LastSelectedServerId);
                }
                else
                    client.SendServerList();
            }
        }
    }
}

using Giny.Core;
using Giny.Zaap.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap
{
    public class MessagesHandler
    {
        public static void Handle(ZaapClient client, ZaapMessage message)
        {
            switch (message)
            {
                case ConnectArgs connectArgs:
                    HandleConnectArgs(client, connectArgs);
                    break;
                case SettingsGet settingsGet:
                    HandleSettingsGet(client, settingsGet);
                    break;
                case UserInfoGet userInfoGet:
                    HandleUserInfoGet(client, userInfoGet);
                    break;
                case AuthGetGameToken authGetGameToken:
                    HandleAuthGetGameToken(client, authGetGameToken);
                    break;
                case ZaapMustUpdateGet zaapMustUpdateGet:
                    HandleZaapMustUpdateGet(client, zaapMustUpdateGet);
                    break;
                default:
                    Logger.Write("Unhandled message " + message.GetType().Name, Channels.Warning);
                    break;
            }
        }

        private static void HandleAuthGetGameToken(ZaapClient client, AuthGetGameToken message)
        {
            client.Send(new AuthGetGameTokenResult(client.Account.Password));
        }

        private static void HandleUserInfoGet(ZaapClient client, UserInfoGet message)
        {
            client.Send(new UserInfosGetResult(client.Account.Username));
        }

        private static void HandleSettingsGet(ZaapClient client, SettingsGet message)
        {
            string result = null;

            switch (message.Key)
            {
                case "autoConnectType":
                    result = "false";
                    break;
                case "language":
                    result = "fr";
                    break;
                case "connectionPort":
                    result = "443";
                    break;
                default:
                    break;
            }

            client.Send(new SettingsGetResult(result));
        }

        private static void HandleConnectArgs(ZaapClient client, ConnectArgs message)
        {
            client.InstanceId = message.InstanceId;
            client.Account = ZaapServer.Instance.AccountProvider.GetAccount(message.InstanceId);
            client.Send(new ConnectResult());
        }

        private static void HandleZaapMustUpdateGet(ZaapClient client, ZaapMustUpdateGet message)
        {
            client.Send(new ZaapMustUpdateGetResult(true));
        }
    }
}

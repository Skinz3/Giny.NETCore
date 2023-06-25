using Giny.Core;
using Giny.Zaap.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Protocol
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
                default:
                    Logger.Write("Unhandled message " + message.GetType().Name, Channels.Warning);
                    break;
            }
        }

        private static void HandleAuthGetGameToken(ZaapClient client, AuthGetGameToken message)
        {
            client.Send(new AuthGetGameTokenResult(client.Password));
        }

        private static void HandleUserInfoGet(ZaapClient client, UserInfoGet message)
        {
            client.Send(new UserInfosGetResult(client.Username));
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
            client.Send(new ConnectResult());
        }
    }
}

using Giny.Core.IO.Configuration;
using Giny.Core.Network;
using Giny.Zaap.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Uplauncher
{

    public class AuthApi
    {
        private static UplConfig Config => ConfigManager<UplConfig>.Instance;

        private static HttpClient HttpClient = new HttpClient();
        public static string GetRemoteVersion()
        {
            return Http.Get($"{Config.GetSelectedHost().GetApiUri()}/version/launcher");
        }
        public static async Task<WebAccount?> Authentificate(string username, string password)
        {
            dynamic request = new
            {
                Username = username,
                Password = password,
            };
            WebAccount? result = await Http.PostAsync<WebAccount?>($"{Config.GetSelectedHost().GetApiUri()}/account/auth", HttpClient, request);

            if (result != null)
            {
                result.Password = password;
            }

            return result;
        }

        public static async Task<WebAccount?> Register(string username, string password)
        {
            dynamic request = new
            {
                Username = username,
                Password = password,
            };
            WebAccount? result = await Http.PostAsync<WebAccount?>($"{Config.GetSelectedHost().GetApiUri()}/account/register", HttpClient, request);

            if (result != null)
            {
                result.Password = password;
            }

            return result;
        }
    }
}

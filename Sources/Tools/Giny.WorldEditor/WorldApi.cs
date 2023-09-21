using Giny.Core.IO.Configuration;
using Giny.Core.Network;
using Giny.World;
using Giny.WorldEditor.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor
{
    public class WorldApi
    {
        public const string Stats = "common/stats";

        public const string ReloadNpcs = "reload/npcs";


        private static string GetUrl(string relativeUrl)
        {
            return ConfigManager<WorldViewConfig>.Instance.WorldApiUrl + relativeUrl;
        }

        private static string? GetRequest(string relativeUrl)
        {
            try
            {
                var result = Http.Get(GetUrl(relativeUrl));
                return result;
            }
            catch
            {
                return null;
            }
        }
        public static bool SendReloadNpcs()
        {
            var result = GetRequest(ReloadNpcs);
            return result != null ? bool.Parse(result) : false;
        }
    }
}

using Giny.Core.IO;
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
    public class Stats
    {
        public int Online
        {
            get;
            set;
        }
        public int OnlineIps
        {
            get;
            set;
        }
        public int Peak
        {
            get;
            set;
        }
        public string Memory
        {
            get;
            set;
        }
        public int Fights
        {
            get;
            set;
        }
    }
    public class WorldApi
    {
        public const string StatsPath = "common/stats";

        public const string ReloadNpcsPath = "reload/npcs";

        public const string ReloadItemsPath = "reload/items";

        private static string GetUrl(string relativeUrl)
        {
            return ConfigManager<WorldViewConfig>.Instance.WorldApiUrl + relativeUrl;
        }

        public static Stats GetStats()
        {
            var result = GetRequest(StatsPath);
            return Json.Deserialize<Stats>(result);
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
        public static bool ReloadNpcs()
        {
            var result = GetRequest(ReloadNpcsPath);
            return result != null ? bool.Parse(result) : false;
        }
        public static bool ReloadItems()
        {
            var result = GetRequest(ReloadItemsPath);
            return result != null ? bool.Parse(result) : false;
        }

        public static bool Available()
        {
            try
            {
                Http.Get(ConfigManager<WorldViewConfig>.Instance.WorldApiUrl + StatsPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

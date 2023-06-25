using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Giny.World
{
    public class ConfigFile
    {
        public const string CONFIG_PATH = "config.json";

        public static ConfigFile Instance
        {
            get;
            private set;
        }
        public short ServerId
        {
            get;
            set;
        } = 1;
        public string Host
        {
            get;
            set;
        } = "127.0.0.1";

        public int Port
        {
            get;
            set;
        } = 5555;
        public string SQLHost
        {
            get;
            set;
        } = "127.0.0.1";

        public string SQLUser
        {
            get;
            set;
        } = "root";

        public string SQLPassword
        {
            get;
            set;
        } = string.Empty;

        public string SQLDBName
        {
            get;
            set;
        } = "giny_world";

        public string IPCHost
        {
            get;
            set;
        } = "127.0.0.1";

        public int IPCPort
        {
            get;
            set;
        } = 800;

        public string APIHost
        {
            get;
            set;
        } = "127.0.0.1";

        public int APIPort
        {
            get;
            set;
        } = 9000;

        public long SpawnMapId
        {
            get;
            set;
        } = 154010883;

        public short SpawnCellId
        {
            get;
            set;
        } = 400;

        public short MaxMerchantPerMap
        {
            get;
            set;
        } = 5;

        public short ApLimit
        {
            get;
            set;
        } = 12;

        public short MpLimit
        {
            get;
            set;
        } = 6;

        public short StartLevel
        {
            get;
            set;
        } = 1;

        public short StartAp
        {
            get;
            set;
        } = 6;

        public short StartMp
        {
            get;
            set;
        } = 3;

        public string WelcomeMessage
        {
            get;
            set;
        } = "Bienvenue sur le <b>serveur de test</b>. Nous sommes heureux de vous revoir.";

        public double JobRate
        {
            get;
            set;
        } = 1;

        public double DropRate
        {
            get;
            set;
        } = 1;
        public double XpRate
        {
            get;
            set;
        } = 1;

        public bool LogProtocol
        {
            get;
            set;
        } = true;

        [StartupInvoke("Config", StartupInvokePriority.Initial)]
        public static void Initialize()
        {
            if (File.Exists(CONFIG_PATH))
            {
                try
                {
                    Instance = Json.Deserialize<ConfigFile>(File.ReadAllText(CONFIG_PATH));
                }
                catch
                {
                    CreateConfig();
                }

            }
            else
            {
                CreateConfig();
            }
        }
        public static void CreateConfig()
        {
            Instance = new ConfigFile();
            Save();
            Logger.Write("Configuration file created!");
        }
        public static void Save()
        {
            File.WriteAllText(CONFIG_PATH, Json.Serialize(Instance));
        }

    }
}

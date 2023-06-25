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

namespace Giny.Auth
{
    public class ConfigFile
    {
        public const string CONFIG_PATH = "config.json";

        public static ConfigFile Instance
        {
            get;
            private set;
        }
        public string Host
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        
        public string SQLHost
        {
            get;
            set;
        }
        public string SQLUser
        {
            get;
            set;
        }
        public string SQLPassword
        {
            get;
            set;
        }
        public string SQLDBName
        {
            get;
            set;
        }
        public string IPCHost
        {
            get;
            set;
        }
        public int IPCPort
        {
            get;
            set;
        }
        public string APIHost
        {
            get;
            set;
        }
        public int APIPort
        {
            get;
            set;
        }

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
            Instance = Default();
            Save();
            Logger.Write("Configuration file created!", Channels.Info);
        }
        public static void Save()
        {
            File.WriteAllText(CONFIG_PATH, Json.Serialize(Instance));
        }

        public static ConfigFile Default()
        {
            return new ConfigFile()
            {
                Host = "127.0.0.1",
                Port = 443,
                SQLHost = "127.0.0.1",
                SQLDBName = "giny_auth",
                SQLPassword = "",
                SQLUser = "root",
                IPCHost = "127.0.0.1",
                IPCPort = 800,
                APIPort = 9001,
                APIHost = "127.0.0.1",
            };
        }
    }
}

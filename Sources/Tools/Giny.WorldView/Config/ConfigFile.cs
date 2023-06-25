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

namespace Giny.WorldView.Config
{
    public class ConfigFile
    {
        public static ConfigFile Instance;

        public const string CONFIG_PATH = "config.json";
    
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
        }
        public static void Save()
        {
            File.WriteAllText(CONFIG_PATH, Json.Serialize(Instance));
        }

    }
}

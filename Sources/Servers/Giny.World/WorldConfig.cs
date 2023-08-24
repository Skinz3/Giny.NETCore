using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using Giny.Protocol.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World
{
    public class WorldConfig : IConfigFile
    {
        public short ServerId
        {
            get;
            set;
        } = 291;

        [JsonConverter(typeof(StringEnumConverter))]
        public GameServerTypeEnum ServerType
        {
            get;
            set;
        } = GameServerTypeEnum.SERVER_TYPE_CLASSICAL;

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

        public double SaveIntervalMinutes
        {
            get;
            set;
        } = 30d;

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public List<PlayableBreedEnum> AllowedBreeds
        {
            get;
            set;
        }


        public void OnCreated()
        {
            AllowedBreeds = new List<PlayableBreedEnum>()
            {
                PlayableBreedEnum.Feca ,
                PlayableBreedEnum.Osamodas,
                PlayableBreedEnum.Enutrof,
                PlayableBreedEnum.Sram,
                PlayableBreedEnum.Xelor,
                PlayableBreedEnum.Ecaflip,
                PlayableBreedEnum.Eniripsa,
                PlayableBreedEnum.Iop,
                PlayableBreedEnum.Cra,
                PlayableBreedEnum.Sadida,
                PlayableBreedEnum.Sacrieur,
                PlayableBreedEnum.Pandawa,
                PlayableBreedEnum.Roublard,
                PlayableBreedEnum.Zobal,
                PlayableBreedEnum.Steamer,
                PlayableBreedEnum.Eliotrope,
                PlayableBreedEnum.Huppermage,
                PlayableBreedEnum.Ouginak,
                PlayableBreedEnum.Forgelance,
            };

            Logger.Write("Configuration file created !");

        }

        public void OnLoaded()
        {
            Logger.Write($"Configuration loaded");
        }


        [StartupInvoke("Configuration", StartupInvokePriority.Initial)]
        public static void Initialize()
        {
            ConfigManager<WorldConfig>.Load("config.json");
        }


    }
}

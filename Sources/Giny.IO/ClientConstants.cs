using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.IO
{
    public class ClientConstants
    {

        public const string i18nPath = "data/i18n/";

        public const string D2oDirectory = "data/common/";
        public const string ExePath = "Dofus.exe";

        public const string Gfx0Path = @"content/gfx/world/gfx0.d2p";

        public const string Maps0Path = "content/maps/maps0.d2p";

        public const string ElementsPath = "content/maps/elements.ele";

        public const string ItemBitmap0Path = "content/gfx/items/bitmap0.d2p";

        public const string MonsterBitmap0Path = "content/gfx/monsters/monsters0.d2p";

        public const string MapEncryptionKey = "649ae451ca33ec53bbcbcc33becf15f4";

        /// <summary>
        /// Debug only
        /// </summary>
        public const string ClientPath = "C:\\Users\\olivi\\Desktop\\Giny .NET Core\\Dofus";


        public static ClientInformation DumpInformations(string clientPath)
        {
            if (!File.Exists(Path.Combine(clientPath, ExePath)))
            {
                return null;
            }
            ClientInformation result = new ClientInformation();

            var versionFilePath = Path.Combine(clientPath, "VERSION");

            if (File.Exists(versionFilePath))
            {
                result.Version = File.ReadAllText(versionFilePath);
            }
            else
            {
                result.Version = "?";
            }

            return result;

        }
    }

    public class ClientInformation
    {
        public string Version
        {
            get;
            set;
        }
    }
}

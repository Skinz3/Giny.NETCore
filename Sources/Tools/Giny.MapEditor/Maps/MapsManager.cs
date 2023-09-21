using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.IO;
using Giny.IO.D2P;
using Giny.IO.DLM;
using Giny.IO.ELE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.Maps
{
    public class MapsManager : Singleton<MapsManager>
    {
        private D2PFile MapsFile
        {
            get;
            set;
        }
        public Dictionary<int, EleGraphicalData> Elements
        {
            get;
            private set;
        }
        public void Initialize()
        {
            this.MapsFile = new D2PFile(Path.Combine(ClientConstants.ClientPath, ClientConstants.Maps0Path));
            this.Elements = EleReader.ReadElements(Path.Combine(ClientConstants.ClientPath, ClientConstants.ElementsPath));
        }


        public D2PFile GetFile()
        {
            return MapsFile;
        }
        public DlmMap ReadMap(int mapId)
        {
            var packageId = mapId % 10;

            var entry = MapsFile.TryGetEntry(string.Format("{0}/{1}.dlm", packageId, mapId));

            return ReadMap(entry);
        }

        public DlmMap ReadMap(D2PEntry entry)
        {
            if (entry == null)
            {
                return null;
            }

            return new DlmMap(MapsFile.ReadFile(entry));
        }


        public DlmMap ReadRandomMap()
        {
            var entry = MapsFile.Entries.Random(new Random());
            return ReadMap(entry);
        }
    }
}

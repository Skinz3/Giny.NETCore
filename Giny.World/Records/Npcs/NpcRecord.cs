using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Entities.Look;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Npcs
{
    [D2OClass("Npc")]
    [Table("npcs")]
    public class NpcRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, NpcRecord> Npcs = new ConcurrentDictionary<long, NpcRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [Update]
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("actions")]
        public List<byte> Actions
        {
            get;
            set;
        }
        [D2OField("gender")]
        public bool Sex
        {
            get;
            set;
        }
        [D2OField("look")]
        public ServerEntityLook Look
        {
            get;
            set;
        }
        [StartupInvoke(StartupInvokePriority.FourthPass)]
        public static void Initialize()
        {
            foreach (var npc in Npcs.Values)
            {
                if (npc.Look.Colors.Count > 0)
                {
                    int[] colors = EntityLookManager.Instance.GetConvertedColors(npc.Look.Colors);
                    npc.Look.SetColors(colors);
                }
            }
        }
        public static bool NpcExist(short templateId)
        {
            return Npcs.ContainsKey(templateId);
        }
        public static NpcRecord GetNpcRecord(short templateId)
        {
            return Npcs[templateId];
        }
    }
}

using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Breeds
{
    [D2OClass("Head")]
    [Table("heads")]
    public class HeadRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, HeadRecord> Heads = new ConcurrentDictionary<long, HeadRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [D2OField("skins")]
        public short SkinId
        {
            get;
            set;
        }

        public static short GetSkinId(short cosmeticId)
        {
            return Heads[cosmeticId].SkinId;
        }
    }
}

using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Generic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Npcs
{
    [Table("npcreplies")]
    public class NpcReplyRecord : ITable, IGenericActionParameter
    {
        [Container]
        private static ConcurrentDictionary<long, NpcReplyRecord> NpcReplies = new ConcurrentDictionary<long, NpcReplyRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }
        public int ReplyId
        {
            get;
            set;
        }
        public long NpcSpawnId
        {
            get;
            set;
        }
        public int MessageId
        {
            get;
            set;
        }
        [Update]
        public GenericActionEnum ActionIdentifier
        {
            get;
            set;
        }
        [Update]
        public string Param1
        {
            get;
            set;
        }
        [Update]
        public string Param2
        {
            get;
            set;
        }
        [Update]
        public string Param3
        {
            get;
            set;
        }
        [Update]
        public string Criteria
        {
            get;
            set;
        }

        public static IEnumerable<NpcReplyRecord> GetNpcReplies(long spawnId, int messageId)
        {
            return NpcReplies.Values.Where(x => x.NpcSpawnId == spawnId && x.MessageId == messageId);
        }
        public static IEnumerable<NpcReplyRecord> GetNpcReplies()
        {
            return NpcReplies.Values;
        }
    }
}

using Giny.ORM;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Look;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Characters
{
    [Table("merchants")]
    public class MerchantRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, MerchantRecord> Merchants = new ConcurrentDictionary<long, MerchantRecord>();

        [Ignore]
        public long Id => CharacterId;

        [Primary]
        public long CharacterId
        {
            get;
            set;
        }
        public long MapId
        {
            get;
            set;    
        }
        public short CellId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        public ServerEntityLook Look
        {
            get;
            set;
        }

        public static MerchantRecord GetMerchant(long characterId)
        {
            MerchantRecord result = null;
            Merchants.TryGetValue(characterId, out result);
            return result;
        }
        public static IEnumerable<MerchantRecord> GetMerchants()
        {
            return Merchants.Values;
        }

        public static MerchantRecord RemoveMerchant(long id)
        {
            MerchantRecord record = GetMerchant(id);
            record?.RemoveInstantElement();
            return record;
        }
    }
}

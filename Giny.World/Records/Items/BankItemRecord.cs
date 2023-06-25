using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [Table("bankitems")]
    public class BankItemRecord : AbstractItem, ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, BankItemRecord> BankItems = new ConcurrentDictionary<long, BankItemRecord>();

        [Ignore]
        public long Id => UId;

        public int AccountId
        {
            get;
            set;
        }

        public override AbstractItem CloneWithUID()
        {
            return new BankItemRecord()
            {
                AccountId = this.AccountId,
                AppearanceId = this.AppearanceId,
                Effects = this.Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = this.Position,
                Quantity = this.Quantity,
                UId = this.UId,
            };
        }


        public override AbstractItem CloneWithoutUID()
        {
            return new BankItemRecord()
            {
                AccountId = this.AccountId,
                AppearanceId = this.AppearanceId,
                Effects = this.Effects.Clone(),
                GId = GId,
                Look = Look,
                Position = this.Position,
                Quantity = this.Quantity,
                UId = ItemsManager.Instance.PopItemUID(),
            };
        }
        public static int GetLastItemUID()
        {
            return (int)BankItems.Keys.OrderByDescending(x => x).FirstOrDefault();
        }
        public static IEnumerable<BankItemRecord> GetBankItems(int accountId)
        {
            return BankItems.Values.Where(x => x.AccountId == accountId);
        }
        public static IEnumerable<BankItemRecord> GetBankItems()
        {
            return BankItems.Values;
        }
        public override void Initialize()
        {
           
        }
    }
}

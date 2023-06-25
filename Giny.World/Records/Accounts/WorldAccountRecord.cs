using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Accounts
{
    [Table("worldaccounts")]
    public class WorldAccountRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, WorldAccountRecord> WorldAccounts = new ConcurrentDictionary<long, WorldAccountRecord>();

        [Ignore]
        public long Id => AccountId;

        [Primary]
        public int AccountId
        {
            get;
            set;
        }

        [Update]
        public long BankKamas
        {
            get;
            set;
        }
        
        public static WorldAccountRecord GetWorldAccount(int accountId)
        {
            WorldAccountRecord account = null;
            if (WorldAccounts.TryGetValue(accountId, out account))
            {
                return account;
            }
            else
            {
                return null;
            }

        }
        public static WorldAccountRecord Create(int accountId)
        {
            return new WorldAccountRecord()
            {
                AccountId = accountId,
                BankKamas = 0,
            };
        }


    }
}

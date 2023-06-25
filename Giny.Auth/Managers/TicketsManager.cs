
using Giny.Auth.Records;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Managers
{
    public class TicketsManager : Singleton<TicketsManager>
    {
        private ConcurrentDictionary<string, AccountRecord> Accounts
        {
            get;
            set;
        } = new ConcurrentDictionary<string, AccountRecord>();

        public void PushAccount(string ticket, AccountRecord account)
        {
            Accounts.TryAdd(ticket, account);
        }
        public AccountRecord RetreiveAccount(string ticket)
        {
            if (Accounts.ContainsKey(ticket))
            {
                var result = Accounts[ticket];
                bool success = Accounts.TryRemove(ticket);

                if (!success)
                {
                    throw new ApplicationException("Unable to remove account ticket.");
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

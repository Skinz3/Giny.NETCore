using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Accounts
{
    public interface IAccountProvider
    {
        public WebAccount GetAccount(int instanceId);
    }
}

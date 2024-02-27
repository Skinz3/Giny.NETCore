using Giny.Core.Pool;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class BuffIdProvider
    {
        public UniqueIdProvider IdProvider
        {
            get;
            private set;
        }

        public int? CurrentTriggeredId = 0;

        public BuffIdProvider()
        {
            IdProvider = new UniqueIdProvider(1);
        }

        public int Pop()
        {
            if (CurrentTriggeredId.HasValue)
            {
                return CurrentTriggeredId.Value;
            }

            return IdProvider.Pop();
        }

        public void Push(int id)
        {
            IdProvider.Push(id);
        }

    }
}

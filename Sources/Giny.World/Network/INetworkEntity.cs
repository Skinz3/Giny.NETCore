using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Network
{
    public interface INetworkEntity
    {
        void Send(NetworkMessage message);
    }
}

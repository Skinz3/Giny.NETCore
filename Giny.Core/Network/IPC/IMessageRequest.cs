using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Giny.Core.Network.IPC.IPCRequestManager;

namespace Giny.Core.Network.IPC
{
    public interface IMessageRequest
    {
        void ProcessMessage(IPCMessage message);
    }
}

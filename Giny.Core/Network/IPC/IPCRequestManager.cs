using Giny.Core.Extensions;
using Giny.Core.Pool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Network.IPC
{
    public class IPCRequestManager
    {
        private static ConcurrentDictionary<short, IMessageRequest> m_requests = new ConcurrentDictionary<short, IMessageRequest>();

        public delegate void RequestCallbackDelegate<in T>(T callbackMessage) where T : IPCMessage;
        public delegate void RequestCallbackErrorDelegate();

        public static void SendRequest<T>(Client client, IPCMessage message, bool authSide, RequestCallbackDelegate<T> requestCallback, RequestCallbackErrorDelegate errorCallback = null)
            where T : IPCMessage
        {
            lock (m_requests)
            {
                var messageRequest = new MessageRequest<T>(requestCallback, PopNextRequestId(), errorCallback);
                m_requests.TryAdd(messageRequest.RequestId, messageRequest);
                message.requestId = (short)messageRequest.RequestId;
                message.authSide = authSide;
                client.Send(message);
            }
        }
        static short PopNextRequestId()
        {
            var requestKeys = m_requests.Keys;
            return requestKeys.Count == 0 ? (short)1 : (short)(requestKeys.Last() + 1);
        }
        public static void ReceiveRequest(IPCMessage ipcMessage)
        {
            lock (m_requests)
            {
                if (m_requests.ContainsKey(ipcMessage.requestId))
                {
                    m_requests[ipcMessage.requestId].ProcessMessage(ipcMessage);
                    m_requests.TryRemove(ipcMessage.requestId);
                }
                else
                {
                    Logger.Write("(IPC) Received IPCMessage with unknown request id :" + ipcMessage, Channels.Warning);
                }
            }
        }
    }
}

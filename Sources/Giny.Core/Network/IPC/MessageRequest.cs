using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static Giny.Core.Network.IPC.IPCRequestManager;
using Timer = System.Timers.Timer;

namespace Giny.Core.Network.IPC
{
    public class MessageRequest<T> : IMessageRequest where T : IPCMessage
    {
        public const int TIMEOUT = 5000;

        private RequestCallbackDelegate<T> Callback
        {
            get;
            set;
        }
        private RequestCallbackErrorDelegate ErrorCallback
        {
            get;
            set;
        }
        public short RequestId
        {
            get;
            set;
        }
        private Timer TimeoutTimer
        {
            get;
            set;
        }

        public MessageRequest(RequestCallbackDelegate<T> callback, short requestId, RequestCallbackErrorDelegate errorCallback)
        {
            this.Callback = callback;
            this.ErrorCallback = errorCallback;
            this.RequestId = requestId;
            this.TimeoutTimer = new Timer(TIMEOUT);
            this.TimeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            this.TimeoutTimer.Start();
        }

        void TimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Logger.Write("(IPC) Message " + typeof(T).Name + " timeout..", Channels.Warning);
            TimeoutTimer.Stop();
            ErrorCallback?.Invoke();
        }

        public void ProcessMessage(IPCMessage message)
        {
            this.TimeoutTimer.Stop();
            this.Callback(message as T);
        }
    }
}

using Giny.Auth.Handlers;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Network
{
    class ConnectionQueue
    {
        private static List<AuthClient> Queue = new List<AuthClient>();

        public const double QueueRefreshDelaySeconds = 0.2f;

        [StartupInvoke("Connection Queue", StartupInvokePriority.Last)]
        public static void CreateTask()
        {
            Task.Factory.StartNewDelayed((int)(QueueRefreshDelaySeconds * 1000), RefreshQueue);
        }
        private static void RefreshQueue()
        {
            try
            {
                lock (Queue)
                {
                    var toRemove = new List<AuthClient>();
                    short count = 0;
                    short queueCount = (short)Queue.Count;

                    var nextClient = Queue.FirstOrDefault();

                    if (nextClient != null)
                    {
                        nextClient.CloseLoginQueue();
                        ConnectionHandler.ProcessIdentification(nextClient);
                        Queue.Remove(nextClient);
                    }

                    foreach (var authClient in Queue)
                    {
                        count++;

                        if (!authClient.Connected)
                        {
                            toRemove.Add(authClient);
                        }

                        if (DateTime.Now - authClient.InQueueUntil <= TimeSpan.FromSeconds(QueueRefreshDelaySeconds))
                            continue;


                        authClient.DisplayLoginQueue(count, queueCount);
                    }

                    foreach (var authClient in toRemove)
                    {
                        Queue.Remove(authClient);
                    }
                }
            }
            finally
            {
                CreateTask();
            }
        }


        public static void AddToQueue(AuthClient identificationClient)
        {
            lock (Queue)
                Queue.Add(identificationClient);
        }
    }
}

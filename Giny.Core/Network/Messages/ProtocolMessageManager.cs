using Giny.Core;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Core.IO.Interfaces;
using Giny.Core.Network;
using Giny.Core.Network.IPC;
using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Network.Messages
{
    public class ProtocolMessageManager
    {
        private static readonly Type[] HandlerMethodParameterTypes = new Type[] { typeof(NetworkMessage), typeof(Client) };

        private static readonly Dictionary<uint, Delegate> Handlers = new Dictionary<uint, Delegate>();

        private static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();

        private static readonly Dictionary<ushort, Func<NetworkMessage>> Constructors = new Dictionary<ushort, Func<NetworkMessage>>();

        public static void Initialize(Assembly messagesAssembly, Assembly handlersAssembly)
        {
            foreach (var type in messagesAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(NetworkMessage))))
            {
                FieldInfo field = type.GetField("Id");
                if (field != null)
                {
                    ushort num = (ushort)field.GetValue(type);
                    if (Messages.ContainsKey(num))
                    {
                        throw new AmbiguousMatchException(string.Format("MessageReceiver() => {0} item is already in the dictionary, old type is : {1}, new type is  {2}",
                            num, Messages[num], type));
                    }
                    Messages.Add(num, type);
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));
                    }
                    Constructors.Add(num, constructor.CreateDelegate<Func<NetworkMessage>>());
                }
            }


            foreach (var item in handlersAssembly.GetTypes())
            {
                foreach (var subItem in item.GetMethods())
                {
                    var attribute = subItem.GetCustomAttribute(typeof(MessageHandlerAttribute));
                    if (attribute != null)
                    {
                        Type methodParameters = subItem.GetParameters()[0].ParameterType;
                        if (methodParameters.BaseType != null)
                        {
                            try
                            {
                                Delegate target = subItem.CreateDelegate(HandlerMethodParameterTypes);
                                FieldInfo field = methodParameters.GetField("Id");
                                Handlers.Add((ushort)field.GetValue(null), target);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Cannot register " + subItem.Name + " has message handler..." + ex);
                            }

                        }
                    }

                }
            }
            Logger.Write(Messages.Count + " Message(s) Loaded | " + Handlers.Count + " Handler(s) Loaded", Channels.Log);
        }
        /// <summary>
        /// Care about reader position!
        /// </summary>
        public static NetworkMessage BuildMessage(BigEndianReader reader)
        {
            var messagePart = new MessagePart();

            if (messagePart.Build(reader))
            {
                try
                {
                    BigEndianReader finalReader = null;

                    ushort messageId = (ushort)messagePart.MessageId.Value;

                    if (!Messages.ContainsKey(messageId))
                    {
                        return null;
                    }
                    NetworkMessage message = Constructors[messageId]();

                    if (message == null)
                    {
                        return null;
                    }

                    if (IsIPCMessage(message)) // something wrong here.
                    {
                        reader.Seek(NetworkMessage.MESSAGE_ID_SIZE + NetworkMessage.ComputeTypeLen(messagePart.LengthBytesCount.Value), SeekOrigin.Begin);
                        finalReader = reader;
                    }
                    else
                    {
                        finalReader = new BigEndianReader(messagePart.Data);
                    }

                    message.Unpack(finalReader);
                    return message;
                }
                catch (Exception ex)
                {
                    Logger.Write("Unable to build message : " + ex.Message, Channels.Warning);
                    return null;
                }
            }
            else
                return null;

        }
        public static ushort[] GetMessageIds()
        {
            return Messages.Keys.ToArray();
        }
        private static bool IsIPCMessage(NetworkMessage message)
        {
            return message is IPCMessage;
        }
        public static bool HandleMessage(NetworkMessage message, Client client)
        {
            if (message == null)
            {
                Logger.Write("Cannot build datas from client " + client.Ip, Channels.Warning);
                client.Disconnect();
                return false;
            }

            var handler = Handlers.FirstOrDefault(x => x.Key == message.MessageId);

            if (handler.Value != null)
            {
                {
                    try
                    {
                        handler.Value.DynamicInvoke(null, message, client);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        client.OnHandlingError(message, handler.Value, ex);
                        return false;
                    }
                }
            }
            else
            {
                client.OnMessageUnhandled(message);
                return true;
            }
        }
    }

}

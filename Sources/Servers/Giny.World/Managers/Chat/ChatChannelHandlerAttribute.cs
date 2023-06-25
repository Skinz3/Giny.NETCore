using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Chat
{
    public class ChatChannelHandlerAttribute : Attribute
    {
        public ChatActivableChannelsEnum Channel
        {
            get; set;
        }

        public ChatChannelHandlerAttribute(ChatActivableChannelsEnum channel)
        {
            this.Channel = channel;
        }
    }
}

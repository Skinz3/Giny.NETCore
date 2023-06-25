using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ConsoleCommandsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 6227;
        public override ushort MessageId => Id;

        public string[] aliases;
        public string[] args;
        public string[] descriptions;

        public ConsoleCommandsListMessage()
        {
        }
        public ConsoleCommandsListMessage(string[] aliases,string[] args,string[] descriptions)
        {
            this.aliases = aliases;
            this.args = args;
            this.descriptions = descriptions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)aliases.Length);
            for (uint _i1 = 0;_i1 < aliases.Length;_i1++)
            {
                writer.WriteUTF((string)aliases[_i1]);
            }

            writer.WriteShort((short)args.Length);
            for (uint _i2 = 0;_i2 < args.Length;_i2++)
            {
                writer.WriteUTF((string)args[_i2]);
            }

            writer.WriteShort((short)descriptions.Length);
            for (uint _i3 = 0;_i3 < descriptions.Length;_i3++)
            {
                writer.WriteUTF((string)descriptions[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            string _val1 = null;
            string _val2 = null;
            string _val3 = null;
            uint _aliasesLen = (uint)reader.ReadUShort();
            aliases = new string[_aliasesLen];
            for (uint _i1 = 0;_i1 < _aliasesLen;_i1++)
            {
                _val1 = (string)reader.ReadUTF();
                aliases[_i1] = (string)_val1;
            }

            uint _argsLen = (uint)reader.ReadUShort();
            args = new string[_argsLen];
            for (uint _i2 = 0;_i2 < _argsLen;_i2++)
            {
                _val2 = (string)reader.ReadUTF();
                args[_i2] = (string)_val2;
            }

            uint _descriptionsLen = (uint)reader.ReadUShort();
            descriptions = new string[_descriptionsLen];
            for (uint _i3 = 0;_i3 < _descriptionsLen;_i3++)
            {
                _val3 = (string)reader.ReadUTF();
                descriptions[_i3] = (string)_val3;
            }

        }


    }
}









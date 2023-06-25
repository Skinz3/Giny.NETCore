using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MigratedServerListMessage : NetworkMessage  
    { 
        public  const ushort Id = 664;
        public override ushort MessageId => Id;

        public short[] migratedServerIds;

        public MigratedServerListMessage()
        {
        }
        public MigratedServerListMessage(short[] migratedServerIds)
        {
            this.migratedServerIds = migratedServerIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)migratedServerIds.Length);
            for (uint _i1 = 0;_i1 < migratedServerIds.Length;_i1++)
            {
                if (migratedServerIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + migratedServerIds[_i1] + ") on element 1 (starting at 1) of migratedServerIds.");
                }

                writer.WriteVarShort((short)migratedServerIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _migratedServerIdsLen = (uint)reader.ReadUShort();
            migratedServerIds = new short[_migratedServerIdsLen];
            for (uint _i1 = 0;_i1 < _migratedServerIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of migratedServerIds.");
                }

                migratedServerIds[_i1] = (short)_val1;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonKeyRingMessage : NetworkMessage  
    { 
        public  const ushort Id = 8007;
        public override ushort MessageId => Id;

        public short[] availables;
        public short[] unavailables;

        public DungeonKeyRingMessage()
        {
        }
        public DungeonKeyRingMessage(short[] availables,short[] unavailables)
        {
            this.availables = availables;
            this.unavailables = unavailables;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)availables.Length);
            for (uint _i1 = 0;_i1 < availables.Length;_i1++)
            {
                if (availables[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + availables[_i1] + ") on element 1 (starting at 1) of availables.");
                }

                writer.WriteVarShort((short)availables[_i1]);
            }

            writer.WriteShort((short)unavailables.Length);
            for (uint _i2 = 0;_i2 < unavailables.Length;_i2++)
            {
                if (unavailables[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + unavailables[_i2] + ") on element 2 (starting at 1) of unavailables.");
                }

                writer.WriteVarShort((short)unavailables[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _availablesLen = (uint)reader.ReadUShort();
            availables = new short[_availablesLen];
            for (uint _i1 = 0;_i1 < _availablesLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of availables.");
                }

                availables[_i1] = (short)_val1;
            }

            uint _unavailablesLen = (uint)reader.ReadUShort();
            unavailables = new short[_unavailablesLen];
            for (uint _i2 = 0;_i2 < _unavailablesLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of unavailables.");
                }

                unavailables[_i2] = (short)_val2;
            }

        }


    }
}









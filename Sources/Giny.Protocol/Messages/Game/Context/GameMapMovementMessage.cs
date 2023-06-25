using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameMapMovementMessage : NetworkMessage  
    { 
        public  const ushort Id = 9191;
        public override ushort MessageId => Id;

        public short[] keyMovements;
        public short forcedDirection;
        public double actorId;

        public GameMapMovementMessage()
        {
        }
        public GameMapMovementMessage(short[] keyMovements,short forcedDirection,double actorId)
        {
            this.keyMovements = keyMovements;
            this.forcedDirection = forcedDirection;
            this.actorId = actorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)keyMovements.Length);
            for (uint _i1 = 0;_i1 < keyMovements.Length;_i1++)
            {
                if (keyMovements[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + keyMovements[_i1] + ") on element 1 (starting at 1) of keyMovements.");
                }

                writer.WriteShort((short)keyMovements[_i1]);
            }

            writer.WriteShort((short)forcedDirection);
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _keyMovementsLen = (uint)reader.ReadUShort();
            keyMovements = new short[_keyMovementsLen];
            for (uint _i1 = 0;_i1 < _keyMovementsLen;_i1++)
            {
                _val1 = (uint)reader.ReadShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of keyMovements.");
                }

                keyMovements[_i1] = (short)_val1;
            }

            forcedDirection = (short)reader.ReadShort();
            actorId = (double)reader.ReadDouble();
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of GameMapMovementMessage.actorId.");
            }

        }


    }
}









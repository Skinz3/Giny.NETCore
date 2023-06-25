using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaFightPropositionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1343;
        public override ushort MessageId => Id;

        public short fightId;
        public double[] alliesId;
        public short duration;

        public GameRolePlayArenaFightPropositionMessage()
        {
        }
        public GameRolePlayArenaFightPropositionMessage(short fightId,double[] alliesId,short duration)
        {
            this.fightId = fightId;
            this.alliesId = alliesId;
            this.duration = duration;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteShort((short)alliesId.Length);
            for (uint _i2 = 0;_i2 < alliesId.Length;_i2++)
            {
                if (alliesId[_i2] < -9.00719925474099E+15 || alliesId[_i2] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + alliesId[_i2] + ") on element 2 (starting at 1) of alliesId.");
                }

                writer.WriteDouble((double)alliesId[_i2]);
            }

            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element duration.");
            }

            writer.WriteVarShort((short)duration);
        }
        public override void Deserialize(IDataReader reader)
        {
            double _val2 = double.NaN;
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayArenaFightPropositionMessage.fightId.");
            }

            uint _alliesIdLen = (uint)reader.ReadUShort();
            alliesId = new double[_alliesIdLen];
            for (uint _i2 = 0;_i2 < _alliesIdLen;_i2++)
            {
                _val2 = (double)reader.ReadDouble();
                if (_val2 < -9.00719925474099E+15 || _val2 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of alliesId.");
                }

                alliesId[_i2] = (double)_val2;
            }

            duration = (short)reader.ReadVarUhShort();
            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element of GameRolePlayArenaFightPropositionMessage.duration.");
            }

        }


    }
}









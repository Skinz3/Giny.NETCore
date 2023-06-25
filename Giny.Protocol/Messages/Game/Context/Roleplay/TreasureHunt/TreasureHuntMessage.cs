using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntMessage : NetworkMessage  
    { 
        public  const ushort Id = 7652;
        public override ushort MessageId => Id;

        public byte questType;
        public double startMapId;
        public TreasureHuntStep[] knownStepsList;
        public byte totalStepCount;
        public int checkPointCurrent;
        public int checkPointTotal;
        public int availableRetryCount;
        public TreasureHuntFlag[] flags;

        public TreasureHuntMessage()
        {
        }
        public TreasureHuntMessage(byte questType,double startMapId,TreasureHuntStep[] knownStepsList,byte totalStepCount,int checkPointCurrent,int checkPointTotal,int availableRetryCount,TreasureHuntFlag[] flags)
        {
            this.questType = questType;
            this.startMapId = startMapId;
            this.knownStepsList = knownStepsList;
            this.totalStepCount = totalStepCount;
            this.checkPointCurrent = checkPointCurrent;
            this.checkPointTotal = checkPointTotal;
            this.availableRetryCount = availableRetryCount;
            this.flags = flags;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)questType);
            if (startMapId < 0 || startMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + startMapId + ") on element startMapId.");
            }

            writer.WriteDouble((double)startMapId);
            writer.WriteShort((short)knownStepsList.Length);
            for (uint _i3 = 0;_i3 < knownStepsList.Length;_i3++)
            {
                writer.WriteShort((short)(knownStepsList[_i3] as TreasureHuntStep).TypeId);
                (knownStepsList[_i3] as TreasureHuntStep).Serialize(writer);
            }

            if (totalStepCount < 0)
            {
                throw new System.Exception("Forbidden value (" + totalStepCount + ") on element totalStepCount.");
            }

            writer.WriteByte((byte)totalStepCount);
            if (checkPointCurrent < 0)
            {
                throw new System.Exception("Forbidden value (" + checkPointCurrent + ") on element checkPointCurrent.");
            }

            writer.WriteVarInt((int)checkPointCurrent);
            if (checkPointTotal < 0)
            {
                throw new System.Exception("Forbidden value (" + checkPointTotal + ") on element checkPointTotal.");
            }

            writer.WriteVarInt((int)checkPointTotal);
            writer.WriteInt((int)availableRetryCount);
            writer.WriteShort((short)flags.Length);
            for (uint _i8 = 0;_i8 < flags.Length;_i8++)
            {
                (flags[_i8] as TreasureHuntFlag).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            TreasureHuntStep _item3 = null;
            TreasureHuntFlag _item8 = null;
            questType = (byte)reader.ReadByte();
            if (questType < 0)
            {
                throw new System.Exception("Forbidden value (" + questType + ") on element of TreasureHuntMessage.questType.");
            }

            startMapId = (double)reader.ReadDouble();
            if (startMapId < 0 || startMapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + startMapId + ") on element of TreasureHuntMessage.startMapId.");
            }

            uint _knownStepsListLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _knownStepsListLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<TreasureHuntStep>((short)_id3);
                _item3.Deserialize(reader);
                knownStepsList[_i3] = _item3;
            }

            totalStepCount = (byte)reader.ReadByte();
            if (totalStepCount < 0)
            {
                throw new System.Exception("Forbidden value (" + totalStepCount + ") on element of TreasureHuntMessage.totalStepCount.");
            }

            checkPointCurrent = (int)reader.ReadVarUhInt();
            if (checkPointCurrent < 0)
            {
                throw new System.Exception("Forbidden value (" + checkPointCurrent + ") on element of TreasureHuntMessage.checkPointCurrent.");
            }

            checkPointTotal = (int)reader.ReadVarUhInt();
            if (checkPointTotal < 0)
            {
                throw new System.Exception("Forbidden value (" + checkPointTotal + ") on element of TreasureHuntMessage.checkPointTotal.");
            }

            availableRetryCount = (int)reader.ReadInt();
            uint _flagsLen = (uint)reader.ReadUShort();
            for (uint _i8 = 0;_i8 < _flagsLen;_i8++)
            {
                _item8 = new TreasureHuntFlag();
                _item8.Deserialize(reader);
                flags[_i8] = _item8;
            }

        }


    }
}









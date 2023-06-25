using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class KohUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 8188;
        public override ushort MessageId => Id;

        public AllianceInformations[] alliances;
        public short[] allianceNbMembers;
        public int[] allianceRoundWeigth;
        public byte[] allianceMatchScore;
        public BasicAllianceInformations[] allianceMapWinners;
        public int allianceMapWinnerScore;
        public int allianceMapMyAllianceScore;
        public double nextTickTime;

        public KohUpdateMessage()
        {
        }
        public KohUpdateMessage(AllianceInformations[] alliances,short[] allianceNbMembers,int[] allianceRoundWeigth,byte[] allianceMatchScore,BasicAllianceInformations[] allianceMapWinners,int allianceMapWinnerScore,int allianceMapMyAllianceScore,double nextTickTime)
        {
            this.alliances = alliances;
            this.allianceNbMembers = allianceNbMembers;
            this.allianceRoundWeigth = allianceRoundWeigth;
            this.allianceMatchScore = allianceMatchScore;
            this.allianceMapWinners = allianceMapWinners;
            this.allianceMapWinnerScore = allianceMapWinnerScore;
            this.allianceMapMyAllianceScore = allianceMapMyAllianceScore;
            this.nextTickTime = nextTickTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)alliances.Length);
            for (uint _i1 = 0;_i1 < alliances.Length;_i1++)
            {
                (alliances[_i1] as AllianceInformations).Serialize(writer);
            }

            writer.WriteShort((short)allianceNbMembers.Length);
            for (uint _i2 = 0;_i2 < allianceNbMembers.Length;_i2++)
            {
                if (allianceNbMembers[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + allianceNbMembers[_i2] + ") on element 2 (starting at 1) of allianceNbMembers.");
                }

                writer.WriteVarShort((short)allianceNbMembers[_i2]);
            }

            writer.WriteShort((short)allianceRoundWeigth.Length);
            for (uint _i3 = 0;_i3 < allianceRoundWeigth.Length;_i3++)
            {
                if (allianceRoundWeigth[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + allianceRoundWeigth[_i3] + ") on element 3 (starting at 1) of allianceRoundWeigth.");
                }

                writer.WriteVarInt((int)allianceRoundWeigth[_i3]);
            }

            writer.WriteShort((short)allianceMatchScore.Length);
            for (uint _i4 = 0;_i4 < allianceMatchScore.Length;_i4++)
            {
                if (allianceMatchScore[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + allianceMatchScore[_i4] + ") on element 4 (starting at 1) of allianceMatchScore.");
                }

                writer.WriteByte((byte)allianceMatchScore[_i4]);
            }

            writer.WriteShort((short)allianceMapWinners.Length);
            for (uint _i5 = 0;_i5 < allianceMapWinners.Length;_i5++)
            {
                (allianceMapWinners[_i5] as BasicAllianceInformations).Serialize(writer);
            }

            if (allianceMapWinnerScore < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceMapWinnerScore + ") on element allianceMapWinnerScore.");
            }

            writer.WriteVarInt((int)allianceMapWinnerScore);
            if (allianceMapMyAllianceScore < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceMapMyAllianceScore + ") on element allianceMapMyAllianceScore.");
            }

            writer.WriteVarInt((int)allianceMapMyAllianceScore);
            if (nextTickTime < 0 || nextTickTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + nextTickTime + ") on element nextTickTime.");
            }

            writer.WriteDouble((double)nextTickTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            AllianceInformations _item1 = null;
            uint _val2 = 0;
            uint _val3 = 0;
            uint _val4 = 0;
            BasicAllianceInformations _item5 = null;
            uint _alliancesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _alliancesLen;_i1++)
            {
                _item1 = new AllianceInformations();
                _item1.Deserialize(reader);
                alliances[_i1] = _item1;
            }

            uint _allianceNbMembersLen = (uint)reader.ReadUShort();
            allianceNbMembers = new short[_allianceNbMembersLen];
            for (uint _i2 = 0;_i2 < _allianceNbMembersLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of allianceNbMembers.");
                }

                allianceNbMembers[_i2] = (short)_val2;
            }

            uint _allianceRoundWeigthLen = (uint)reader.ReadUShort();
            allianceRoundWeigth = new int[_allianceRoundWeigthLen];
            for (uint _i3 = 0;_i3 < _allianceRoundWeigthLen;_i3++)
            {
                _val3 = (uint)reader.ReadVarUhInt();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of allianceRoundWeigth.");
                }

                allianceRoundWeigth[_i3] = (int)_val3;
            }

            uint _allianceMatchScoreLen = (uint)reader.ReadUShort();
            allianceMatchScore = new byte[_allianceMatchScoreLen];
            for (uint _i4 = 0;_i4 < _allianceMatchScoreLen;_i4++)
            {
                _val4 = (uint)reader.ReadByte();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of allianceMatchScore.");
                }

                allianceMatchScore[_i4] = (byte)_val4;
            }

            uint _allianceMapWinnersLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _allianceMapWinnersLen;_i5++)
            {
                _item5 = new BasicAllianceInformations();
                _item5.Deserialize(reader);
                allianceMapWinners[_i5] = _item5;
            }

            allianceMapWinnerScore = (int)reader.ReadVarUhInt();
            if (allianceMapWinnerScore < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceMapWinnerScore + ") on element of KohUpdateMessage.allianceMapWinnerScore.");
            }

            allianceMapMyAllianceScore = (int)reader.ReadVarUhInt();
            if (allianceMapMyAllianceScore < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceMapMyAllianceScore + ") on element of KohUpdateMessage.allianceMapMyAllianceScore.");
            }

            nextTickTime = (double)reader.ReadDouble();
            if (nextTickTime < 0 || nextTickTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + nextTickTime + ") on element of KohUpdateMessage.nextTickTime.");
            }

        }


    }
}









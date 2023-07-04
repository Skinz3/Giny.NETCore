using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SocialFightInfo
    {
        public const ushort Id = 7501;
        public virtual ushort TypeId => Id;

        public short fightId;
        public byte fightType;
        public double mapId;

        public SocialFightInfo()
        {
        }
        public SocialFightInfo(short fightId, byte fightType, double mapId)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.mapId = mapId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteByte((byte)fightType);
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of SocialFightInfo.fightId.");
            }

            fightType = (byte)reader.ReadByte();
            if (fightType < 0)
            {
                throw new System.Exception("Forbidden value (" + fightType + ") on element of SocialFightInfo.fightType.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of SocialFightInfo.mapId.");
            }

        }


    }
}



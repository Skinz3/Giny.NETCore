using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TeleportDestination  
    { 
        public const ushort Id = 6958;
        public virtual ushort TypeId => Id;

        public byte type;
        public double mapId;
        public short subAreaId;
        public short level;
        public short cost;

        public TeleportDestination()
        {
        }
        public TeleportDestination(byte type,double mapId,short subAreaId,short level,short cost)
        {
            this.type = type;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.level = level;
            this.cost = cost;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            if (cost < 0)
            {
                throw new System.Exception("Forbidden value (" + cost + ") on element cost.");
            }

            writer.WriteVarShort((short)cost);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of TeleportDestination.type.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of TeleportDestination.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of TeleportDestination.subAreaId.");
            }

            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of TeleportDestination.level.");
            }

            cost = (short)reader.ReadVarUhShort();
            if (cost < 0)
            {
                throw new System.Exception("Forbidden value (" + cost + ") on element of TeleportDestination.cost.");
            }

        }


    }
}









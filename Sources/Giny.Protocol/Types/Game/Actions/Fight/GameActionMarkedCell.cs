using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameActionMarkedCell  
    { 
        public const ushort Id = 4763;
        public virtual ushort TypeId => Id;

        public short cellId;
        public byte zoneSize;
        public int cellColor;
        public byte cellsType;

        public GameActionMarkedCell()
        {
        }
        public GameActionMarkedCell(short cellId,byte zoneSize,int cellColor,byte cellsType)
        {
            this.cellId = cellId;
            this.zoneSize = zoneSize;
            this.cellColor = cellColor;
            this.cellsType = cellsType;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
            writer.WriteByte((byte)zoneSize);
            writer.WriteInt((int)cellColor);
            writer.WriteByte((byte)cellsType);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of GameActionMarkedCell.cellId.");
            }

            zoneSize = (byte)reader.ReadByte();
            cellColor = (int)reader.ReadInt();
            cellsType = (byte)reader.ReadByte();
        }


    }
}









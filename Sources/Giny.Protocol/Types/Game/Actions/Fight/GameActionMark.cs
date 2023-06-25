using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameActionMark  
    { 
        public const ushort Id = 5902;
        public virtual ushort TypeId => Id;

        public double markAuthorId;
        public byte markTeamId;
        public int markSpellId;
        public short markSpellLevel;
        public short markId;
        public byte markType;
        public short markimpactCell;
        public GameActionMarkedCell[] cells;
        public bool active;

        public GameActionMark()
        {
        }
        public GameActionMark(double markAuthorId,byte markTeamId,int markSpellId,short markSpellLevel,short markId,byte markType,short markimpactCell,GameActionMarkedCell[] cells,bool active)
        {
            this.markAuthorId = markAuthorId;
            this.markTeamId = markTeamId;
            this.markSpellId = markSpellId;
            this.markSpellLevel = markSpellLevel;
            this.markId = markId;
            this.markType = markType;
            this.markimpactCell = markimpactCell;
            this.cells = cells;
            this.active = active;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (markAuthorId < -9.00719925474099E+15 || markAuthorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + markAuthorId + ") on element markAuthorId.");
            }

            writer.WriteDouble((double)markAuthorId);
            writer.WriteByte((byte)markTeamId);
            if (markSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + markSpellId + ") on element markSpellId.");
            }

            writer.WriteInt((int)markSpellId);
            if (markSpellLevel < 1 || markSpellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + markSpellLevel + ") on element markSpellLevel.");
            }

            writer.WriteShort((short)markSpellLevel);
            writer.WriteShort((short)markId);
            writer.WriteByte((byte)markType);
            if (markimpactCell < -1 || markimpactCell > 559)
            {
                throw new System.Exception("Forbidden value (" + markimpactCell + ") on element markimpactCell.");
            }

            writer.WriteShort((short)markimpactCell);
            writer.WriteShort((short)cells.Length);
            for (uint _i8 = 0;_i8 < cells.Length;_i8++)
            {
                (cells[_i8] as GameActionMarkedCell).Serialize(writer);
            }

            writer.WriteBoolean((bool)active);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            GameActionMarkedCell _item8 = null;
            markAuthorId = (double)reader.ReadDouble();
            if (markAuthorId < -9.00719925474099E+15 || markAuthorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + markAuthorId + ") on element of GameActionMark.markAuthorId.");
            }

            markTeamId = (byte)reader.ReadByte();
            if (markTeamId < 0)
            {
                throw new System.Exception("Forbidden value (" + markTeamId + ") on element of GameActionMark.markTeamId.");
            }

            markSpellId = (int)reader.ReadInt();
            if (markSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + markSpellId + ") on element of GameActionMark.markSpellId.");
            }

            markSpellLevel = (short)reader.ReadShort();
            if (markSpellLevel < 1 || markSpellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + markSpellLevel + ") on element of GameActionMark.markSpellLevel.");
            }

            markId = (short)reader.ReadShort();
            markType = (byte)reader.ReadByte();
            markimpactCell = (short)reader.ReadShort();
            if (markimpactCell < -1 || markimpactCell > 559)
            {
                throw new System.Exception("Forbidden value (" + markimpactCell + ") on element of GameActionMark.markimpactCell.");
            }

            uint _cellsLen = (uint)reader.ReadUShort();
            for (uint _i8 = 0;_i8 < _cellsLen;_i8++)
            {
                _item8 = new GameActionMarkedCell();
                _item8.Deserialize(reader);
                cells[_i8] = _item8;
            }

            active = (bool)reader.ReadBoolean();
        }


    }
}









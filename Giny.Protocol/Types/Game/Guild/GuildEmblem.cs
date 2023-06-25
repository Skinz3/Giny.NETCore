using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildEmblem  
    { 
        public const ushort Id = 4435;
        public virtual ushort TypeId => Id;

        public short symbolShape;
        public int symbolColor;
        public byte backgroundShape;
        public int backgroundColor;

        public GuildEmblem()
        {
        }
        public GuildEmblem(short symbolShape,int symbolColor,byte backgroundShape,int backgroundColor)
        {
            this.symbolShape = symbolShape;
            this.symbolColor = symbolColor;
            this.backgroundShape = backgroundShape;
            this.backgroundColor = backgroundColor;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (symbolShape < 0)
            {
                throw new System.Exception("Forbidden value (" + symbolShape + ") on element symbolShape.");
            }

            writer.WriteVarShort((short)symbolShape);
            writer.WriteInt((int)symbolColor);
            if (backgroundShape < 0)
            {
                throw new System.Exception("Forbidden value (" + backgroundShape + ") on element backgroundShape.");
            }

            writer.WriteByte((byte)backgroundShape);
            writer.WriteInt((int)backgroundColor);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            symbolShape = (short)reader.ReadVarUhShort();
            if (symbolShape < 0)
            {
                throw new System.Exception("Forbidden value (" + symbolShape + ") on element of GuildEmblem.symbolShape.");
            }

            symbolColor = (int)reader.ReadInt();
            backgroundShape = (byte)reader.ReadByte();
            if (backgroundShape < 0)
            {
                throw new System.Exception("Forbidden value (" + backgroundShape + ") on element of GuildEmblem.backgroundShape.");
            }

            backgroundColor = (int)reader.ReadInt();
        }


    }
}









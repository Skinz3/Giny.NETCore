using Giny.Core.IO;

namespace Giny.IO.DLM.Elements
{
    public class GraphicalElement : BasicElement
    {
        public const uint CELL_HALF_WIDTH = 43;
        public const float CELL_HALF_HEIGHT = 21.5F;

        public sbyte HueR
        {
            get;
            set;
        }
        public sbyte HueG
        {
            get;
            set;
        }
        public sbyte HueB
        {
            get;
            set;
        }

        public sbyte ShadowR
        {
            get;
            set;
        }
        public sbyte ShadowG
        {
            get;
            set;
        }
        public sbyte ShadowB
        {
            get;
            set;
        }

        public int OffsetX
        {
            get;
            set;
        }

        public int OffsetY
        {
            get;
            set;
        }

        public int PixelOffsetX
        {
            get;
            set;
        }

        public int PixelOffsetY
        {
            get;
            set;
        }

        public int Altitude
        {
            get;
            set;
        }

        public uint Identifier
        {
            get;
            set;
        }
        public GraphicalElement()
        {

        }

        public GraphicalElement(BigEndianReader _reader, sbyte mapVersion)
        {
            ElementId = _reader.ReadUInt();

            this.HueR = _reader.ReadSByte();
            this.HueG = _reader.ReadSByte();
            this.HueB = _reader.ReadSByte();

            this.ShadowR = _reader.ReadSByte();
            this.ShadowG = _reader.ReadSByte();
            this.ShadowB = _reader.ReadSByte();

            if (mapVersion <= 4)
            {
                OffsetX = _reader.ReadSByte();
                OffsetY = _reader.ReadSByte();

                PixelOffsetX = (int)(OffsetX * CELL_HALF_WIDTH);
                PixelOffsetY = (int)(OffsetY * CELL_HALF_HEIGHT);
            }

            else
            {
                PixelOffsetX = _reader.ReadShort();
                PixelOffsetY = _reader.ReadShort();

                OffsetX = (int)(PixelOffsetX / CELL_HALF_WIDTH);
                OffsetY = (int)(PixelOffsetY / CELL_HALF_HEIGHT);
            }

            Altitude = _reader.ReadSByte();
            Identifier = _reader.ReadUInt();
        }

        public override void Serialize(BigEndianWriter writer, sbyte mapVersion)
        {
            writer.WriteUInt(ElementId);

            writer.WriteSByte((sbyte)HueR);
            writer.WriteSByte((sbyte)HueG);
            writer.WriteSByte((sbyte)HueB);
            writer.WriteSByte((sbyte)ShadowR);
            writer.WriteSByte((sbyte)ShadowG);
            writer.WriteSByte((sbyte)ShadowB);

            if (mapVersion <= 4)
            {
                writer.WriteSByte((sbyte)OffsetX);
                writer.WriteSByte((sbyte)OffsetY);
            }
            else
            {
                writer.WriteShort((short)PixelOffsetX);
                writer.WriteShort((short)PixelOffsetY);
            }

            writer.WriteSByte((sbyte)Altitude);
            writer.WriteUInt((uint)Identifier);

        }
    }
}

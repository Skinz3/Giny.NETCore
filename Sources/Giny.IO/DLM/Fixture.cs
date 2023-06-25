using System;
using Giny.Core.IO;

namespace Giny.IO.DLM
{
    public class Fixture
    {
        public int FixtureId
        {
            get;
            private set;
        }

        public int OffsetX
        {
            get;
            private set;
        }

        public int OffsetY
        {
            get;
            private set;
        }

        public int Rotation
        {
            get;
            private set;
        }

        public int XScale
        {
            get;
            private set;
        }

        public int YScale
        {
            get;
            private set;
        }

        public int RedMultiplier
        {
            get;
            private set;
        }

        public int GreenMultiplier
        {
            get;
            private set;
        }

        public int BlueMultiplier
        {
            get;
            private set;
        }

        public int Hue
        {
            get;
            private set;
        }

        public uint Alpha
        {
            get;
            private set;
        }

        public Fixture(BigEndianReader _reader)
        {
            FixtureId = _reader.ReadInt();
            OffsetX = _reader.ReadShort();
            OffsetY = _reader.ReadShort();
            Rotation = _reader.ReadShort();
            XScale = _reader.ReadShort();
            YScale = _reader.ReadShort();
            RedMultiplier = _reader.ReadSByte();
            GreenMultiplier = _reader.ReadSByte();
            BlueMultiplier = _reader.ReadSByte();
            Hue = RedMultiplier | GreenMultiplier | BlueMultiplier;
            Alpha = _reader.ReadByte();
        }

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(FixtureId);
            writer.WriteShort((short)OffsetX);
            writer.WriteShort((short)OffsetY);
            writer.WriteShort((short)Rotation);
            writer.WriteShort((short)XScale);
            writer.WriteShort((short)YScale);

            writer.WriteSByte((sbyte)RedMultiplier);
            writer.WriteSByte((sbyte)GreenMultiplier);
            writer.WriteSByte((sbyte)BlueMultiplier);
            writer.WriteByte((byte)Alpha);
        }
    }
}

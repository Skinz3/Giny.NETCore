using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Giny.Core.IO;
using Giny.IO.DLM;
using Giny.IO.D2P;
using System.Reflection;
using Giny.IO.Compression;
using System.Threading;
using System.Drawing;

namespace Giny.IO.DLM
{
    public class DlmMap
    {
        public const string EncryptionKeyString = "649ae451ca33ec53bbcbcc33becf15f4";

        private byte[] EncryptionKey = Encoding.UTF8.GetBytes(EncryptionKeyString);

        public const byte MAP_HEADER = 77;

        public const int MAP_CELLS_COUNT = 560;

        public sbyte MapVersion
        {
            get;
            set;
        }

        public bool Encrypted
        {
            get;
            set;
        }

        public uint EncryptionVersion
        {
            get;
            set;
        }

        public int GroundCRC
        {
            get;
            set;
        }

        public int ZoomScale
        {
            get;
            set;
        }

        public int ZoomOffsetX
        {
            get;
            set;
        }

        public int ZoomOffsetY
        {
            get;
            set;
        }

        public int GroundCacheCurrentlyUsed
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public uint RelativeId
        {
            get;
            set;
        }

        public int MapType
        {
            get;
            set;
        }

        public List<Fixture> BackgroundFixtures
        {
            get;
            set;
        }
        public List<Fixture> ForegroundFixtures
        {
            get;
            set;
        }

        public short SubareaId
        {
            get;
            set;
        }

        public uint ShadowBonusOnEntities
        {
            get;
            set;
        }
        public long BackgroundAlpha
        {
            get;
            set;
        }
        public int BackgroundRed
        {
            get;
            set;
        }

        public int BackgroundGreen
        {
            get;
            set;
        }

        public int BackgroundBlue
        {
            get;
            set;
        }

        public long GridAlpha
        {
            get;
            set;
        }


        public int GridRed
        {
            get;
            set;
        }

        public int GridGreen
        {
            get;
            set;
        }

        public int GridBlue
        {
            get;
            set;
        }


        public int TopNeighbourId
        {
            get;
            set;
        }

        public int BottomNeighbourId
        {
            get;
            set;
        }

        public int LeftNeighbourId
        {
            get;
            set;
        }

        public int RightNeighbourId
        {
            get;
            set;
        }

        public bool UseLowPassFilter
        {
            get;
            set;
        }

        public bool UseReverb
        {
            get;
            set;
        }

        public int PresetId
        {
            get;
            set;
        }

        public bool IsUsingNewMovementSystem
        {
            get;
            set;
        }

        public List<Layer> Layers
        {
            get;
            set;
        }

        public CellData[] Cells
        {
            get;
            set;
        }

        public WorldPoint Position
        {
            get;
            set;
        }
        public int TacticalModeTemplateId
        {
            get;
            set;
        }
        public string D2PFilePath
        {
            get;
            set;
        }

        public DlmMap(byte[] compressedBuffer)
        {
            byte[] headerlessBuffer = new byte[compressedBuffer.Length - 2];
            Array.Copy(compressedBuffer, 2, headerlessBuffer, 0, compressedBuffer.Length - 2);

            using (var decompressMapStream = new MemoryStream(headerlessBuffer))
            {
                using (DeflateStream stream = new DeflateStream(decompressMapStream, CompressionMode.Decompress))
                {
                    using (BigEndianReader reader = new BigEndianReader(stream))
                    {
                        Deserialize(reader);
                    }

                }
            }
        }
       
        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte((sbyte)MAP_HEADER);

            writer.WriteSByte(MapVersion);

            writer.WriteUInt((uint)Id);

            if (MapVersion >= 7)
            {
                writer.WriteBoolean(Encrypted);
                writer.WriteSByte((sbyte)EncryptionVersion);
                writer.WriteInt(0); // null encryption
            }

            writer.WriteUInt(RelativeId);
            writer.WriteSByte((sbyte)MapType);
            writer.WriteInt(SubareaId);

            writer.WriteInt(TopNeighbourId);
            writer.WriteInt(BottomNeighbourId);
            writer.WriteInt(LeftNeighbourId);
            writer.WriteInt(RightNeighbourId);
            writer.WriteUInt(ShadowBonusOnEntities);

            if (MapVersion >= 9)
            {

                int backgroundColor = (int)(((BackgroundAlpha << 32) & 4278190080) | ((BackgroundRed << 16) & 16711680) |
                    (BackgroundGreen << 8 & 65280) | (BackgroundBlue & 255));

                writer.WriteInt(backgroundColor);

                uint gridColor = (uint)((GridAlpha << 32 & 4278190080) | (GridRed << 16 & 16711680) |
                   (GridGreen << 8 & 65280) | (GridBlue & 255));

                writer.WriteUInt(gridColor);
            }
            else if (MapVersion >= 3)
            {
                writer.WriteSByte((sbyte)BackgroundRed);
                writer.WriteSByte((sbyte)BackgroundGreen);
                writer.WriteSByte((sbyte)BackgroundBlue);
            }

            if (MapVersion >= 4)
            {
                writer.WriteUShort((ushort)(ZoomScale * 100));
                writer.WriteShort((short)ZoomOffsetX);
                writer.WriteShort((short)ZoomOffsetY);
            }
            if (MapVersion > 10)
            {
                writer.WriteInt(TacticalModeTemplateId);
            }

            writer.WriteBoolean(UseLowPassFilter);
            writer.WriteBoolean(UseReverb);

            if (UseReverb)
            {
                writer.WriteInt(PresetId);
            }


            writer.WriteByte((byte)BackgroundFixtures.Count);

            foreach (var fixture in BackgroundFixtures)
            {
                fixture.Serialize(writer);
            }

            writer.WriteByte((byte)ForegroundFixtures.Count);

            foreach (var fixture in ForegroundFixtures)
            {
                fixture.Serialize(writer);
            }


            writer.WriteInt(0); // fk ankama

            writer.WriteInt(GroundCRC); // dont forget ground is cached by client !

            writer.WriteByte((byte)Layers.Count);

            foreach (var layer in Layers)
            {
                layer.Serialize(writer, MapVersion);
            }

            foreach (var cell in Cells)
            {
                cell.Serialize(writer, MapVersion);
            }
        }
        private void Deserialize(BigEndianReader reader)
        {
            int header = reader.ReadSByte();
            int dataLen = 0;
            byte[] decryptionKey = EncryptionKey;

            if (header != MAP_HEADER)
                throw new FormatException("Unknown file header, first byte must be " + MAP_HEADER);


            MapVersion = reader.ReadSByte();
            Id = (int)reader.ReadUInt();

            if (MapVersion >= 7)
            {
                Encrypted = reader.ReadBoolean();
                EncryptionVersion = (uint)reader.ReadSByte();
                dataLen = reader.ReadInt();

                if (Encrypted)
                {
                    byte[] encryptedData = reader.ReadBytes(dataLen);

                    for (int i = 0; i < encryptedData.Length; i++)
                        encryptedData[i] = (byte)(encryptedData[i] ^ decryptionKey[i % decryptionKey.Length]);

                    reader = new BigEndianReader(new MemoryStream(encryptedData));
                }
            }

            RelativeId = reader.ReadUInt();
            Position = new WorldPoint(RelativeId);

            MapType = reader.ReadSByte();
            SubareaId = (short)reader.ReadInt();
            TopNeighbourId = reader.ReadInt();
            BottomNeighbourId = reader.ReadInt();
            LeftNeighbourId = reader.ReadInt();
            RightNeighbourId = reader.ReadInt();
            ShadowBonusOnEntities = reader.ReadUInt();

            if (MapVersion >= 9)
            {
                int readColor = 0;

                readColor = reader.ReadInt();
                this.BackgroundAlpha = (readColor & 4278190080) >> 32;
                this.BackgroundRed = (readColor & 16711680) >> 16;
                this.BackgroundGreen = (readColor & 65280) >> 8;
                this.BackgroundBlue = readColor & 255;
                readColor = (int)reader.ReadUInt();

                GridAlpha = (readColor & 4278190080) >> 32;
                GridRed = (readColor & 16711680) >> 16;
                GridGreen = (readColor & 65280) >> 8;
                GridBlue = readColor & 255;
            }
            else if (MapVersion >= 3)
            {
                BackgroundRed = reader.ReadSByte();
                BackgroundGreen = reader.ReadSByte();
                BackgroundBlue = reader.ReadSByte();
            }

            if (MapVersion >= 4)
            {
                ZoomScale = (ushort)(reader.ReadUShort() / 100);
                ZoomOffsetX = reader.ReadShort();
                ZoomOffsetY = reader.ReadShort();

                if (ZoomScale < 1)
                {
                    this.ZoomScale = 1;
                    this.ZoomOffsetX = this.ZoomOffsetY = 0;
                }
            }
            if (this.MapVersion > 10)
            {
                this.TacticalModeTemplateId = reader.ReadInt();
            }

            int bgCount = reader.ReadSByte();

            BackgroundFixtures = new List<Fixture>();

            for (int i = 0; i < bgCount; i++)
            {
                Fixture backgroundFixture = new Fixture(reader);
                BackgroundFixtures.Add(backgroundFixture);
            }


            var foregroundCount = reader.ReadByte();

            ForegroundFixtures = new List<Fixture>();

            for (int i = 0; i < foregroundCount; i++)
            {
                Fixture foregroundFixture = new Fixture(reader);
                ForegroundFixtures.Add(foregroundFixture);
            }

            reader.ReadInt(); // -_- ankama wtf... -_-

            GroundCRC = reader.ReadInt();
            var layersCount = reader.ReadByte();
            Layers = new List<Layer>();

            for (int i = 0; i < layersCount; i++)
            {
                Layer layer = new Layer(reader, MapVersion);
                Layers.Add(layer);
            }

            Cells = new CellData[MAP_CELLS_COUNT];

            for (short i = 0; i < Cells.Length; i++)
            {
                CellData cell = new CellData(reader, MapVersion, i);
                Cells[i] = cell;
            }
        }

        public byte[] Serialize()
        {
            using (var writer = new BigEndianWriter())
            {
                this.Serialize(writer);
                return writer.Data;
            }
        }

        
    }
}

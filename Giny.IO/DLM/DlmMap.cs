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
        private BigEndianReader _reader;

        public const string DefaultEncryptionKeyString = "649ae451ca33ec53bbcbcc33becf15f4";

        private byte[] _encryptionKey = Encoding.UTF8.GetBytes(DefaultEncryptionKeyString);

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
        public string HashCode
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
        public DlmMap()
        {

        }
        public DlmMap(CompressedMap compressedMap)
        {

            D2PFilePath = compressedMap.D2pFilePath;
            InitializeReader(compressedMap.D2pFilePath, (int)compressedMap.Offset, (int)compressedMap.BytesCount);
            Deserialize();
            _reader.Dispose();
        }
        public DlmMap(D2PEntry entry)
        {
            InitializeReader(entry.Container.FilePath, entry.Offset, entry.Size);
            _encryptionKey = Encoding.UTF8.GetBytes(DefaultEncryptionKeyString);
            Deserialize();
            _reader.Dispose();
        }
        public DlmMap(BigEndianReader uncompressedReader)
        {
            this._reader = uncompressedReader;
            _encryptionKey = Encoding.UTF8.GetBytes(DefaultEncryptionKeyString);
            Deserialize();
        }

        private void InitializeReader(string d2pFilePath, int offset, int bytesCount)
        {
            FileStream compressedMapStream = new FileStream(d2pFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader compressedMapReader = new BinaryReader(compressedMapStream);
            compressedMapReader.BaseStream.Position = offset;

            byte[] compressedMapBuffer = compressedMapReader.ReadBytes(bytesCount);

            if (compressedMapBuffer[0] == MAP_HEADER)
            {
                _reader = new BigEndianReader(compressedMapBuffer);
                return;
            }

            byte[] compressedMapBufferWithoutHeader = new byte[compressedMapBuffer.Length - 2];
            Array.Copy(compressedMapBuffer, 2, compressedMapBufferWithoutHeader, 0, compressedMapBuffer.Length - 2);

            StringBuilder mapHashCodeBuilder = new StringBuilder();
            byte[] compressedMapMd5Buffer = MD5.Create().ComputeHash(compressedMapBufferWithoutHeader);

            for (int i = 0; i < compressedMapMd5Buffer.Length; i++)
                mapHashCodeBuilder.Append(compressedMapMd5Buffer[i].ToString("X2"));

            HashCode = mapHashCodeBuilder.ToString();


            MemoryStream decompressMapStream = new MemoryStream(compressedMapBufferWithoutHeader);
            DeflateStream mapDeflateStream = new DeflateStream(decompressMapStream, CompressionMode.Decompress);

            _reader = new BigEndianReader(mapDeflateStream);

            compressedMapReader.Dispose();
            compressedMapStream.Close();
        }
        public static byte[] GetCompressedData(CompressedMap compressed)
        {
            FileStream compressedMapStream = new FileStream(compressed.D2pFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader compressedMapReader = new BinaryReader(compressedMapStream);
            compressedMapReader.BaseStream.Position = compressed.Offset;
            byte[] compressedMapBuffer = compressedMapReader.ReadBytes((int)compressed.BytesCount);
            return compressedMapBuffer;
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
        private void Deserialize()
        {
            int header = _reader.ReadSByte();
            int dataLen = 0;
            byte[] decryptionKey = _encryptionKey;

            if (header != MAP_HEADER)
                throw new FormatException("Unknown file header, first byte must be " + MAP_HEADER);


            MapVersion = this._reader.ReadSByte();
            Id = (int)this._reader.ReadUInt();

            if (MapVersion >= 7)
            {
                Encrypted = _reader.ReadBoolean();
                EncryptionVersion = (uint)_reader.ReadSByte();
                dataLen = this._reader.ReadInt();

                if (Encrypted)
                {
                    byte[] encryptedData = _reader.ReadBytes(dataLen);

                    for (int i = 0; i < encryptedData.Length; i++)
                        encryptedData[i] = (byte)(encryptedData[i] ^ decryptionKey[i % decryptionKey.Length]);

                    _reader = new BigEndianReader(new MemoryStream(encryptedData));
                }
            }

            RelativeId = _reader.ReadUInt();
            Position = new WorldPoint(RelativeId);

            MapType = _reader.ReadSByte();
            SubareaId = (short)_reader.ReadInt();
            TopNeighbourId = _reader.ReadInt();
            BottomNeighbourId = _reader.ReadInt();
            LeftNeighbourId = _reader.ReadInt();
            RightNeighbourId = _reader.ReadInt();
            ShadowBonusOnEntities = _reader.ReadUInt();

            if (MapVersion >= 9)
            {
                int readColor = 0;

                readColor = _reader.ReadInt();
                this.BackgroundAlpha = (readColor & 4278190080) >> 32;
                this.BackgroundRed = (readColor & 16711680) >> 16;
                this.BackgroundGreen = (readColor & 65280) >> 8;
                this.BackgroundBlue = readColor & 255;
                readColor = (int)_reader.ReadUInt();

                GridAlpha = (readColor & 4278190080) >> 32;
                GridRed = (readColor & 16711680) >> 16;
                GridGreen = (readColor & 65280) >> 8;
                GridBlue = readColor & 255;
            }
            else if (MapVersion >= 3)
            {
                BackgroundRed = _reader.ReadSByte();
                BackgroundGreen = _reader.ReadSByte();
                BackgroundBlue = _reader.ReadSByte();
            }

            if (MapVersion >= 4)
            {
                ZoomScale = (ushort)(_reader.ReadUShort() / 100);
                ZoomOffsetX = _reader.ReadShort();
                ZoomOffsetY = _reader.ReadShort();

                if (ZoomScale < 1)
                {
                    this.ZoomScale = 1;
                    this.ZoomOffsetX = this.ZoomOffsetY = 0;
                }
            }
            if (this.MapVersion > 10)
            {
                this.TacticalModeTemplateId = _reader.ReadInt();
            }

            int bgCount = _reader.ReadSByte();

            BackgroundFixtures = new List<Fixture>();

            for (int i = 0; i < bgCount; i++)
            {
                Fixture backgroundFixture = new Fixture(_reader);
                BackgroundFixtures.Add(backgroundFixture);
            }


            var foregroundCount = _reader.ReadByte();

            ForegroundFixtures = new List<Fixture>();

            for (int i = 0; i < foregroundCount; i++)
            {
                Fixture foregroundFixture = new Fixture(_reader);
                ForegroundFixtures.Add(foregroundFixture);
            }

            _reader.ReadInt(); // -_- ankama wtf... -_-

            GroundCRC = _reader.ReadInt();
            var layersCount = _reader.ReadByte();
            Layers = new List<Layer>();

            for (int i = 0; i < layersCount; i++)
            {
                Layer layer = new Layer(_reader, MapVersion);
                Layers.Add(layer);
            }

            Cells = new CellData[MAP_CELLS_COUNT];

            for (short i = 0; i < Cells.Length; i++)
            {
                CellData cell = new CellData(_reader, MapVersion, i);
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

        public static DlmMap Uncompress(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                var uncompressed = Deflate.Decompress(memoryStream);

                using (var reader = new BigEndianReader(uncompressed))
                {
                    return new DlmMap(reader);
                }
            }
        }
    }
}

using Giny.Core.IO;

namespace Giny.IO.DLM.Elements
{
    public class SoundElement : BasicElement
    {
        public int SoundId
        {
            get;
            set;
        }

        public int BaseVolume
        {
            get;
            set;
        }

        public int FullVolumeDistance
        {
            get;
            set;
        }

        public int NullVolumeDistance
        {
            get;
            set;
        }

        public int MinDelayBetweenLoops
        {
            get;
            set;
        }

        public int MaxDelayBetweenLoops
        {
            get;
            set;
        }

        public SoundElement(BigEndianReader _reader)
        {
            this.SoundId = _reader.ReadInt();
            this.BaseVolume = _reader.ReadShort();
            this.FullVolumeDistance = _reader.ReadInt();
            this.NullVolumeDistance = _reader.ReadInt();
            this.MinDelayBetweenLoops = _reader.ReadShort();
            this.MaxDelayBetweenLoops = _reader.ReadShort();
        }

        public override void Serialize(BigEndianWriter writer, sbyte mapVersion)
        {
            writer.WriteInt(SoundId);
            writer.WriteSByte((sbyte)BaseVolume);
            writer.WriteInt(FullVolumeDistance);
            writer.WriteInt(NullVolumeDistance);
            writer.WriteSByte((sbyte)MinDelayBetweenLoops);
            writer.WriteSByte((sbyte)MaxDelayBetweenLoops);
        }
    }
}

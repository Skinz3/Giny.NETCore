using Giny.Core.IO;
using System;
using System.IO;

namespace Giny.IO.DLM.Elements
{
    public abstract class BasicElement
    {
        public uint ElementId
        {
            get;
            set;
        }
        public static BasicElement GetElementFromType(int type, BigEndianReader _reader, sbyte mapVersion)
        {
            switch (type)
            {
                case 2:
                    var graph = new GraphicalElement(_reader, mapVersion);
                    return graph;
                case 33:
                    return new SoundElement(_reader);
                default:
                    throw new ArgumentException("Invalid Element type " + type.ToString());
            }
        }
        public abstract void Serialize(BigEndianWriter writer, sbyte mapVersion);
    }
}

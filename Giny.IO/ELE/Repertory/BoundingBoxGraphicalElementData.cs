using Giny.Core.IO;
using System.ComponentModel;

namespace Giny.IO.ELE.Repertory
{
    public class BoundingBoxGraphicalElementData : NormalGraphicalElementData
    {
        

        public override EleGraphicalElementTypes Type
        {
            get
            {
                return EleGraphicalElementTypes.BOUNDING_BOX;
            }
        }

        public BoundingBoxGraphicalElementData(Elements instance, int id)
            : base(instance, id)
        {
        }

        public new static BoundingBoxGraphicalElementData ReadFromStream(Elements instance, int id, BigEndianReader reader)
        {
            return new BoundingBoxGraphicalElementData(instance, id)
            {
                Gfx = reader.ReadInt(),
                Height = reader.ReadByte(),
                HorizontalSymmetry = reader.ReadBoolean(),
                OriginX = reader.ReadShort(),
                OriginY =reader.ReadShort(),
                Size = new System.Drawing.Size((int)reader.ReadShort(), (int)reader.ReadShort())
            };
        }
    }
}
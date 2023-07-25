using Giny.IO.DLM.Elements;
using Giny.IO.ELE.Repertory;
using Giny.Rendering.Textures;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.Maps.Elements
{
    public class MapEntityElement : MapElement
    {
        public EntityGraphicalElementData ElementData
        {
            get;
            private set;
        }
        public MapEntityElement(Cell cell, GraphicalElement dlmElement, EntityGraphicalElementData elementData) : base(cell, dlmElement)
        {
            this.ElementData = elementData;

        }

        public override void Draw(RenderWindow window)
        {
          
        }

        protected override Vector2f ComputePosition()
        {
            return Cell.Center;
        }
    }
}

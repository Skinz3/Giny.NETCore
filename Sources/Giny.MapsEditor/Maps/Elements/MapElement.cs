using Giny.IO.DLM.Elements;
using Giny.IO.ELE;
using Giny.Rendering.SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.Maps.Elements
{
    public abstract class MapElement : IDrawable 
    {
        public Cell Cell
        {
            get;
            private set;
        }
        public GraphicalElement DlmElement
        {
            get;
            private set;
        }
        public MapElement(Cell cell, GraphicalElement dlmElement)
        {
            this.Cell = cell;
            this.DlmElement = dlmElement;
        }

        protected abstract Vector2f ComputePosition();

        public abstract void Draw(RenderWindow window);
    }
}

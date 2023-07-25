using Giny.IO.DLM.Elements;
using Giny.IO.ELE.Repertory;
using Giny.Rendering.Maps.Elements;
using Giny.Rendering.SFML;
using Giny.Rendering.Textures;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.Maps
{
    public class Layer : IDrawable
    {
        public Layer()
        {
            this.Elements = new SortedDictionary<Cell, List<MapElement>>();
        }
        public SortedDictionary<Cell, List<MapElement>> Elements
        {
            get;
            set;
        }

        public void AddElement(Cell cell, TextureRecord record, GraphicalElement dlmElement, NormalGraphicalElementData elementData)
        {
            MapGraphicalElement element = new MapGraphicalElement(cell, record, dlmElement, elementData);

            if (Elements.ContainsKey(cell))
            {
                Elements[cell].Add(element);
            }
            else
            {
                Elements[cell] = new List<MapElement>() { element };
            }
        }
        public void AddElement(Cell cell, EntityGraphicalElementData elementData, GraphicalElement dlmElement)
        {
            MapEntityElement element = new MapEntityElement(cell, dlmElement, elementData);
            AddElement(element);
        }
        private void AddElement(MapElement element)
        {
            if (Elements.ContainsKey(element.Cell))
            {
                Elements[element.Cell].Add(element);
            }
            else
            {
                Elements[element.Cell] = new List<MapElement>() { element };
            }
        }
        public void Draw(RenderWindow window)
        {
            foreach (var cellElements in Elements.Values)
            {
                foreach (var element in cellElements)
                {
                    element.Draw(window);
                }
            }
        }

    }
}

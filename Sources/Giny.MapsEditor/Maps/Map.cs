using Giny.IO.DLM;
using Giny.IO.DLM.Elements;
using Giny.IO.ELE;
using Giny.IO.ELE.Repertory;
using Giny.Rendering.GFX;
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
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace Giny.Rendering.Maps
{
    public class Map : IDrawable
    {
        public Vector2f Position
        {
            get;
            private set;
        }
        public int Id
        {
            get;
            set;
        }
        public int Top
        {
            get;
            set;
        }
        public int Bottom
        {
            get;
            set;
        }
        public int ZoomScale
        {
            get;
            set;
        }
        public int Right
        {
            get;
            set;
        }
        public int Left
        {
            get;
            set;
        }
        public short SubareaId
        {
            get;
            set;
        }
        public bool DisplayBorders
        {
            get;
            set;
        } = true;

        public List<MapFixture> ForegroundFixtures
        {
            get;
            set;
        }

        public List<MapFixture> BackgroundFixtures
        {
            get;
            set;
        }

        public Cell GetCell(int id)
        {
            return Cells[id];
        }
        /// <summary>
        /// Get Cell from Pixel position
        /// </summary>
        /// <param name="position">Pixel position</param>
        public Cell GetCell(Vector2f position)
        {
            return Cells.FirstOrDefault(x => x.Contains(position));
        }

        public Cell[] Cells
        {
            get;
            private set;
        }
        public Dictionary<LayerEnum, Layer> Layers
        {
            get;
            private set;
        }
        private VertexBuffer GridBuffer
        {
            get;
            set;
        }
        public Color BackgroundColor
        {
            get;
            set;
        }
        public Map(Vector2f position = new Vector2f())
        {
            this.Position = position;
            this.BackgroundFixtures = new List<MapFixture>();
            this.ForegroundFixtures = new List<MapFixture>();
            CreateCells();
            Build();

            DisplayBorders = true;

        }

        public void AddElement(LayerEnum layer, int gfxId, int cellId, NormalGraphicalElementData elementData, IO.DLM.Elements.GraphicalElement dlmElement)
        {
            TextureRecord record = TextureManager.Instance.GetTexture(gfxId);

            if (record == null)
            {
                throw new Exception("Unable to find texture record! ");
            }

            Cell cell = GetCell(cellId);

            if (!Layers.ContainsKey(layer))
            {
                Layers.Add(layer, new Layer());
            }

            Layers[layer].AddElement(cell, record, dlmElement, elementData);
        }
        public T FindElement<T>(Func<T, bool> func) where T : MapElement
        {
            foreach (var layer in Layers.Values)
            {
                foreach (var pair in layer.Elements)
                {
                    var element = pair.Value.OfType<T>().FirstOrDefault(func);

                    if (element != null)
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public void Draw(RenderWindow window, params LayerEnum[] layerEnums)
        {
            View view = window.GetView();

            foreach (var fixture in BackgroundFixtures)
            {
                fixture.Draw(window);
            }
            foreach (var layer in Layers.Where(x => x.Key < LayerEnum.LAYER_DECOR))
            {
                if (layerEnums.Contains(layer.Key))
                {
                    layer.Value.Draw(window);
                }
            }

            if (DisplayBorders)
            {
                GridBuffer.Draw(window, RenderStates.Default);
            }

            foreach (var layer in Layers.Where(x => x.Key >= LayerEnum.LAYER_DECOR))
            {
                if (layerEnums.Contains(layer.Key))
                {
                    layer.Value.Draw(window);
                }
            }

            foreach (var fixture in ForegroundFixtures)
            {
                fixture.Draw(window);
            }
        }
        /// <summary>
        /// Redondance, voir si HasFlag reduit les performances.
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            Draw(window, LayerEnum.LAYER_GROUND, LayerEnum.LAYER_ADDITIONAL_GROUND, LayerEnum.LAYER_DECOR, LayerEnum.LAYER_ADDITIONAL_DECOR);
        }
        private void CreateCells()
        {
            this.Cells = new Cell[Constants.MAP_WIDTH * Constants.MAP_HEIGHT * 2];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell(this, i);
            }

            this.Layers = new Dictionary<LayerEnum, Layer>();

            ToogleBorders(true);
        }

        public void ToogleBorders(bool display)
        {
            DisplayBorders = display;
        }

        private void Build()
        {
            int cellId = 0;
            float cellWidth = Constants.CELL_WIDTH;
            float cellHeight = Constants.CELL_HEIGHT;
            float offsetX = Position.X;
            float offsetY = Position.Y;

            float midCellHeight = cellHeight / 2;
            float midCellWidth = cellWidth / 2;

            for (float y = 0; y < (2 * Constants.MAP_HEIGHT); y += 1)
            {
                if (y % 2 == 0)
                {
                    for (int x = 0; x < Constants.MAP_WIDTH; x++)
                    {
                        var left = new Vector2f(offsetX + x * cellWidth, offsetY + y * midCellHeight + midCellHeight);
                        var top = new Vector2f(offsetX + x * cellWidth + midCellWidth, offsetY + y * midCellHeight);
                        var right = new Vector2f(offsetX + x * cellWidth + cellWidth, offsetY + y * midCellHeight + midCellHeight);
                        var down = new Vector2f(offsetX + x * cellWidth + midCellWidth, offsetY + y * midCellHeight + cellHeight);

                        Cells[cellId++].SetPoints(new[] { left, top, right, down });
                    }
                }
                else
                {
                    for (int x = 0; x < Constants.MAP_WIDTH; x++)
                    {
                        var left = new Vector2f(offsetX + x * cellWidth + midCellWidth, offsetY + y * midCellHeight + midCellHeight);
                        var top = new Vector2f(offsetX + x * cellWidth + cellWidth, offsetY + y * midCellHeight);
                        var right = new Vector2f(offsetX + x * cellWidth + cellWidth + midCellWidth, offsetY + y * midCellHeight + midCellHeight);
                        var down = new Vector2f(offsetX + x * cellWidth + cellWidth, offsetY + y * midCellHeight + cellHeight);


                        Cells[cellId++].SetPoints(new[] { left, top, right, down });
                    }
                }
            }


            foreach (var cell in Cells)
            {
                cell.ComputePolygon();

            }

            this.GridBuffer = new VertexBuffer(Cell.VerticesCount * (uint)Cells.Count(), PrimitiveType.Lines, VertexBuffer.UsageSpecifier.Static);

            uint i = 0;

            foreach (var cell in Cells)
            {
                Vertex[] cellVertices = cell.GetLineVertices();
                this.GridBuffer.Update(cellVertices, (uint)cellVertices.Length, i);
                i += (uint)cellVertices.Count();
            }
        }
        public static Map FromDLM(DlmMap dlmMap, Dictionary<int, EleGraphicalData> elements, Vector2f position = default(Vector2f))
        {
            Map map = new Map(position);

            map.Id = dlmMap.Id;
            map.Top = dlmMap.TopNeighbourId;
            map.Left = dlmMap.LeftNeighbourId;
            map.Right = dlmMap.RightNeighbourId;
            map.Bottom = dlmMap.BottomNeighbourId;
            map.ZoomScale = dlmMap.ZoomScale;
            map.SubareaId = dlmMap.SubareaId;
            map.BackgroundColor = new Color((byte)dlmMap.BackgroundRed, (byte)dlmMap.BackgroundGreen, (byte)dlmMap.BackgroundBlue, (byte)dlmMap.BackgroundAlpha);

            foreach (var dlmLayer in dlmMap.Layers)
            {
                map.Layers.Add((LayerEnum)dlmLayer.LayerId, new Layer());

                foreach (var dlmCell in dlmLayer.Cells)
                {
                    foreach (var dlmElement in dlmCell.Elements.OfType<IO.DLM.Elements.GraphicalElement>())
                    {
                        EleGraphicalData element = null;

                        if (!elements.TryGetValue((int)dlmElement.ElementId, out element))
                        {
                            throw new Exception("Cannot find element: " + dlmElement.ElementId);
                        }

                        var cell = map.GetCell(dlmCell.CellId);

                        if (element is NormalGraphicalElementData)
                        {
                            var elementData = (NormalGraphicalElementData)element;

                            if (TextureManager.Instance.Exist(elementData.Gfx))
                            {
                                var textureRecord = TextureManager.Instance.GetTexture(elementData.Gfx);

                                map.Layers[(LayerEnum)dlmLayer.LayerId].AddElement(cell, textureRecord, dlmElement, elementData);
                            }
                            else
                            {
                                throw new Exception("unknown gfx.");
                            }
                        }


                        if (element is EntityGraphicalElementData)
                        {
                            var elementData = (EntityGraphicalElementData)element;
                            map.Layers[(LayerEnum)dlmLayer.LayerId].AddElement(cell, elementData, dlmElement);
                        }

                    }
                }
            }

            foreach (var fixture in dlmMap.BackgroundFixtures)
            {
                var mapFixture = new MapFixture(fixture);

                mapFixture.Sprite.Position += map.Position;

                map.BackgroundFixtures.Add(mapFixture);
            }

            foreach (var fixture in dlmMap.ForegroundFixtures)
            {
                var mapFixture = new MapFixture(fixture);
                mapFixture.Sprite.Position += map.Position;
                map.ForegroundFixtures.Add(mapFixture);
            }


            foreach (var cellData in dlmMap.Cells)
            {
                map.GetCell(cellData.Id).Data = cellData;
            }
            return map;
        }

    }
}

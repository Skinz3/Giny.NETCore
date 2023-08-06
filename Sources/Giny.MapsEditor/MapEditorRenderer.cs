using Giny.Core.Logging;
using Giny.IO;
using Giny.IO.D2P;
using Giny.IO.DLM;
using Giny.IO.ELE;
using Giny.Rendering.GFX;
using Giny.Rendering.Graphics;
using Giny.Rendering.Maps;
using Giny.Rendering.Textures;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.MapEditor
{
    public class SelectedTile
    {
        public bool Selected => Sprite != null;

        public Sprite? Sprite
        {
            get;
            private set;
        }
        public TextureRecord? TextureRecord
        {
            get;
            private set;
        }

        public SelectedTile()
        {

        }

        public void Assign(TextureRecord texture)
        {
            TextureRecord = texture;
            Sprite = texture.CreateSprite(true);
            Sprite.Color = new Color(255, 255, 255, 120);
        }


    }
    public class MapEditorRenderer : Renderer
    {
        private const float CameraSpeed = 20f;
        public List<Map> Maps
        {
            get;
            private set;
        } = new List<Map>();

        public Map CurrentMap
        {
            get;
            set;
        }
        RectangleShape ClientBounds
        {
            get;
            set;
        }

        public List<LayerEnum> DisplayedLayers
        {
            get;
            set;
        }

        public SelectedTile CurrentTile
        {
            get;
            set;
        } = new SelectedTile();

        private float CurrentZoom
        {
            get;
            set;
        } = 1f;

        public Map GetMap(int id, Vector2f position)
        {
            var dlmMap = MapsManager.Instance.ReadMap(id);

            if (dlmMap == null)
            {
                return null;
            }
            return Map.FromDLM(dlmMap, MapsManager.Instance.Elements, position);
        }

        public MapEditorRenderer(IntPtr handle) : base(handle)
        {
            var height = Constants.CELL_HEIGHT * Constants.MAP_HEIGHT;

            var width = Constants.CELL_WIDTH * Constants.MAP_WIDTH;

            var boundsWidth = width + Constants.CELL_HALF_WIDTH;

            var boundsHeight = height + (float)Constants.CELL_HALF_HEIGHT;

            ClientBounds = new RectangleShape(new Vector2f(boundsWidth, boundsHeight));
            ClientBounds.OutlineThickness = 2;
            ClientBounds.OutlineColor = Color.Red;
            ClientBounds.FillColor = Color.Transparent;

            DisplayedLayers = Map.AllLayers.ToList();

            Window.MouseWheelScrolled += MouseWheelScrolled;

        }

        private void MouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
        {
            var zoom = 1 - (e.Delta / 50);
            CurrentZoom = zoom;
            this.View.Zoom(zoom);
        }

        public override Color ClearColor => Color.White;

        public override void Draw()
        {
            HandleCameraMovement();

            foreach (var map in Maps)
            {
                map.Draw(Window, DisplayedLayers);
            }

            // Window.Draw(ClientBounds);


            if (CurrentTile.Selected)
            {
                var pos = Mouse.GetPosition(Window);

                var worldPos = Window.MapPixelToCoords(pos, View);

                var cell = CurrentMap.GetCell(worldPos);

                if (cell != null)
                {
                    CurrentTile.Sprite!.Position = cell.Center;
                    Window.Draw(CurrentTile.Sprite);
                }
            }

        }


        public void LoadMap(Map map)
        {
            Maps.Clear();
            Maps.Add(map);

            this.CurrentMap = map;

            //ClientView();

            /* var height = Constants.CELL_HEIGHT * Constants.MAP_HEIGHT;

               var width = Constants.CELL_WIDTH * Constants.MAP_WIDTH;
            var topMap = GetMap(map.Top, new Vector2f(0, -height));

            var leftMap = GetMap(map.Left, new Vector2f(-width, 0));

            var rightMap = GetMap(map.Right, new Vector2f(width, 0));

            var bottomMap = GetMap(map.Bottom, new Vector2f(0, height));


            Maps.Add(GetMap(topMap.Right, new Vector2f(width, -height)));

            Maps.Add(topMap);


            Maps.Add(GetMap(topMap.Left, new Vector2f(-width, -height)));
            Maps.Add(leftMap);

            Maps.Add(rightMap); 
            Maps.Add(GetMap(bottomMap.Right, new Vector2f(width, height)));

            Maps.Add(bottomMap);

            Maps.Add(GetMap(bottomMap.Left, new Vector2f(-width, height)));
            */


        }


        private void HandleCameraMovement()
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                MoveCamera(new Vector2f(-CameraSpeed, 0));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                MoveCamera(new Vector2f(CameraSpeed, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                MoveCamera(new Vector2f(0, -CameraSpeed));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                MoveCamera(new Vector2f(0, CameraSpeed));
            }


        }

        private void MoveCamera(Vector2f input)
        {
            MainWindow.Instance.FocusRenderer();
            View.Move(input);
        }



        public void ClientView()
        {
            View.Center = new Vector2f(CurrentMap.Size.X / 2, CurrentMap.Size.Y / 2);
            View.Size = new Vector2f(1920, 1080);
            View.Move(new Vector2f(50, 85));

        }
    }
}

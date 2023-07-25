using Giny.Core.Logging;
using Giny.IO;
using Giny.IO.D2P;
using Giny.IO.DLM;
using Giny.IO.ELE;
using Giny.Rendering.GFX;
using Giny.Rendering.Maps;
using Giny.Rendering.SFML;
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
    internal class MapEditorRenderer : Renderer
    {
        List<Map> Maps = new List<Map>();

        RectangleShape ClientBounds
        {
            get;
            set;
        }
        private Map GetMap(int id, Vector2f position)
        {
            return Map.FromDLM(MapsManager.Instance.ReadMap(id), MapsManager.Instance.Elements, position);
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

            Window.MouseWheelScrolled += MouseWheelScrolled;

        }

        private void MouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
        {
            this.View.Zoom(1 - (e.Delta / 50));
        }

        public override Color ClearColor => Color.White;

        public override void Draw()
        {
            HandleCameraMovement();

            foreach (var map in Maps)
            {
                map.Draw(Window);
            }

            Window.Draw(ClientBounds);

        }


        public Map LoadMap(int id)
        {
            Maps.Clear();


            //  var height = Constants.CELL_HEIGHT * Constants.MAP_HEIGHT;

            //   var width = Constants.CELL_WIDTH * Constants.MAP_WIDTH;

            var map = GetMap(id, new Vector2f());

            //   var topMap = LoadMap(map.Top, new Vector2f(0, -height));

            //    var leftMap = LoadMap(map.Left, new Vector2f(-width, 0));

            //   var rightMap = LoadMap(map.Right, new Vector2f(width, 0));

            //    var bottomMap = LoadMap(map.Bottom, new Vector2f(0, height));


            //  Maps.Add(LoadMap(topMap.Right, new Vector2f(width, -height)));

            //    Maps.Add(topMap);
            //    Maps.Add(rightMap);

            Maps.Add(map);

            //    Maps.Add(LoadMap(topMap.Left, new Vector2f(-width, -height)));
            //    Maps.Add(leftMap);

            //   Maps.Add(LoadMap(bottomMap.Right, new Vector2f(width, height)));

            //   Maps.Add(bottomMap);


            //  Maps.Add(LoadMap(bottomMap.Left, new Vector2f(-width, height)));

            return map;

        }
        private void HandleCameraMovement()
        {
            var CameraSpeed = 20f;


            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                View.Move(new Vector2f(-CameraSpeed, 0));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                View.Move(new Vector2f(CameraSpeed, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                View.Move(new Vector2f(0, -CameraSpeed));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                View.Move(new Vector2f(0, CameraSpeed));
            }


        }

    }
}

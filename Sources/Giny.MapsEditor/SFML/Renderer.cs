using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = SFML.Graphics.Color;

namespace Giny.Rendering.SFML
{
    public abstract class Renderer
    {
        private const uint FrameRateLimit = 60;

        protected RenderWindow Window
        {
            get;
            private set;
        }

        public abstract Color ClearColor
        {
            get;
        }

        public View View
        {
            get;
            set;
        }
        public Renderer(IntPtr handle)
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 7;


            this.Window = new RenderWindow(handle, settings);
            this.View = Window.GetView();
            Initialize();
        }

        private void Initialize()
        {
            Window.SetFramerateLimit(FrameRateLimit);
        }

        public void Display()
        {
            Window.SetActive();

            while (Window.IsOpen)
            {
                Loop();
            }
        }
        public void Loop()
        {
            Window.Clear(ClearColor);
            Window.DispatchEvents();
            Window.SetView(View);
            Draw();
            Window.Display();
        }

        public abstract void Draw();

        public RenderWindow GetWindow()
        {
            return this.Window;
        }

    }
}

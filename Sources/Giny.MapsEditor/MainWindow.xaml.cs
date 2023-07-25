using Giny.IO;
using Giny.MapEditor.Textures;
using Giny.MapEditor.Utils;
using Giny.Rendering.GFX;
using Giny.Rendering.Maps;
using Giny.Rendering.SFML;
using Giny.Rendering.Winforms;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace Giny.MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private MapEditorRenderer MapEditorRenderer
        {
            get;
            set;
        }
        public MainWindow()
        {

            InitializeComponent();

            MapsManager.Instance.Initialize();
            TextureManager.Instance.Initialize();
            TextureMapper.Instance.Initialize();

            host.Child = new DrawingSurface();
            this.Loaded += OnLoaded;

            MapEditorRenderer = new MapEditorRenderer(host.Child.Handle);
            MapEditorRenderer.View = new View(new Vector2f(1920 / 2f, 1080 / 2f), new Vector2f(1920, 1080));
            this.tileSelection.Load();

            var timer = new HighPrecisionTimer((int)(1000 / 60));
            timer.Tick += OnTick;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadMap(153880323);
        }

        private void OnTick(object? sender, HighPrecisionTimer.TickEventArgs e)
        {
            if (this.IsVisible)
            {
                Dispatcher.Invoke(() =>
                {
                    MapEditorRenderer.Loop();
                });
            }

        }

        private void LoadMap(int id)
        {
            var map = this.MapEditorRenderer.LoadMap(id);
            mapIdText.Text = id.ToString();
            topText.Text =  map.Top.ToString();
            rightText.Text = map.Right.ToString();
            leftText.Text = map.Left.ToString();
            bottomText.Text = map.Bottom.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadMap(int.Parse(mapIdText.Text));
        }
    }
}

using Giny.MapEditor.Textures;
using Giny.Rendering.GFX;
using Giny.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Giny.MapEditor
{
    /// <summary>
    /// Logique d'interaction pour TileSelection.xaml
    /// </summary>
    public partial class TileSelection : UserControl
    {
        public const int TileSize = 100;
        public const int TilePerLine = 15;

        public TileSelection()
        {
            InitializeComponent();
        }

        public void Load()
        {
            elementCategory.SelectionChanged += ElementCategory_SelectionChanged;
            foreach (var category in TextureMapper.Instance.Mapping.Textures.Keys)
            {
                elementCategory.Items.Add(category);
            }
        }

        private void DisplayCategory(string category)
        {

            tileCanvas.Children.Clear();

            var gfxIds = TextureMapper.Instance.Mapping.Textures[category];

            int i = 0;
            tileCanvas.Width = TilePerLine * TileSize;
            tileCanvas.Height = gfxIds.Count / TilePerLine * TileSize + 50;


            foreach (var gfxId in gfxIds)
            {
                var x = (i % TilePerLine) * TileSize;
                var y = (i / TilePerLine) * TileSize;

                TextureRecord texture = TextureManager.Instance.GetTexture(gfxId);

                BitmapImage icon = texture.GetIcon();

                Image rect = new Image();
                rect.Uid = gfxId.ToString();

                rect.MouseLeftButtonDown += OnTileClicked;
                rect.MouseEnter += OnTileEnter;
                rect.MouseLeave += OnTileLeave;
                rect.Source = icon;
                rect.Width = TileSize;
                rect.Height = TileSize;
                rect.Stretch = Stretch.Uniform;
                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                tileCanvas.Children.Add(rect);
                i++;

            }
        }

        private void OnTileLeave(object sender, MouseEventArgs e)
        {
            var img = (Image)sender;
            img.Opacity = 1;
        }

        private void OnTileEnter(object sender, MouseEventArgs e)
        {
            var img = (Image)sender;
            img.Opacity = 0.6;
        }

        private void OnTileClicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void ElementCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elementCategory.SelectedValue == null)
            {
                return;
            }

            var category = elementCategory.SelectedValue.ToString()!;

            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    DisplayCategory(category);
                }, System.Windows.Threading.DispatcherPriority.Background);

            });


        }
    }
}

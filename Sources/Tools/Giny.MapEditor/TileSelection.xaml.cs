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
        public event Action<TextureRecord> SelectedTextureChanged;

        private const int TileSize = 101;
        private const int TilePerLine = 12;

        public TileSelection()
        {
            InitializeComponent();
            InitializeScrollViewer();
        }

        private void InitializeScrollViewer()
        {
            scrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            

            scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // Calculate the number of visible lines based on the ScrollViewer's offset.
            int firstVisibleLine = (int)e.VerticalOffset / TileSize;
            int lastVisibleLine = (int)(e.VerticalOffset + e.ViewportHeight) / TileSize;

            // Assuming that TilePerLine is the number of tiles per line, calculate the number of visible columns.
            int firstVisibleColumn = (int)e.HorizontalOffset / TileSize;
            int lastVisibleColumn = (int)(e.HorizontalOffset + e.ViewportWidth) / TileSize;

            // Determine which categories are currently displayed.
            var category = elementCategory.SelectedValue?.ToString();
            if (category == null)
                return;

            // Clear existing children.
            tileCanvas.Children.Clear();

            var gfxIds = TextureMapper.Instance.Mapping.Textures[category];

            // Calculate the index range of visible tiles.
            int firstVisibleIndex = firstVisibleLine * TilePerLine + firstVisibleColumn;
            int lastVisibleIndex = Math.Min(gfxIds.Count - 1, lastVisibleLine * TilePerLine + lastVisibleColumn);

            // Create and display visible tiles.
            for (int i = firstVisibleIndex; i <= lastVisibleIndex; i++)
            {
                var x = (i % TilePerLine) * TileSize;
                var y = (i / TilePerLine) * TileSize;

                var gfxId = gfxIds[i];
                TextureRecord texture = TextureManager.Instance.GetTexture(gfxId);
                BitmapImage icon = texture.GetIcon();

                Image rect = new Image
                {
                    Uid = gfxId.ToString(),
                    Source = icon,
                    Width = TileSize,
                    Height = TileSize,
                    Stretch = Stretch.Uniform
                };

                rect.MouseLeftButtonDown += OnTileClicked;
                rect.MouseEnter += OnTileEnter;
                rect.MouseLeave += OnTileLeave;
                rect.DataContext = texture;

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                tileCanvas.Children.Add(rect);
            }
        }

        public void Load()
        {
            elementCategory.SelectionChanged += ElementCategory_SelectionChanged;
            foreach (var category in TextureMapper.Instance.Mapping.Textures.Keys)
            {
                elementCategory.Items.Add(category);
            }

            elementCategory.KeyDown += ((object sender, KeyEventArgs e) =>
            {
                e.Handled = true;
            });

            elementCategory.SelectedIndex = 0;
        }

        private void DisplayCategory(string category)
        {
            const int TileSize = 101;
            const int TilePerLine = 12;

            tileCanvas.Children.Clear();

            var gfxIds = TextureMapper.Instance.Mapping.Textures[category];


            int i = 0;
            tileCanvas.Width = TilePerLine * TileSize;
            tileCanvas.Height = gfxIds.Count / TilePerLine * TileSize + TileSize;
            gfxIds = gfxIds.Take(TilePerLine * 2).ToList();



            foreach (var gfxId in gfxIds)
            {
                var x = (i % TilePerLine) * TileSize;
                var y = (i / TilePerLine) * TileSize;

                TextureRecord texture = TextureManager.Instance.GetTexture(gfxId);

                BitmapImage icon = texture.GetIcon();

                Image rect = new Image();
                rect.DataContext = texture;
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
            var texture = (((Image)sender).DataContext as TextureRecord)!;

            SelectedTextureChanged?.Invoke(texture);    
        }

        private void ElementCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elementCategory.SelectedValue == null)
            {
                return;
            }

            var category = elementCategory.SelectedValue.ToString()!;

            DisplayCategory(category);
          


        }
    }
}

using Giny.Core;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.IO;
using Giny.IO.DLM;
using Giny.IO.ELE.Repertory;
using Giny.MapEditor.Textures;
using Giny.MapEditor.Utils;
using Giny.Rendering.GFX;
using Giny.Rendering.Maps;
using Giny.Rendering.Maps.Elements;
using Giny.Rendering.Textures;
using Giny.Rendering.Winforms;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
using MessageBox = System.Windows.MessageBox;
using View = SFML.Graphics.View;

namespace Giny.MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public static MainWindow Instance
        {
            get;
            private set;
        }

        public Map CurrentMap => MapEditorRenderer.CurrentMap;

        private MapEditorRenderer MapEditorRenderer
        {
            get;
            set;
        }

        private bool Fullscreen
        {
            get;
            set;
        } = false;

        public LayerEnum DrawingLayer => (LayerEnum)drawingLayer.SelectedValue;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();

            MapsManager.Instance.Initialize();
            TextureManager.Instance.Initialize();
            TextureMapper.Instance.Initialize();

            host.Child = new DrawingSurface();
            this.Loaded += OnLoaded;

            tileSelection.SelectedTextureChanged += OnSelectedTextureChanged;
            MapEditorRenderer = new MapEditorRenderer(host.Child.Handle);
            MapEditorRenderer.View = new View(new Vector2f(), new Vector2f(1920, 1080));
            this.tileSelection.Load();


            foreach (var layer in Map.AllLayers)
            {
                drawingLayer.Items.Add(layer);
            }

            drawingLayer.SelectedIndex = 0;

            var timer = new HighPrecisionTimer((int)(1000 / 60));
            timer.Tick += OnTick;



        }

        private void OnSelectedTextureChanged(TextureRecord obj)
        {
            MapEditorRenderer.CurrentTile.Assign(obj);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //   NewMap();  191105026
            LoadMap(MapEditorRenderer.GetMap(147850242, new Vector2f()));
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

        private void NewMap()
        {
            var map = new Map();

            map.Layers.Add(LayerEnum.LAYER_GROUND, new Layer());
            map.Layers.Add(LayerEnum.LAYER_ADDITIONAL_GROUND, new Layer());
            map.Layers.Add(LayerEnum.LAYER_DECOR, new Layer());
            map.Layers.Add(LayerEnum.LAYER_ADDITIONAL_DECOR, new Layer());
            LoadMap(map);
        }
        private void LoadMap(Map map)
        {
            this.MapEditorRenderer.LoadMap(map);


            UpdateMapInformations();

            map.MouseLeftClickCell += OnCellLeftClick;
            map.MouseRightClickCell += OnCellRightClick;
            this.DataContext = map;
        }


        private void LoadMap(int mapId)
        {
            var map = MapEditorRenderer.GetMap(mapId, new Vector2f());

            if (map != null)
            {
                LoadMap(map);
            }
            else
            {
                MessageBox.Show("Unable to find map : " + mapId + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Remove element
        /// </summary>
        private void OnCellRightClick(Cell cell)
        {
            if (CurrentMap.Layers.ContainsKey(DrawingLayer))
            {
                var element = CurrentMap.Layers[DrawingLayer].FirstOrDefault(cell);

                if (element != null)
                {
                    CurrentMap.Layers[DrawingLayer].Remove(element);
                }
            }

        }

        /// <summary>
        /// Add element
        /// </summary>
        private void OnCellLeftClick(Cell cell)
        {
            if (!MapEditorRenderer.CurrentTile.Selected)
            {
                return;
            }

            if (!host.IsFocused)
            {
                return;
            }
            var texture = MapEditorRenderer.CurrentTile.TextureRecord;

            var ele = MapsManager.Instance.Elements.Values.OfType<NormalGraphicalElementData>().Where(x => x.Gfx == texture.Id && !x.HorizontalSymmetry).FirstOrDefault();

            if (ele != null)
            {
                MapElement? previousElement = null;

                if (CurrentMap.Layers.ContainsKey(DrawingLayer))
                {
                    previousElement = CurrentMap.Layers[DrawingLayer].Find(cell, ele.Id);
                }

                if (previousElement != null)
                {
                    Console.WriteLine("Canceling duplicate element");
                    return;
                }

                CurrentMap.AddElement(DrawingLayer, cell, texture!, ele);

            }


        }

        private void LoadMapClick(object sender, RoutedEventArgs e)
        {
            var map = MapEditorRenderer.GetMap(int.Parse(mapIdText.Text), new Vector2f());

            if (map != null)
            {
                LoadMap(map);
            }
        }

        private void NewMapClick(object sender, RoutedEventArgs e)
        {
            NewMap();
        }


        public void FocusRenderer()
        {
            if (!host.IsFocused)
            {
                host.Focus();
                MapEditorRenderer.GetWindow().RequestFocus();
            }


        }



        private void ClientViewClick(object sender, RoutedEventArgs e)
        {
            MapEditorRenderer.ClientView();

        }

        private void ToggleFullScreenClick(object sender, RoutedEventArgs e)
        {
            if (Fullscreen)
            {
                host.Width = 1920 / 1.7;
                host.Height = 1080 / 1.7;
            }
            else
            {
                host.Width = 1920;
                host.Height = 1080;
            }

            Fullscreen = !Fullscreen;

        }

        private void ToggleGridClick(object sender, RoutedEventArgs e)
        {
            CurrentMap.DisplayGrid = !CurrentMap.DisplayGrid;
        }

        private void CacheDetailsClick(object sender, RoutedEventArgs e)
        {
            StringBuilder msg = new StringBuilder();

            msg.AppendLine("PNG count : " + TextureManager.Instance.GetTextures(TextureType.Png).Count);

            msg.AppendLine("SWF count : " + TextureManager.Instance.GetTextures(TextureType.Swf).Count);

            msg.AppendLine("JPG count : " + TextureManager.Instance.GetTextures(TextureType.Jpg).Count);

            MessageBox.Show(msg.ToString(), "Texture cache details", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DLM files (*.dlm) | *.dlm";
            var result = saveFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DlmMap map = CurrentMap.ToDLM();
                var bytes = map.Compress();

                File.WriteAllBytes(saveFileDialog.FileName, bytes);
            }
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLM files (*.dlm) | *.dlm";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);

                DlmMap map = new DlmMap(bytes);

                LoadMap(Map.FromDLM(map, MapsManager.Instance.Elements));
            }
        }

        private void UpdateMapInformations()
        {
            mapIdText.Text = CurrentMap.Id.ToString();
            topText.Text = CurrentMap.Top.ToString();
            rightText.Text = CurrentMap.Right.ToString();
            leftText.Text = CurrentMap.Left.ToString();
            bottomText.Text = CurrentMap.Bottom.ToString();

            displayGround.IsChecked = MapEditorRenderer.DisplayedLayers.Contains(LayerEnum.LAYER_GROUND);
            displayAdditionalGround.IsChecked = MapEditorRenderer.DisplayedLayers.Contains(LayerEnum.LAYER_ADDITIONAL_GROUND);
            displayDecor.IsChecked = MapEditorRenderer.DisplayedLayers.Contains(LayerEnum.LAYER_DECOR);
            displayAdditionalDecor.IsChecked = MapEditorRenderer.DisplayedLayers.Contains(LayerEnum.LAYER_ADDITIONAL_DECOR);
            displayBackgroundFixtures.IsChecked = MapEditorRenderer.CurrentMap.DisplayFixtures;


        }
        private void DisplayLayerClick(object sender, RoutedEventArgs e)
        {
            MapEditorRenderer.DisplayedLayers = new List<LayerEnum>();

            if (displayGround.IsChecked!.Value)
            {
                MapEditorRenderer.DisplayedLayers.Add(LayerEnum.LAYER_GROUND);
            }
            if (displayAdditionalGround.IsChecked!.Value)
            {
                MapEditorRenderer.DisplayedLayers.Add(LayerEnum.LAYER_ADDITIONAL_GROUND);
            }
            if (displayDecor.IsChecked!.Value)
            {
                MapEditorRenderer.DisplayedLayers.Add(LayerEnum.LAYER_DECOR);
            }
            if (displayAdditionalDecor.IsChecked!.Value)
            {
                MapEditorRenderer.DisplayedLayers.Add(LayerEnum.LAYER_ADDITIONAL_DECOR);
            }
            foreach (var map in MapEditorRenderer.Maps)
            {
                map.DisplayFixtures = displayBackgroundFixtures.IsChecked.Value;
            }
        }

        private void GoTopClick(object sender, RoutedEventArgs e)
        {
            LoadMap(CurrentMap.Top);
        }

        private void GoRightClick(object sender, RoutedEventArgs e)
        {
            LoadMap(CurrentMap.Right);
        }

        private void GoLeftClick(object sender, RoutedEventArgs e)
        {
            LoadMap(CurrentMap.Left);
        }

        private void GoBottomClick(object sender, RoutedEventArgs e)
        {
            LoadMap(CurrentMap.Bottom);
        }

        private void RandomMapClick(object sender, RoutedEventArgs e)
        {
            LoadMap(Map.FromDLM(MapsManager.Instance.ReadRandomMap(), MapsManager.Instance.Elements));
        }
    }
}

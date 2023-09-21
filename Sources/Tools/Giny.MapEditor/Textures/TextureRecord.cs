using Giny.Core.IO;
using Giny.IO.D2P;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Giny.Rendering.Textures
{
    public class TextureRecord : IDisposable
    {
        private const bool PixelInterpolation = true;

        private D2PEntry Entry
        {
            get;
            set;
        }

        public bool Loaded
        {
            get
            {
                return Texture != null;
            }
        }
        public Texture Texture
        {
            get;
            private set;
        }

        private MemoryStream Stream
        {
            get;
            set;
        }
        private BitmapImage Icon
        {
            get;
            set;
        }
        public int Id => int.Parse(Path.GetFileNameWithoutExtension(Name));

        public string Name => Entry.FileName;

        public TextureRecord(D2PEntry entry)
        {
            this.Entry = entry;
        }



        public void Load(D2PFile file)
        {
            Stream = new MemoryStream(file.ReadFile(Entry));
            Texture = new Texture(Stream);
            Texture.Smooth = PixelInterpolation;
        }



        public BitmapImage GetIcon()
        {
            if (Icon != null)
            {
                return Icon;
            }

            BitmapImage originalImage = new BitmapImage();
            originalImage.BeginInit();
            originalImage.CacheOption = BitmapCacheOption.OnLoad;
            originalImage.StreamSource = Stream;
            originalImage.EndInit();
            originalImage.Freeze();

            Icon = originalImage;

            return Icon;
        }

        public Sprite CreateSprite(bool centerOrigin = false)
        {
            Sprite result = new Sprite(Texture);

            if (centerOrigin)
            {
                result.Origin = new Vector2f(result.Texture.Size.X / 2, result.Texture.Size.Y / 2);
            }
            return result;
        }

        public void Dispose()
        {
            this.Icon = null;
            this.Stream.Dispose();
            this.Stream = null;
            this.Texture.Dispose();
            this.Texture = null;
        }
    }
}

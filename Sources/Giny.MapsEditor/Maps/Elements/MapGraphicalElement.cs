using Giny.Core.Extensions;
using Giny.IO.DLM.Elements;
using Giny.IO.ELE.Repertory;
using Giny.Rendering.Textures;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Giny.Rendering.Maps.Elements
{
    public class MapGraphicalElement : MapElement
    {
        private TextureRecord TextureRecord
        {
            get;
            set;
        }

        public string SpriteName
        {
            get
            {
                return TextureRecord.Name;
            }
        }
        public Sprite Sprite
        {
            get;
            private set;
        }

        public NormalGraphicalElementData ElementData
        {
            get;
            private set;
        }
        public MapGraphicalElement(Cell cell, TextureRecord record, GraphicalElement dlmElement, NormalGraphicalElementData elementData) : base(cell, dlmElement)
        {
            ElementData = elementData;
            TextureRecord = record;
            Sprite = TextureRecord.CreateSprite();

            Sprite.Position = ComputePosition();

            if (ElementData.HorizontalSymmetry)
            {
                Sprite.Scale = new Vector2f(-1, 1);
                Sprite.Position += new Vector2f(Sprite.TextureRect.Width, 0);
            }


            if (DlmElement.HueR == 0)
            {
                //  Sprite.Scale = new Vector2f();

            }
            else
            {

            }
            //   var r = (DlmElement.HueR) + (DlmElement.ShadowR);
            //  var g = (DlmElement.HueG) + (DlmElement.ShadowG);
            // var b = (DlmElement.HueB) + (DlmElement.ShadowB); 


            var r = MathExtensions.Clamp((DlmElement.HueR + DlmElement.ShadowR + 128) * 2, 0, 512);
            var g = MathExtensions.Clamp((DlmElement.HueG + DlmElement.ShadowG + 128) * 2, 0, 512);
            var b = MathExtensions.Clamp((DlmElement.HueB + DlmElement.ShadowB + 128) * 2, 0, 512);


            var high = Math.Max(r, Math.Max(g, b));

            r *= 255 / high;
            g *= 255 / high;
            b *= 255 / high;
            Sprite.Color = new Color((byte)r, (byte)g, (byte)b);


        }
        protected override Vector2f ComputePosition()
        {
            var finalPosition = new Vector2f(Cell.Center.X, Cell.Center.Y);

            finalPosition.X -= ElementData.OriginX;
            finalPosition.Y -= ElementData.OriginY;

            finalPosition.X += (float)(Math.Round((double)(Constants.CELL_HALF_WIDTH + DlmElement.PixelOffsetX)));

            finalPosition.Y += (float)(Math.Round(Constants.CELL_HALF_HEIGHT - (DlmElement.Altitude * 10d) + DlmElement.PixelOffsetY));
            return finalPosition;

        }




        public void Dispose()
        {
            Sprite.Dispose();
        }

        public override void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }

        public void ComputeCenterPixelOffset()
        {
            DlmElement.PixelOffsetX = (int)(this.ElementData.OriginX - (TextureRecord.Texture.Size.X / 2) - Constants.CELL_HALF_WIDTH);

            DlmElement.PixelOffsetY = (int)(this.ElementData.OriginY - (TextureRecord.Texture.Size.Y / 2) - Constants.CELL_HALF_HEIGHT);

            Sprite.Position = ComputePosition();
        }
    }
}

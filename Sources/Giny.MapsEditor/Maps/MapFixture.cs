using Giny.IO.DLM;
using Giny.Rendering.GFX;
using Giny.Rendering.SFML;
using Giny.Rendering.Textures;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.Maps
{
    public class MapFixture : IDrawable
    {
        private Fixture FixtureData
        {
            get;
            set;
        }
        private TextureRecord Texture
        {
            get;
            set;
        }
        public Sprite Sprite
        {
            get;
            set;
        }
        public MapFixture(Fixture fixtureData)
        {
            this.FixtureData = fixtureData;

            Texture = TextureManager.Instance.GetTexture(FixtureData.FixtureId, TextureType.Png);

            if (Texture == null)
            {
                Texture = TextureManager.Instance.GetTexture(FixtureData.FixtureId, TextureType.Jpg);
            }
            if (Texture == null)
            {
                var test = TextureManager.Instance.GetTexture(FixtureData.FixtureId, TextureType.Swf);
                throw new Exception("Unhandled fixture");
            }

            this.Sprite = Texture.CreateSprite();

            var width = Sprite.TextureRect.Width;
            var height = Sprite.TextureRect.Height;
            var halfWidth = width * 0.5f;
            var halfHeight = height * 0.5f;
          
            Sprite.Origin = new Vector2f(halfWidth, halfHeight);
            Sprite.Scale = new Vector2f(FixtureData.XScale / 1000, FixtureData.YScale / 1000);

            Sprite.Rotation = FixtureData.Rotation / 100f;
            Sprite.Position += new Vector2f((FixtureData.OffsetX + Constants.CELL_HALF_WIDTH) * 1 + halfWidth, (FixtureData.OffsetY + Constants.CELL_HEIGHT) * 1 + halfHeight);
            
            if (FixtureData.RedMultiplier > 0)
            {

            }
           /* if (int(fixture.redMultiplier) || int(fixture.greenMultiplier) || fixture.blueMultiplier || fixture.alpha != 1)
            {
                this._clTrans.redMultiplier = fixture.redMultiplier / 127 + 1;
                this._clTrans.greenMultiplier = fixture.greenMultiplier / 127 + 1;
                this._clTrans.blueMultiplier = fixture.blueMultiplier / 127 + 1;
                this._clTrans.alphaMultiplier = fixture.alpha / 255;
                container.bitmapData.draw(bmpdt, this._m, this._clTrans, null, null, smoothing);
            }*/
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }
    }
}

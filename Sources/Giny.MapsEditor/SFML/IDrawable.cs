using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Rendering.SFML
{
    public interface IDrawable
    {
        void Draw(RenderWindow window);
    }
}

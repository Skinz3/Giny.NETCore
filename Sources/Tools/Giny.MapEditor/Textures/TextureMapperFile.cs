using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.MapEditor.Textures
{
    public class TextureMapperFile
    {
        public Dictionary<string, List<int>> Textures
        {
            get;
            set;
        } = new Dictionary<string, List<int>>();
    }
}

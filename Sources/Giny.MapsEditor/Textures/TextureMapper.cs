using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO;
using Giny.Core.Logging;
using Giny.IO;
using Giny.IO.D2I;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.IO.DLM;
using Giny.IO.DLM.Elements;
using Giny.IO.ELE.Repertory;
using Giny.Rendering.GFX;
using Giny.Rendering.Maps;
using Giny.Rendering.Textures;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.MapEditor.Textures
{
    public class TextureMapper : Singleton<TextureMapper>
    {
        private const string TextureMapperFilepath = "textures.json";

        private const string UnknownTextureCategory = "Unknown";

        public TextureMapperFile Mapping
        {
            get;
            private set;
        } = new TextureMapperFile();

        public void Initialize()
        {
            if (!File.Exists(TextureMapperFilepath))
            {
                MapTextures();
            }
            else
            {
                this.Mapping = Json.Deserialize<TextureMapperFile>(File.ReadAllText(TextureMapperFilepath));
            }


        }

        void MapTextures()
        {
            Logger.Write("Mapping textures...");


            Mapping = new TextureMapperFile();


            D2IManager.Initialize(Path.Combine(ClientConstants.ClientPath, ClientConstants.i18nPathFR));


            D2OManager.Initialize(Path.Combine(ClientConstants.ClientPath, ClientConstants.D2oDirectory));

            var subareas = D2OManager.GetObjects("SubAreas.d2o");

            Dictionary<int, string> subareaNames = new Dictionary<int, string>();


            foreach (SubArea subarea in subareas)
            {
                subareaNames.Add(subarea.Id, D2IManager.GetText((int)subarea.NameId));
            }


            var d2p = MapsManager.Instance.GetFile();

            ProgressLogger logger = new ProgressLogger();

            int current = 0;
            int length = d2p.Entries.Count();


            foreach (var entry in d2p.Entries)
            {
                var map = new DlmMap(d2p.ReadFile(entry));
                AssignGfxCategories(map, subareaNames);
                current++;
                logger.WriteProgressBar(current, length);
            }



            var content = Json.Serialize(Mapping);

            File.WriteAllText(TextureMapperFilepath, content);
        }

        string AssignGfxCategories(DlmMap map, Dictionary<int, string> categoryNames)
        {
            foreach (DlmLayer layer in map.Layers)
            {
                foreach (var cell in layer.Cells)
                {
                    var elements = cell.Elements.OfType<GraphicalElement>();

                    foreach (var element in elements)
                    {
                        if (MapsManager.Instance.Elements.ContainsKey((int)element.ElementId))
                        {
                            var normalGraphical = MapsManager.Instance.Elements[(int)element.ElementId] as NormalGraphicalElementData;

                            if (normalGraphical != null)
                            {
                                if (categoryNames.ContainsKey(map.SubareaId))
                                {
                                    var name = categoryNames[map.SubareaId];

                                    if (Mapping.Textures.ContainsKey(name))
                                    {
                                        if (!Mapping.Textures[name].Contains(normalGraphical.Gfx))
                                        {
                                            Mapping.Textures[name].Add(normalGraphical.Gfx);
                                        }
                                    }
                                    else
                                    {
                                        Mapping.Textures.Add(name, new List<int>() { normalGraphical.Gfx });
                                    }
                                }
                            }

                        }
                    }

                }
            }

            return UnknownTextureCategory;

        }
    }
}

using Giny.IO.DLM;
using Giny.IO.DLM.Elements;
using Giny.Core;
using Giny.IO.D2P;
using Giny.IO.DLM;
using Giny.IO.ELE;
using Giny.IO.ELE.Repertory;
using Giny.ORM;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.IO;

namespace Giny.DatabaseSynchronizer
{
    class MapSynchronizer
    {
        public const string MAP_ENCRYPTION_KEY = "649ae451ca33ec53bbcbcc33becf15f4";

        public const string MAPS_PATH = "content/maps/";

        static Dictionary<int, EleGraphicalData> Elements;

        public static void Synchronize()
        {
            if (!Program.SYNC_MAPS)
            {
                return;
            }

            Dictionary<long, MapPosition> mapPositions = D2OSynchronizer.d2oReaders.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "MapPosition")).
                EnumerateObjects().Cast<MapPosition>().ToDictionary(x => (long)x.id, x => x);

            Logger.Write("Building Maps...");

            foreach (var file in Directory.GetFiles(Path.Combine(ClientConstants.ClientPath, MAPS_PATH)))
            {
                if (Path.GetExtension(file) == ".ele")
                {
                    Elements = EleReader.ReadElements(file);
                }
                if (Path.GetExtension(file).ToLower() == ".d2p")
                {
                    Logger.Write(Path.GetFileName(file) + "...");
                    D2PFastMapFile d2p = new D2PFastMapFile(file);

                    foreach (var compressedMap in d2p.CompressedMaps)
                    {
                        DlmMap map = new DlmMap(compressedMap.Value);
                        if (map.Cells == null)
                            continue;

                        if (!mapPositions.ContainsKey(map.Id))
                        {
                            Logger.Write("Map " + map.Id + " has no Map Position in D2O files. Skipping", Channels.Warning);
                            continue;
                        }
                        MapRecord record = new MapRecord();

                        record.Version = map.MapVersion;
                        record.Id = map.Id;
                        record.SubareaId = map.SubareaId;
                        record.RightMap = map.RightNeighbourId;
                        record.LeftMap = map.LeftNeighbourId;
                        record.TopMap = map.TopNeighbourId;
                        record.BottomMap = map.BottomNeighbourId;
                        record.Cells = new CellRecord[560];

                        for (short i = 0; i < record.Cells.Length; i++)
                        {
                            var cell = map.Cells[i];

                            record.Cells[i] = new CellRecord()
                            {
                                Blue = cell.Blue,
                                Red = cell.Red,
                                Id = i,
                                LosMov = cell.Losmov,
                                MapChangeData = cell.MapChangeData,
                            };
                        }

                        var layers = map.Layers;

                        List<InteractiveElementRecord> elements = new List<InteractiveElementRecord>();

                        foreach (var layer in layers)
                        {
                            foreach (DlmCell layerCell in layer.Cells)
                            {
                                foreach (var graphicalElement in layerCell.Elements.OfType<GraphicalElement>())
                                {
                                    if (graphicalElement.Identifier != 0)
                                    {
                                        InteractiveElementRecord interactiveRecord = new InteractiveElementRecord();

                                        if (!Elements.ContainsKey((int)graphicalElement.ElementId))
                                        {
                                            Logger.Write("Unknown element id " + graphicalElement.ElementId, Channels.Warning);
                                            continue;
                                        }

                                        var gfxElement = Elements[(int)graphicalElement.ElementId];

                                        interactiveRecord.OffsetX = graphicalElement.OffsetX;
                                        interactiveRecord.OffsetY = graphicalElement.OffsetY;
                                        interactiveRecord.Identifier = (int)graphicalElement.Identifier;
                                        interactiveRecord.CellId = layerCell.CellId;


                                        if (gfxElement.Type != EleGraphicalElementTypes.ENTITY)
                                        {
                                            NormalGraphicalElementData normalElement = gfxElement as NormalGraphicalElementData;

                                            if (normalElement != null)
                                                interactiveRecord.GfxId = normalElement.Gfx;

                                            interactiveRecord.BonesId = -1;

                                        }
                                        else
                                        {
                                            EntityGraphicalElementData entityElement = gfxElement as EntityGraphicalElementData;

                                            interactiveRecord.BonesId = ushort.Parse(entityElement.EntityLook.Replace("{", "").Replace("}", ""));
                                            interactiveRecord.GfxId = -1;

                                        }
                                        elements.Add(interactiveRecord);


                                    }
                                }
                            }
                        }
                        record.Elements = elements.ToArray();
                        record.AddInstantElement();
                    }
                }
            }
            // parse .ele
        }
    }
}

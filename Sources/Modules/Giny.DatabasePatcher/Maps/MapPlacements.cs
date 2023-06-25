using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.ORM;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Maps
{
    public class MapPlacements
    {
        public static bool SortByComplexity = true;

        public static byte SearchDeep = 5;

        public static string PlacementPatternDirectory = "PlacementPatterns/";

        public static void Patch()
        {
            Logger.Write("Building map placements ...");

            IEnumerable<MapRecord> maps = MapRecord.GetMaps();

            int mapsCount = maps.Count();

            List<PlacementPattern> patterns = new List<PlacementPattern>();
            Dictionary<PlacementPattern, string> patternsNames = new Dictionary<PlacementPattern, string>();
            foreach (string file in Directory.EnumerateFiles(PlacementPatternDirectory, "*.xml", SearchOption.AllDirectories))
            {
                try
                {
                    PlacementPattern pattern = Xml.Deserialize<PlacementPattern>(File.ReadAllText(file));

                    if (SortByComplexity)
                    {
                        PlacementComplexityCalculator calc = new PlacementComplexityCalculator(pattern.Blues.Concat(pattern.Reds).ToArray<System.Drawing.Point>());
                        pattern.Complexity = calc.Compute();
                    }
                    patterns.Add(pattern);
                    patternsNames.Add(pattern, Path.GetFileNameWithoutExtension(file));
                }
                catch
                {
                    Logger.Write("Unable to load pattern " + Path.GetFileNameWithoutExtension(file), Channels.Critical);
                    Console.Read();
                }
            }


            Random rand = new Random();

            int succesCount = 0;

            foreach (var map in maps)
            {
                int[] relativePatternsComplx = (from entry in patterns
                                                where entry.Relativ
                                                select entry.Complexity).ToArray<int>();
                PlacementPattern[] relativPatterns = (from entry in patterns
                                                      where entry.Relativ
                                                      select entry).ShuffleWithProbabilities(relativePatternsComplx).ToArray<PlacementPattern>();
                Lozenge searchZone = new Lozenge(0, SearchDeep);

                var centerCell = map.GetCell(300);

                CellRecord[] cells = searchZone.GetCells(centerCell, centerCell, map);

                foreach (var cell in map.Cells)
                {
                    cell.Blue = false;
                    cell.Red = false;
                }
                for (int j = 0; j < cells.Length; j++)
                {
                    CellRecord center = cells[j];

                    var pattern = relativPatterns.FirstOrDefault((PlacementPattern entry) => entry.TestPattern(center.Point, map));

                    if (pattern != null)
                    {
                        CellRecord[] blues = (from entry in pattern.Blues
                                              select map.GetCell(entry.X + center.Point.X, entry.Y + center.Point.Y)).ToArray();
                        CellRecord[] reds = (from entry in pattern.Reds
                                             select map.GetCell(entry.X + center.Point.X, entry.Y + center.Point.Y)).ToArray();

                        for (int i = 0; i < blues.Length; i++)
                        {
                            blues[i].Blue = true;
                        }

                        for (int i = 0; i < reds.Length; i++)
                        {
                            reds[i].Red = true;
                        }

                        succesCount++;
                        break;
                    }
                }

                map.ReloadMembers();
                map.UpdateInstantElement();

            }

            Logger.Write(string.Format("{0} on {1} maps fixed ({2:0.0}%)", succesCount, mapsCount, succesCount / (double)mapsCount * 100.0));

            Logger.Write("Fixing maps with random pattern ...");

            var randomizedMaps = maps.Where(x => x.BlueCells.Count() == 0 || x.RedCells.Count() == 0);

            const int MaxPlacements = 11;

            foreach (var map in randomizedMaps)
            {
                var cells = map.Cells.Where(x => x.IsValidFightCell());

                var n = cells.Count() / 2;

                if (n / 2d > MaxPlacements)
                {
                    n = MaxPlacements;
                }

                var blues = cells.Shuffle().Take(n);

                var reds = cells.Shuffle().Where(x => !blues.Contains(x)).Take(n);


                foreach (var blue in blues)
                {
                    blue.Blue = true;
                }

                foreach (var red in reds)
                {
                    red.Red = true;
                }

                succesCount++;

                map.ReloadMembers();
                map.UpdateInstantElement();
            }

            Logger.Write(string.Format("{0} on {1} maps fixed ({2:0.0}%)", succesCount, mapsCount, succesCount / (double)mapsCount * 100.0));
        }

    }
}

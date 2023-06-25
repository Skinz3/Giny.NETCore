using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Giny.DatabasePatcher.Maps
{
    public class PlacementPattern
    {
        public bool Relativ
        {
            get;
            set;
        }

        public Point[] Blues
        {
            get;
            set;
        }

        public Point[] Reds
        {
            get;
            set;
        }

        public Point Center
        {
            get;
            set;
        }

        [XmlIgnore]
        public int Complexity
        {
            get;
            set;
        }

        public bool TestPattern(MapRecord map)
        {
            bool result;
            try
            {
                bool bluesOk;
                bool redsOk;
                if (this.Relativ)
                {
                    bluesOk = this.Blues.All((Point entry) => map.IsValidFightCell(entry.X + this.Center.X, entry.Y + this.Center.Y));
                    redsOk = this.Reds.All((Point entry) => map.IsValidFightCell(entry.X + this.Center.X, entry.Y + this.Center.Y));
                }
                else
                {
                    bluesOk = this.Blues.All((Point entry) => map.IsValidFightCell(entry.X, entry.Y));
                    redsOk = this.Reds.All((Point entry) => map.IsValidFightCell(entry.X, entry.Y));
                }
                result = (bluesOk && redsOk);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool TestPattern(MapPoint center, MapRecord map)
        {
            bool result;
            try
            {
                bool bluesOk;
                bool redsOk;

                if (this.Relativ)
                {
                    bluesOk = this.Blues.All((Point entry) => map.IsValidFightCell(entry.X + center.X, entry.Y + center.Y));
                    redsOk = this.Reds.All((Point entry) => map.IsValidFightCell(entry.X + center.X, entry.Y + center.Y));
                }
                else
                {
                    bluesOk = this.Blues.All((Point entry) => map.IsValidFightCell(entry.X, entry.Y));
                    redsOk = this.Reds.All((Point entry) => map.IsValidFightCell(entry.X, entry.Y));
                }
                result = (bluesOk && redsOk);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}

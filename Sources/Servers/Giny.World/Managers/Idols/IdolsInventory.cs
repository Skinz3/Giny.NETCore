using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Network;
using Giny.World.Records.Idols;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Idols
{
    public class IdolsInventory
    {
        private const int MaxEquippedIdols = 6;

        private Dictionary<IdolRecord, bool> Idols
        {
            get;
            set;
        }
        private int EquippedIdolsCount
        {
            get
            {
                return Idols.Where(x => x.Value).Count();
            }
        }

        public IdolsInventory(Character owner)
        {
            this.Idols = new Dictionary<IdolRecord, bool>();

            foreach (var item in owner.Inventory.GetItems(x => x.Record.TypeEnum == ItemTypeEnum.IDOL))
            {
                IdolRecord idol = IdolRecord.GetIdolRecordByItem(item.GId);
                Idols.Add(idol, false);
            }
        }
        public IdolsInventory(IEnumerable<IdolRecord> idols)
        {
            this.Idols = new Dictionary<IdolRecord, bool>();

            foreach (var idol in idols)
            {
                Idols.Add(idol, false);
            }
        }

        public void Update(Character owner)
        {
            foreach (var item in owner.Inventory.GetItems(x => x.Record.TypeEnum == ItemTypeEnum.IDOL))
            {
                IdolRecord idol = IdolRecord.GetIdolRecordByItem(item.GId);

                if (!Idols.ContainsKey(idol))
                {
                    Idols.Add(idol, false);
                }
            }


            foreach (var idol in Idols.Keys.ToArray())
            {
                if (!owner.Inventory.GetItems().Any(x => x.GId == idol.ItemId))
                {
                    Idols.Remove(idol);
                }
            }

        }

        public IdolXp GetIdolXp()
        {
            return new IdolXp(Idols.Keys);
        }

        public IEnumerable<IdolRecord> GetActiveIdols()
        {
            return Idols.Where(x => x.Value).Select(x => x.Key);
        }
        public IEnumerable<IdolRecord> GetAllIdols()
        {
            return Idols.Keys;
        }

        public bool Select(short idolId, bool activate)
        {
            IdolRecord record = IdolRecord.GetIdolRecord(idolId);

            if (record == null)
            {
                return false;
            }

            if (activate && EquippedIdolsCount >= MaxEquippedIdols)
            {
                return false;
            }

            if (!activate && EquippedIdolsCount == 0)
            {
                return false;
            }

            if (activate)
            {
                if (Idols.ContainsKey(record))
                {
                    Idols[record] = true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Idols[record] = false;
            }

            return true;
        }
    }
}
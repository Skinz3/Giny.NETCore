using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Items
{
    [D2OClass("ItemSet")]
    [Table("itemSets")]
    public class ItemSetRecord : ITable
    {
        [Container]
        private static Dictionary<long, ItemSetRecord> ItemSets = new Dictionary<long, ItemSetRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }

        [ProtoSerialize]
        [D2OField("items")]
        public List<short> Items
        {
            get;
            set;
        }

        [ProtoSerialize]
        [D2OField("effects")]
        public List<EffectCollection> Effects
        {
            get;
            set;
        }

        [StartupInvoke("Item sets", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (ItemSetRecord itemSet in ItemSets.Values)
            {
                for (int i = 0; i < itemSet.Effects.Count; i++)
                {
                    itemSet.Effects[i] = itemSet.Effects[i].Generate();
                }
            }
        }

        public EffectCollection GetEffects(int itemCount)
        {
            if (Effects.Count >= itemCount)
                return Effects[itemCount - 1];
            else
                return new EffectCollection();
        }

        public static ItemSetRecord GetItemSet(int itemSetId)
        {
            ItemSetRecord result;
            ItemSets.TryGetValue(itemSetId, out result);
            return result;
        }


    }
}

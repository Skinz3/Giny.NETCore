using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Records.Characters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Merchants
{
    public class MerchantsManager : Singleton<MerchantsManager>
    {
        [StartupInvoke(StartupInvokePriority.SixthPath)]
        public void SpawnMerchants()
        {
            foreach (var merchant in MerchantRecord.GetMerchants())
            {
                MapRecord map = MapRecord.GetMap(merchant.MapId);
                SpawnMerchant(map.Instance, merchant);
            }
        }
        public void AddMerchant(Character character)
        {
            MerchantRecord merchantRecord = new MerchantRecord()
            {
                CellId = character.CellId,
                CharacterId = character.Id,
                Direction = character.Direction,
                Look = character.Look.Clone(),
                MapId = character.Map.Id,
                Name = character.Name,
            };

            merchantRecord.AddElement();

            SpawnMerchant(character.Map.Instance, merchantRecord);
        }
        public void SpawnMerchant(MapInstance mapInstance, MerchantRecord merchantRecord)
        {
            CharacterMerchant merchant = new CharacterMerchant(merchantRecord, mapInstance.Record);
            mapInstance.AddEntity(merchant);
        }

        public void RemoveMerchant(MerchantRecord merchantRecord)
        {
            merchantRecord.RemoveElement();
            MapRecord targetMap = MapRecord.GetMap(merchantRecord.MapId);
            targetMap.Instance.RemoveEntity((int)merchantRecord.Id);
        }
    }
}

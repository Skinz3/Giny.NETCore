using Giny.Core;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.World.Api;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Modules;
using Giny.World.Network;
using Giny.World.Records.Items;
using System.Drawing;
using Giny.Core.Extensions;

namespace Giny.DofusDrop
{
    [Module("Dofus drop")]
    public class Module : IModule
    {
        private const double DofusDropPercentage = 0.2d;

        private const string DropMessage = "<b>{0}</b> vient de drop le <b>{1}</b>. Félicitation a lui !";

        public void CreateHooks()
        {
            FightEventApi.OnPlayerResultApplied += OnResultApplied;
        }

        private void OnResultApplied(FightPlayerResult result)
        {
            if (result.Fight.Winners != result.Fighter.Team)
            {
                return;
            }
            if (!(result.Fight is FightPvM))
            {
                return;
            }

            var bosses = result.Fighter.EnemyTeam.GetFighters<MonsterFighter>(false).Where(x => x.Record.IsBoss);

            if (bosses.Count() > 0)
            {
                var items = ItemRecord.GetItems().Where(x => x.TypeEnum == ItemTypeEnum.DOFUS).Where(x => x.Level <= result.Character.Level);

                if (items.Count() > 0)
                {
                    var dofusItem = items.Random();

                    AsyncRandom random = new AsyncRandom();

                    var chance = (random.Next(0, 100) + random.NextDouble());
                    var dropRate = DofusDropPercentage;

                    if (!(dropRate >= chance))
                        return;

                    result.Character.Inventory.AddItem((short)dofusItem.Id, 1);
                    result.Loot.AddItem((short)dofusItem.Id, 1);

                    foreach (var client in WorldServer.Instance.GetOnlineClients())
                    {
                        client.Character.Reply(string.Format(DropMessage, result.Character.Name, dofusItem.Name), Color.HotPink);
                    }

                }
            }
        }

        public void Initialize()
        {
            Logger.Write("Dofus loot chance : " + DofusDropPercentage + "%");
        }

    }
}
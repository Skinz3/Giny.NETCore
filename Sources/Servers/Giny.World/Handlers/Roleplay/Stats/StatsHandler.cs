using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Stats
{
    class StatsHandler
    {
        [MessageHandler]
        public static void HandleResetCharacterStatsRequestMessage(ResetCharacterStatsRequestMessage message,WorldClient client)
        {
            client.Character.Restat();
        }
        [MessageHandler]
        public static void HandleStatsUpgradeRequestMessage(StatsUpgradeRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                StatsBoostEnum statId = (StatsBoostEnum)message.statId;

                var characteristic = client.Character.Record.Stats.GetCharacteristic(statId);

                if (characteristic == null)
                {
                    client.Character.ReplyError("Wrong StatId.");
                    client.Character.OnStatUpgradeResult(StatsUpgradeResultEnum.NONE, message.boostPoint);
                    return;
                }
                if (message.boostPoint > 0)
                {
                    int num = characteristic.Base;
                    short num2 = message.boostPoint;
                    if (num2 >= 1 && message.boostPoint <= client.Character.Stats[CharacteristicEnum.STATS_POINTS].Total())
                    {
                        var upgradeCosts = client.Character.Breed.GetStatUpgradeCost(statId);
                        int thresholdIndex = client.Character.Breed.GetStatUpgradeCostIndex(num, upgradeCosts);

                        while (num2 >= upgradeCosts[thresholdIndex].Cost)
                        {
                            short num3;
                            short num4;
                            if (thresholdIndex < upgradeCosts.Length - 1 && (double)num2 / upgradeCosts[thresholdIndex].Cost > upgradeCosts[thresholdIndex + 1].Until - num)
                            {
                                num3 = (short)(upgradeCosts[thresholdIndex + 1].Until - (num));
                                num4 = (short)(num3 * (upgradeCosts[thresholdIndex].Cost));
                            }
                            else
                            {
                                num3 = (short)Math.Floor((double)num2 / upgradeCosts[thresholdIndex].Cost);
                                num4 = (short)(num3 * upgradeCosts[thresholdIndex].Cost);
                            }
                            num += num3;
                            num2 -= num4;
                            thresholdIndex = client.Character.Breed.GetStatUpgradeCostIndex(num, upgradeCosts);
                        }

                        if (statId == StatsBoostEnum.VITALITY)
                        {
                            int num5 = num - characteristic.Base;

                            client.Character.Record.Stats.Life.Base += num5;
                        }

                        characteristic.Base = (short)num;

                        client.Character.Stats[CharacteristicEnum.STATS_POINTS].Base -= (short)(message.boostPoint - num2);

                        client.Character.OnStatUpgradeResult(StatsUpgradeResultEnum.SUCCESS, message.boostPoint);
                        client.Character.RefreshStats();

                    }
                    else
                    {
                        client.Character.OnStatUpgradeResult(StatsUpgradeResultEnum.NOT_ENOUGH_POINT, message.boostPoint);
                    }
                }
            }
            else
            {
                client.Character.OnStatUpgradeResult(StatsUpgradeResultEnum.IN_FIGHT, 0);
            }
        }
    }
}

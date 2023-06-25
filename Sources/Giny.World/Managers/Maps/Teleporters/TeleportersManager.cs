using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Generic;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps.Teleporters
{
    public class TeleportersManager : Singleton<TeleportersManager>
    {
        private Dictionary<TeleporterTypeEnum, Dictionary<int, List<long>>> m_destinations = new Dictionary<TeleporterTypeEnum, Dictionary<int, List<long>>>();

        [StartupInvoke(StartupInvokePriority.SixthPath)]
        public void Intialize()
        {
            m_destinations.Add(TeleporterTypeEnum.TELEPORTER_ZAAP, new Dictionary<int, List<long>>());
            m_destinations.Add(TeleporterTypeEnum.TELEPORTER_SUBWAY, new Dictionary<int, List<long>>());

            foreach (var interactiveSkill in InteractiveSkillRecord.GetInteractiveSkills())
            {
                if (interactiveSkill.ActionIdentifier == GenericActionEnum.Zaap)
                {
                    int zoneId = int.Parse(interactiveSkill.Param1);

                    var destinations = m_destinations[TeleporterTypeEnum.TELEPORTER_ZAAP];

                    if (!destinations.ContainsKey(zoneId))
                    {
                        destinations.Add(zoneId, new List<long>() { interactiveSkill.MapId });
                    }
                    else
                    {
                        destinations[zoneId].Add(interactiveSkill.MapId);
                    }
                }
                else if (interactiveSkill.ActionIdentifier == GenericActionEnum.Zaapi)
                {
                    int zoneId = int.Parse(interactiveSkill.Param1);

                    var destinations = m_destinations[TeleporterTypeEnum.TELEPORTER_SUBWAY];

                    if (!destinations.ContainsKey(zoneId))
                    {
                        destinations.Add(zoneId, new List<long>() { interactiveSkill.MapId });
                    }
                    else
                    {
                        destinations[zoneId].Add(interactiveSkill.MapId);
                    }
                }
            }
        }

        public List<long> GetMaps(TeleporterTypeEnum teleporterType, int zoneId)
        {
            return m_destinations[teleporterType][zoneId];
        }

        public void AddDestination(TeleporterTypeEnum teleporterType, InteractiveTypeEnum interactiveType, GenericActionEnum genericAction, MapRecord targetMap, InteractiveElementRecord element, int zoneId)
        {
            var destinations = m_destinations[teleporterType];

            if (!destinations.ContainsKey(zoneId))
            {
                destinations.Add(zoneId, new List<long>());
            }

            if (destinations[zoneId].Contains(targetMap.Id))
            {
                return;
            }

            MapsManager.Instance.AddInteractiveSkill(targetMap, element.Identifier, genericAction,
                interactiveType, SkillTypeEnum.USE114, zoneId.ToString());


            if (!destinations.ContainsKey(zoneId))
            {
                destinations.Add(zoneId, new List<long>() { targetMap.Id });
            }
            else
            {
                destinations[zoneId].Add(targetMap.Id);
            }

        }

    }
}

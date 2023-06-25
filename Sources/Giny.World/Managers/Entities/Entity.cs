using Giny.Core.Network.Messages;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Maps;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities
{
    public abstract class Entity
    {
        public abstract long Id
        {
            get;
        }
        public abstract string Name
        {
            get;
        }
        public MapPoint Point
        {
            get
            {
                return new MapPoint((short)CellId);
            }
        }
        public abstract short CellId
        {
            get;
            set;
        }
        public MapRecord Map
        {
            get;
            set;
        }

        public abstract DirectionsEnum Direction
        {
            get;
            set;
        }

        public abstract ServerEntityLook Look
        {
            get;
            set;
        }

        public Entity(MapRecord map)
        {
            this.Map = map;
        }

        public abstract GameRolePlayActorInformations GetActorInformations();

        public void SendMap(NetworkMessage message)
        {
            if (Map != null && Map.Instance != null)
                Map.Instance.Send(message);
        }
        public void RefreshActorOnMap()
        {
            SendMap(new GameRolePlayShowActorMessage(GetActorInformations()));
        }
        public void Say(string msg)
        {
            SendMap(new EntityTalkMessage(Id, 4, new string[] { msg }));
        }
        public virtual void DisplaySmiley(short smileyId)
        {
            SendMap(new ChatSmileyMessage(Id, smileyId, 0));
        }
        public static DirectionsEnum RandomDirection(Random random)
        {
            Array values = Enum.GetValues(typeof(DirectionsEnum));
            return (DirectionsEnum)values.GetValue(random.Next(values.Length));
        }
        public static DirectionsEnum RandomDirection4D(Random random)
        {
            DirectionsEnum[] values = new DirectionsEnum[] { DirectionsEnum.DIRECTION_SOUTH_WEST, DirectionsEnum.DIRECTION_SOUTH_EAST ,
            DirectionsEnum.DIRECTION_NORTH_WEST, DirectionsEnum.DIRECTION_NORTH_EAST};

            return (DirectionsEnum)values.GetValue(random.Next(values.Length));
        }
        public CellRecord GetCell()
        {
            return Map.GetCell(CellId);
        }
    }
}

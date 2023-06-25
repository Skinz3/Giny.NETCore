using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ProtectedEntityWaitingForHelpInfo  
    { 
        public const ushort Id = 691;
        public virtual ushort TypeId => Id;

        public int timeLeftBeforeFight;
        public int waitTimeForPlacement;
        public byte nbPositionForDefensors;

        public ProtectedEntityWaitingForHelpInfo()
        {
        }
        public ProtectedEntityWaitingForHelpInfo(int timeLeftBeforeFight,int waitTimeForPlacement,byte nbPositionForDefensors)
        {
            this.timeLeftBeforeFight = timeLeftBeforeFight;
            this.waitTimeForPlacement = waitTimeForPlacement;
            this.nbPositionForDefensors = nbPositionForDefensors;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)timeLeftBeforeFight);
            writer.WriteInt((int)waitTimeForPlacement);
            if (nbPositionForDefensors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPositionForDefensors + ") on element nbPositionForDefensors.");
            }

            writer.WriteByte((byte)nbPositionForDefensors);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            timeLeftBeforeFight = (int)reader.ReadInt();
            waitTimeForPlacement = (int)reader.ReadInt();
            nbPositionForDefensors = (byte)reader.ReadByte();
            if (nbPositionForDefensors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPositionForDefensors + ") on element of ProtectedEntityWaitingForHelpInfo.nbPositionForDefensors.");
            }

        }


    }
}









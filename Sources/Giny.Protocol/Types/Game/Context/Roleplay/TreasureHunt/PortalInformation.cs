using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PortalInformation  
    { 
        public const ushort Id = 8424;
        public virtual ushort TypeId => Id;

        public int portalId;
        public short areaId;

        public PortalInformation()
        {
        }
        public PortalInformation(int portalId,short areaId)
        {
            this.portalId = portalId;
            this.areaId = areaId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)portalId);
            writer.WriteShort((short)areaId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            portalId = (int)reader.ReadInt();
            areaId = (short)reader.ReadShort();
        }


    }
}









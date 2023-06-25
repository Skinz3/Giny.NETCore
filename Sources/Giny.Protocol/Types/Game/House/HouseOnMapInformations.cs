using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HouseOnMapInformations : HouseInformations  
    { 
        public new const ushort Id = 7144;
        public override ushort TypeId => Id;

        public int[] doorsOnMap;
        public HouseInstanceInformations[] houseInstances;

        public HouseOnMapInformations()
        {
        }
        public HouseOnMapInformations(int[] doorsOnMap,HouseInstanceInformations[] houseInstances,int houseId,short modelId)
        {
            this.doorsOnMap = doorsOnMap;
            this.houseInstances = houseInstances;
            this.houseId = houseId;
            this.modelId = modelId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)doorsOnMap.Length);
            for (uint _i1 = 0;_i1 < doorsOnMap.Length;_i1++)
            {
                if (doorsOnMap[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + doorsOnMap[_i1] + ") on element 1 (starting at 1) of doorsOnMap.");
                }

                writer.WriteInt((int)doorsOnMap[_i1]);
            }

            writer.WriteShort((short)houseInstances.Length);
            for (uint _i2 = 0;_i2 < houseInstances.Length;_i2++)
            {
                (houseInstances[_i2] as HouseInstanceInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            HouseInstanceInformations _item2 = null;
            base.Deserialize(reader);
            uint _doorsOnMapLen = (uint)reader.ReadUShort();
            doorsOnMap = new int[_doorsOnMapLen];
            for (uint _i1 = 0;_i1 < _doorsOnMapLen;_i1++)
            {
                _val1 = (uint)reader.ReadInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of doorsOnMap.");
                }

                doorsOnMap[_i1] = (int)_val1;
            }

            uint _houseInstancesLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _houseInstancesLen;_i2++)
            {
                _item2 = new HouseInstanceInformations();
                _item2.Deserialize(reader);
                houseInstances[_i2] = _item2;
            }

        }


    }
}









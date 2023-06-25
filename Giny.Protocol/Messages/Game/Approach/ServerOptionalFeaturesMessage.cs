using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerOptionalFeaturesMessage : NetworkMessage  
    { 
        public  const ushort Id = 7447;
        public override ushort MessageId => Id;

        public int[] features;

        public ServerOptionalFeaturesMessage()
        {
        }
        public ServerOptionalFeaturesMessage(int[] features)
        {
            this.features = features;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)features.Length);
            for (uint _i1 = 0;_i1 < features.Length;_i1++)
            {
                if (features[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + features[_i1] + ") on element 1 (starting at 1) of features.");
                }

                writer.WriteInt((int)features[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _featuresLen = (uint)reader.ReadUShort();
            features = new int[_featuresLen];
            for (uint _i1 = 0;_i1 < _featuresLen;_i1++)
            {
                _val1 = (uint)reader.ReadInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of features.");
                }

                features[_i1] = (int)_val1;
            }

        }


    }
}









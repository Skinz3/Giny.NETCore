using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachBranchesMessage : NetworkMessage  
    { 
        public  const ushort Id = 2636;
        public override ushort MessageId => Id;

        public ExtendedBreachBranch[] branches;

        public BreachBranchesMessage()
        {
        }
        public BreachBranchesMessage(ExtendedBreachBranch[] branches)
        {
            this.branches = branches;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)branches.Length);
            for (uint _i1 = 0;_i1 < branches.Length;_i1++)
            {
                writer.WriteShort((short)(branches[_i1] as ExtendedBreachBranch).TypeId);
                (branches[_i1] as ExtendedBreachBranch).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            ExtendedBreachBranch _item1 = null;
            uint _branchesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _branchesLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<ExtendedBreachBranch>((short)_id1);
                _item1.Deserialize(reader);
                branches[_i1] = _item1;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionTitle : HumanOption  
    { 
        public new const ushort Id = 854;
        public override ushort TypeId => Id;

        public short titleId;
        public string titleParam;

        public HumanOptionTitle()
        {
        }
        public HumanOptionTitle(short titleId,string titleParam)
        {
            this.titleId = titleId;
            this.titleParam = titleParam;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (titleId < 0)
            {
                throw new System.Exception("Forbidden value (" + titleId + ") on element titleId.");
            }

            writer.WriteVarShort((short)titleId);
            writer.WriteUTF((string)titleParam);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            titleId = (short)reader.ReadVarUhShort();
            if (titleId < 0)
            {
                throw new System.Exception("Forbidden value (" + titleId + ") on element of HumanOptionTitle.titleId.");
            }

            titleParam = (string)reader.ReadUTF();
        }


    }
}









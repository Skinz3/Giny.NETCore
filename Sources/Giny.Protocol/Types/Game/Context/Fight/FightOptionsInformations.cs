using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightOptionsInformations  
    { 
        public const ushort Id = 4873;
        public virtual ushort TypeId => Id;

        public bool isSecret;
        public bool isRestrictedToPartyOnly;
        public bool isClosed;
        public bool isAskingForHelp;

        public FightOptionsInformations()
        {
        }
        public FightOptionsInformations(bool isSecret,bool isRestrictedToPartyOnly,bool isClosed,bool isAskingForHelp)
        {
            this.isSecret = isSecret;
            this.isRestrictedToPartyOnly = isRestrictedToPartyOnly;
            this.isClosed = isClosed;
            this.isAskingForHelp = isAskingForHelp;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,isSecret);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isRestrictedToPartyOnly);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,isClosed);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,isAskingForHelp);
            writer.WriteByte((byte)_box0);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            isSecret = BooleanByteWrapper.GetFlag(_box0,0);
            isRestrictedToPartyOnly = BooleanByteWrapper.GetFlag(_box0,1);
            isClosed = BooleanByteWrapper.GetFlag(_box0,2);
            isAskingForHelp = BooleanByteWrapper.GetFlag(_box0,3);
        }


    }
}









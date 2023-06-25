using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayHumanoidInformations : GameRolePlayNamedActorInformations  
    { 
        public new const ushort Id = 5016;
        public override ushort TypeId => Id;

        public HumanInformations humanoidInfo;
        public int accountId;

        public GameRolePlayHumanoidInformations()
        {
        }
        public GameRolePlayHumanoidInformations(HumanInformations humanoidInfo,int accountId,double contextualId,EntityDispositionInformations disposition,EntityLook look,string name)
        {
            this.humanoidInfo = humanoidInfo;
            this.accountId = accountId;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)humanoidInfo.TypeId);
            humanoidInfo.Serialize(writer);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            humanoidInfo = ProtocolTypeManager.GetInstance<HumanInformations>((short)_id1);
            humanoidInfo.Deserialize(reader);
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of GameRolePlayHumanoidInformations.accountId.");
            }

        }


    }
}









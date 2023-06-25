using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayMerchantInformations : GameRolePlayNamedActorInformations  
    { 
        public new const ushort Id = 2246;
        public override ushort TypeId => Id;

        public byte sellType;
        public HumanOption[] options;

        public GameRolePlayMerchantInformations()
        {
        }
        public GameRolePlayMerchantInformations(byte sellType,HumanOption[] options,double contextualId,EntityDispositionInformations disposition,EntityLook look,string name)
        {
            this.sellType = sellType;
            this.options = options;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (sellType < 0)
            {
                throw new System.Exception("Forbidden value (" + sellType + ") on element sellType.");
            }

            writer.WriteByte((byte)sellType);
            writer.WriteShort((short)options.Length);
            for (uint _i2 = 0;_i2 < options.Length;_i2++)
            {
                writer.WriteShort((short)(options[_i2] as HumanOption).TypeId);
                (options[_i2] as HumanOption).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            HumanOption _item2 = null;
            base.Deserialize(reader);
            sellType = (byte)reader.ReadByte();
            if (sellType < 0)
            {
                throw new System.Exception("Forbidden value (" + sellType + ") on element of GameRolePlayMerchantInformations.sellType.");
            }

            uint _optionsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _optionsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<HumanOption>((short)_id2);
                _item2.Deserialize(reader);
                options[_i2] = _item2;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachStateMessage : NetworkMessage  
    { 
        public  const ushort Id = 6075;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations owner;
        public ObjectEffectInteger[] bonuses;
        public int bugdet;
        public bool saved;

        public BreachStateMessage()
        {
        }
        public BreachStateMessage(CharacterMinimalInformations owner,ObjectEffectInteger[] bonuses,int bugdet,bool saved)
        {
            this.owner = owner;
            this.bonuses = bonuses;
            this.bugdet = bugdet;
            this.saved = saved;
        }
        public override void Serialize(IDataWriter writer)
        {
            owner.Serialize(writer);
            writer.WriteShort((short)bonuses.Length);
            for (uint _i2 = 0;_i2 < bonuses.Length;_i2++)
            {
                (bonuses[_i2] as ObjectEffectInteger).Serialize(writer);
            }

            if (bugdet < 0)
            {
                throw new System.Exception("Forbidden value (" + bugdet + ") on element bugdet.");
            }

            writer.WriteVarInt((int)bugdet);
            writer.WriteBoolean((bool)saved);
        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectEffectInteger _item2 = null;
            owner = new CharacterMinimalInformations();
            owner.Deserialize(reader);
            uint _bonusesLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _bonusesLen;_i2++)
            {
                _item2 = new ObjectEffectInteger();
                _item2.Deserialize(reader);
                bonuses[_i2] = _item2;
            }

            bugdet = (int)reader.ReadVarUhInt();
            if (bugdet < 0)
            {
                throw new System.Exception("Forbidden value (" + bugdet + ") on element of BreachStateMessage.bugdet.");
            }

            saved = (bool)reader.ReadBoolean();
        }


    }
}









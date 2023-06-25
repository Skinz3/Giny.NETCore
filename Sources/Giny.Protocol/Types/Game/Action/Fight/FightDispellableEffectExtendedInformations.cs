using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightDispellableEffectExtendedInformations  
    { 
        public const ushort Id = 9766;
        public virtual ushort TypeId => Id;

        public short actionId;
        public double sourceId;
        public AbstractFightDispellableEffect effect;

        public FightDispellableEffectExtendedInformations()
        {
        }
        public FightDispellableEffectExtendedInformations(short actionId,double sourceId,AbstractFightDispellableEffect effect)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.effect = effect;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteVarShort((short)actionId);
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element sourceId.");
            }

            writer.WriteDouble((double)sourceId);
            writer.WriteShort((short)effect.TypeId);
            effect.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            actionId = (short)reader.ReadVarUhShort();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of FightDispellableEffectExtendedInformations.actionId.");
            }

            sourceId = (double)reader.ReadDouble();
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element of FightDispellableEffectExtendedInformations.sourceId.");
            }

            uint _id3 = (uint)reader.ReadUShort();
            effect = ProtocolTypeManager.GetInstance<AbstractFightDispellableEffect>((short)_id3);
            effect.Deserialize(reader);
        }


    }
}









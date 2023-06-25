using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AbstractGameActionFightTargetedAbilityMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 1710;
        public override ushort MessageId => Id;

        public double targetId;
        public short destinationCellId;
        public byte critical;
        public bool silentCast;
        public bool verboseCast;

        public AbstractGameActionFightTargetedAbilityMessage()
        {
        }
        public AbstractGameActionFightTargetedAbilityMessage(double targetId,short destinationCellId,byte critical,bool silentCast,bool verboseCast,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.destinationCellId = destinationCellId;
            this.critical = critical;
            this.silentCast = silentCast;
            this.verboseCast = verboseCast;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,silentCast);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,verboseCast);
            writer.WriteByte((byte)_box0);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (destinationCellId < -1 || destinationCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + destinationCellId + ") on element destinationCellId.");
            }

            writer.WriteShort((short)destinationCellId);
            writer.WriteByte((byte)critical);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            silentCast = BooleanByteWrapper.GetFlag(_box0,0);
            verboseCast = BooleanByteWrapper.GetFlag(_box0,1);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of AbstractGameActionFightTargetedAbilityMessage.targetId.");
            }

            destinationCellId = (short)reader.ReadShort();
            if (destinationCellId < -1 || destinationCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + destinationCellId + ") on element of AbstractGameActionFightTargetedAbilityMessage.destinationCellId.");
            }

            critical = (byte)reader.ReadByte();
            if (critical < 0)
            {
                throw new System.Exception("Forbidden value (" + critical + ") on element of AbstractGameActionFightTargetedAbilityMessage.critical.");
            }

        }


    }
}









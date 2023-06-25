using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SlaveSwitchContextMessage : NetworkMessage  
    { 
        public  const ushort Id = 1189;
        public override ushort MessageId => Id;

        public double masterId;
        public double slaveId;
        public short slaveTurn;
        public SpellItem[] slaveSpells;
        public CharacterCharacteristicsInformations slaveStats;
        public Shortcut[] shortcuts;

        public SlaveSwitchContextMessage()
        {
        }
        public SlaveSwitchContextMessage(double masterId,double slaveId,short slaveTurn,SpellItem[] slaveSpells,CharacterCharacteristicsInformations slaveStats,Shortcut[] shortcuts)
        {
            this.masterId = masterId;
            this.slaveId = slaveId;
            this.slaveTurn = slaveTurn;
            this.slaveSpells = slaveSpells;
            this.slaveStats = slaveStats;
            this.shortcuts = shortcuts;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element masterId.");
            }

            writer.WriteDouble((double)masterId);
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element slaveId.");
            }

            writer.WriteDouble((double)slaveId);
            if (slaveTurn < 0)
            {
                throw new System.Exception("Forbidden value (" + slaveTurn + ") on element slaveTurn.");
            }

            writer.WriteVarShort((short)slaveTurn);
            writer.WriteShort((short)slaveSpells.Length);
            for (uint _i4 = 0;_i4 < slaveSpells.Length;_i4++)
            {
                (slaveSpells[_i4] as SpellItem).Serialize(writer);
            }

            slaveStats.Serialize(writer);
            writer.WriteShort((short)shortcuts.Length);
            for (uint _i6 = 0;_i6 < shortcuts.Length;_i6++)
            {
                writer.WriteShort((short)(shortcuts[_i6] as Shortcut).TypeId);
                (shortcuts[_i6] as Shortcut).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            SpellItem _item4 = null;
            uint _id6 = 0;
            Shortcut _item6 = null;
            masterId = (double)reader.ReadDouble();
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element of SlaveSwitchContextMessage.masterId.");
            }

            slaveId = (double)reader.ReadDouble();
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element of SlaveSwitchContextMessage.slaveId.");
            }

            slaveTurn = (short)reader.ReadVarUhShort();
            if (slaveTurn < 0)
            {
                throw new System.Exception("Forbidden value (" + slaveTurn + ") on element of SlaveSwitchContextMessage.slaveTurn.");
            }

            uint _slaveSpellsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _slaveSpellsLen;_i4++)
            {
                _item4 = new SpellItem();
                _item4.Deserialize(reader);
                slaveSpells[_i4] = _item4;
            }

            slaveStats = new CharacterCharacteristicsInformations();
            slaveStats.Deserialize(reader);
            uint _shortcutsLen = (uint)reader.ReadUShort();
            for (uint _i6 = 0;_i6 < _shortcutsLen;_i6++)
            {
                _id6 = (uint)reader.ReadUShort();
                _item6 = ProtocolTypeManager.GetInstance<Shortcut>((short)_id6);
                _item6.Deserialize(reader);
                shortcuts[_i6] = _item6;
            }

        }


    }
}









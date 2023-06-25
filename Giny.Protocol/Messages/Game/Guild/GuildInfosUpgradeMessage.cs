using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInfosUpgradeMessage : NetworkMessage  
    { 
        public  const ushort Id = 9899;
        public override ushort MessageId => Id;

        public byte maxTaxCollectorsCount;
        public byte taxCollectorsCount;
        public short taxCollectorLifePoints;
        public short taxCollectorDamagesBonuses;
        public short taxCollectorPods;
        public short taxCollectorProspecting;
        public short taxCollectorWisdom;
        public short boostPoints;
        public short[] spellId;
        public short[] spellLevel;

        public GuildInfosUpgradeMessage()
        {
        }
        public GuildInfosUpgradeMessage(byte maxTaxCollectorsCount,byte taxCollectorsCount,short taxCollectorLifePoints,short taxCollectorDamagesBonuses,short taxCollectorPods,short taxCollectorProspecting,short taxCollectorWisdom,short boostPoints,short[] spellId,short[] spellLevel)
        {
            this.maxTaxCollectorsCount = maxTaxCollectorsCount;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorLifePoints = taxCollectorLifePoints;
            this.taxCollectorDamagesBonuses = taxCollectorDamagesBonuses;
            this.taxCollectorPods = taxCollectorPods;
            this.taxCollectorProspecting = taxCollectorProspecting;
            this.taxCollectorWisdom = taxCollectorWisdom;
            this.boostPoints = boostPoints;
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (maxTaxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxTaxCollectorsCount + ") on element maxTaxCollectorsCount.");
            }

            writer.WriteByte((byte)maxTaxCollectorsCount);
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element taxCollectorsCount.");
            }

            writer.WriteByte((byte)taxCollectorsCount);
            if (taxCollectorLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorLifePoints + ") on element taxCollectorLifePoints.");
            }

            writer.WriteVarShort((short)taxCollectorLifePoints);
            if (taxCollectorDamagesBonuses < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorDamagesBonuses + ") on element taxCollectorDamagesBonuses.");
            }

            writer.WriteVarShort((short)taxCollectorDamagesBonuses);
            if (taxCollectorPods < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorPods + ") on element taxCollectorPods.");
            }

            writer.WriteVarShort((short)taxCollectorPods);
            if (taxCollectorProspecting < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorProspecting + ") on element taxCollectorProspecting.");
            }

            writer.WriteVarShort((short)taxCollectorProspecting);
            if (taxCollectorWisdom < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorWisdom + ") on element taxCollectorWisdom.");
            }

            writer.WriteVarShort((short)taxCollectorWisdom);
            if (boostPoints < 0)
            {
                throw new System.Exception("Forbidden value (" + boostPoints + ") on element boostPoints.");
            }

            writer.WriteVarShort((short)boostPoints);
            writer.WriteShort((short)spellId.Length);
            for (uint _i9 = 0;_i9 < spellId.Length;_i9++)
            {
                if (spellId[_i9] < 0)
                {
                    throw new System.Exception("Forbidden value (" + spellId[_i9] + ") on element 9 (starting at 1) of spellId.");
                }

                writer.WriteVarShort((short)spellId[_i9]);
            }

            writer.WriteShort((short)spellLevel.Length);
            for (uint _i10 = 0;_i10 < spellLevel.Length;_i10++)
            {
                writer.WriteShort((short)spellLevel[_i10]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val9 = 0;
            int _val10 = 0;
            maxTaxCollectorsCount = (byte)reader.ReadByte();
            if (maxTaxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxTaxCollectorsCount + ") on element of GuildInfosUpgradeMessage.maxTaxCollectorsCount.");
            }

            taxCollectorsCount = (byte)reader.ReadByte();
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element of GuildInfosUpgradeMessage.taxCollectorsCount.");
            }

            taxCollectorLifePoints = (short)reader.ReadVarUhShort();
            if (taxCollectorLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorLifePoints + ") on element of GuildInfosUpgradeMessage.taxCollectorLifePoints.");
            }

            taxCollectorDamagesBonuses = (short)reader.ReadVarUhShort();
            if (taxCollectorDamagesBonuses < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorDamagesBonuses + ") on element of GuildInfosUpgradeMessage.taxCollectorDamagesBonuses.");
            }

            taxCollectorPods = (short)reader.ReadVarUhShort();
            if (taxCollectorPods < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorPods + ") on element of GuildInfosUpgradeMessage.taxCollectorPods.");
            }

            taxCollectorProspecting = (short)reader.ReadVarUhShort();
            if (taxCollectorProspecting < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorProspecting + ") on element of GuildInfosUpgradeMessage.taxCollectorProspecting.");
            }

            taxCollectorWisdom = (short)reader.ReadVarUhShort();
            if (taxCollectorWisdom < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorWisdom + ") on element of GuildInfosUpgradeMessage.taxCollectorWisdom.");
            }

            boostPoints = (short)reader.ReadVarUhShort();
            if (boostPoints < 0)
            {
                throw new System.Exception("Forbidden value (" + boostPoints + ") on element of GuildInfosUpgradeMessage.boostPoints.");
            }

            uint _spellIdLen = (uint)reader.ReadUShort();
            spellId = new short[_spellIdLen];
            for (uint _i9 = 0;_i9 < _spellIdLen;_i9++)
            {
                _val9 = (uint)reader.ReadVarUhShort();
                if (_val9 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val9 + ") on elements of spellId.");
                }

                spellId[_i9] = (short)_val9;
            }

            uint _spellLevelLen = (uint)reader.ReadUShort();
            spellLevel = new short[_spellLevelLen];
            for (uint _i10 = 0;_i10 < _spellLevelLen;_i10++)
            {
                _val10 = (int)reader.ReadShort();
                spellLevel[_i10] = (short)_val10;
            }

        }


    }
}









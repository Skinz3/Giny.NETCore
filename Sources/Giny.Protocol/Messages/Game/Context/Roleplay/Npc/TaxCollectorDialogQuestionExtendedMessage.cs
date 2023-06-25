using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionBasicMessage  
    { 
        public new const ushort Id = 9849;
        public override ushort MessageId => Id;

        public short maxPods;
        public short prospecting;
        public short wisdom;
        public byte taxCollectorsCount;
        public int taxCollectorAttack;
        public long kamas;
        public long experience;
        public int pods;
        public long itemsValue;

        public TaxCollectorDialogQuestionExtendedMessage()
        {
        }
        public TaxCollectorDialogQuestionExtendedMessage(short maxPods,short prospecting,short wisdom,byte taxCollectorsCount,int taxCollectorAttack,long kamas,long experience,int pods,long itemsValue,BasicGuildInformations guildInfo)
        {
            this.maxPods = maxPods;
            this.prospecting = prospecting;
            this.wisdom = wisdom;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorAttack = taxCollectorAttack;
            this.kamas = kamas;
            this.experience = experience;
            this.pods = pods;
            this.itemsValue = itemsValue;
            this.guildInfo = guildInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (maxPods < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPods + ") on element maxPods.");
            }

            writer.WriteVarShort((short)maxPods);
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element prospecting.");
            }

            writer.WriteVarShort((short)prospecting);
            if (wisdom < 0)
            {
                throw new System.Exception("Forbidden value (" + wisdom + ") on element wisdom.");
            }

            writer.WriteVarShort((short)wisdom);
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element taxCollectorsCount.");
            }

            writer.WriteByte((byte)taxCollectorsCount);
            writer.WriteInt((int)taxCollectorAttack);
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarInt((int)pods);
            if (itemsValue < 0 || itemsValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element itemsValue.");
            }

            writer.WriteVarLong((long)itemsValue);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            maxPods = (short)reader.ReadVarUhShort();
            if (maxPods < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPods + ") on element of TaxCollectorDialogQuestionExtendedMessage.maxPods.");
            }

            prospecting = (short)reader.ReadVarUhShort();
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element of TaxCollectorDialogQuestionExtendedMessage.prospecting.");
            }

            wisdom = (short)reader.ReadVarUhShort();
            if (wisdom < 0)
            {
                throw new System.Exception("Forbidden value (" + wisdom + ") on element of TaxCollectorDialogQuestionExtendedMessage.wisdom.");
            }

            taxCollectorsCount = (byte)reader.ReadByte();
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element of TaxCollectorDialogQuestionExtendedMessage.taxCollectorsCount.");
            }

            taxCollectorAttack = (int)reader.ReadInt();
            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of TaxCollectorDialogQuestionExtendedMessage.kamas.");
            }

            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of TaxCollectorDialogQuestionExtendedMessage.experience.");
            }

            pods = (int)reader.ReadVarUhInt();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of TaxCollectorDialogQuestionExtendedMessage.pods.");
            }

            itemsValue = (long)reader.ReadVarUhLong();
            if (itemsValue < 0 || itemsValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element of TaxCollectorDialogQuestionExtendedMessage.itemsValue.");
            }

        }


    }
}









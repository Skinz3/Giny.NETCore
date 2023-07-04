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
        public new const ushort Id = 2917;
        public override ushort MessageId => Id;

        public short maxPods;
        public short prospecting;
        public BasicNamedAllianceInformations alliance;
        public byte taxCollectorsCount;
        public int taxCollectorAttack;
        public int pods;
        public long itemsValue;

        public TaxCollectorDialogQuestionExtendedMessage()
        {
        }
        public TaxCollectorDialogQuestionExtendedMessage(short maxPods, short prospecting, BasicNamedAllianceInformations alliance, byte taxCollectorsCount, int taxCollectorAttack, int pods, long itemsValue, BasicAllianceInformations allianceInfo)
        {
            this.maxPods = maxPods;
            this.prospecting = prospecting;
            this.alliance = alliance;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorAttack = taxCollectorAttack;
            this.pods = pods;
            this.itemsValue = itemsValue;
            this.allianceInfo = allianceInfo;
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
            alliance.Serialize(writer);
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element taxCollectorsCount.");
            }

            writer.WriteByte((byte)taxCollectorsCount);
            writer.WriteInt((int)taxCollectorAttack);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarInt((int)pods);
            if (itemsValue < 0 || itemsValue > 9007199254740992)
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

            alliance = new BasicNamedAllianceInformations();
            alliance.Deserialize(reader);
            taxCollectorsCount = (byte)reader.ReadByte();
            if (taxCollectorsCount < 0)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorsCount + ") on element of TaxCollectorDialogQuestionExtendedMessage.taxCollectorsCount.");
            }

            taxCollectorAttack = (int)reader.ReadInt();
            pods = (int)reader.ReadVarUhInt();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of TaxCollectorDialogQuestionExtendedMessage.pods.");
            }

            itemsValue = (long)reader.ReadVarUhLong();
            if (itemsValue < 0 || itemsValue > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + itemsValue + ") on element of TaxCollectorDialogQuestionExtendedMessage.itemsValue.");
            }

        }

    }
}



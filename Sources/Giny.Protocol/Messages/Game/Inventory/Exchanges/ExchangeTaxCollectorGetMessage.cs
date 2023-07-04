using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeTaxCollectorGetMessage : NetworkMessage
    {
        public const ushort Id = 7672;
        public override ushort MessageId => Id;

        public string collectorName;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;
        public string userName;
        public long callerId;
        public string callerName;
        public short pods;
        public ObjectItemGenericQuantity[] objectsInfos;
        public EntityLook look;

        public ExchangeTaxCollectorGetMessage()
        {
        }
        public ExchangeTaxCollectorGetMessage(string collectorName, short worldX, short worldY, double mapId, short subAreaId, string userName, long callerId, string callerName, short pods, ObjectItemGenericQuantity[] objectsInfos, EntityLook look)
        {
            this.collectorName = collectorName;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.userName = userName;
            this.callerId = callerId;
            this.callerName = callerName;
            this.pods = pods;
            this.objectsInfos = objectsInfos;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)collectorName);
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element worldX.");
            }

            writer.WriteShort((short)worldX);
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element worldY.");
            }

            writer.WriteShort((short)worldY);
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteUTF((string)userName);
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element callerId.");
            }

            writer.WriteVarLong((long)callerId);
            writer.WriteUTF((string)callerName);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarShort((short)pods);
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i10 = 0; _i10 < objectsInfos.Length; _i10++)
            {
                (objectsInfos[_i10] as ObjectItemGenericQuantity).Serialize(writer);
            }

            look.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemGenericQuantity _item10 = null;
            collectorName = (string)reader.ReadUTF();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of ExchangeTaxCollectorGetMessage.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of ExchangeTaxCollectorGetMessage.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of ExchangeTaxCollectorGetMessage.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of ExchangeTaxCollectorGetMessage.subAreaId.");
            }

            userName = (string)reader.ReadUTF();
            callerId = (long)reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element of ExchangeTaxCollectorGetMessage.callerId.");
            }

            callerName = (string)reader.ReadUTF();
            pods = (short)reader.ReadVarUhShort();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of ExchangeTaxCollectorGetMessage.pods.");
            }

            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i10 = 0; _i10 < _objectsInfosLen; _i10++)
            {
                _item10 = new ObjectItemGenericQuantity();
                _item10.Deserialize(reader);
                objectsInfos[_i10] = _item10;
            }

            look = new EntityLook();
            look.Deserialize(reader);
        }

    }
}



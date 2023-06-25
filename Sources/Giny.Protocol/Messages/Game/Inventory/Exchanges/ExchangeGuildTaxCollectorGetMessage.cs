using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeGuildTaxCollectorGetMessage : NetworkMessage  
    { 
        public  const ushort Id = 8831;
        public override ushort MessageId => Id;

        public string collectorName;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;
        public string userName;
        public long callerId;
        public string callerName;
        public double experience;
        public short pods;
        public ObjectItemGenericQuantity[] objectsInfos;

        public ExchangeGuildTaxCollectorGetMessage()
        {
        }
        public ExchangeGuildTaxCollectorGetMessage(string collectorName,short worldX,short worldY,double mapId,short subAreaId,string userName,long callerId,string callerName,double experience,short pods,ObjectItemGenericQuantity[] objectsInfos)
        {
            this.collectorName = collectorName;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.userName = userName;
            this.callerId = callerId;
            this.callerName = callerName;
            this.experience = experience;
            this.pods = pods;
            this.objectsInfos = objectsInfos;
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
            if (mapId < 0 || mapId > 9.00719925474099E+15)
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
            if (callerId < 0 || callerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element callerId.");
            }

            writer.WriteVarLong((long)callerId);
            writer.WriteUTF((string)callerName);
            if (experience < -9.00719925474099E+15 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteDouble((double)experience);
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element pods.");
            }

            writer.WriteVarShort((short)pods);
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i11 = 0;_i11 < objectsInfos.Length;_i11++)
            {
                (objectsInfos[_i11] as ObjectItemGenericQuantity).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemGenericQuantity _item11 = null;
            collectorName = (string)reader.ReadUTF();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of ExchangeGuildTaxCollectorGetMessage.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of ExchangeGuildTaxCollectorGetMessage.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of ExchangeGuildTaxCollectorGetMessage.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of ExchangeGuildTaxCollectorGetMessage.subAreaId.");
            }

            userName = (string)reader.ReadUTF();
            callerId = (long)reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element of ExchangeGuildTaxCollectorGetMessage.callerId.");
            }

            callerName = (string)reader.ReadUTF();
            experience = (double)reader.ReadDouble();
            if (experience < -9.00719925474099E+15 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of ExchangeGuildTaxCollectorGetMessage.experience.");
            }

            pods = (short)reader.ReadVarUhShort();
            if (pods < 0)
            {
                throw new System.Exception("Forbidden value (" + pods + ") on element of ExchangeGuildTaxCollectorGetMessage.pods.");
            }

            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i11 = 0;_i11 < _objectsInfosLen;_i11++)
            {
                _item11 = new ObjectItemGenericQuantity();
                _item11.Deserialize(reader);
                objectsInfos[_i11] = _item11;
            }

        }


    }
}









using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceSummaryRequestMessage : PaginationRequestAbstractMessage  
    { 
        public new const ushort Id = 4135;
        public override ushort MessageId => Id;

        public string nameFilter;
        public string tagFilter;
        public string playerNameFilter;
        public byte sortType;
        public bool sortDescending;

        public AllianceSummaryRequestMessage()
        {
        }
        public AllianceSummaryRequestMessage(string nameFilter,string tagFilter,string playerNameFilter,byte sortType,bool sortDescending,double offset,uint count)
        {
            this.nameFilter = nameFilter;
            this.tagFilter = tagFilter;
            this.playerNameFilter = playerNameFilter;
            this.sortType = sortType;
            this.sortDescending = sortDescending;
            this.offset = offset;
            this.count = count;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)nameFilter);
            writer.WriteUTF((string)tagFilter);
            writer.WriteUTF((string)playerNameFilter);
            writer.WriteByte((byte)sortType);
            writer.WriteBoolean((bool)sortDescending);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            nameFilter = (string)reader.ReadUTF();
            tagFilter = (string)reader.ReadUTF();
            playerNameFilter = (string)reader.ReadUTF();
            sortType = (byte)reader.ReadByte();
            if (sortType < 0)
            {
                throw new System.Exception("Forbidden value (" + sortType + ") on element of AllianceSummaryRequestMessage.sortType.");
            }

            sortDescending = (bool)reader.ReadBoolean();
        }


    }
}









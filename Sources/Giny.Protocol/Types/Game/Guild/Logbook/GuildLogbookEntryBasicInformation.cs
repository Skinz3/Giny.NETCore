using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildLogbookEntryBasicInformation
    {
        public const ushort Id = 3876;
        public virtual ushort TypeId => Id;

        public int id;
        public double date;

        public GuildLogbookEntryBasicInformation()
        {
        }
        public GuildLogbookEntryBasicInformation(int id, double date)
        {
            this.id = id;
            this.date = date;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarInt((int)id);
            if (date < 0 || date > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element date.");
            }

            writer.WriteDouble((double)date);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (int)reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GuildLogbookEntryBasicInformation.id.");
            }

            date = (double)reader.ReadDouble();
            if (date < 0 || date > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element of GuildLogbookEntryBasicInformation.date.");
            }

        }


    }
}



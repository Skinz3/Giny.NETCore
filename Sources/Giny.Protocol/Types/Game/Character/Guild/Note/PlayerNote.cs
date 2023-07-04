using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class PlayerNote
    {
        public const ushort Id = 9466;
        public virtual ushort TypeId => Id;

        public string content;
        public double lastEditDate;

        public PlayerNote()
        {
        }
        public PlayerNote(string content, double lastEditDate)
        {
            this.content = content;
            this.lastEditDate = lastEditDate;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)content);
            if (lastEditDate < -9007199254740992 || lastEditDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element lastEditDate.");
            }

            writer.WriteDouble((double)lastEditDate);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            content = (string)reader.ReadUTF();
            lastEditDate = (double)reader.ReadDouble();
            if (lastEditDate < -9007199254740992 || lastEditDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element of PlayerNote.lastEditDate.");
            }

        }


    }
}



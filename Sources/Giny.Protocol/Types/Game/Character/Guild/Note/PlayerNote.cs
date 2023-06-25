using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PlayerNote  
    { 
        public const ushort Id = 6851;
        public virtual ushort TypeId => Id;

        public string content;
        public double lastEditDate;

        public PlayerNote()
        {
        }
        public PlayerNote(string content,double lastEditDate)
        {
            this.content = content;
            this.lastEditDate = lastEditDate;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)content);
            if (lastEditDate < -9.00719925474099E+15 || lastEditDate > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element lastEditDate.");
            }

            writer.WriteDouble((double)lastEditDate);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            content = (string)reader.ReadUTF();
            lastEditDate = (double)reader.ReadDouble();
            if (lastEditDate < -9.00719925474099E+15 || lastEditDate > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element of PlayerNote.lastEditDate.");
            }

        }


    }
}









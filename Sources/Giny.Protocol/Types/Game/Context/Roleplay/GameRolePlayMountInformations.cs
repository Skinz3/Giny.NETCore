using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayMountInformations : GameRolePlayNamedActorInformations  
    { 
        public new const ushort Id = 3190;
        public override ushort TypeId => Id;

        public string ownerName;
        public byte level;

        public GameRolePlayMountInformations()
        {
        }
        public GameRolePlayMountInformations(string ownerName,byte level,double contextualId,EntityDispositionInformations disposition,EntityLook look,string name)
        {
            this.ownerName = ownerName;
            this.level = level;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)ownerName);
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ownerName = (string)reader.ReadUTF();
            level = (byte)reader.ReadSByte();
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of GameRolePlayMountInformations.level.");
            }

        }


    }
}









using Giny.Core.IO.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Types
{
    public class Account
    {
        public int Id;

        public string Username;

        public string Nickname;

        public byte CharacterSlots;

        public ServerRoleEnum Role;

        public Account(int id, string username, string nickname, byte characterSlots, ServerRoleEnum role)
        {
            this.Id = id;
            this.Username = username;
            this.Nickname = nickname;
            this.CharacterSlots = characterSlots;
            this.Role = role;
        }
        public Account()
        {

        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(Id);
            writer.WriteUTF(Username);
            writer.WriteUTF(Nickname);
            writer.WriteByte(CharacterSlots);
            writer.WriteByte((byte)Role);
        }
        public void Deserialize(IDataReader reader)
        {
            this.Id = reader.ReadInt();
            this.Username = reader.ReadUTF();
            this.Nickname = reader.ReadUTF();
            this.CharacterSlots = reader.ReadByte();
            this.Role = (ServerRoleEnum)reader.ReadByte();
        }

    }
}

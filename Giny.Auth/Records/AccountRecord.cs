using Giny.Auth.Network;
using Giny.Core.Extensions;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.IPC.Types;
using ProtoBuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Records
{
    [Table("accounts", false)]
    public class AccountRecord : ITable
    {
        public const int DefaultCharacterSlots = 5;

        long ITable.Id => Id;

        [Primary]
        public int Id
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
        [Update]
        public short LastSelectedServerId
        {
            get;
            set;
        } = 0;

        [Update]
        public List<string> IPs
        {
            get;
            set;
        } = new List<string>();

        public byte CharacterSlots
        {
            get;
            set;
        } = DefaultCharacterSlots;

        [Update]
        public bool Banned
        {
            get;
            set;
        } = false;

        public ServerRoleEnum Role
        {
            get;
            set;
        } = ServerRoleEnum.Player;

        [Update]
        public string Nickname
        {
            get;
            set;
        }

        public int Ogrines
        {
            get;
            set;
        } = 0;


        public Account ToAccount()
        {
            return new Account(Id, Username, Nickname, CharacterSlots, Role);
        }


        public static IEnumerable<AccountRecord> ReadAccountRecords()
        {
            return DatabaseReader.Read<AccountRecord>();
        }
        public static AccountRecord ReadAccount(int accountId)
        {
            return DatabaseReader.ReadFirst<AccountRecord>("Id", accountId.ToString());
        }
        public static AccountRecord ReadAccount(string username)
        {
            return DatabaseReader.ReadFirst<AccountRecord>("Username", username);
        }

        public static bool NicknameExist(string nickname)
        {
            return DatabaseReader.ReadFirst<AccountRecord>("Nickname", nickname) != null;
        }

    }
}

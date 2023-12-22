using Giny.Core.IO.Configuration;
using Giny.Zaap.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Uplauncher
{
    public class UplConfig : IConfigFile, IAccountProvider
    {
        public const string Filepath = "config.json";
        public List<WebAccount> Accounts
        {
            get;
            set;
        } = new List<WebAccount>();

        private int AccountIndex
        {
            get;
            set;
        } = 0;

        public string Wallpaper
        {
            get;
            set;
        } = "https://i.imgur.com/9eMnv7A.jpeg";

        public string LocalVersion
        {
            get;
            set;
        } = "1.0.0";

        public bool StartAllInstances
        {
            get;
            set;
        }

        public string ClientPath
        {
            get;
            set;
        } = @"C:\Users\olivi\Desktop\Giny .NET Core\Dofus";

        public string AuthHost
        {
            get;
            set;
        } = "http://localhost:9001";

        public WebAccount GetSelectedAccount()
        {
            if (Accounts.Count == 0)
            {
                return null;
            }
            if (AccountIndex > Accounts.Count - 1)
            {
                AccountIndex = 0;
            }

            return Accounts[AccountIndex];
        }


        public void SelectAccount(WebAccount acc)
        {
            AccountIndex = Accounts.IndexOf(acc);
        }

        public int GetIndex(WebAccount acc)
        {
            return Accounts.IndexOf(acc);
        }
        public void OnCreated()
        {

        }

        public void OnLoaded()
        {

        }

        public WebAccount GetAccount(int instanceId)
        {
            return Accounts[instanceId];
        }
    }
}

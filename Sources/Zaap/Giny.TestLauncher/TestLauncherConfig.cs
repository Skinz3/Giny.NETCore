using Giny.Core.IO.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.TestLauncher
{
    public class TestLauncherConfig : IConfigFile
    {
        public string ClientPath
        {
            get;
            set;
        } = "";
        public string Username
        {
            get;
            set;
        } = "";
        public string Password
        {
            get;
            set;
        } = "";
        public void OnCreated()
        {
            
        }

        public void OnLoaded()
        {
          
        }
    }
}

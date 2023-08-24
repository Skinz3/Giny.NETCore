using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldView.Config
{
    public class WorldViewConfig : IConfigFile
    {
        public const string Filepath = "config.json";

        public string SQLHost
        {
            get;
            set;
        } = "127.0.0.1";

        public string SQLUser
        {
            get;
            set;
        } = "root";

        public string SQLPassword
        {
            get;
            set;
        } = string.Empty;

        public string SQLDBName
        {
            get;
            set;
        } = "giny_world";

        public string ClientPath
        {
            get;
            set;
        } = "";


        public void OnCreated()
        {
            Logger.Write("Configuration file created !");

        }

        public void OnLoaded()
        {
            Logger.Write($"Configuration loaded");
        }


    }
}

using Giny.Core;
using Giny.Core.DesignPattern;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Web
{
    public class WebApi : Singleton<WebApi>
    {
        public static string Address = "http://" + ConfigFile.Instance.APIHost + ":" + ConfigFile.Instance.APIPort + "/";

        private IDisposable WebServer
        {
            get;
            set;
        }

        [StartupInvoke("Web Api", StartupInvokePriority.Last)] 
        public void Start()
        {
            this.WebServer = WebApp.Start<Startup>(Address);
            Logger.Write("Web Api started : " + Address);
        }

        public void Stop()
        {
            Logger.Write("Web Api stopped");
            this.WebServer.Dispose();
        }
    }
}

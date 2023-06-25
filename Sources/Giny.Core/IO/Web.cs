using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.IO
{
    public class Web
    {
        public static byte[] DownloadData(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("User-Agent: Other");
                return webClient.DownloadData(url);
            }
        }
    }
}

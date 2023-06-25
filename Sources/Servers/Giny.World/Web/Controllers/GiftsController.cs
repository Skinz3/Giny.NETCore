using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Giny.World.Web.Controllers
{
    [RoutePrefix("api/gifts")]
    public class GiftsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "gifts" };
        }

        public StatusCodeResult Post([FromBody] Dictionary<string, int> value)
        {
            int accountId = value["accountId"];
            int tokenId = value["tokenId"];
            int tokenCount = value["tokenCount"];

            if (WebGiftManager.Instance.AddGift(accountId, tokenId, tokenCount))
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
    }

}

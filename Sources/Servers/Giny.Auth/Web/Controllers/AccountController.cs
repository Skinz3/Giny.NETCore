using Giny.Auth.Records;
using Giny.Core.Misc;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.IPC.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Giny.World.Web.Controllers
{
    public class CredentialsRequest
    {
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
    }
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public Account? Auth(CredentialsRequest request)
        {
            System.Threading.Thread.Sleep(500);
            var record = AccountRecord.ReadAccount(request.Username);

            if (record == null || record.Password != request.Password)
            {
                return null;
            }

    
            return record.ToAccount();
        }

        [HttpPost]
        [Route("register")]
        public Account? Register(CredentialsRequest request)
        {
            if (AccountRecord.UsernameExist(request.Username))
            {
                return null;
            }

            var record = AccountRecord.CreateAccount(request.Username, request.Password, ServerRoleEnum.Player);
            record.AddNow();
            return record.ToAccount();
        }
    }
}
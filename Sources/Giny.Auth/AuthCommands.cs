using Giny.Core.Commands;
using Giny.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth
{
    public class AuthCommands
    {
        [ConsoleCommand("clear")]
        public static void ClearCommand()
        {
            Console.Clear();
            Logger.DrawLogo();
        }
    }
}

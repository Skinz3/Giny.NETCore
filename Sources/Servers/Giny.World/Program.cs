using Giny.Core;
using Giny.Core.Commands;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using Giny.Core.Network.Messages;
using Giny.IO.RawPatch;
using Giny.Protocol;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Modules;
using Giny.World.Network;
using Giny.World.Records.Maps;
using Giny.World.Records.Npcs;
using Giny.World.Records.Quests;
using System.Configuration;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


namespace Giny.World
{
    /// <summary>
    /// Entry point.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {


            /* WIPManager.Analyse(Assembly.GetExecutingAssembly());
              Console.Read(); */

            Logger.DrawLogo();
            StartupManager.Instance.Initialize(Assembly.GetExecutingAssembly());
            IPCManager.Instance.ConnectToAuth();

            ConsoleCommandsManager.Instance.ReadCommand();
        }



        [StartupInvoke("Protocol", StartupInvokePriority.SecondPass)]
        public static void InitializeProtocolManager()
        {
            ProtocolMessageManager.Initialize(Assembly.GetAssembly(typeof(RawDataMessage)), Assembly.GetAssembly(typeof(Program)));
            ProtocolTypeManager.Initialize();
        }
        [StartupInvoke("SWF patches", StartupInvokePriority.Last)]
        public static void InitializeRawPatches()
        {
            RawPatchManager.Instance.Initialize();
        }
        [StartupInvoke("Console commands", StartupInvokePriority.Last)]
        public static void InitializeConsoleCommand()
        {
            ConsoleCommandsManager.Instance.Initialize(AssemblyCore.GetTypes());
        }
    }
}

using Giny.Core;
using Giny.Core.Commands;
using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.IO.RawPatch;
using Giny.Protocol;
using Giny.Protocol.Messages;
using Giny.World.Modules;
using Giny.World.Network;
using System.Reflection;


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

        [StartupInvoke("Protocol Manager", StartupInvokePriority.SecondPass)]
        public static void InitializeProtocolManager()
        {
            ProtocolMessageManager.Initialize(Assembly.GetAssembly(typeof(RawDataMessage)), Assembly.GetAssembly(typeof(Program)));
            ProtocolTypeManager.Initialize();
        }
        [StartupInvoke("Raw Patches", StartupInvokePriority.Last)]
        public static void InitializeRawPatches()
        {
            RawPatchManager.Instance.Initialize();
        }
        [StartupInvoke("Console Commands", StartupInvokePriority.Last)]
        public static void InitializeConsoleCommand()
        {
            ConsoleCommandsManager.Instance.Initialize(AssemblyCore.GetTypes());
        }
    }
}

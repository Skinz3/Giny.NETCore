using Giny.Auth.Network;
using Giny.Auth.Records;
using Giny.Auth;
using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.IO.D2OClasses;
using Giny.IO.RawPatch;
using Giny.ORM;
using Giny.ORM.IO;
using Giny.Protocol;
using Giny.Protocol.Messages;
using System.Reflection;
using Giny.Auth.Network.IPC;
using Giny.Core.Commands;

namespace Giny.Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.DrawLogo();
            StartupManager.Instance.Initialize(Assembly.GetExecutingAssembly());

            IPCServer.Instance.Start(ConfigFile.Instance.IPCHost, ConfigFile.Instance.IPCPort);
            AuthServer.Instance.Start(ConfigFile.Instance.Host, ConfigFile.Instance.Port);
            ConsoleCommandsManager.Instance.ReadCommand();
            
        }
        [StartupInvoke("Database", StartupInvokePriority.SecondPass)]
        public static void InitializeDatabase()
        {
            DatabaseManager.Instance.Initialize(Assembly.GetExecutingAssembly(), ConfigFile.Instance.SQLHost,
               ConfigFile.Instance.SQLDBName, ConfigFile.Instance.SQLUser, ConfigFile.Instance.SQLPassword);
            DatabaseManager.Instance.LoadTables();
        }
        [StartupInvoke("Protocol Manager", StartupInvokePriority.Initial)]
        public static void InitializeProtocolManager()
        {
            ProtocolMessageManager.Initialize(Assembly.GetAssembly(typeof(RawDataMessage)), Assembly.GetAssembly(typeof(Program)));
            ProtocolTypeManager.Initialize();
        }
        [StartupInvoke("Raw Patches",StartupInvokePriority.Last)]
        public static void InitializeRawPatches()
        {
            RawPatchManager.Instance.Initialize();
        }
        [StartupInvoke("Console Commands", StartupInvokePriority.Last)]
        public static void InitializeConsoleCommand()
        {
            ConsoleCommandsManager.Instance.Initialize(Assembly.GetExecutingAssembly().GetTypes());
        }
    }
}

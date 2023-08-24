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
using Giny.Core.IO.Configuration;

namespace Giny.Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.DrawLogo();
            StartupManager.Instance.Initialize(Assembly.GetExecutingAssembly());

            AuthConfig config = ConfigManager<AuthConfig>.Instance;

            IPCServer.Instance.Start(config.IPCHost, config.IPCPort);
            AuthServer.Instance.Start(config.Host, config.Port);
            ConsoleCommandsManager.Instance.ReadCommand();

        }
        [StartupInvoke("Database", StartupInvokePriority.SecondPass)]
        public static void InitializeDatabase()
        {
            AuthConfig config = ConfigManager<AuthConfig>.Instance;

            DatabaseManager.Instance.Initialize(Assembly.GetExecutingAssembly(), config.SQLHost,
               config.SQLDBName, config.SQLUser, config.SQLPassword);
            DatabaseManager.Instance.LoadTables();
        }
        [StartupInvoke("Protocol", StartupInvokePriority.Initial)]
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
            ConsoleCommandsManager.Instance.Initialize(Assembly.GetExecutingAssembly().GetTypes());
        }
    }
}

using Giny.Core;
using Giny.Core.Commands;
using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.IO.D2OClasses;
using Giny.IO.RawPatch;
using Giny.Protocol;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Monsters;
using Giny.World.Modules;
using Giny.World.Network;
using Giny.World.Records.Jobs;
using Giny.World.Records.Monsters;
using System.Drawing;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


namespace Giny.World
{
    /// <summary>
    /// Entry point.
    /// </summary>
    class Program
    {



        static short EncodeColor(int argb) // 22 8 0 
        {
            var color = Color.FromArgb(argb);

            int red = color.R;
            int green = color.G;
            int blue = color.B;

            // Make sure the values are within the range of 0-255
            red = Math.Max(0, Math.Min(red, 255));
            green = Math.Max(0, Math.Min(green, 255));
            blue = Math.Max(0, Math.Min(blue, 255));

            // Combine the components into a single value
            int rs = (int)((red << 16) | (green << 8) | (blue));

            return (short)rs;
        }

        static void Main(string[] args)
        {
            int rs = EncodeColor(5911580);


            Random rd = new Random();

            while (true)
            {
                var a = rd.Next(0, 100);
                var b = rd.Next(0, 100);
                var c = rd.Next(0, 100);

                var d = rd.Next(0, 255);
                var e = rd.Next(0, 255);
                var f = rd.Next(0, 255);

                int R = (rs >> a) & d;
                int G = (rs >> b) & e;
                int B = (rs >> c) & f;

                if (R == 90 && G == 52 && B == 28)
                {

                }
            }




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

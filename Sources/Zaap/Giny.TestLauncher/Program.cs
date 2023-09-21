

using Giny.Core;
using Giny.Core.IO.Configuration;
using Giny.IO;
using Giny.TestLauncher;
using Giny.Zaap;
using System.Diagnostics;
using System.Reflection;


ConfigManager<TestLauncherConfig>.Load("config.json");

var config = ConfigManager<TestLauncherConfig>.Instance;

if (!Directory.Exists(config.ClientPath))
{
    Logger.Write("Invalid client path. Set it in 'config.json'", Channels.Warning);
    Console.ReadLine();
    Environment.Exit(0);
}

if (string.IsNullOrWhiteSpace(config.Username) || string.IsNullOrWhiteSpace(config.Password))
{
    Logger.Write("Invalid credentials. Set it in 'config.json'", Channels.Warning);
    Console.ReadLine();
    Environment.Exit(0);
}

var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

foreach (var process in processes)
{
    if (process.Id != Process.GetCurrentProcess().Id)
    {
        process.Kill();
    }
}

const int Port = 3001;
string ClientPath = config.ClientPath;
Console.Title = Assembly.GetExecutingAssembly().GetName().Name;

int instanceId = 1;


ZaapServer server = new ZaapServer();

server.Start(Port);

server.SetCredentials(config.Username, config.Password);

Utils.StartClient(ClientPath, Port, instanceId++);

while (true)
{
    Logger.Write("Username : ");
    var username = Console.ReadLine();

    Logger.Write("Password : ");
    var password = Console.ReadLine();

    server.SetCredentials(username, password);

    Utils.StartClient(ClientPath, Port, instanceId++);

}


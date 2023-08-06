

using Giny.Core;
using Giny.IO;
using Giny.Zaap;
using System.Diagnostics;
using System.Reflection;




var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

foreach (var process in processes)
{
    if (process.Id != Process.GetCurrentProcess().Id)
    {
        process.Kill();
    }
}

const int Port = 3001;
string ClientPath = ClientConstants.ClientPath;
Console.Title = Assembly.GetExecutingAssembly().GetName().Name;

int instanceId = 1;



ZaapServer server = new ZaapServer();

server.Start(Port);

server.SetCredentials("admin", "overflow35");

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


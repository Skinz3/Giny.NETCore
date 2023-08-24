using Giny.Core;
using Giny.Core.Commands;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO.Configuration;
using Giny.Core.Misc;
using Giny.ORM;
using Giny.ORM.Cyclic;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.IPC.Messages;
using Giny.World.Managers;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Items;
using Giny.World.Managers.Maps.Npcs;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Modules;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Accounts;
using Giny.World.Records.Achievements;
using Giny.World.Records.Characters;
using Giny.World.Records.Guilds;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World
{
    class WorldCommands
    {
        [ConsoleCommand("clear")]
        public static void ClearCommand()
        {
            Console.Clear();
            Logger.DrawLogo();
        }

        [ConsoleCommand("rate")]
        public static void ExperienceRateCommand(double ratio)
        {
            ConfigManager<WorldConfig>.Instance.XpRate = ratio;

            foreach (var client in WorldServer.Instance.GetOnlineClients())
            {
                client.Character.SendServerExperienceModificator();
            }

            Logger.Write("Experience rate multiplicator is now set to : " + ConfigManager<WorldConfig>.Instance.XpRate, Channels.Info);


        }

        [ConsoleCommand("reset")]
        public static void ResetCommand()
        {
            Logger.Write("Reset world...", Channels.Log);

            IPCManager.Instance.SendRequest(new ResetWorldRequestMessage(),
            delegate (ResetWorldResultMessage msg)
            {
                if (msg.success)
                {
                    foreach (var character in CharacterRecord.GetCharacterRecords().ToArray())
                    {
                        CharacterManager.Instance.DeleteCharacter(character);
                    }

                    DatabaseManager.Instance.DeleteTable<WorldAccountRecord>();
                    DatabaseManager.Instance.DeleteTable<BankItemRecord>();
                    DatabaseManager.Instance.DeleteTable<BidShopItemRecord>();
                    DatabaseManager.Instance.DeleteTable<CharacterItemRecord>();
                    DatabaseManager.Instance.DeleteTable<BidShopItemRecord>();
                    DatabaseManager.Instance.DeleteTable<GuildRecord>();

                    Environment.Exit(0);
                }
                else
                {
                    Logger.Write("AuthServer is unable to validate world reset.", Channels.Warning);
                }
            },
           delegate ()
           {
               Logger.Write("AuthServer is unable to validate world reset. timeout", Channels.Warning);
           });
        }

        [ConsoleCommand("infos")]
        public static void InfosCommand()
        {
            Process currentProcess = Process.GetCurrentProcess();
            // PrivateMemorySize64 represents the private memory usage in bytes
            long memoryUsage = currentProcess.PrivateMemorySize64;
            currentProcess.Dispose();

            Logger.Write($"Clients : {WorldServer.Instance.GetClients().Count()}", Channels.Info);
            Logger.Write($"Clients (by ip) : {WorldServer.Instance.GetClients().DistinctBy(x => x.Ip).Count()}", Channels.Info);
            Logger.Write($"Client peak {WorldServer.Instance.MaximumClients}", Channels.Info);
            Logger.Write($"Fights count : {FightManager.Instance.GetFightCount()}", Channels.Info);
            Logger.Write($"Current Memory Usage: {MemoryFormatter.FormatBytes(memoryUsage)}", Channels.Info);


        }
        [ConsoleCommand("save")]
        public static void SaveCommand()
        {
            WorldSaveManager.Instance.PerformSave();
        }

        [ConsoleCommand("npcs")]
        public static void ReloadNpcsCommand()
        {
            NpcsManager.Instance.ReloadNpcs();
            Logger.Write("Npcs reloaded");
        }

        [ConsoleCommand("items")]
        public static void ReloadItemsCommand()
        {
            ItemsManager.Instance.Reload();
            Logger.Write("Items Reloaded.", Channels.Log);
        }
    }
}

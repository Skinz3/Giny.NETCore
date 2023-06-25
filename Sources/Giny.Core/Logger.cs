using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core
{
    [Flags]
    public enum Channels
    {
        Info = 1,
        Warning = 2,
        Critical = 4,
        Log = 8,
    }
    public class Logger
    {
        public static readonly Channels DefaultChannels = Channels.Info | Channels.Warning | Channels.Critical | Channels.Log;

        private static Channels Channels = DefaultChannels;

        private const ConsoleColor Color1 = ConsoleColor.DarkBlue;
        private const ConsoleColor Color2 = ConsoleColor.Blue;

        private static Dictionary<Channels, ConsoleColor> ChannelsColors = new Dictionary<Channels, ConsoleColor>()
        {
            { Channels.Info,     ConsoleColor.Gray },
            { Channels.Log,     ConsoleColor.DarkGray },
            { Channels.Warning,  ConsoleColor.Yellow },
            { Channels.Critical, ConsoleColor.Red },
        };

        public static void SetChannels(Channels channels)
        {
            Channels = channels;
        }
        public static void EnableChannel(Channels channels)
        {
            Channels |= channels;
        }
        public static void Enable()
        {
            Channels = DefaultChannels;
        }
        public static void Disable()
        {
            Channels = 0x00;
        }
        public static void DisableChannel(Channels channels)
        {
            Channels &= ~channels;
        }

        public static void Write(object value, Channels state = Channels.Log)
        {
            if (Channels.HasFlag(state))
            {
                WriteColored(value, ChannelsColors[state]);
            }
        }
        public static void WriteColor1(object value)
        {
            WriteColored(value, Color1);
        }
        public static void WriteColor2(object value)
        {
            WriteColored(value, Color2);
        }
        private static void WriteColored(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }
        public static void NewLine()
        {
            if (Channels != 0x00)
            {
                Console.WriteLine(Environment.NewLine);
            }
        }
        public static void DrawLogo()
        {
            Console.Title = Assembly.GetCallingAssembly().GetName().Name;
            WriteColor1(@"   ______   _                     ");
            WriteColor1(@" .' ___  | (_)                    ");
            WriteColor1(@"/ .'   \_| __   _ .--.    _   __  ");
            WriteColor2(@"| |   ____[  | [ `.-. |  [ \ [  ] ");
            WriteColor2(@"\ `.___]   | |  | | | |   \ '/ /  ");
            WriteColor2(@" `._____.'[___][___||__][\_:  /   ");
            WriteColor2(@"  github.com/Skinz3/Giny \__.'    ");
            NewLine();
        }
        
    }
}

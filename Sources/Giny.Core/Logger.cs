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
        private static Dictionary<ConsoleColor, ConsoleColor> Colors = new Dictionary<ConsoleColor, ConsoleColor>()
        {
           // {ConsoleColor.DarkBlue,ConsoleColor.Blue },
           // {ConsoleColor.DarkGreen,ConsoleColor.Green },
            {ConsoleColor.DarkMagenta,ConsoleColor.Magenta },
        };
        static Logger()
        {
            var index = new Random().Next(0, Colors.Count);
            var sessionColors = Colors.ElementAt(index);

            Color1 = sessionColors.Key;
            Color2 = sessionColors.Value;
        }
        public static readonly Channels DefaultChannels = Channels.Info | Channels.Warning | Channels.Critical | Channels.Log;

        private static Channels Channels = DefaultChannels;

        private static ConsoleColor Color1;
        private static ConsoleColor Color2;

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
            NewLine();

            WriteColor2(@"    ..|'''.|   ||                    ");
            WriteColor2(@"   .|'     '  ...  .. ...   .... ... ");
            WriteColor2(@"   ||    ....  ||   ||  ||   '|.  |  ");
            WriteColor1(@"   '|.    ||   ||   ||  ||    '|.|   ");
            WriteColor1(@"    ''|...'|  .||. .||. ||.    '|    ");
            WriteColor1(@"                            .. |     ");
            WriteColor1(@"                             ''      ");
            WriteColor1("");

            /*WriteColor2(@"   _|_|_|  _|                      ");
              WriteColor2(@" _|            _|_|_|    _|    _|  ");
              WriteColor2(@" _|  _|_|  _|  _|    _|  _|    _|  ");
              WriteColor1(@" _|    _|  _|  _|    _|  _|    _|  ");
              WriteColor1(@"   _|_|_|  _|  _|    _|    _|_|_|  ");
              WriteColor1(@"             Dofus Emulator    _|  ");
              WriteColor1(@"                            _|_|  "); */


            /* WriteColor2(@" ______   _                     ");
             WriteColor2(@" .' ___  | (_)                    ");
             WriteColor2(@"/ .'   \_| __   _ .--.    _   __  ");
             WriteColor1(@"| |   ____[  | [ `.-. |  [ \ [  ] ");
             WriteColor1(@"\ `.___]   | |  | | | |   \ '/ /  ");
             WriteColor1(@" `._____.'[___][___||__][\_:  /   ");
             WriteColor1(@"          Dofus emulator \__.'    "); */


        }

    }
}

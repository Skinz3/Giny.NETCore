using Giny.Core.Extensions;
using Giny.Core.Misc;
using Giny.IO.D2O;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.DatabaseSynchronizer
{
    /// <summary>
    /// Not used anymore?
    /// </summary>
    class MiscBuilder
    {
        public static void Build(List<D2OReader> readers)
        {
            BuildTextInformations(readers);
            BuildNpcsDialogs(readers);
        }
       
        private static void BuildTextInformations(List<D2OReader> readers)
        {
            var msgs = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "InfoMessage")).EnumerateObjects().Cast<Giny.IO.D2OClasses.InfoMessage>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*Information Messages*");

            foreach (var msg in msgs)
            {
                sb.AppendLine(msg.textId + "->" + Program.D2IFileFR.GetText((int)msg.messageId));
            }

            Notepad.Open(sb.ToString());
        }
    
        private static void BuildNpcsDialogs(List<D2OReader> readers)
        {
            var npcs = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "Npc")).EnumerateObjects().Cast<Giny.IO.D2OClasses.Npc>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*Npc Messages*");
            foreach (var npc in npcs)
            {
                var l1 = ((IEnumerable)npc.DialogMessages).Cast<List<int>>().ToList();

                foreach (var value in l1)
                {
                    sb.AppendLine("");
                    sb.AppendLine("NPC_ID:" + npc.id + " MessageId : " + value[0] + ":" + Program.D2IFileFR.GetText(value[1]));
                }

            }

            var sb2 = new StringBuilder();
            sb2.AppendLine("*Npc Replies*");
            foreach (var npc in npcs)
            {
                var l1 = ((IEnumerable)npc.DialogReplies).Cast<List<int>>().ToList();

                foreach (var value in l1)
                {
                    sb2.AppendLine("");
                    sb2.AppendLine("NPC_ID:" + npc.id + " ReplyId : " + value[0] + " => " + Program.D2IFileFR.GetText(value[1]));
                }

            }

            Notepad.Open(sb.ToString());
            Notepad.Open(sb2.ToString());

        }
       
    }
}

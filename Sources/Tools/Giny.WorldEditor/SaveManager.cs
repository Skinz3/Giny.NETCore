using Giny.IO.D2I;
using Giny.ORM.Cyclic;
using Giny.WorldEditor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor
{
    public class SaveManager
    {
        public static void Save(Home home)
        {
            var notify = home.NotifySaving(true);
            notify.Wait();

            CyclicSaveTask.Instance.Save();

            D2IManager.SaveAll();

            home.NotifySaving(false);

        }
    }
}

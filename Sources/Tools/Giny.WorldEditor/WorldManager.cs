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
    public class WorldManager
    {
        public static void RequestReload(Home home)
        {
            var task = home.SetNotification("Processing world reload...", MudBlazor.Severity.Info);
            task.Wait();

            WorldApi.ReloadNpcs();
            WorldApi.ReloadItems();

            home.RemoveNotification();
        }

        public static void SaveI18N(Home home)
        {
            var task = home.SetNotification("Updating i18n file...", MudBlazor.Severity.Info);
            task.Wait();

            try
            {
                D2IManager.SaveAll();
            }
            catch
            {
                home.AddSnackbar("Unable to apply I18n changes to client. Please close it and try again", MudBlazor.Severity.Error);

            }

            home.RemoveNotification();
        }
        public static void SaveDatabase(Home home)
        {
            var task = home.SetNotification("Saving database...", MudBlazor.Severity.Info);
            task.Wait();


            CyclicSaveTask.Instance.Save();


            home.RemoveNotification();

        }
    }
}

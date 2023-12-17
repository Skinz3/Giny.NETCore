using Giny.World.Records.Npcs;
using Giny.WorldEditor.Components.Roleplay.Npcs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Giny.WorldEditor
{
    public class DialogHelper
    {
        public static void OpenDialog(IDialogService service, string title, string content)
        {
            DialogOptions options = new DialogOptions() { ClassBackground = "dialog-open" };
            service.ShowMessageBox("Loading", "Please wait for the application to finish loading.", "OK", null, null, options);
        }
        public static async Task<MudBlazor.DialogResult> OpenDialog<T>(IDialogService service, DialogParameters<T> parameters, string title = "Infos") where T : ComponentBase
        {
            var dialog = service.Show<T>(title, parameters, CreateOptions());
            var result = await dialog.Result;
            return result;
        }

        private static DialogOptions CreateOptions()
        {
            return new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, CloseButton = true, ClassBackground = "dialog-open" };
        }


    }
}

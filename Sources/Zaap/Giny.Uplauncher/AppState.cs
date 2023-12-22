using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Uplauncher
{
    public class AppState
    {
        public static PageEnum Page
        {
            get;
            set;
        }
        public static int Port
        {
            get;
            set;
        } = 3001;


        public static bool UpToDate
        {
            get;
            set;
        }

        public static MudTheme GinyTheme = new MudTheme()
        {
            Palette = new PaletteDark()
            {
                Primary = new MudBlazor.Utilities.MudColor(111, 125, 24, 255),
                Surface = new MudBlazor.Utilities.MudColor(60, 60, 60, 255),
                AppbarBackground = new MudBlazor.Utilities.MudColor(39, 39, 47, 255),
            },
            PaletteDark = new PaletteDark()
            {
                Primary = new MudBlazor.Utilities.MudColor(134, 158, 0, 255),
                Surface = new MudBlazor.Utilities.MudColor(30, 30, 30, 255),
                AppbarBackground = new MudBlazor.Utilities.MudColor(19, 19, 27, 255),

            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };


    }
}

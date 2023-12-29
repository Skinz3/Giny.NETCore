using Giny.World.Records.Spells;
using Giny.WorldEditor.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor
{
    public class LoadingStep
    {
        public string Name
        {
            get;
            set;
        }
        public bool Done
        {
            get;
            set;
        }
        Action Func
        {
            get;
            set;
        }
        public bool Running
        {
            get;
            private set;
        }

        public bool Error
        {
            get;
            set;
        }

        public LoadingStep(string name, Action func)
        {
            Name = name;
            Done = false;
            Func = func;
        }


        public void Execute()
        {
            this.Running = true;
            this.Func();
            this.Running = false;
            this.Done = true;
        }
    }
    public class AppState
    {
        public static bool Initialized
        {
            get;
            set;
        } = false;

        public static PageEnum Page
        {
            get;
            set;
        } = PageEnum.Loader;

        public static MudTheme GinyTheme = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Background = new MudBlazor.Utilities.MudColor(242, 241, 234, 255),

                Surface = new MudBlazor.Utilities.MudColor(237, 236, 229, 255),
                AppbarBackground = new MudBlazor.Utilities.MudColor(237, 236, 229, 255),
                DrawerBackground = new MudBlazor.Utilities.MudColor(237, 236, 229, 255),
            },
            PaletteDark = new PaletteDark()
            {
                //  Background = new MudBlazor.Utilities.MudColor(39, 39, 47, 255),
                //Primary = new MudBlazor.Utilities.MudColor(134, 158, 0, 255),
                AppbarBackground = new MudBlazor.Utilities.MudColor(19, 19, 27, 255),
                DrawerBackground = new MudBlazor.Utilities.MudColor(49, 49, 57, 255),
                //  Surface = new MudBlazor.Utilities.MudColor(49, 49, 57, 255), 


            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };


    }
}

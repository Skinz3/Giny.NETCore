using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldView
{
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
    }
}

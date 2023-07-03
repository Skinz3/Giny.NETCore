using Giny.World.Records.Spells;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldView
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

        public static SpellRecord? SpellExplorerTarget
        {
            get;
            set;
        }
        public static PageEnum Page
        {
            get;
            set;
        } = PageEnum.Loader;

    }
}

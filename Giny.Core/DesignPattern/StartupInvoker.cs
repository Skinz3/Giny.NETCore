using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.DesignPattern
{
    public enum StartupInvokePriority
    {
        Initial = 0,
        SecondPass = 1,
        ThirdPass = 2,
        FourthPass = 4,
        FifthPass = 5,
        SixthPath = 6,
        Last = 7,
        Modules = 8,
        Disabled,
    }
    public class StartupInvoke : Attribute
    {
        public StartupInvokePriority Type
        {
            get; set;
        }

        public bool Hided
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public bool ErrorStopProgram
        {
            get;
            set;
        }

        public StartupInvoke(string name, StartupInvokePriority type, bool errorStopProgram = true)
        {
            this.Type = type;
            this.Name = name;
            this.Hided = false;
            this.ErrorStopProgram = errorStopProgram;
        }
        public StartupInvoke(StartupInvokePriority type)
        {
            this.Hided = true;
            this.Type = type;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class StartupManager : Singleton<StartupManager>
    {
        public static string SingletonInstancePropretyName = "Instance";

        private static StartupInvokePriority[] VALID_PRIORITIES = new StartupInvokePriority[]
        {
            StartupInvokePriority.Initial,
            StartupInvokePriority.SecondPass,
            StartupInvokePriority.ThirdPass,
            StartupInvokePriority.FourthPass,
            StartupInvokePriority.FifthPass,
            StartupInvokePriority.SixthPath,
            StartupInvokePriority.Last,
            StartupInvokePriority.Modules,
        };

        public void Initialize(Assembly startupAssembly)
        {
            Logger.WriteColor2("-- Initialisation");
            Stopwatch watch = Stopwatch.StartNew();

            foreach (var pass in VALID_PRIORITIES)
            {
                foreach (var item in startupAssembly.GetTypes())
                {
                    var methods = item.GetMethods().ToList().FindAll(x => x.GetCustomAttribute(typeof(StartupInvoke), false) != null);
                    var attributes = methods.ConvertAll<KeyValuePair<StartupInvoke, MethodInfo>>(x => new KeyValuePair<StartupInvoke, MethodInfo>(x.GetCustomAttribute(typeof(StartupInvoke), false) as StartupInvoke, x)).FindAll(x => x.Key.Type == (StartupInvokePriority)pass); ;

                    foreach (var data in attributes)
                    {
                        if (!data.Key.Hided)
                        {
                            Logger.Write("(" + pass + ") Loading " + data.Key.Name + " ...", Channels.Info);
                        }

                        Delegate del = null;

                        if (data.Value.IsStatic)
                        {
                            del = Delegate.CreateDelegate(typeof(Action), data.Value);
                        }
                        else
                        {
                            PropertyInfo field = data.Value.DeclaringType.BaseType.GetProperty(SingletonInstancePropretyName);
                            Object singletonInstance = field.GetValue(null);
                            del = Delegate.CreateDelegate(typeof(Action), singletonInstance, data.Value.Name);
                        }
                        try
                        {
                            del.DynamicInvoke();
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex.ToString(), Channels.Critical);
                            Console.ReadKey();
                            Environment.Exit(0);
                            return;
                        }
                    }


                }
            }
            watch.Stop();
            Logger.WriteColor2("-- Initialisation Complete (" + watch.Elapsed.Minutes + "min" + watch.Elapsed.Seconds + "s)");
        }
    }
}

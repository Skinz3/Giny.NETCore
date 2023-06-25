using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.DesignPattern
{
    public class WIPManager
    {
        public static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Public
          | BindingFlags.Default | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | BindingFlags.Static;

        public const string TypeLog = "Class: ({1}.cs) Type : ({0})  Comment: ({2})";
        public const string MethodLog = "Class: ({1}.cs) Method: {0}()  Comment: ({2})";
        public const string FieldLog = "Class: ({1}.cs) Field: ({0})  Comment: ({2})";

        public static void Analyse(Assembly assembly)
        {
            string result = string.Empty;
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var type in assembly.GetTypes())
            {
                WIPAttribute attribute = type.GetCustomAttributes(false).OfType<WIPAttribute>().FirstOrDefault();

                if (attribute != null)
                {
                    Print(TypeLog, type.Name, type.Name, attribute.Comment);
                }

                foreach (var method in type.GetMethods(BindingFlags).Where(x => x.DeclaringType == type))
                {
                    attribute = method.GetCustomAttributes(false).OfType<WIPAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        Print(MethodLog, method.Name, type.Name, attribute.Comment);
                    }
                }
                foreach (var field in type.GetFields(BindingFlags))
                {
                    attribute = field.GetCustomAttributes(false).OfType<WIPAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        Print(FieldLog, field.Name, type.Name, attribute.Comment);
                    }
                }
                foreach (var property in type.GetProperties(BindingFlags))
                {
                    attribute = property.GetCustomAttributes(false).OfType<WIPAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        Print(FieldLog, property.Name, type.Name, attribute.Comment);
                    }
                }
            }

            Logger.Write("Analysis executed in " + stopwatch.ElapsedMilliseconds + "ms", Channels.Info);

        }
        private static void Print(string formatter, string name, string type, string comment)
        {
            string result = string.Format(formatter, name, type, comment);
            Logger.Write(result, Channels.Info);
        }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class WIPAttribute : Attribute
    {
        public string Comment
        {
            get;
            private set;
        }
        public WIPAttribute(string comment = null)
        {
            this.Comment = comment;
        }
    }
}

using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Commands
{
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; set; }

        public ConsoleCommandAttribute(string name)
        {
            this.Name = name;
        }
    }
    public class ConsoleCommandsManager : Singleton<ConsoleCommandsManager>
    {
        private readonly Dictionary<string, MethodInfo> m_commands = new Dictionary<string, MethodInfo>();

        public void Initialize(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<ConsoleCommandAttribute>();

                    if (attribute != null)
                    {
                        m_commands.Add(attribute.Name, method);
                    }

                }
            }
            Logger.Write(m_commands.Count + " command(s) registered");
        }
        private void Help()
        {
            Logger.Write("Commands :");
            foreach (var item in m_commands)
            {
                Logger.WriteColor2("-" + item.Key);
            }
        }
        public void ReadCommand()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input != string.Empty)
                {
                    Handle(input);
                }
            }
        }
        private void Handle(string input)
        {
            var split = input.Split(null);

            string commandName = split.First().ToLower();

            if (commandName == "help")
            {
                Help();
                return;
            }

            var command = m_commands.FirstOrDefault(x => x.Key == commandName);

            if (command.Value != null)
            {
                var methodParameters = command.Value.GetParameters();

                var parametersString = split.Skip(1).ToArray();

                object[] parameters = new object[parametersString.Length];

                if (methodParameters.Length != parameters.Length)
                {
                    Logger.Write("Command " + command.Key + " required " + methodParameters.Length + " parameters. (" + string.Join(",", methodParameters.Select(x => x.ParameterType.Name + " " + x.Name)) + ")", Channels.Warning);
                    return;
                }

                try
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = Convert.ChangeType(parametersString[i], methodParameters[i].ParameterType);
                    }
                }

                catch
                {
                    Logger.Write("Invalid parameters for command " + command.Key + " (" + string.Join(",", methodParameters.Select(x => x.ParameterType.Name + " " + x.Name)) + ")", Channels.Warning);
                    return;
                }
                try
                {
                    command.Value.Invoke(null, parameters);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, Channels.Warning);
                }
            }
            else
            {
                Logger.WriteColor2(string.Format("{0} is not a valid command. ('help' to get a list of commands)", commandName));
            }
        }
    }
}

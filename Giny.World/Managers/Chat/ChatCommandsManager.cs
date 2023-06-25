using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Modules;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Chat
{
    public class ChatCommandAttribute : Attribute
    {
        public string Name
        {
            get; set;
        }
        public ServerRoleEnum RequiredRole
        {
            get;
            set;
        }

        public ChatCommandAttribute(string name, ServerRoleEnum requiredRole)
        {
            this.Name = name;
            this.RequiredRole = requiredRole;
        }
    }
    public class ChatCommandsManager : Singleton<ChatCommandsManager>
    {
        public const string COMMANDS_PREFIX = ".";

        private readonly Dictionary<ChatCommandAttribute, MethodInfo> m_commands = new Dictionary<ChatCommandAttribute, MethodInfo>();

        [StartupInvoke("Chat Commands", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var type in AssemblyCore.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<ChatCommandAttribute>();

                    if (attribute != null)
                    {
                        m_commands.Add(attribute, method);
                    }
                }
            }
            Logger.Write(m_commands.Count + " command(s) registered");
        }

        private void OnCommandError(WorldClient client, ParameterInfo[] parameters, ChatCommandAttribute chatCommandAttribute)
        {
            client.Character.ReplyWarning("Command <b>" + chatCommandAttribute.Name + "</b>(" + string.Join(",", parameters.Select(x => x.ParameterType.Name + " " + x.Name)) + ")");
        }
        public void Handle(string input, WorldClient source, WorldClient target)
        {
            var split = input.Split(null);

            string commandName = split.First().ToLower();

            var command = m_commands.FirstOrDefault(x => x.Key.Name.ToLower() == commandName);

            if (command.Value != null)
            {
                if (source.Account.Role < command.Key.RequiredRole)
                {
                    source.Character.ReplyWarning("Vous n'avez pas les droits pour executer cette commande.");
                    return;
                }

                var methodParameters = command.Value.GetParameters().Skip(1).ToArray();

                var parametersString = split.Skip(1).ToArray();

                object[] parameters = new object[parametersString.Length];

                if ((methodParameters.Length) != parameters.Length)
                {
                    OnCommandError(source, methodParameters, command.Key);
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
                    OnCommandError(source, methodParameters, command.Key);
                    return;
                }
                try
                {
                    command.Value.Invoke(null, new object[] { target }.Concat(parameters).ToArray());
                }
                catch (Exception ex)
                {
                    source.Character.ReplyError("Unable to execute command. Server side error");
                }
            }
            else
            {
                source.Character.ReplyWarning(string.Format("{0} is not a valid command. ('help' to get a list of commands)", commandName));
            }
        }
        public IEnumerable<ChatCommandAttribute> GetCommandsAttribute()
        {
            return m_commands.Keys;
        }
    }
}
